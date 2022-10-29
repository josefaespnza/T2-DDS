namespace Server;

public class Game
{
    private int _playerDistributor = 0;
    private const int NumOfPlayers = 2;

    private Players _players;
    private int _idPlayerTurn;
    private Table _table;
    
    private View _view = new ConsoleView();
    public Game()
    {
        EmptyTable();
        InitializePlayers();
        DistributeCards();
        DecideWhoStars();
        _view.Welcome();
        
    }
    private void EmptyTable() => _table = new Table();
    private void InitializePlayers() => _players = new Players(NumOfPlayers);
    private void DistributeCards() => _players.DistributeCards(_table, _playerDistributor);
    private void DecideWhoStars() => _idPlayerTurn = (_playerDistributor + 1) % NumOfPlayers;
    public void Play()
    {
        while (!IsGameOver())
        {
            
        }
    }

    private bool IsGameOver()
    {
        return false;
    }


}