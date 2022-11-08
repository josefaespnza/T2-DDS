//Codigo basado en solución actividad Domino Sem 2022-2
using System.Net;
using System.Net.Sockets;
namespace Server;

public class SocketView:View
{
    private TcpListener _listener;
    private List<TcpClient> _clients;
    private Dictionary<int, StreamReader> _reader;
    private Dictionary<int, StreamWriter> _writer;

    public SocketView()
    {
        _clients = new List<TcpClient>();
        _listener = new TcpListener(IPAddress.Loopback, 8001);
        _listener.Start();
        _reader= new Dictionary<int, StreamReader>();
        _writer = new Dictionary<int, StreamWriter>();
        AcceptConnections();
     
    }

    private void AcceptConnections()
    { 
        int numOfPlayers = 0;
       while (numOfPlayers < 2)
        {
            AcceptClient(numOfPlayers);
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