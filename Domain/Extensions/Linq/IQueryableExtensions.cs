using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ExcludeProperties<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] excludedProperties)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var bindings = typeof(T).GetProperties()
                .Where(prop => !excludedProperties.Any(ex => ex.Body is MemberExpression member && member.Member == prop))
                .Select(prop => Expression.Bind(prop, Expression.Property(parameter, prop)));

            var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);
            var selector = Expression.Lambda<Func<T, T>>(body, parameter);

            return query.Select(selector);
        }
    }
}
