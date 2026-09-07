using FluentAssertions;
using TCGManager.Application.Common.Models;
using Xunit;

namespace TCGManager.Application.Tests.Common;

public class PagedResultTests
{
    [Fact]
    public void TotalPages_CalculatesCorrectly()
    {
        // Arrange
        var result = PagedResult<string>.Create(
            items: ["a", "b", "c"],
            totalCount: 25,
            page: 1,
            pageSize: 10);

        // Act & Assert
        result.TotalPages.Should().Be(3);
    }

    [Fact]
    public void HasNextPage_ReturnsTrue_WhenNotLastPage()
    {
        var result = PagedResult<string>.Create([], 25, 1, 10);
        result.HasNextPage.Should().BeTrue();
    }

    [Fact]
    public void HasNextPage_ReturnsFalse_WhenLastPage()
    {
        var result = PagedResult<string>.Create([], 25, 3, 10);
        result.HasNextPage.Should().BeFalse();
    }

    [Fact]
    public void HasPreviousPage_ReturnsFalse_WhenFirstPage()
    {
        var result = PagedResult<string>.Create([], 25, 1, 10);
        result.HasPreviousPage.Should().BeFalse();
    }

    [Fact]
    public void HasPreviousPage_ReturnsTrue_WhenNotFirstPage()
    {
        var result = PagedResult<string>.Create([], 25, 2, 10);
        result.HasPreviousPage.Should().BeTrue();
    }

    [Fact]
    public void TotalPages_ReturnsOne_WhenTotalCountIsZero()
    {
        var result = PagedResult<string>.Create([], 0, 1, 10);
        result.TotalPages.Should().Be(0);
    }
}