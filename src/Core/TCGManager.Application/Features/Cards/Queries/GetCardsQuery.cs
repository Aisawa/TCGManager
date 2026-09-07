using MediatR;
using TCGManager.Application.Common.Models;
using TCGManager.Application.DTOs.Cards;

namespace TCGManager.Application.Features.Cards.Queries;

public record GetCardsQuery(
    string GameSlug,
    int Page = 1,
    int PageSize = 24,
    string? Search = null,
    bool? OwnedOnly = null,
    string? UserId = null)
    : IRequest<PagedResult<CardDto>>;