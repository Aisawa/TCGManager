using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCGManager.Domain.Entities;

namespace TCGManager.Infrastructure.Persistence.Configurations;

public class UserCardConfiguration : IEntityTypeConfiguration<UserCard>
{
    public void Configure(EntityTypeBuilder<UserCard> builder)
    {
        builder.HasKey(uc => uc.Id);
        builder.Property(uc => uc.UserId).IsRequired().HasMaxLength(450);
        builder.Property(uc => uc.Quantity).HasDefaultValue(1);

        builder.HasIndex(uc => new { uc.UserId, uc.CardSetCardId })
            .HasDatabaseName("IX_UserCard_UserId_CardSetCardId");

        builder.HasQueryFilter(uc => !uc.IsDeleted);

        builder.HasOne(uc => uc.CardSetCard)
            .WithMany(csc => csc.UserCards)
            .HasForeignKey(uc => uc.CardSetCardId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}