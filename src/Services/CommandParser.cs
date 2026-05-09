namespace Deltatre.MarsRovers.Services;

public class CommandParser : ICommandParser
{
    public string Parse(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
        {
            throw new FormatException("The command string cannot be null or empty.");
        }

        command = command.Trim().ToUpper();

        var isNotValid = command.Any(c => c != 'L' && c != 'R' && c != 'M');

        if (isNotValid)
        {
            throw new FormatException("Parsing failed: The command can only assume the values L, R and M.");
        }

        return command;
    }
}