using TCGManager.Domain.Common;
using TCGManager.Domain.Enums;

namespace TCGManager.Domain.Entities;

public class Product : BaseEntity
{
    public long CardSetId { get; set; }
    public CardSet CardSet { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public ProductType ProductType { get; set; }
    public int CardsPerUnit { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? PriceEur { get; set; }

    public ICollection<UserProduct> UserProducts { get; set; } = [];
}
