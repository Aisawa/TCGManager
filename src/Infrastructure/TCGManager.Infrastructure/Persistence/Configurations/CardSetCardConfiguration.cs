using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCGManager.Domain.Entities;

namespace TCGManager.Infrastructure.Persistence.Configurations;

public class CardSetCardConfiguration : IEntityTypeConfiguration<CardSetCard>
{
    public void Configure(EntityTypeBuilder<CardSetCard> builder)
    {
        builder.HasKey(csc => csc.Id);
        builder.Property(csc => csc.SetNumber).HasMaxLength(50);
        builder.Property(csc => csc.Rarity).HasMaxLength(100);
        builder.Property(csc => csc.RarityCode).HasMaxLength(20);

        builder.HasQueryFilter(csc => !csc.IsDeleted);

        builder.HasOne(csc => csc.Card)
            .WithMany(c => c.CardSetCards)
            .HasForeignKey(csc => csc.CardId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(csc => csc.CardSet)
            .WithMany(s => s.CardSetCards)
            .HasForeignKey(csc => csc.CardSetId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}