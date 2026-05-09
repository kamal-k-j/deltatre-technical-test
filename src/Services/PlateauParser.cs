using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Services;

public class PlateauParser : IPlateauParser
{
    public Plateau Parse(string plateauDimensions)
    {
        if (string.IsNullOrWhiteSpace(plateauDimensions))
        {
            throw new FormatException("The plateau dimensions string cannot be null or empty.");
        }

        var plateauDimensionsSplit = plateauDimensions.Trim().Split(" ");

        if (plateauDimensionsSplit.Length != 2)
        {
            throw new FormatException("Parsing failed: Invalid plateau dimensions string provided.");
        }

        var xIsValid = int.TryParse(plateauDimensionsSplit[0], out var x);

        if (!xIsValid)
        {
            throw new FormatException("Parsing failed: The x value provided in the plateau dimensions string is not a valid int.");
        }

        var yIsValid = int.TryParse(plateauDimensionsSplit[1], out var y);

        if (!yIsValid)
        {
            throw new FormatException("Parsing failed: The y value provided in the plateau dimensions string is not a valid int.");
        }

        return new(x, y);
    }
}