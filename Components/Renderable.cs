using csharp_cli_game.Worlds;

namespace csharp_cli_game.Components
{
    public class Renderable : Component
    {
        public char Symbol { get; set; }
        public ConsoleColor Color { get; set; }

        public Renderable(char symbol, ConsoleColor color)
        {
            Color = color;
            Symbol = symbol;
        }

        public void Render(World world, Position position)
        {
            var tile = world.GetTileAt(position.X * 2, position.Y);
            if (tile == null)
                return;

            Console.CursorVisible = false;
            Console.BackgroundColor = tile.Color;
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(Symbol);
            Console.ResetColor();
        }
    }
}