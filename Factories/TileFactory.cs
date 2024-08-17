using csharp_cli_game.Worlds;

namespace csharp_cli_game.Components
{
    public class TileFactory
    {
        public static Tile CreateGrassTile()
        {
            return new Tile('.', true, "Grass", ConsoleColor.DarkGreen);
        }

        public static Tile CreateWaterTile()
        {
            return new Tile('W', false, "Water", ConsoleColor.DarkBlue);
        }

        public static Tile CreateStoneTile()
        {
            return new Tile('S', true, "Stone", ConsoleColor.DarkGray);
        }
    }
}