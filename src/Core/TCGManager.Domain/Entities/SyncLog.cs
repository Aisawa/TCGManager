using TCGManager.Domain.Common;
using TCGManager.Domain.Enums;

namespace TCGManager.Domain.Entities;

public class SyncLog : BaseEntity
{
    public long GameId { get; set; }
    public Game Game { get; set; } = null!;
    public SyncStatus Status { get; set; } = SyncStatus.Pending;
    public int CardsSynced { get; set; }
    public int SetsSynced { get; set; }
    public int Errors { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TimeSpan? Duration { get; set; }
}
