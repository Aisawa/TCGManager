using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Entities;
using TCGManager.Domain.Interfaces.Repositories;

namespace TCGManager.Infrastructure.Persistence.Repositories;

public class UserCardRepository(TcgDbContext context)
    : GenericRepository<UserCard>(context), IUserCardRepository
{
    public async Task<IReadOnlyList<UserCard>> GetByUserIdAsync(
        string userId, CancellationToken ct = default) =>
        await _set
            .Where(uc => uc.UserId == userId)
            .Include(uc => uc.CardSetCard)
                .ThenInclude(csc => csc.Card)
            .ToListAsync(ct);

    public Task<UserCard?> GetByUserAndCardAsync(
        string userId, long cardSetCardId, CancellationToken ct = default) =>
        _set.FirstOrDefaultAsync(
            uc => uc.UserId == userId && uc.CardSetCardId == cardSetCardId, ct);
}