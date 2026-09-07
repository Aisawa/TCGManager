using MediatR;
using TCGManager.Application.DTOs.Sets;

namespace TCGManager.Application.Features.Sets.Queries;

public record GetSetByIdQuery(long Id, string? UserId = null)
    : IRequest<SetDetailDto?>;