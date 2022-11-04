//Codigo basado en solución actividad Domino Sem 2022-2
using System.Net;
using System.Net.Sockets;
namespace Server;

public class SocketView:View
{
    private TcpListener _listener;
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;

    public SocketView()
    {
        _listener = new TcpListener(IPAddress.Loopback, 8001);
        _listener.Start();
        Console.WriteLine("Waiting for connection");
        _client = _listener.AcceptTcpClient();
        Console.WriteLine("Client accepted");
        _reader = new StreamReader(_client.GetStream());
        _writer = new StreamWriter(_client.GetStream());
    }
    protected override void Write(string message)
    {
        _writer.Write(message);
        _writer.Flush();
    }


    protected override string ReadLine()
    {
        Write("[INGRESE INPUT]");
        return _reader.ReadLine();
    }

    public override void Close()
    {
        Write("[FIN JUEGO]");
        _client.Close();
        _listener.Stop();
    }
}