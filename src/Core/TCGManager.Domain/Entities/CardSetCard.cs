using TCGManager.Domain.Common;

namespace TCGManager.Domain.Entities;

public class CardSetCard : BaseEntity
{
    public long CardId { get; set; }
    public Card Card { get; set; } = null!;
    public long CardSetId { get; set; }
    public CardSet CardSet { get; set; } = null!;
    public string? SetNumber { get; set; }
    public string? Rarity { get; set; }
    public string? RarityCode { get; set; }

    public ICollection<UserCard> UserCards { get; set; } = [];
}
