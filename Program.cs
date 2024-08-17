using System.Diagnostics;
using csharp_cli_game.Components;
using csharp_cli_game.Systems;
using csharp_cli_game.Entities;
using csharp_cli_game.Worlds;

class Program
{
    public const int GridWidth = 30; // 60
    public const int GridHeight = 10;
    const int FrameRate = 30; // Target frames per second
    const int FramesToAverage = 10; // Number of frames to average for FPS calculation

    static readonly LoggingSystem LoggingSystem = LoggingSystem.Instance;

    static EntityFactory EntityFactory = new();
    public static InputSystem InputSystem = null!;


    static void Main(string[] args)
    {
        var player = EntityFactory.CreatePlayer();
        InputSystem = new InputSystem(player);

        HashSet<Entity> enemies = [];

        World world = new(GridWidth, GridHeight, 0.5f);
        world.GenerateWorld();

        var healthSystem = new HealthSystem();
        var enemySpawnerSystem = new EnemySpawnerSystem(5, 3, enemies, EntityFactory, world);
        var renderSystem = new RenderSystem();

        var game = new GameState
        {
            Player = player,
            Enemies = enemies,
            World = world,
            HealthSystem = healthSystem,
            EnemySpawnerSystem = enemySpawnerSystem,
            RenderSystem = renderSystem
        };

        var inputThread = new Thread(() => InputLoop(game));
        var renderThread = new Thread(() => GameLoop(game));

        inputThread.Start();
        renderThread.Start();

        inputThread.Join();
        renderThread.Join();
    }

    static void InputLoop(GameState game)
    {
        var stopwatch = new Stopwatch();

        while (true)
        {
            stopwatch.Restart();

            // Process input
            InputSystem.ProcessInput();

            // Check for game over
            if (CheckGameOver(game)) break;

            // Sleep to control input rate
            stopwatch.Stop();

            // Control input rate
            var elapsedTime = DateTime.Now - stopwatch.Elapsed;
            var delay = Math.Max(1000 / FrameRate - (int)elapsedTime.Millisecond, 0);
            Thread.Sleep(delay);
        }
    }

    static void GameLoop(GameState game)
    {
        var stopwatch = new Stopwatch();
        var frameTimes = new Queue<double>();

        while (true)
        {
            stopwatch.Restart();

            // Render the game world
            game.World.RenderWorld();

            game.RenderSystem?.Render(game.Player, game.World);

            // Update entities
            foreach (var enemy in game.Enemies)
            {
                game.HealthSystem?.Update(enemy);
                game.RenderSystem?.Render(enemy, game.World);
            }

            // Spawn new enemies
            game.EnemySpawnerSystem?.Update(game.Player);



            LoggingSystem.Update();

            // Sleep to control frame rate
            stopwatch.Stop();
            var frameTime = stopwatch.Elapsed.TotalMilliseconds;

            frameTimes.Enqueue(frameTime);

            if (frameTimes.Count > FramesToAverage)
            {
                frameTimes.Dequeue();
            }

            var avgFrameTime = 0.0;
            foreach (var time in frameTimes)
            {
                avgFrameTime += time;
            }
            avgFrameTime /= frameTimes.Count;

            var fps = (int)(1000.0 / avgFrameTime);
            UpdateFPSDisplay(fps, avgFrameTime);

            var delay = Math.Max(1000 / FrameRate - (int)frameTime, 0);
            Thread.Sleep(delay);
        }
    }

    static void UpdateFPSDisplay(int fps, double avgFrameTime)
    {
        Console.ResetColor();
        Console.SetCursorPosition(0, 0);
        Console.CursorVisible = false;
        Console.WriteLine($"FPS: {fps} | Frame Time: {avgFrameTime:F2} ms");
    }

    static bool CheckGameOver(GameState game)
    {
        var playerHealth = game.Player.GetComponent<Health>()!;
        if (playerHealth.Value <= 0)
        {
            Console.SetCursorPosition(0, GridHeight + 3);
            Console.WriteLine("Player is dead. Game over.");
            return true;
        }

        return false;
    }
}

class GameState
{
    public required Entity Player { get; set; }
    public required IEnumerable<Entity> Enemies { get; set; } = [];
    public required World World { get; set; }
    public HealthSystem? HealthSystem { get; set; }
    public EnemySpawnerSystem? EnemySpawnerSystem { get; set; }
    public RenderSystem? RenderSystem { get; set; }
}