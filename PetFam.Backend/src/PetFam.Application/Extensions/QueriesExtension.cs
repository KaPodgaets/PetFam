using Microsoft.EntityFrameworkCore;

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
    }
}
