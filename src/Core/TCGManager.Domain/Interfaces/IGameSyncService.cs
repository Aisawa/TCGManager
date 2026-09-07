namespace TCGManager.Domain.Interfaces;

public record SyncResult(int CardsSynced, int SetsSynced, int Errors, TimeSpan Duration);
public record SyncProgress(int Current, int Total, string CurrentStep);

public interface IGameSyncService
{
    string GameSlug { get; }
    Task<SyncResult> SyncAsync(
        IProgress<SyncProgress> progress,
        CancellationToken ct = default);
}
