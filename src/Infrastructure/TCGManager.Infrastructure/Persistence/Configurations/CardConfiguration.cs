using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCGManager.Domain.Entities;

namespace TCGManager.Infrastructure.Persistence.Configurations;

public class CardConfiguration : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.ExternalId).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(500);
        builder.Property(c => c.NameFr).HasMaxLength(500);
        builder.Property(c => c.Attributes).HasColumnType("nvarchar(max)");

        builder.HasIndex(c => new { c.ExternalId, c.GameId })
            .IsUnique()
            .HasDatabaseName("IX_Card_ExternalId_GameId");
        builder.HasIndex(c => c.GameId)
            .HasDatabaseName("IX_Card_GameId");

        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasOne(c => c.Game)
            .WithMany(g => g.Cards)
            .HasForeignKey(c => c.GameId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}