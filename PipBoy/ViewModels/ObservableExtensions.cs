using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reactive.Linq;
using PipBoy.Protocol;
using ReactiveUI;

namespace PipBoy.ViewModels
{
    internal static class ObservableExtensions
    {
        public static void ToBoxedProperty<TObj, TRet>(this IObservable<Dictionary<string, Box>> @this, TObj source, Expression<Func<TObj, TRet>> property, out ObservableAsPropertyHelper<TRet> result, Func<Box, TRet> func, string keyName = null) where TObj : ReactiveObject
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
