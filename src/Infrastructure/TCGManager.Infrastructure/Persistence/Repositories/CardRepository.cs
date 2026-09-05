using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Entities;
using TCGManager.Domain.Interfaces.Repositories;

namespace TCGManager.Infrastructure.Persistence.Repositories;

public class CardRepository(TcgDbContext context)
    : GenericRepository<Card>(context), ICardRepository
{
    public Task<Card?> GetByExternalIdAsync(
        string externalId, int gameId, CancellationToken ct = default) =>
        _set.FirstOrDefaultAsync(
            c => c.ExternalId == externalId && c.GameId == gameId, ct);

    public async Task<IReadOnlyList<Card>> SearchAsync(
        string searchTerm, int gameId, CancellationToken ct = default) =>
        await _set
            .Where(c => c.GameId == gameId &&
                (c.Name.Contains(searchTerm) ||
                 (c.NameFr != null && c.NameFr.Contains(searchTerm))))
            .Take(20)
            .ToListAsync(ct);
}