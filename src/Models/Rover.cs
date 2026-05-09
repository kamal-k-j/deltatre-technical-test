using Deltatre.MarsRovers.Enums;

namespace Deltatre.MarsRovers.Models;

public class Rover
{
    public Rover(int x, int y, Heading heading)
    {
        X = x;
        Y = y;
        Heading = heading;
    }

    public int X { get; private set; }

    public int Y { get; private set; }

    public Heading Heading { get; private set; }

    public override string ToString() => $"{X} {Y} {Heading}";

    public void Rotate(RotationDirection rotationDirection)
    {
        if (rotationDirection is RotationDirection.L)
        {
            if (Heading is Heading.N)
            {
                Heading = Heading.W;

                return;
            }

            Heading--;

            return;
        }

        if (Heading is Heading.W)
        {
            Heading = Heading.N;

            return;
        }

        Heading++;
    }

    public void MoveForward()
    {
        switch (Heading)
        {
            case Heading.N: Y++; break;
            case Heading.E: X++; break;
            case Heading.S: Y--; break;
            case Heading.W: X--; break;
        }
    }
}