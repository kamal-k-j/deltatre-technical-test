using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Services;

public interface IRoverParser
{
    Rover Parse(string roverPosition);
}