using TCGManager.Domain.Interfaces;
using TCGManager.Domain.Interfaces.Repositories;
using TCGManager.Infrastructure.Persistence.Repositories;

namespace TCGManager.Infrastructure.Persistence;

public class UnitOfWork(
    TcgDbContext db,
    ICardRepository cards,
    ICardSetRepository sets,
    IUserCardRepository userCards)
    : IUnitOfWork
{
    public ICardRepository Cards { get; } = cards;
    public ICardSetRepository Sets { get; } = sets;
    public IUserCardRepository UserCards { get; } = userCards;

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        db.SaveChangesAsync(ct);

    public void Dispose() => db.Dispose();
}