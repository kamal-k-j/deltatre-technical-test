using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Services;

public interface IPlateauParser
{
    Plateau Parse(string plateauDimensions);
}