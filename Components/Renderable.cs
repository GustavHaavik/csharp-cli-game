using csharp_cli_game.Worlds;

namespace csharp_cli_game.Components
{
    public class Renderable(char symbol, ConsoleColor color) : Component
    {
        public char Symbol { get; set; } = symbol;
        public ConsoleColor Color { get; set; } = color;

        public Position LastPosition { get; private set; } = new Position();

        public void Render(World world, Position position)
        {
            if (LastPosition.IsEqual(position))
                return;

            if (!world.IsInBounds(position.X, position.Y))
                return;

            world.RenderWorld();

            var tile = world.GetTileAt(position.X, position.Y);
            if (tile == null)
                return;

            Console.CursorVisible = false;
            Console.BackgroundColor = tile.Color;
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(position.X, position.Y + 1);
            Console.Write(Symbol);
            Console.ResetColor();

            LastPosition.SetPosition(position.X, position.Y);
            return;
        }
    }
}