using TCGManager.Domain.Common;

namespace TCGManager.Domain.Entities;

public class Game : BaseEntity
{
    public string Slug { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? ApiBaseUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime? LastSyncAt { get; set; }

    public ICollection<Card> Cards { get; set; } = [];
    public ICollection<CardSet> CardSets { get; set; } = [];
}
