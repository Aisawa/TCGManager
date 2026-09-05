using MediatR;
using Microsoft.AspNetCore.Mvc;
using TCGManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TCGManager.Api.Controllers.V1;

public class GamesController(IMediator mediator, TcgDbContext db)
    : ApiControllerBase(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetGames(CancellationToken ct)
    {
        var games = await db.Games
            .Where(g => g.IsActive && !g.IsDeleted)
            .Select(g => new { g.Id, g.Slug, g.Name, g.LastSyncAt })
            .ToListAsync(ct);

        return Ok(games);
    }
}