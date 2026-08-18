namespace TCGManager.Application.DTOs.Sync;

public class SyncLogDto
{
    public long Id { get; set; }
    public int GameId { get; set; }
    public string GameName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int CardsSynced { get; set; }
    public int SetsSynced { get; set; }
    public int Errors { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TimeSpan? Duration { get; set; }
}