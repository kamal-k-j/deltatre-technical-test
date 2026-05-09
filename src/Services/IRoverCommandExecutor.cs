using Deltatre.MarsRovers.Data;
using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Services;

public interface IRoverCommandExecutor
{
    string Execute(Plateau plateau, RoverCommand roverCommand);
}