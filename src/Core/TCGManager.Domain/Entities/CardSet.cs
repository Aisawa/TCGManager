using TCGManager.Domain.Common;

namespace TCGManager.Domain.Entities;

public class CardSet : BaseEntity
{
    public string ExternalId { get; set; } = string.Empty;
    public long GameId { get; set; }
    public Game Game { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string? NameFr { get; set; }
    public string? SetCode { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public string? SetType { get; set; }
    public string? ImageUrl { get; set; }
    public int TotalCards { get; set; }

    public ICollection<CardSetCard> CardSetCards { get; set; } = [];
    public ICollection<Product> Products { get; set; } = [];

    public string GetLocalizedName(string lang) =>
        lang == "fr" && !string.IsNullOrEmpty(NameFr) ? NameFr : Name;
}