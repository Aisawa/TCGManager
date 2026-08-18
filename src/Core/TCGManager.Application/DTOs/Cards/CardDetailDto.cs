namespace TCGManager.Application.DTOs.Cards;

public class CardDetailDto : CardDto
{
    public string? Description { get; set; }
    public string? DescriptionFr { get; set; }
    public List<CardInSetDto> Sets { get; set; } = [];
}

public class CardInSetDto
{
    public long SetId { get; set; }
    public string SetName { get; set; } = string.Empty;
    public string? SetNameFr { get; set; }
    public string? SetNumber { get; set; }
    public string? Rarity { get; set; }
}