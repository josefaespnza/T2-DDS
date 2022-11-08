namespace Server;

public class ConsoleView:View
{
    protected override void WriteForAll(string message) => Console.WriteLine(message);
    protected override void WriteByPlayer(string message, int playerId) => Console.WriteLine(message);

    protected override string ReadLine(int playerId) => Console.ReadLine();
}