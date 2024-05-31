using FlexCore.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FlexCore.Extentions
{
    public static class QueryableExtensions
    {
        public static async Task<Page<T>> ApplyPaginationAsync<T>(this IQueryable<T> query, Pageable pageable)
        {
                var totalCount = await query.CountAsync();
                var items = await query.Skip(pageable.Page * pageable.Size).Take(pageable.Size).ToListAsync();
                return new Page<T>(items, totalCount, pageable.Page, pageable.Size);
        }

        public static IOrderedQueryable<T> ApplySorting<T>(this IQueryable<T> query, List<Sort>? sorts)
        {
            IOrderedQueryable<T>? orderedQuery = null;
            if (sorts != null)
            {
                foreach (var sort in sorts)
                {
                    if (sort.By != null)
                    {
                        orderedQuery = sort.Descending ? query.OrderByDescending(e => EF.Property<object>(e, sort.By)) : query.OrderBy(e => EF.Property<object>(e, sort.By));
                    }
                }
            }
            return orderedQuery ?? (IOrderedQueryable<T>)query;
        }
    }
}
