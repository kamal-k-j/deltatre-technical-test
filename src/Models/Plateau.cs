namespace Deltatre.MarsRovers.Models;

public readonly struct Plateau
{
    public Plateau(int x, int y)
    {
        X = x;
        Y = y;
    }

    public readonly int X { get; }

    public readonly int Y { get; }

    public readonly bool IsWithinBounds(Rover rover)
    {
        if (rover.X > X || rover.X < 0)
        {
            return false;
        }

        if (rover.Y > Y || rover.Y < 0)
        {
            return false;
        }

        return true;
    }
}