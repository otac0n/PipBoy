namespace PipBoy.Protocol.ViewModels
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reactive.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using ReactiveUI;

    public abstract class BoxedProperties : ReactiveObject
    {
        private static readonly ConcurrentDictionary<Type, Binder> Binders = new ConcurrentDictionary<Type, Binder>();

        public BoxedProperties(Box box)
        {
            var binder = Binders.GetOrAdd(this.GetType(), t => (Binder)Activator.CreateInstance(typeof(Binder<>).MakeGenericType(t)));
            binder.Bind(box, this);
        }

        private abstract class Binder
        {
            public abstract void Bind(Box box, object @this);

            protected static Func<IObservable<Dictionary<string, Box>>, string, IObservable<T>> MakeMap<T>()
            {
                var mappingFunc = (Expression<Func<Box, T>>)GetMapping(typeof(T));
                var compiled = mappingFunc.Compile();
                return (dict, key) =>
                {
                    return dict.Select(p =>
                    {
                        Box box;
                        return p != null && p.TryGetValue(key, out box)
                            ? compiled(box)
                            : default(T);
                    });
                };
            }

            private static Expression GetMapping(Type returnType)
            {
                var box = Expression.Parameter(typeof(Box));

                Expression body;
                if (returnType.GetTypeInfo().IsPrimitive || returnType == typeof(string))
                {
                    var boxValue = Expression.MakeMemberAccess(box, typeof(Box).GetProperty(nameof(Box.Value)));
                    body = Expression.Condition(
                        Expression.TypeIs(boxValue, returnType),
                        Expression.Convert(boxValue, returnType),
                        Expression.Default(returnType));
                }
                else if (returnType.IsConstructedGenericType && returnType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var boxValue = Expression.MakeMemberAccess(box, typeof(Box).GetProperty(nameof(Box.Value)));
                    var underlyingType = Nullable.GetUnderlyingType(returnType);
                    body = Expression.Condition(
                        Expression.TypeIs(boxValue, underlyingType),
                        Expression.Convert(Expression.Convert(boxValue, underlyingType), returnType),
                        Expression.Default(returnType));
                }
                else if (returnType.IsConstructedGenericType && returnType.GetGenericTypeDefinition() == typeof(ObservableBoxedList<>))
                {
                    var innerType = returnType.GetGenericArguments().Single();
                    var constructor = returnType.GetConstructor(new[] { typeof(Box), typeof(Func<,>).MakeGenericType(typeof(Box), innerType) });
                    body = Expression.New(
                        constructor,
                        box,
                        GetMapping(innerType));
                }
                else
                {
                    var constructor = returnType.GetConstructor(new[] { typeof(Box) });
                    body = Expression.New(constructor, box);
                }

                return Expression.Lambda(body, box);
            }
        }

        private class Binder<T> : Binder
            where T : BoxedProperties
        {
            private static readonly IList<Action<IObservable<Dictionary<string, Box>>, T>> Bindings;

            public override void Bind(Box box, object @this) => Bind(box, (T)@this);

            static Binder()
            {
                var bindings = new List<Action<IObservable<Dictionary<string, Box>>, T>>();

                var thisType = typeof(T);

                var fields = thisType.GetRuntimeFields().ToList();
                var t = thisType.GetTypeInfo().BaseType;
                while (t != typeof(BoxedProperties))
                {
                    fields.AddRange(t.GetRuntimeFields());
                    t = t.GetTypeInfo().BaseType;
                }

                var props = (from f in fields
                             where f.FieldType.IsConstructedGenericType
                             where f.FieldType.GetGenericTypeDefinition() == typeof(ObservableAsPropertyHelper<>)
                             join p in thisType.GetRuntimeProperties() on f.Name.ToUpperInvariant() equals p.Name.ToUpperInvariant()
                             where f.FieldType.GetGenericArguments()[0] == p.PropertyType
                             let n = p.GetCustomAttribute<JsonPropertyAttribute>()?.PropertyName ?? p.Name
                             select new { Property = p, Field = f, Name = n }).ToList();

                var toProperty = (from m in typeof(OAPHCreationHelperMixin).GetMethods()
                                  where m.Name == nameof(OAPHCreationHelperMixin.ToProperty)
                                  where m.ContainsGenericParameters
                                  let typeArgs = m.GetGenericArguments()
                                  where typeArgs.Length == 2
                                  let args = m.GetParameters()
                                  where args.Length == 6
                                  let mC = m.MakeGenericMethod(new[] { thisType, typeof(object) })
                                  let cArgs = mC.GetParameters()
                                  where cArgs[0].ParameterType == typeof(IObservable<object>) &&
                                        cArgs[1].ParameterType == thisType &&
                                        cArgs[2].ParameterType == typeof(Expression<>).MakeGenericType(typeof(Func<,>).MakeGenericType(thisType, typeof(object))) &&
                                        cArgs[3].ParameterType == typeof(ObservableAsPropertyHelper<>).MakeGenericType(typeof(object)).MakeByRefType() &&
                                        cArgs[4].ParameterType == typeof(object) &&
                                        cArgs[5].ParameterType == typeof(System.Reactive.Concurrency.IScheduler)
                                  select m).Single();
                var makeMap = typeof(Binder).GetMethod(nameof(Binder.MakeMap), BindingFlags.Static | BindingFlags.NonPublic);

                foreach (var p in props)
                {
                    var type = p.Property.PropertyType;
                    var name = p.Name;
                    var defaultValue = type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
                    var v = Expression.Parameter(thisType);
                    var expr = Expression.Lambda(Expression.MakeMemberAccess(v, p.Property), v);
                    var makeMapConstructed = makeMap.MakeGenericMethod(type);
                    var map = (Delegate)makeMapConstructed.Invoke(null, new object[0]);
                    var toPropertyConstructed = toProperty.MakeGenericMethod(thisType, type);

                    bindings.Add((dict, @this) =>
                    {
                        var args = new object[]
                        {
                            map.DynamicInvoke(dict, name),
                            @this,
                            expr,
                            null,
                            defaultValue,
                            null,
                        };
                        toPropertyConstructed.Invoke(null, args);
                        p.Field.SetValue(@this, args[3]);
                    });
                }

#if DEBUG
                var allNames = new HashSet<string>(props.Select(p => p.Name));
                bindings.Add((dict, @this) =>
                {
                    dict.Subscribe(d =>
                    {
                        if (d == null)
                        {
                            return;
                        }

                        var extra = d.Keys.Where(k => !allNames.Contains(k));
                        if (extra.Any())
                        {
                            Debug.WriteLine($"{typeof(T)} is missing properties {string.Join(", ", extra)}");
                        }
                    });
                });
#endif

                Bindings = bindings;
            }

            private void Bind(Box box, T @this)
            {
                var dict = box.WhenAny(x => x.Value, x => x.Value as Dictionary<string, Box>);
                foreach (var binding in Bindings)
                {
                    binding(dict, @this);
                }
            }
        }
    }
}
