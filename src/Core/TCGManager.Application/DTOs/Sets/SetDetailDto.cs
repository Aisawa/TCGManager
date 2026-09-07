using TCGManager.Application.DTOs.Cards;

namespace TCGManager.Application.DTOs.Sets;

public class SetDetailDto : SetSummaryDto
{
    public List<CardDto> Cards { get; set; } = [];
    public List<ProductDto> Products { get; set; } = [];
}

public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ProductType { get; set; } = string.Empty;
    public int CardsPerUnit { get; set; }
    public string? ImageUrl { get; set; }
    public decimal? PriceEur { get; set; }
    public int OwnedQuantity { get; set; }
}