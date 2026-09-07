using MediatR;
using TCGManager.Domain.Enums;

namespace TCGManager.Application.Features.Collection.Commands;

public record AddCardToCollectionCommand(
    string UserId,
    long CardSetCardId,
    int Quantity,
    CardCondition Condition,
    CardLanguage Language)
    : IRequest<long>;