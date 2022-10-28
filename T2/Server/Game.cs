namespace Server;

public class Game
{
    private Players _players;
    private int _idPlayerTurn;
    private Table _table;
    
    private View _view = new View();
    public Game()
    {
        EmptyTable();
        InitializePlayers();
        //repartir cartas -> la mesa es la que reparte las cartas y reuelve?
        //decidir quien parte -> el jugador 0 parte primero y si se quiere volver a jugar parte el 1
    }

    private void EmptyTable() => _table = new Table();
    private void InitializePlayers() => _players = new Players();
    
}