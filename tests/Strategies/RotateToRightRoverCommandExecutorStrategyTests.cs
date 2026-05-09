using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Strategies;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Strategies;

public class RotateToRightRoverCommandExecutorStrategyTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void RotateToRightRoverCommandExecutorStrategy_ApplyOn_PassingCorrectCommand_ShouldReturnTrue()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToRightRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn('R');

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void RotateToRightRoverCommandExecutorStrategy_ApplyOn_PassingIncorrectCommand_ShouldReturnFalse()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToRightRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn(It.Is<char>(c => c != 'R'));

        // Then
        result.Should().BeFalse();
    }

    [Fact]
    public void RotateToRightRoverCommandExecutorStrategy_Execute_ShouldRotateTheRoverToRight()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToRightRoverCommandExecutorStrategy>();
        var x = _fixture.Create<int>();
        var y = _fixture.Create<int>();
        var rover = new Rover(x, y, Heading.W);

        // When
        subject.Execute(rover);

        // Then
        rover.X.Should().Be(x);
        rover.Y.Should().Be(y);
        rover.Heading.Should().Be(Heading.N);
    }
}