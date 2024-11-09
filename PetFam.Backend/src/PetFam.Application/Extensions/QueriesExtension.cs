using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PetFam.Application.Extensions
{
    public static class QueriesExtension
    {
        public static async Task<PagedList<T>> ToPagedList<T>(
            this IQueryable<T> source,
            int page,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var count = await source.CountAsync(cancellationToken);

            var items = await source
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var pagedList = new PagedList<T>()
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalCount = count
            };

            return pagedList;
        }

        public static IQueryable<T> WhereIf<T>(
            this IQueryable<T> source,
            bool condition,
            Expression<Func<T, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
