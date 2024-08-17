namespace csharp_cli_game.Components;

public class Movement : Component
{
    public Position Position { get; set; }
    public int Speed { get; set; }

    public Movement(Position position, int speed)
    {
        Position = position;
        Speed = speed;
    }

    public void Move(int x, int y)
    {
        Position.X += x;
        Position.Y += y;
    }
}