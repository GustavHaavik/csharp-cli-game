namespace csharp_cli_game.Systems;

public class LogSystem
{
    private static LogSystem? _instance;
    public static LogSystem Instance => _instance ??= new LogSystem();

    private readonly Queue<string> _logs;
    private const int MaxLogCount = 3;

    private LogSystem()
    {
        _logs = new Queue<string>(MaxLogCount);
    }

    public void Log(string message)
    {
        if (_logs.Count >= MaxLogCount)
        {
            _logs.Dequeue();
        }
        _logs.Enqueue(message);
        // UpdateLogsDisplay();
    }

    public void UpdateLogsDisplay()
    {
        // Start rendering logs below the world grid
        int logStartY = Program.GridHeight + 2;

        int index = 0;
        foreach (var log in _logs)
        {
            Console.SetCursorPosition(0, logStartY + index);
            Console.WriteLine(log.PadRight(Console.WindowWidth)); // Clear the line and print the log
            index++;
        }

        // // Clear any extra lines from previous logs
        // for (int i = index; i < MaxLogCount; i++)
        // {
        //     Console.SetCursorPosition(0, logStartY + i);
        //     Console.WriteLine(new string(' ', Console.WindowWidth));
        // }
    }
}