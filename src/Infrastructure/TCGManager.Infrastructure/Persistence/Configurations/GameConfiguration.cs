using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCGManager.Domain.Entities;

namespace TCGManager.Infrastructure.Persistence.Configurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Slug).IsRequired().HasMaxLength(50);
        builder.Property(g => g.Name).IsRequired().HasMaxLength(100);
        builder.Property(g => g.ApiBaseUrl).HasMaxLength(500);
        builder.HasIndex(g => g.Slug).IsUnique();
        builder.HasQueryFilter(g => !g.IsDeleted);
    }
}