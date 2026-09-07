using MediatR;
using TCGManager.Application.DTOs.Cards;

namespace TCGManager.Application.Features.Cards.Queries;

public record GetCardByIdQuery(long Id, string? UserId = null)
    : IRequest<CardDetailDto?>;