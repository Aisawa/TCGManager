using FluentAssertions;
using TCGManager.Domain.Entities;
using Xunit;

namespace TCGManager.Domain.Tests.Entities;

public class CardTests
{
    [Fact]
    public void GetLocalizedName_ReturnsFrench_WhenFrenchAvailable()
    {
        // Arrange
        var card = new Card
        {
            Name = "Dark Magician",
            NameFr = "Magicien des Ténèbres"
        };

        // Act
        var result = card.GetLocalizedName("fr");

        // Assert
        result.Should().Be("Magicien des Ténèbres");
    }

    [Fact]
    public void GetLocalizedName_FallsBackToEnglish_WhenFrenchMissing()
    {
        // Arrange
        var card = new Card
        {
            Name = "Dark Magician",
            NameFr = null
        };

        // Act
        var result = card.GetLocalizedName("fr");

        // Assert
        result.Should().Be("Dark Magician");
    }

    [Fact]
    public void GetLocalizedName_ReturnsEnglish_WhenLangIsEn()
    {
        // Arrange
        var card = new Card
        {
            Name = "Dark Magician",
            NameFr = "Magicien des Ténèbres"
        };

        // Act
        var result = card.GetLocalizedName("en");

        // Assert
        result.Should().Be("Dark Magician");
    }

    [Fact]
    public void GetLocalizedDescription_ReturnsFrench_WhenAvailable()
    {
        // Arrange
        var card = new Card
        {
            Description = "The ultimate wizard in terms of attack and defense.",
            DescriptionFr = "Le magicien ultime en termes d'attaque et de défense."
        };

        // Act
        var result = card.GetLocalizedDescription("fr");

        // Assert
        result.Should().Be("Le magicien ultime en termes d'attaque et de défense.");
    }

    [Fact]
    public void GetLocalizedDescription_FallsBackToEnglish_WhenFrenchMissing()
    {
        // Arrange
        var card = new Card
        {
            Description = "The ultimate wizard.",
            DescriptionFr = null
        };

        // Act
        var result = card.GetLocalizedDescription("fr");

        // Assert
        result.Should().Be("The ultimate wizard.");
    }
}