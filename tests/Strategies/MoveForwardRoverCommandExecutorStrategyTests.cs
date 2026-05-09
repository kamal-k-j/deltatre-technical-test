using AutoFixture;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Strategies;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Strategies;

public class MoveForwardRoverCommandExecutorStrategyTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void MoveForwardRoverCommandExecutorStrategy_ApplyOn_PassingCorrectCommand_ShouldReturnTrue()
    {
        // Given
        var subject = _mocker.CreateInstance<MoveForwardRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn('M');

        // Then
        result.Should().BeTrue();
    }

    [Fact]
    public void MoveForwardRoverCommandExecutorStrategy_ApplyOn_PassingIncorrectCommand_ShouldReturnFalse()
    {
        // Given
        var subject = _mocker.CreateInstance<MoveForwardRoverCommandExecutorStrategy>();

        // When
        var result = subject.ApplyOn(It.Is<char>(c => c != 'M'));

        // Then
        result.Should().BeFalse();
    }

    [Fact]
    public void MoveForwardRoverCommandExecutorStrategy_Execute_ShouldMoveTheRoverForward()
    {
        // Given
        var subject = _mocker.CreateInstance<MoveForwardRoverCommandExecutorStrategy>();
        var x = _fixture.Create<int>();
        var y = _fixture.Create<int>();
        var rover = new Rover(x, y, Heading.N);

        // When
        subject.Execute(rover);

        // Then
        rover.X.Should().Be(x);
        rover.Y.Should().Be(y + 1);
        rover.Heading.Should().Be(Heading.N);
    }
}
