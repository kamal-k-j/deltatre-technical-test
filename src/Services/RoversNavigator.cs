namespace Deltatre.MarsRovers.Services;

public class RoversNavigator : IRoversNavigator
{
    private readonly IInstructionDecoder _instructionDecoder;
    private readonly IRoverCommandExecutor _roverCommandExecutor;

    public RoversNavigator(
        IInstructionDecoder instructionDecoder,
        IRoverCommandExecutor roverCommandExecutor)
    {
        _instructionDecoder = instructionDecoder;
        _roverCommandExecutor = roverCommandExecutor;
    }

    public IReadOnlyList<string> Navigate(string instructions)
    {
        var instruction = _instructionDecoder.Decode(instructions);

        var roversLastPositions = new List<string>();

        foreach (var roverCommand in instruction.RoverCommands)
        {
            var roverLastPosition =
                _roverCommandExecutor.Execute(instruction.Plateau, roverCommand);

            roversLastPositions.Add(roverLastPosition);
        }

        return roversLastPositions;
    }
}