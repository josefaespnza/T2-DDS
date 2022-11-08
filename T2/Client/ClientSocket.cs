//Codigo basado en solución actividad Domino Sem 2022-2

using System.Data;

namespace Client;
using System.Net;
using System.Net.Sockets;

public class ClientSocket
{
    private TcpClient _client;
    private StreamWriter _writer;
    private StreamReader _reader;
    
    public ClientSocket()
    {
        _client = new TcpClient ();
        _client.Connect(IPAddress.Loopback, 8001);
        ConfigStream();
        StableConnection();
        
    }

    private void ConfigStream()
    {
        NetworkStream ns = _client.GetStream();
        _writer = new StreamWriter(ns);
        _reader = new StreamReader(ns);
    }

    private void StableConnection()
    {
        string message = "";
        while (message != "[FIN JUEGO]")
        {
            message = _reader.ReadLine();
            if (message == "[INGRESE INPUT]")
            {
                string input = Console.ReadLine();
                _writer.WriteLine(input);
                _writer.Flush();
            }
            else if (message != "[FIN JUEGO]")
                Console.WriteLine(message);
        }
        _client.Close();
    }
    
    
}