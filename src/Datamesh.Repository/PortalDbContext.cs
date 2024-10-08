
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PortalBackend.PortalEntities.EntitiesIsoPay;
using System;

/// <summary>
/// Db Context
/// </summary>
/// <remarks>
/// The Trigger Framework requires new Guid() to convert it to gen_random_uuid(),
/// for the Id field we'll use a randomly set UUID to satisfy SonarCloud.
/// </remarks>
public class PortalDbContext : DbContext
{
    public PortalDbContext(DbContextOptions<PortalDbContext> options) : base(options) { }

    //protected PortalDbContext()
    //{
    //    throw new InvalidOperationException("IdentityService should never be null");
    //}
     

    /// <summary>
    ///  IsoPay
    /// </summary>
    public virtual DbSet<Account> Account { get; set; } = default!;
    public virtual DbSet<TransactionType> TransactionTypes { get; set; } = default!;
    public virtual DbSet<AccountHead> AccountHead { get; set; } = default!;
    public virtual DbSet<Currency> Currency { get; set; } = default!;
    public virtual DbSet<Transaction> Transaction { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
         
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       // modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");
      //  modelBuilder.HasDefaultSchema("portal");
    }

    /// <inheritdoc />
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        EnhanceChangedEntries();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        EnhanceChangedEntries();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override int SaveChanges()
    {
        EnhanceChangedEntries();
        return base.SaveChanges();
    }

    private void EnhanceChangedEntries()
    {
        //_auditHandler.HandleAuditForChangedEntries(
        //    ChangeTracker.Entries().Where(entry =>
        //        entry.State != EntityState.Unchanged && entry.State != EntityState.Detached &&
        //        entry.Entity is IAuditableV1).ToImmutableList(),
        //    ChangeTracker.Context);
    }
}
