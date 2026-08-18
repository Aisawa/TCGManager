using MediatR;

namespace TCGManager.Application.Features.Collection.Commands;

public record RemoveCardFromCollectionCommand(
    string UserId,
    long UserCardId)
    : IRequest<bool>;