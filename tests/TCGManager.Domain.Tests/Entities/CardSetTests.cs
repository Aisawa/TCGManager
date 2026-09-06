using FluentAssertions;
using TCGManager.Domain.Entities;
using Xunit;

namespace TCGManager.Domain.Tests.Entities;

public class CardSetTests
{
    [Fact]
    public void GetLocalizedName_ReturnsFrench_WhenAvailable()
    {
        // Arrange
        var set = new CardSet
        {
            Name = "Duelist Nexus",
            NameFr = "Duelliste Nexus"
        };

        // Act
        var result = set.GetLocalizedName("fr");

        // Assert
        result.Should().Be("Duelliste Nexus");
    }

    [Fact]
    public void GetLocalizedName_FallsBackToEnglish_WhenFrenchMissing()
    {
        // Arrange
        var set = new CardSet
        {
            Name = "Duelist Nexus",
            NameFr = null
        };

        // Act
        var result = set.GetLocalizedName("fr");

        // Assert
        result.Should().Be("Duelist Nexus");
    }
}