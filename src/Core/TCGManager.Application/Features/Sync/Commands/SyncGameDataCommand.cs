using MediatR;
using TCGManager.Application.DTOs.Sync;

namespace TCGManager.Application.Features.Sync.Commands;

public record SyncGameDataCommand(string GameSlug)
    : IRequest<SyncLogDto>;