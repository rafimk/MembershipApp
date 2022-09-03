using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Membership.Application.Queries.Pagination;

public static class LinqExtensions
{
    public static async Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
        int page, int pageSize) where T : class
    {
        var result = new PagedResult<T>();
        result.CurrentPage = page;
        result.PageSize = pageSize;
        result.RowCount = query.Count();

        var pageCount = (double)result.RowCount / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (page - 1) * pageSize;
        result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();

        return result;
    }
    
    public static IQueryable<T> AddFilter<T>(IQueryable<T> query, string propertyName, string searchTerm)
    {
        var param = Expression.Parameter(typeof(T), "e");
        var propExpression = Expression.Property(param, propertyName);
    
        object value = searchTerm;
        if (propExpression.Type != typeof(string))
            value = Convert.ChangeType(value, propExpression.Type);

        var filterLambda = Expression.Lambda<Func<T, bool>>(
            Expression.Equal(
                propExpression,
                Expression.Constant(value)
            ),
            param
        );

        return query.Where(filterLambda);
    }
}