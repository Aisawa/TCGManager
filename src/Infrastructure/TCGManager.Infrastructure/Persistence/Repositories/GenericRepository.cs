using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Common;
using TCGManager.Domain.Interfaces.Repositories;

namespace TCGManager.Infrastructure.Persistence.Repositories;

public class GenericRepository<TEntity>(TcgDbContext context)
    : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly TcgDbContext _db = context;
    protected readonly DbSet<TEntity> _set = context.Set<TEntity>();

    public Task<TEntity?> GetByIdAsync(long id, CancellationToken ct = default) =>
        _set.FirstOrDefaultAsync(e => e.Id == id, ct);

    public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default) =>
        await _set.ToListAsync(ct);

    public async Task AddAsync(TEntity entity, CancellationToken ct = default) =>
        await _set.AddAsync(entity, ct);

    public void Update(TEntity entity) =>
        _set.Update(entity);

    public void SoftDelete(TEntity entity)
    {
        entity.IsDeleted = true;
        _set.Update(entity);
    }

    public IQueryable<TEntity> Query() => _set.AsQueryable();
}