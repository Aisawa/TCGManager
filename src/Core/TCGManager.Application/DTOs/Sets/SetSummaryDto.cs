namespace TCGManager.Application.DTOs.Sets;

public class SetSummaryDto
{
    public long Id { get; set; }
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NameFr { get; set; }
    public string? SetCode { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public string? SetType { get; set; }
    public int TotalCards { get; set; }
    public int OwnedCards { get; set; }
    public int GameId { get; set; }

    public double CompletionPercentage =>
        TotalCards == 0 ? 0 : Math.Round((double)OwnedCards / TotalCards * 100, 1);

    public string GetLocalizedName(string lang) =>
        lang == "fr" && !string.IsNullOrEmpty(NameFr) ? NameFr : Name;
}