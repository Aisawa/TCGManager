using TCGManager.Domain.Common;

namespace TCGManager.Domain.Entities;

public class Card : BaseEntity
{
    public string ExternalId { get; set; } = string.Empty;
    public long GameId { get; set; }
    public Game Game { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? NameFr { get; set; }
    public string? Description { get; set; }
    public string? DescriptionFr { get; set; }
    public string? ImageUrl { get; set; }
    public string? Attributes { get; set; }

    public ICollection<CardSetCard> CardSetCards { get; set; } = [];

    public string GetLocalizedName(string lang) =>
        lang == "fr" && !string.IsNullOrEmpty(NameFr) ? NameFr : Name;

    public string GetLocalizedDescription(string lang) =>
        lang == "fr" && !string.IsNullOrEmpty(DescriptionFr) ? DescriptionFr : Description ?? string.Empty;
}