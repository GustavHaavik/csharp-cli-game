using csharp_cli_game.Components;
using csharp_cli_game.Entities;

namespace csharp_cli_game.Systems;
public class InputSystem(Entity player)
{
    private readonly Entity player = player;

    public void ProcessInput()
    {
        if (!Console.KeyAvailable)
            return;

        var keyInfo = Console.ReadKey(intercept: true);
        var direction = keyInfo.Key switch
        {
            ConsoleKey.W => Direction.Up,
            ConsoleKey.S => Direction.Down,
            ConsoleKey.A => Direction.Left,
            ConsoleKey.D => Direction.Right,
            ConsoleKey.Spacebar => Direction.None, // Spacebar could trigger an action like attack
            _ => Direction.None
        };

        if (direction != Direction.None)
        {
            MovePlayer(player, direction);
        }
    }

    void MovePlayer(Entity player, Direction direction)
    {
        var position = player.GetComponent<Position>()!;
        switch (direction)
        {
            case Direction.Up:
                position.Y = Math.Max(position.Y - 1, 0);
                break;
            case Direction.Down:
                position.Y = Math.Min(position.Y + 1, Program.GridHeight - 1);
                break;
            case Direction.Left:
                position.X = Math.Max(position.X - 1, 0);
                break;
            case Direction.Right:
                position.X = Math.Min(position.X + 1, Program.GridWidth - 1);
                break;
            default:
                break;
        }
    }

    public enum Direction
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}