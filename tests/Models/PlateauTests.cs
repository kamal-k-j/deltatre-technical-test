using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using FluentAssertions;

namespace Deltatre.MarsRovers.Tests.Models;

public class PlateauTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Plateau_IsWithinBounds_WhenIsWithinBounds_ShouldReturnTrue()
    {
        // Given
        var plateau = new Plateau(5, 5);
        var rover = new Rover(1, 3, _fixture.Create<Heading>());

        // When
        var result = plateau.IsWithinBounds(rover);

        // Then
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(7)]
    [InlineData(-7)]
    public void Plateau_IsWithinBounds_WhenXIsOutOfBounds_ShouldReturnFalse(int x)
    {
        // Given
        var plateau = new Plateau(5, 5);
        var rover = new Rover(x, _fixture.Create<int>(), _fixture.Create<Heading>());

        // When
        var result = plateau.IsWithinBounds(rover);

        // Then
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(7)]
    [InlineData(-7)]
    public void Plateau_IsWithinBounds_WhenYIsOutOfBounds_ShouldReturnFalse(int y)
    {
        // Given
        var plateau = new Plateau(5, 5);
        var rover = new Rover(_fixture.Create<int>(), y, _fixture.Create<Heading>());

        // When
        var result = plateau.IsWithinBounds(rover);

        // Then
        result.Should().BeFalse();
    }
}