using TCGManager.Domain.Entities;

namespace TCGManager.Domain.Interfaces.Repositories;

public interface ICardSetRepository : IRepository<CardSet>
{
    Task<CardSet?> GetByExternalIdAsync(string externalId, int gameId, CancellationToken ct = default);
    Task<CardSet?> GetWithCardsAsync(long setId, CancellationToken ct = default);
}
