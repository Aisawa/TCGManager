using MediatR;
using TCGManager.Application.Common.Models;
using TCGManager.Application.DTOs.Sets;

namespace TCGManager.Application.Features.Sets.Queries;

public record GetSetsQuery(
    string GameSlug,
    int Page = 1,
    int PageSize = 24,
    string? Search = null,
    string? UserId = null)
    : IRequest<PagedResult<SetSummaryDto>>;