namespace TCGManager.Application.DTOs.Cards;

public class CardDto
{
    public long Id { get; set; }
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NameFr { get; set; }
    public string? ImageUrl { get; set; }
    public string? Attributes { get; set; }
    public int GameId { get; set; }
    public int OwnedQuantity { get; set; }

    public string GetLocalizedName(string lang) =>
        lang == "fr" && !string.IsNullOrEmpty(NameFr) ? NameFr : Name;
}