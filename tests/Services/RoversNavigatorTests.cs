using Deltatre.MarsRovers.Services;
using Deltatre.MarsRovers.Strategies;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class RoversNavigatorTests
{
    private readonly AutoMocker _mocker = new();

    [Fact]
    public void RoversNavigator_Navigate_WhenPassingValidInstructions_ShouldReturnCorrectRoversLastPositions()
    {
        // Given
        _mocker.Use<IRoverCommandExecutor>
        (
            new RoverCommandExecutor
            (
                [
                    new RotateToLeftRoverCommandExecutorStrategy(),
                    new RotateToRightRoverCommandExecutorStrategy(),
                    new MoveForwardRoverCommandExecutorStrategy()
                ]
            )
        );
        _mocker
            .Use<IInstructionDecoder>
            (
                new InstructionDecoder
                (
                    new PlateauParser(),
                    new RoverParser(),
                    new CommandParser()
                )
            );
        var subject = _mocker.CreateInstance<RoversNavigator>();
        var instructions = 
        @"
            5 5
            1 2 N
            LMLMLMLMM
            3 3 E
            MMRMMRMRRM
        ";

        // When
        var result = subject.Navigate(instructions);

        // Then
        result.Should().HaveCount(2);
        result[0].Should().Be("1 3 N");
        result[1].Should().Be("5 1 E");
    }

    [Fact]
    public void RoversNavigator_Navigate_WhenPassingValidInstructions_ButNotPassingInstructions_ShouldThrowException()
    {
        // Given
        _mocker
            .Use<IInstructionDecoder>
            (
                new InstructionDecoder
                (
                    new PlateauParser(),
                    new RoverParser(),
                    new CommandParser()
                )
            );
        var subject = _mocker.CreateInstance<RoversNavigator>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Navigate(string.Empty));

        // Then
        exception.Message.Should().Be("The instructions string cannot be null or empty.");
    }
}