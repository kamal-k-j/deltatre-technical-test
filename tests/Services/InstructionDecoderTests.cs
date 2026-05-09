using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Services;
using FluentAssertions;
using Moq.AutoMock;

namespace Deltatre.MarsRovers.Tests.Services;

public class InstructionDecoderTests
{
    private readonly AutoMocker _mocker = new();

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void InstructionDecoder_Decode_WhenPassingNullOrEmptyInstructions_ShouldThrowException(string instructions)
    {
        // Given
        var subject = _mocker.CreateInstance<InstructionDecoder>();

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Decode(instructions));

        // Then
        exception.Message.Should().Be("The instructions string cannot be null or empty.");
    }

    [Fact]
    public void InstructionDecoder_Decode_WhenPassingValidInstructions_ShouldReturnTheInstructionsDecoded()
    {
        // Given
        _mocker.Use<IPlateauParser>(new PlateauParser());
        _mocker.Use<IRoverParser>(new RoverParser());
        _mocker.Use<ICommandParser>(new CommandParser());
        var subject = _mocker.CreateInstance<InstructionDecoder>();
        var instructions =
        @"
            5 5
            1 2 N
            LMLMLMLMM
            3 3 E
            MMRMMRMRRM
        ";
        var expectedPlateau = new Plateau(5, 5);
        var expectedRover1 = new Rover(1, 2, Heading.N);
        var expectedRover2 = new Rover(3, 3, Heading.E);
        var expectedCommand1 = "LMLMLMLMM";
        var expectedCommand2 = "MMRMMRMRRM";

        // When
        var result = subject.Decode(instructions);

        // Then
        result.Plateau.Should().BeEquivalentTo(expectedPlateau);
        result.RoverCommands.Should().HaveCount(2);
        result.RoverCommands[0].Rover.Should().BeEquivalentTo(expectedRover1);
        result.RoverCommands[0].Command.Should().BeEquivalentTo(expectedCommand1);
        result.RoverCommands[1].Rover.Should().BeEquivalentTo(expectedRover2);
        result.RoverCommands[1].Command.Should().BeEquivalentTo(expectedCommand2);
    }

    [Fact]
    public void InstructionDecoder_Decode_WhenPassingInvalidInstructions_MissingRoverPosition_ShouldThrowException()
    {
        // Given
        _mocker.Use<IPlateauParser>(new PlateauParser());
        _mocker.Use<IRoverParser>(new RoverParser());
        _mocker.Use<ICommandParser>(new CommandParser());
        var subject = _mocker.CreateInstance<InstructionDecoder>();
        var instructions =
        @"
            5 5
                   
            LMLMLMLMM
        ";

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Decode(instructions));

        // Then
        exception.Message.Should().Be("Invalid instructions format: each rover must have both a position line and a command line.");
    }

    [Fact]
    public void InstructionDecoder_Decode_WhenPassingInvalidInstructions_MissingRoverCommand_ShouldThrowException()
    {
        // Given
        _mocker.Use<IPlateauParser>(new PlateauParser());
        _mocker.Use<IRoverParser>(new RoverParser());
        _mocker.Use<ICommandParser>(new CommandParser());
        var subject = _mocker.CreateInstance<InstructionDecoder>();
        var instructions =
        @"
            5 5
            1 2 N
        ";

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Decode(instructions));

        // Then
        exception.Message.Should().Be("Invalid instructions format: each rover must have both a position line and a command line.");
    }

    [Fact]
    public void InstructionDecoder_Decode_WhenPassingInvalidInstructions_PassingOnlyPlateauDimensions_ShouldReturnPlateauDecodedButNoRoverCommands()
    {
        // Given
        _mocker.Use<IPlateauParser>(new PlateauParser());
        _mocker.Use<IRoverParser>(new RoverParser());
        _mocker.Use<ICommandParser>(new CommandParser());
        var subject = _mocker.CreateInstance<InstructionDecoder>();
        var instructions =
        @"
            5 5
        ";
        var expectedPlateau = new Plateau(5, 5);

        // When
        var exception = Assert.Throws<FormatException>(() => subject.Decode(instructions));

        // Then
        exception.Message.Should().Be("No rover instructions provided.");
    }
}