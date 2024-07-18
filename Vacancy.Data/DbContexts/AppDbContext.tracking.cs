using Microsoft.EntityFrameworkCore;
using Vacancy.Domain.Commons;

namespace Vacancy.Data.DbContexts;

public partial class AppDbContext
{
    public override int SaveChanges()
    {
        TrackActionAt();
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        TrackActionAt();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        TrackActionAt();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        TrackActionAt();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected void TrackActionAt()
    {
        //Added entities createdAt and updatedAt are set
        var addedEntities = this.ChangeTracker
        .Entries()
        .Where(x => x.State == EntityState.Added && x.Entity is Auditable)
        .Select(x => x.Entity as Auditable);

        foreach (var entity in addedEntities)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        //Modified entites updatedAt is set
        var modifiedEntities = this.ChangeTracker
            .Entries()
            .Where(x => x.State == EntityState.Modified && x.Entity is Auditable)
            .Select(y => y.Entity as Auditable);

        foreach (var entity in modifiedEntities)
        {
            entity.UpdatedAt = DateTime.UtcNow;
        }
    }
}