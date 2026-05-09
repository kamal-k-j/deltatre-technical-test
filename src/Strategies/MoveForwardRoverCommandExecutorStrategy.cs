using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Strategies;

public class MoveForwardRoverCommandExecutorStrategy : IRoverCommandExecutorStrategy
{
    public bool ApplyOn(char command) => command == 'M';

    public void Execute(Rover rover) => rover.MoveForward();
}