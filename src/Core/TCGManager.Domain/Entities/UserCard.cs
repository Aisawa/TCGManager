using TCGManager.Domain.Common;
using TCGManager.Domain.Enums;

namespace TCGManager.Domain.Entities;

public class UserCard : BaseEntity
{
    public string UserId { get; set; } = string.Empty;
    public long CardSetCardId { get; set; }
    public CardSetCard CardSetCard { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public CardCondition Condition { get; set; } = CardCondition.NearMint;
    public CardLanguage Language { get; set; } = CardLanguage.French;
    public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
}
