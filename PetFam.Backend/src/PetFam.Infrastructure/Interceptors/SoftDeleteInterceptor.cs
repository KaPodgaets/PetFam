using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PetFam.Domain.Shared;

namespace PetFam.Infrastructure.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public async override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {

            if (eventData.Context is null)
            {
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }

            var entries = eventData.Context.ChangeTracker
                .Entries<ISoftDeletable>()
                .Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                entry.State = EntityState.Modified;
                entry.Entity.Delete();
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
