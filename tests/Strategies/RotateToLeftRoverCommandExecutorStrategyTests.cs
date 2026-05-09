using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Strategies;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Strategies;

public class RotateToLeftRoverCommandExecutorStrategyTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void RotateToLeftRoverCommandExecutorStrategy_ApplyOn_PassingCorrectCommand_ShouldReturnTrue()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToLeftRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn('L');

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void RotateToLeftRoverCommandExecutorStrategy_ApplyOn_PassingIncorrectCommand_ShouldReturnFalse()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToLeftRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn(It.Is<char>(c => c != 'L'));

        // Then
        result.Should().BeFalse();
    }

    [Fact]
    public void RotateToLeftRoverCommandExecutorStrategy_Execute_ShouldRotateTheRoverToLeft()
    {
        // Given
        var subject = _mocker.CreateInstance<RotateToLeftRoverCommandExecutorStrategy>();
        var x = _fixture.Create<int>();
        var y = _fixture.Create<int>();
        var rover = new Rover(x, y, Heading.N);

        // When
        subject.Execute(rover);

        // Then
        rover.X.Should().Be(x);
        rover.Y.Should().Be(y);
        rover.Heading.Should().Be(Heading.W);
    }
}