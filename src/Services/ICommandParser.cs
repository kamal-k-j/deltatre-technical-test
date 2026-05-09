namespace Deltatre.MarsRovers.Services;

public interface ICommandParser
{
    string Parse(string command);
}