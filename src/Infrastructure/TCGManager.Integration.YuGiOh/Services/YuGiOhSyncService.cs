using System.Diagnostics;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using TCGManager.Domain.Entities;
using TCGManager.Domain.Interfaces;
using TCGManager.Domain.Interfaces.Repositories;
using TCGManager.Integration.YuGiOh.Clients;
using TCGManager.Integration.YuGiOh.Dtos;

namespace TCGManager.Integration.YuGiOh.Services;

public class YuGiOhSyncService(
    IYuGiOhClient client,
    ICardRepository cardRepository,
    ICardSetRepository setRepository,
    IUnitOfWork unitOfWork,
    ILogger<YuGiOhSyncService> logger)
    : IGameSyncService
{
    private const int BatchSize = 100;
    private const int DelayBetweenBatchesMs = 300;
    private const int YuGiOhGameId = 1;

    public string GameSlug => "yugioh";

    public async Task<SyncResult> SyncAsync(
        IProgress<SyncProgress> progress,
        CancellationToken ct = default)
    {
        var sw = Stopwatch.StartNew();
        int cardsSynced = 0;
        int setsSynced = 0;
        int errors = 0;

        try
        {
            // Sync des sets
            logger.LogInformation("Starting YuGiOh sets sync");
            progress.Report(new SyncProgress(0, 0, "Synchronisation des sets..."));

            var sets = await client.GetSetsAsync(ct);
            var setCache = new Dictionary<string, long>();

            foreach (var dto in sets)
            {
                try
                {
                    var existing = await setRepository
                        .GetByExternalIdAsync(dto.SetCode, YuGiOhGameId, ct);

                    if (existing is null)
                    {
                        var newSet = MapToCardSet(dto);
                        await setRepository.AddAsync(newSet, ct);
                        await unitOfWork.SaveChangesAsync(ct);
                        setCache[dto.SetCode] = newSet.Id;
                    }
                    else
                    {
                        UpdateCardSet(existing, dto);
                        setRepository.Update(existing);
                        setCache[dto.SetCode] = existing.Id;
                    }

                    setsSynced++;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error syncing set {SetCode}", dto.SetCode);
                    errors++;
                }
            }

            await unitOfWork.SaveChangesAsync(ct);
            logger.LogInformation("Sets sync completed: {Count} sets", setsSynced);

            // Sync des cartes par batch
            logger.LogInformation("Starting YuGiOh cards sync");
            int offset = 0;
            int total = 0;

            while (true)
            {
                ct.ThrowIfCancellationRequested();

                var response = await client.GetCardsAsync(BatchSize, offset, ct);

                if (response.Data.Count == 0) break;

                if (total == 0) total = response.Meta.TotalRows;

                foreach (var dto in response.Data)
                {
                    try
                    {
                        var existing = await cardRepository
                            .GetByExternalIdAsync(dto.Id.ToString(), YuGiOhGameId, ct);

                        if (existing is null)
                        {
                            var newCard = MapToCard(dto);
                            await cardRepository.AddAsync(newCard, ct);
                        }
                        else
                        {
                            UpdateCard(existing, dto);
                            cardRepository.Update(existing);
                        }

                        cardsSynced++;
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Error syncing card {CardId}", dto.Id);
                        errors++;
                    }
                }

                await unitOfWork.SaveChangesAsync(ct);

                progress.Report(new SyncProgress(cardsSynced, total, "Synchronisation des cartes..."));
                logger.LogInformation("Cards synced: {Count}/{Total}", cardsSynced, total);

                if (response.Meta.NextPageOffset is null) break;

                offset += BatchSize;
                await Task.Delay(DelayBetweenBatchesMs, ct);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "YuGiOh sync failed");
            errors++;
        }

        sw.Stop();
        return new SyncResult(cardsSynced, setsSynced, errors, sw.Elapsed);
    }

    private static Card MapToCard(YgoCardDto dto)
    {
        var miscInfo = dto.MiscInfo.FirstOrDefault();
        var attributes = JsonSerializer.Serialize(new
        {
            dto.Type,
            dto.Atk,
            dto.Def,
            dto.Level,
            dto.Race,
            dto.Attribute
        });

        return new Card
        {
            ExternalId = dto.Id.ToString(),
            GameId = YuGiOhGameId,
            Name = dto.Name,
            NameFr = miscInfo?.NameFr,
            Description = dto.Desc,
            DescriptionFr = miscInfo?.DescFr,
            ImageUrl = dto.CardImages.FirstOrDefault()?.ImageUrl,
            Attributes = attributes
        };
    }

    private static void UpdateCard(Card card, YgoCardDto dto)
    {
        var miscInfo = dto.MiscInfo.FirstOrDefault();
        card.Name = dto.Name;
        card.NameFr = miscInfo?.NameFr;
        card.Description = dto.Desc;
        card.DescriptionFr = miscInfo?.DescFr;
        card.ImageUrl = dto.CardImages.FirstOrDefault()?.ImageUrl;
    }

    private static CardSet MapToCardSet(YgoSetDto dto)
    {
        DateOnly? releaseDate = null;
        if (!string.IsNullOrEmpty(dto.TcgDate) &&
            DateOnly.TryParse(dto.TcgDate, out var date))
            releaseDate = date;

        return new CardSet
        {
            ExternalId = dto.SetCode,
            GameId = YuGiOhGameId,
            Name = dto.SetName,
            SetCode = dto.SetCode,
            TotalCards = dto.NumOfCards,
            ReleaseDate = releaseDate,
            ImageUrl = dto.SetImage,
            SetType = "Booster Pack"
        };
    }

    private static void UpdateCardSet(CardSet set, YgoSetDto dto)
    {
        set.Name = dto.SetName;
        set.TotalCards = dto.NumOfCards;
        set.ImageUrl = dto.SetImage;
    }
}