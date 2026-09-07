using TCGManager.Domain.Entities;

namespace TCGManager.Domain.Interfaces.Repositories;

public interface ICardRepository : IRepository<Card>
{
    Task<Card?> GetByExternalIdAsync(string externalId, int gameId, CancellationToken ct = default);
    Task<IReadOnlyList<Card>> SearchAsync(string searchTerm, int gameId, CancellationToken ct = default);
}
