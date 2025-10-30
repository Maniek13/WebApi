using System.Linq.Expressions;

public static class IQueryableExtensions
{
    public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string propertyName, bool descending = false)
    {
        var param = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(param, propertyName);

        Expression keySelector = property;

        if (property.Type.IsValueType || property.Type == typeof(string))
        {
            keySelector = Expression.Convert(property, property.Type);
        }

        var lambda = Expression.Lambda(keySelector, param);

        string methodName = descending ? "OrderByDescending" : "OrderBy";
        var resultExp = Expression.Call(typeof(Queryable), methodName,
            new Type[] { typeof(T), keySelector.Type },
            query.Expression, Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(resultExp);
    }
}
