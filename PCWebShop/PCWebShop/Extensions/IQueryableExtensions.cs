using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PCWebShop.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, bool isSortAscending, Expression<Func<T, object>> columnsMap)
        {
            if (columnsMap == null)
                return query;

            IOrderedQueryable<T> orderedQueryable;

            if (isSortAscending)
                orderedQueryable = query.OrderBy(columnsMap);
            else
                orderedQueryable = query.OrderByDescending(columnsMap);

            return orderedQueryable;
        }

        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int skip, int pageSize, bool retrieveAll = false)
        {
            if (retrieveAll)
            {
                return query;
            }
            else
            {
                return query.Skip(skip).Take(pageSize);
            }
        }
    }
}

