using AutoFixture;
using Deltatre.MarsRovers.Data;
using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Exceptions;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Services;
using Deltatre.MarsRovers.Strategies;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class RoverCommandExecutorTests
{
    private readonly AutoMocker _mocker = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public void RoverCommandExecutor_Execute_WhenNoStrategyFound_ShouldThrowException()
    {
        // Given
        _mocker.Use<IEnumerable<IRoverCommandExecutorStrategy>>([]);
        var subject = _mocker.CreateInstance<RoverCommandExecutor>();
        var plateau = _fixture.Create<Plateau>();
        var roverCommand = new RoverCommand
        (
            _fixture.Create<Rover>(),
            "LMRMRMRMRRMLLLL"
        );

        // When
        var exception = Assert.Throws<NotImplementedException>(() => subject.Execute(plateau, roverCommand));

        // Then
        exception.Message.Should().Be($"Strategy for command L not implemented.");
    }

    [Fact]
    public void RoverCommandExecutor_Execute_WhenRoverWentOutOfPlateauBounds_ShouldThrowException()
    {
        // Given
        _mocker.Use<IEnumerable<IRoverCommandExecutorStrategy>>([new MoveForwardRoverCommandExecutorStrategy()]);
        var subject = _mocker.CreateInstance<RoverCommandExecutor>();
        var plateau = new Plateau(10, 10);
        var roverCommand = new RoverCommand
        (
            new Rover(1, 1, Heading.N),
            "MMMMMMMMMMMMMMM"
        );

        // When
        var exception = Assert.Throws<RoverOutOfBoundException>(() => subject.Execute(plateau, roverCommand));

        // Then
        exception.Message.Should().Be("Rover went out of plateau bounds.");
    }

    [Fact]
    public void RoverCommandExecutor_Execute_WhenNoRoverCommand_ShouldThrowException()
    {
        // Given
        _mocker.Use<IEnumerable<IRoverCommandExecutorStrategy>>([new MoveForwardRoverCommandExecutorStrategy()]);
        var subject = _mocker.CreateInstance<RoverCommandExecutor>();
        var plateau = new Plateau(1, 1);
        var roverCommand = new RoverCommand
        (
            new Rover(1, 1, Heading.N),
            string.Empty
        );

        // When
        var result = subject.Execute(plateau, roverCommand);

        // Then
        result.Should().Be("1 1 N");
    }

    [Fact]
    public void RoverCommandExecutor_Execute_WhenMoveForwardRoverCommand_ShouldThrowException()
    {
        // Given
        _mocker.Use<IEnumerable<IRoverCommandExecutorStrategy>>([new MoveForwardRoverCommandExecutorStrategy()]);
        var subject = _mocker.CreateInstance<RoverCommandExecutor>();
        var plateau = new Plateau(2, 2);
        var roverCommand = new RoverCommand
        (
            new Rover(1, 1, Heading.N),
            "M"
        );

        // When
        var result = subject.Execute(plateau, roverCommand);

        // Then
        result.Should().Be("1 2 N");
    }
}