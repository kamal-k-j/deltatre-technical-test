namespace Deltatre.MarsRovers.Services;

public interface IRoversNavigator
{
    IReadOnlyList<string> Navigate(string instructions);
}