namespace csharp_cli_game.Systems;

public class LoggingSystem
{
    private static LoggingSystem? instance;

    public static LoggingSystem Instance
    {
        get
        {
            instance ??= new LoggingSystem();

            return instance;
        }
    }

    private readonly Queue<Dictionary<string, Log>> logQueue = new(3);

    public void Update()
    {
        Console.SetCursorPosition(0, Program.GridHeight + 4);
        foreach (var log in logQueue)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{log.First().Value.message} x{log.First().Value.amount}");
        }
    }

    public void LogMessage(string message)
    {
        // check if the message is already in the queue
        var log = logQueue.FirstOrDefault(x => x.ContainsKey(message));

        if (log != null)
        {
            log[message].amount++;
        }
        else
        {
            logQueue.Enqueue(new Dictionary<string, Log>
            {
                { message, new Log { message = message, amount = 1 } }
            });
        }
    }
}

class Log
{
    public required string message;
    public int amount;
}