using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Strategies;

public class RotateToLeftRoverCommandExecutorStrategy : IRoverCommandExecutorStrategy
{
    public bool ApplyOn(char command) => command == 'L';

    public void Execute(Rover rover) => rover.Rotate(RotationDirection.L);
}