using csharp_cli_game.Components;
using csharp_cli_game.Systems;

namespace csharp_cli_game.Worlds;

public class World
{
    public int Width { get; }
    public int Height { get; }
    public Tile[,] Tiles { get; }
    public float Scale { get; }

    public World(int width, int height, float scale = 0.4f)
    {
        Width = width;
        Height = height;
        Scale = scale;
        Tiles = new Tile[height, width];
    }

    public Tile[,] GenerateWorld()
    {
        Random random = new Random();
        int seed = random.Next();

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                float noiseValue = PerlinNoise(x * Scale, y * Scale, seed);

                if (noiseValue < 0.1f)
                {
                    Tiles[y, x] = TileFactory.CreateWaterTile();
                }
                else if (noiseValue < 0.8f)
                {
                    Tiles[y, x] = TileFactory.CreateGrassTile();
                }
                else
                {
                    Tiles[y, x] = TileFactory.CreateStoneTile();
                }

            }
        }

        return Tiles;
    }

    static float PerlinNoise(float x, float y, int seed)
    {
        int n = (int)x + (int)y * 57 + seed * 131;
        n = (n << 13) ^ n;
        int nn = (n * (n * n * 60493 + 19990303) + 1376312589) & 0x7fffffff;
        return 1.0f - (nn / 1073741824.0f);
    }

    public void RenderWorld()
    {
        Console.CursorVisible = false;
        Console.SetCursorPosition(0, 1);

        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var tile = Tiles[y, x];
                Console.BackgroundColor = tile.Color;
                Console.Write(" ");
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    public Tile? GetTileAt(int x, int y)
    {
        try
        {
            return Tiles[y, x];
        }
        catch (IndexOutOfRangeException)
        {
            return null;
        }
    }

    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }
}
