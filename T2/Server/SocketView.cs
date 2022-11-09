//Codigo basado en solución actividad Domino Sem 2022-2
using System.Net;
using System.Net.Sockets;
using Microsoft.VisualBasic;

namespace Server;

public class SocketView:View
{
    private TcpListener _listener;
    private List<TcpClient> _clients;
    
    private Dictionary<int, StreamReader> _reader;
    private Dictionary<int, StreamWriter> _writer;

    public SocketView()
    {
        InitializeClients();
        StartListener();
        InitializeStreams();
        AcceptConnections();
        StartGameMessage();

    }

    private void InitializeClients()
    {
        _clients = new List<TcpClient>();
    }

    private void StartListener()
    {
        _listener = new TcpListener(IPAddress.Loopback, 8001);
        _listener.Start();
    }

    private void InitializeStreams()
    {
        _reader= new Dictionary<int, StreamReader>();
        _writer = new Dictionary<int, StreamWriter>();
    }

    private void AcceptConnections()
    { 
        int numOfPlayers = 0;
        while (numOfPlayers < 2)
        {
            Console.WriteLine("Waiting for connections");
            AcceptClient(numOfPlayers);
            Console.WriteLine("Client accepted");
            numOfPlayers++;
        }
    }

    private void AcceptClient(int playerIndex)
    {
        TcpClient client = _listener.AcceptTcpClient();
        _clients.Add(client);
        _reader[playerIndex] = new StreamReader(_clients[playerIndex].GetStream());
        _writer[playerIndex] = new StreamWriter(_clients[playerIndex].GetStream());
    }

    private void StartGameMessage()
    {
        string msg = "Que comience el juego!!!";
        Console.WriteLine(msg);
            
    }
    
    
    protected override void WriteForAll(string message)
    {
        
        foreach (var playerId in _writer.Keys)
        {
            _writer[playerId].WriteLine(message);
            _writer[playerId].Flush();
        }
    }
    protected override void WriteByPlayer(string message, int playerId)
    {
       
        _writer[playerId].WriteLine(message);
        _writer[playerId].Flush();
       
    }


    protected override string ReadLine(int playerId)
    {
       
        WriteByPlayer("[INGRESE INPUT]",playerId);
        return _reader[playerId].ReadLine();
        
    }

    public override void Close()
    {
        WriteForAll("[FIN JUEGO]");
        foreach (var client in _clients)
        {
            client.Close();
        }
        _listener.Stop();
        
    }
    
    
}