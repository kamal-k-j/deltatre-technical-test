using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Strategies;

public class RotateToRightRoverCommandExecutorStrategy : IRoverCommandExecutorStrategy
{
    public bool ApplyOn(char command) => command == 'R';

    public void Execute(Rover rover) => rover.Rotate(RotationDirection.R);
}