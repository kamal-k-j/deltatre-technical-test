using Deltatre.MarsRovers.Enums;
using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Services;

public class RoverParser : IRoverParser
{
    public Rover Parse(string roverPosition)
    {
        if (string.IsNullOrWhiteSpace(roverPosition))
        {
            throw new FormatException("The rover position string cannot be null or empty.");
        }

        var roverPositionSplit = roverPosition.Trim().Split(" ");

        if (roverPositionSplit.Length != 3)
        {
            throw new FormatException("Parsing failed: Invalid rover position string provided.");
        }

        var xIsValid = int.TryParse(roverPositionSplit[0], out var x);

        if (!xIsValid)
        {
            throw new FormatException("Parsing failed: The x value provided in the rover position string is not a valid int.");
        }

        var yIsValid = int.TryParse(roverPositionSplit[1], out var y);

        if (!yIsValid)
        {
            throw new FormatException("Parsing failed: The y value provided in the rover position string is not a valid int.");
        }

        var headingIsValid = Enum.TryParse<Heading>(roverPositionSplit[2], out var heading);

        if (!headingIsValid)
        {
            throw new FormatException("Parsing failed: The heading value provided in the rover position string is not a valid enum value.");
        }

        return new(x, y, heading);
    }
}