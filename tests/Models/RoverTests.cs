using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using FluentAssertions;

namespace Deltatre.MarsRovers.Tests.Models;

public class RoverTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void Rover_ToString_ShouldReturnCorrectPosition()
    {
        // Given
        var rover = new Rover(1, 3, Heading.W);

        // When
        var position = rover.ToString();

        // Then
        position.Should().Be("1 3 W");
    }

    [Theory]
    [InlineData(Heading.N, Heading.W)]
    [InlineData(Heading.E, Heading.N)]
    [InlineData(Heading.S, Heading.E)]
    [InlineData(Heading.W, Heading.S)]
    public void Rover_Rotate_ShouldRotateTheRoverToLeft(Heading currentHeading, Heading expectedHeading)
    {
        // Given
        var rover = new Rover(_fixture.Create<int>(), _fixture.Create<int>(), currentHeading);

        // When
        rover.Rotate(RotationDirection.L);

        // Then
        rover.Heading.Should().Be(expectedHeading);
    }

    [Theory]
    [InlineData(Heading.N, Heading.E)]
    [InlineData(Heading.E, Heading.S)]
    [InlineData(Heading.S, Heading.W)]
    [InlineData(Heading.W, Heading.N)]
    public void Rover_Rotate_ShouldRotateTheRoverToRight(Heading currentHeading, Heading expectedHeading)
    {
        // Given
        var rover = new Rover(_fixture.Create<int>(), _fixture.Create<int>(), currentHeading);

        // When
        rover.Rotate(RotationDirection.R);

        // Then
        rover.Heading.Should().Be(expectedHeading);
    }

    [Fact]
    public void Rover_MoveForward_AndCurrentHeadingIsNorth_ShouldChangeTheCoordinates()
    {
        // Given
        var rover = new Rover(1, 3, Heading.N);

        // When
        rover.MoveForward();

        // Then
        rover.X.Should().Be(1);
        rover.Y.Should().Be(4);
        rover.Heading.Should().Be(Heading.N);
    }

    [Fact]
    public void Rover_MoveForward_AndCurrentHeadingIsEast_ShouldChangeTheCoordinates()
    {
        // Given
        var rover = new Rover(1, 3, Heading.E);

        // When
        rover.MoveForward();

        // Then
        rover.X.Should().Be(2);
        rover.Y.Should().Be(3);
        rover.Heading.Should().Be(Heading.E);
    }

    [Fact]
    public void Rover_MoveForward_AndCurrentHeadingIsWest_ShouldChangeTheCoordinates()
    {
        // Given
        var rover = new Rover(1, 3, Heading.W);

        // When
        rover.MoveForward();

        // Then
        rover.X.Should().Be(0);
        rover.Y.Should().Be(3);
        rover.Heading.Should().Be(Heading.W);
    }

    [Fact]
    public void Rover_MoveForward_AndCurrentHeadingIsSouth_ShouldChangeTheCoordinates()
    {
        // Given
        var rover = new Rover(1, 3, Heading.S);

        // When
        rover.MoveForward();

        // Then
        rover.X.Should().Be(1);
        rover.Y.Should().Be(2);
        rover.Heading.Should().Be(Heading.S);
    }
}