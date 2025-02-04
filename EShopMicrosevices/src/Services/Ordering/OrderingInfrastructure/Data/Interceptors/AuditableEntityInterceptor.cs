namespace OrderingInfrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context!);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context!);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext dbContext)
        {
            if (dbContext == null) return;

            foreach (var entry in dbContext.ChangeTracker.Entries<IEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedBy = "ARJ";
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.HasChangeOwnedEntities())
                {
                    entry.Entity.LastModifiedBy = "ARJ";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
            }
        }
    }

    public static class Extensions
    {// it checks with TRUE or FALSE if the Entry has had any kind of modification
        public static bool HasChangeOwnedEntities(this EntityEntry entry) =>
            entry.References.Any(e => e.TargetEntry != null && e.TargetEntry.Metadata.IsOwned() &&
            (e.TargetEntry.State == EntityState.Added || e.TargetEntry.State == EntityState.Modified));

    }
}
