using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Data;

public record RoverCommand
(
    Rover Rover,
    string Command
);