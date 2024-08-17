namespace csharp_cli_game.Components;

public class Health : Component
{
    public int Value { get; private set; }

    public Health(int value) => Value = value;

    public void TakeDamage(int amount) => Value = Math.Max(Value - amount, 0);
}