namespace Server;

public class Game
{
    private int _playerDistributor = 0;
    private const int NumOfPlayers = 2;

    private int _idPlayerTurn;
    
    private Players _players;
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
            while (_table.IsThereAnyCardOnThePile())
            {
                PlayTurn();
            }
        }
    }

    private bool IsGameOver()
    {
        return false;
    }

    private void PlayTurn()
    {
        _view.ShowPlayerInTurn(_idPlayerTurn);
        _view.ShowInformationTable(_table);
        PlayerPlayTurn();
        ProceedTurn();
        
    }
    private void PlayerPlayTurn()
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        _view.ShowHandPlayer(player);
        if (player.IsThereCardsToPlay())
        {
            int cardIndexToPlay = _view.AskCardToPlay(player.Hand.Count);
            PossibleMovesOnTable(player.Hand[cardIndexToPlay - 1], player);
        }
        else
        {
            //no se que pasa si el jugador se queda sin cartas en la mano
        }
    }

    private void PossibleMovesOnTable(Card chosenCard, Player player)
    {
        List<Move> validMoves = player.GetPossibleMoves(chosenCard);
        if (validMoves.Count == 1)
        {
            DropCards(validMoves[0], player);
        }
        else if  (validMoves.Count>1)
        {
            int moveIndex = _view.AskMoveToPlay(validMoves);
            DropCards(validMoves[moveIndex],player);
        }
        else
        {
            _view.InformThereIsNoPossibleMoves();
        }
        
    }

    private void DropCards(Move played, Player player)
    {
        
    }

    private void ProceedTurn() => _idPlayerTurn = (_idPlayerTurn + 1) % NumOfPlayers;

}