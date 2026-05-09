using Deltatre.MarsRovers.Data;
using Deltatre.MarsRovers.Exceptions;
using Deltatre.MarsRovers.Models;
using Deltatre.MarsRovers.Strategies;

namespace Deltatre.MarsRovers.Services;

public class RoverCommandExecutor : IRoverCommandExecutor
{
    private readonly IEnumerable<IRoverCommandExecutorStrategy> _strategies;

    public RoverCommandExecutor(IEnumerable<IRoverCommandExecutorStrategy> strategies) => 
        _strategies = strategies;

    public string Execute(Plateau plateau, RoverCommand roverCommand)
    {
        var rover = roverCommand.Rover;

        foreach (var command in roverCommand.Command)
        {
            var strategy =
                _strategies.SingleOrDefault(strategy => strategy.ApplyOn(command));

            if (strategy == null)
            {
                throw new NotImplementedException($"Strategy for command {command} not implemented.");
            }

            strategy.Execute(rover);

            if (!plateau.IsWithinBounds(rover))
            {
                throw new RoverOutOfBoundException("Rover went out of plateau bounds.");
            }
        }

        return rover.ToString();
    }
}