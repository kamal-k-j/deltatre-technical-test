using Deltatre.MarsRovers.Data;

namespace Deltatre.MarsRovers.Services;

public interface IInstructionDecoder
{
    Instruction Decode(string instructions);
}