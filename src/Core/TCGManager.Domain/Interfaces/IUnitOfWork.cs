using TCGManager.Domain.Interfaces.Repositories;

namespace TCGManager.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    ICardRepository Cards { get; }
    ICardSetRepository Sets { get; }
    IUserCardRepository UserCards { get; }
    Task<int> SaveChangesAsync(CancellationToken ct = default);
}
