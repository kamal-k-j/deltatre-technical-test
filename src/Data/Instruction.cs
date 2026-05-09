using Deltatre.MarsRovers.Models;

namespace Deltatre.MarsRovers.Data;

public record Instruction
(
    Plateau Plateau,
    IReadOnlyList<RoverCommand> RoverCommands
);