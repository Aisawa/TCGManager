using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Common;
using TCGManager.Domain.Entities;
using TCGManager.Infrastructure.Identity;

namespace TCGManager.Infrastructure.Persistence;

public class TcgDbContext(DbContextOptions<TcgDbContext> options)
    : IdentityDbContext<AppUser>(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Card> Cards => Set<Card>();
    public DbSet<CardSet> CardSets => Set<CardSet>();
    public DbSet<CardSetCard> CardSetCards => Set<CardSetCard>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<UserCard> UserCards => Set<UserCard>();
    public DbSet<UserProduct> UserProducts => Set<UserProduct>();
    public DbSet<SyncLog> SyncLogs => Set<SyncLog>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(TcgDbContext).Assembly);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            entry.Entity.UpdatedAt = DateTime.UtcNow;
            if (entry.State == EntityState.Added)
                entry.Entity.CreatedAt = DateTime.UtcNow;
        }
        return await base.SaveChangesAsync(ct);
    }
}