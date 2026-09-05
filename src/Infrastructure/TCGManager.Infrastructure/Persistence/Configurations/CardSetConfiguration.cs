using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCGManager.Domain.Entities;

namespace TCGManager.Infrastructure.Persistence.Configurations;

public class CardSetConfiguration : IEntityTypeConfiguration<CardSet>
{
    public void Configure(EntityTypeBuilder<CardSet> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.ExternalId).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Name).IsRequired().HasMaxLength(500);
        builder.Property(s => s.NameFr).HasMaxLength(500);
        builder.Property(s => s.SetCode).HasMaxLength(50);
        builder.Property(s => s.SetType).HasMaxLength(100);

        builder.HasIndex(s => new { s.ExternalId, s.GameId })
            .IsUnique()
            .HasDatabaseName("IX_CardSet_ExternalId_GameId");

        builder.HasQueryFilter(s => !s.IsDeleted);

        builder.HasOne(s => s.Game)
            .WithMany(g => g.CardSets)
            .HasForeignKey(s => s.GameId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}