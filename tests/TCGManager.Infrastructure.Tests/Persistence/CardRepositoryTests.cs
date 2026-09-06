using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TCGManager.Domain.Entities;
using TCGManager.Infrastructure.Persistence;
using TCGManager.Infrastructure.Persistence.Repositories;
using Testcontainers.MsSql;
using Xunit;

namespace TCGManager.Infrastructure.Tests.Persistence;

public class CardRepositoryTests : IAsyncLifetime
{
    private readonly MsSqlContainer _sqlContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
        .Build();

    private TcgDbContext _dbContext = null!;
    private CardRepository _repository = null!;

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();

        var options = new DbContextOptionsBuilder<TcgDbContext>()
            .UseSqlServer(_sqlContainer.GetConnectionString())
            .Options;

        _dbContext = new TcgDbContext(options);
        await _dbContext.Database.MigrateAsync();

        _repository = new CardRepository(_dbContext);

        // Seed : ajouter un Game nécessaire (FK)
        var game = new Game
        {
            Slug = "yugioh",
            Name = "Yu-Gi-Oh!",
            IsActive = true
        };
        await _dbContext.Games.AddAsync(game);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContext.DisposeAsync();
        await _sqlContainer.DisposeAsync();
    }

    [Fact]
    public async Task AddAsync_ThenGetById_ReturnsCard()
    {
        // Arrange
        var game = await _dbContext.Games.FirstAsync();
        var card = new Card
        {
            ExternalId = "46986414",
            GameId = game.Id,
            Name = "Dark Magician",
            NameFr = "Magicien des Ténèbres"
        };

        // Act
        await _repository.AddAsync(card);
        await _dbContext.SaveChangesAsync();

        var found = await _repository.GetByIdAsync(card.Id);

        // Assert
        found.Should().NotBeNull();
        found!.Name.Should().Be("Dark Magician");
        found.NameFr.Should().Be("Magicien des Ténèbres");
    }

    [Fact]
    public async Task GetByExternalIdAsync_ReturnsCard_WhenExists()
    {
        // Arrange
        var game = await _dbContext.Games.FirstAsync();
        var card = new Card
        {
            ExternalId = "99999",
            GameId = game.Id,
            Name = "Test Card"
        };
        await _repository.AddAsync(card);
        await _dbContext.SaveChangesAsync();

        // Act
        var found = await _repository.GetByExternalIdAsync("99999", (int)game.Id);

        // Assert
        found.Should().NotBeNull();
        found!.Name.Should().Be("Test Card");
    }

    [Fact]
    public async Task SoftDelete_MarksCardAsDeleted_NotReturnedByQuery()
    {
        // Arrange
        var game = await _dbContext.Games.FirstAsync();
        var card = new Card
        {
            ExternalId = "11111",
            GameId = game.Id,
            Name = "Card To Delete"
        };
        await _repository.AddAsync(card);
        await _dbContext.SaveChangesAsync();

        // Act
        _repository.SoftDelete(card);
        await _dbContext.SaveChangesAsync();

        var found = await _repository.GetByIdAsync(card.Id);

        // Assert
        found.Should().BeNull(); // Le global query filter exclut IsDeleted = true
    }
}