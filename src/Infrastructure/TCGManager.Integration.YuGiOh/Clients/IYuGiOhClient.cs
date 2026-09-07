using Refit;
using TCGManager.Integration.YuGiOh.Dtos;

namespace TCGManager.Integration.YuGiOh.Clients;

public interface IYuGiOhClient
{
    [Get("/api/v7/cardinfo.php?num={pageSize}&offset={offset}&misc=yes")]
    Task<YgoCardListResponse> GetCardsAsync(
        int pageSize,
        int offset,
        CancellationToken ct = default);

    [Get("/api/v7/cardsets.php")]
    Task<List<YgoSetDto>> GetSetsAsync(
        CancellationToken ct = default);
}