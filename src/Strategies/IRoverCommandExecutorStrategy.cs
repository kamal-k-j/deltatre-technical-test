using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Strategies;

public interface IRoverCommandExecutorStrategy
{
    bool ApplyOn(char command);

    void Execute(Rover rover);
}