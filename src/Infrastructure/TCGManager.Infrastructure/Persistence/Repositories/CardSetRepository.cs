using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Entities;
using TCGManager.Domain.Interfaces.Repositories;

namespace TCGManager.Infrastructure.Persistence.Repositories;

public class CardSetRepository(TcgDbContext context)
    : GenericRepository<CardSet>(context), ICardSetRepository
{
    public Task<CardSet?> GetByExternalIdAsync(
        string externalId, int gameId, CancellationToken ct = default) =>
        _set.FirstOrDefaultAsync(
            s => s.ExternalId == externalId && s.GameId == gameId, ct);

    public Task<CardSet?> GetWithCardsAsync(long setId, CancellationToken ct = default) =>
        _set
            .Include(s => s.CardSetCards)
                .ThenInclude(csc => csc.Card)
            .FirstOrDefaultAsync(s => s.Id == setId, ct);
}