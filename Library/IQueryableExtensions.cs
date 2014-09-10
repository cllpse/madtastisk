using System;
using System.Linq;
using System.Linq.Expressions;


namespace Library
{
    // ReSharper disable InconsistentNaming
    public static class IQueryableExtensions
    // ReSharper restore InconsistentNaming
    {
        public static IQueryable<TSource> WhereTrueForAny<TSource, TValue>
            (
            this IQueryable<TSource> source,
            Func<TValue, Expression<Func<TSource, Boolean>>> selector,
            params TValue[] values
            )
        {
            return source.Where(BuildTrueForAny(selector, values));
        }


        public static Expression<Func<TSource, Boolean>> BuildTrueForAny<TSource, TValue>
            (
            Func<TValue, Expression<Func<TSource, Boolean>>> selector,
            params TValue[] values
            )
        {
            if (selector == null) throw new ArgumentNullException("selector");

            if (values == null) throw new ArgumentNullException("values");

            if (values.Length == 0) return x => false;

            if (values.Length == 1) return selector(values[0]);

            var param = Expression.Parameter(typeof(TSource), "x");

            Expression body = Expression.Invoke(selector(values[0]), param);

            for (int i = 1; i < values.Length; i++) body = Expression.OrElse(body, Expression.Invoke(selector(values[i]), param));

            return Expression.Lambda<Func<TSource, Boolean>>(body, param);
        }  
    }
}