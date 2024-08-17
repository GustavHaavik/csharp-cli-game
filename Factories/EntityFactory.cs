using csharp_cli_game.Components;

namespace csharp_cli_game.Entities;

public class EntityFactory
{
    public Entity CreatePlayer()
    {
        var player = new Entity();
        var position = new Position(0, 1);
        player.AddComponent(position);
        player.AddComponent(new Movement(position, 1));
        player.AddComponent(new Renderable('P', ConsoleColor.Black));
        player.AddComponent(new Health(100));
        return player;
    }

    public Entity CreateEnemy()
    {
        var enemy = new Entity();
        enemy.AddComponent(new Position(0, 0));
        enemy.AddComponent(new Renderable('E', ConsoleColor.Red));
        enemy.AddComponent(new Health(50));
        return enemy;
    }
}