using csharp_cli_game.Components;
using csharp_cli_game.Entities;
using csharp_cli_game.Worlds;

namespace csharp_cli_game.Systems;

public class EnemySpawnerSystem
{
    private readonly EntityFactory EntityFactory;

    private readonly int spawnInterval;
    private readonly int maxEnemiesActive;
    private int turnsSinceLastSpawn;
    private readonly HashSet<Entity> enemies;
    private readonly World world;



    public EnemySpawnerSystem(int spawnInterval, int maxEnemiesActive, HashSet<Entity> enemies, EntityFactory entityFactory, World world)
    {
        this.spawnInterval = spawnInterval;
        this.maxEnemiesActive = maxEnemiesActive;
        turnsSinceLastSpawn = 0;
        this.enemies = enemies;
        this.world = world;

        EntityFactory = entityFactory;
    }


    public void Update(Entity player)
    {
        turnsSinceLastSpawn++;

        if (turnsSinceLastSpawn >= spawnInterval)
        {
            if (enemies.Count >= maxEnemiesActive) return;

            // 25% chance to spawn an enemy each turn
            if (new Random().Next(0, 4) == 0)
            {
                SpawnEnemy(player);
                turnsSinceLastSpawn = 0;
            }
        }
    }


    private void SpawnEnemy(Entity player)
    {
        var enemy = EntityFactory.CreateEnemy();

        // Set enemy position near the player (for simplicity, adjust as needed)
        var playerPosition = player.GetComponent<Position>()!;
        var enemyPosition = enemy.GetComponent<Position>()!;

        // random number between 5 and 10
        var randomX = new Random().Next(5, 10);
        var randomY = new Random().Next(5, 10);

        var newX = playerPosition.X + randomX;
        var newY = playerPosition.Y + randomY;

        // Check if the new position is within bounds
        if (!world.IsInBounds(newX, newY)) return;

        enemyPosition.SetPosition(playerPosition.X + randomX, playerPosition.Y + randomY);

        enemies.Add(enemy);

        // Console.WriteLine($"Enemy spawned at position (${enemyPosition})");
        // LoggingSystem.Instance.LogMessage($"Enemy spawned at position (${enemyPosition})");
        LogSystem.Instance.Log($"Enemy spawned at position (${enemyPosition})");
    }
}