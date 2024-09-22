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

        public static IQueryable<T> FilterByField<T>(this IQueryable<T> source, string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return source;
            }

            var filterParts = filter.Split('.');
            if (filterParts.Length > 0)
            {
                return source.Where(CreateFilterExpression<T>(filterParts));
            }

            return source;
        }

        private static Expression<Func<T, bool>> CreateFilterExpression<T>(string[] filterParts)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression propertyAccess = parameter;

            foreach (var part in filterParts)
            {
                propertyAccess = Expression.PropertyOrField(propertyAccess, part);
            }

            var value = Expression.Constant(filterParts.Last());
            var equality = Expression.Equal(propertyAccess, value);

            return Expression.Lambda<Func<T, bool>>(equality, parameter);
        }
    }
}
