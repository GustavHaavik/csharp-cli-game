namespace csharp_cli_game.Components
{
    public class Position : Component
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x = 0, int y = 0)
        {
            X = x;
            Y = y;
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEqual(Position other) => X == other.X && Y == other.Y;

        public override string ToString() => $"X: {X}, Y: {Y}";


    }
}