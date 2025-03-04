using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RatingEngine.Persistance.Configurations;
using RatingEngine.Persistance.Entities;

namespace RatingEngine.Persistance
{
    public class RatingEngineContext : DbContext
    {
        public RatingEngineContext(DbContextOptions<RatingEngineContext> options) : base(options)
        {
        }
        public DbSet<Province> Provinces { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);
            foreach (var entity in modifiedEntries)
            {
                entity.CurrentValues["UpdatedDateTime"] = DateTime.Now;
            }
            var newEntries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            foreach (var entity in newEntries)
            {
                entity.CurrentValues["CreatedDateTime"] = DateTime.Now;
                entity.CurrentValues["IsActive"] = true;
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
