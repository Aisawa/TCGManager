using TCGManager.Domain.Entities;

namespace TCGManager.Domain.Interfaces.Repositories;

public interface IUserCardRepository : IRepository<UserCard>
{
    Task<IReadOnlyList<UserCard>> GetByUserIdAsync(string userId, CancellationToken ct = default);
    Task<UserCard?> GetByUserAndCardAsync(string userId, long cardSetCardId, CancellationToken ct = default);
}
