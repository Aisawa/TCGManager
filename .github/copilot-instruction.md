## Architecture : Clean Architecture
Layers: Domain → Application → Infrastructure → API / Web (Blazor)

## Pattern : CQRS avec MediatR
- Commands : <Action><Entity>Command (ex: AddCardToCollectionCommand)
- Queries : Get<Entity>Query
- Handlers : <Name>Handler

## Stack
- .NET 10, C# 13, EF Core 10 Code-First
- MediatR, FluentValidation, Mapster
- Refit + Polly pour les clients HTTP externes
- xUnit + NSubstitute + FluentAssertions pour les tests
- Couverture minimale : 80%

## Multi-TCG
Chaque jeu implémente IGameSyncService.
Les attributs spécifiques sont stockés en JSON dans Card.Attributes.