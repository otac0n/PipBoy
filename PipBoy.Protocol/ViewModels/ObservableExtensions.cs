// Copyright (c) John and Katie Gietzen. All rights reserved.

namespace PipBoy.Protocol.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reactive.Linq;
    using ReactiveUI;

    public static class ObservableExtensions
    {
        public static void ToBoxedListProperty<TObj, TRet>(this IObservable<Dictionary<string, Box>> @this, TObj source, Expression<Func<TObj, ObservableBoxedList<TRet>>> property, out ObservableAsPropertyHelper<ObservableBoxedList<TRet>> result, string keyName = null)
            where TObj : ReactiveObject
        {
            ToBoxedProperty(@this, source, property, out result, box => new ObservableBoxedList<TRet>(box, box2 => (TRet)box2.Value), keyName);
        }

        public static void ToBoxedListProperty<TObj, TRet>(this IObservable<Dictionary<string, Box>> @this, TObj source, Expression<Func<TObj, ObservableBoxedList<TRet>>> property, out ObservableAsPropertyHelper<ObservableBoxedList<TRet>> result, Func<Box, TRet> func, string keyName = null)
            where TObj : ReactiveObject
        {
            ToBoxedProperty(@this, source, property, out result, box => new ObservableBoxedList<TRet>(box, func), keyName);
        }

        public static void ToBoxedProperty<TObj, TRet>(this IObservable<Dictionary<string, Box>> @this, TObj source, Expression<Func<TObj, TRet>> property, out ObservableAsPropertyHelper<TRet> result, string keyName = null)
            where TObj : ReactiveObject
        {
            ToBoxedProperty(@this, source, property, out result, box => (TRet)box.Value, keyName);
        }

        public static void ToBoxedProperty<TObj, TRet>(this IObservable<Dictionary<string, Box>> @this, TObj source, Expression<Func<TObj, TRet>> property, out ObservableAsPropertyHelper<TRet> result, Func<Box, TRet> func, string keyName = null)
            where TObj : ReactiveObject
        {
            if (keyName == null)
            {
                keyName = ((MemberExpression)property.Body).Member.Name;
            }

            @this
                .Select(p =>
                {
                    Box box;
                    if (p != null && p.TryGetValue(keyName, out box))
                    {
                        return func(box);
                    }
                    else
                    {
                        return default(TRet);
                    }
                })
                .ToProperty(source, property, out result);
        }
    }
}
