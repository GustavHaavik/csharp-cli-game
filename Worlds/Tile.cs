namespace csharp_cli_game.Worlds;

public class Tile
{
    public char Symbol { get; set; }
    public bool IsWalkable { get; set; }
    public string Name { get; set; }
    public ConsoleColor Color { get; set; }

    public Tile(char symbol, bool isWalkable, string name, ConsoleColor color)
    {
        Symbol = symbol;
        IsWalkable = isWalkable;
        Name = name;
        Color = color;
    }
}