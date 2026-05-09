using Deltatre.MarsRovers.Data;

namespace Deltatre.MarsRovers.Services;

public class InstructionDecoder : IInstructionDecoder
{
    private readonly IPlateauParser _plateauParser;
    private readonly IRoverParser _roverParser;
    private readonly ICommandParser _commandParser;

    public InstructionDecoder(
        IPlateauParser plateauParser,
        IRoverParser roverParser,
        ICommandParser commandParser)
    {
        _plateauParser = plateauParser;
        _roverParser = roverParser;
        _commandParser = commandParser;
    }

    public Instruction Decode(string instructions)
    {
        if (string.IsNullOrWhiteSpace(instructions))
        {
            throw new FormatException("The instructions string cannot be null or empty.");
        }

        var instructionsSplit = instructions.Split
        (
            Environment.NewLine, 
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );

        var roverInstructions = instructionsSplit.Skip(1).ToArray();

        if (roverInstructions.Length == 0)
        {
            throw new FormatException("No rover instructions provided.");
        }

        if (roverInstructions.Length % 2 != 0)
        {
            throw new FormatException
            (
                "Invalid instructions format: each rover must have both a position line and a command line."
            );
        }

        var plateau = _plateauParser.Parse(instructionsSplit[0]);

        var roverCommands = new List<RoverCommand>();

        for (var i = 0; i < roverInstructions.Length; i += 2)
        {
            var rover = _roverParser.Parse(roverInstructions[i]);

            var command = _commandParser.Parse(roverInstructions[i + 1]);

            roverCommands.Add(new(rover, command));
        }

        return new(plateau, roverCommands);
    }
}