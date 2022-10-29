namespace Server;

public class ConsoleView:View
{
    protected override void Write(string message) => Console.WriteLine(message);
    

    protected override string ReadLine() => Console.ReadLine();
}