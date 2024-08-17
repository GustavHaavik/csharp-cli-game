using csharp_cli_game.Components;
using csharp_cli_game.Entities;

namespace csharp_cli_game.Systems;

public class HealthSystem
{
    public void Update(Entity entity)
    {
        var health = entity.GetComponent<Health>();
        if (health != null && health.Value <= 0)
        {
            Console.WriteLine("Entity is dead.");
        }
    }
}