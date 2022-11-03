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
            //las cartas que queden en la mesa se las lleva el ultimo
            //jugador que se haya llevado cartas en el turno
            
            //volver a barajar
            //y limpiar para volver a jugar una ronda
            
            //IDEA: quizas play puede ser recursivo en caso de que nadie llegue a los 16 puntos
            //y no un while dentro de un while porque queda raro
            //si es que el juego si se termina se va a hacer trabajo demas volviendo a barajar etc
        }
        
    }

    private bool IsGameOver()
    {
        //se hace conteo de puntos y si alguien tiene 16 puntos se acaba el juego
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
        if (_players.IsBothPlayersHandEmpty())
        {
            //repartir cartas -> ACA VOY
        }

        if (player.IsThereCardsToPlay())
        {
            int cardIndexToPlay = _view.AskCardToPlay(player.Hand.Count);
            PossibleMovesOnTable(player.Hand[cardIndexToPlay - 1]);
        }
        
        
    }

    private void PossibleMovesOnTable(Card chosenCard)
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        List<Move> validMoves = player.GetPossibleMoves(chosenCard, _table.CardsOnTable);
        if (validMoves.Count == 1)
        {
            HandleMove(validMoves[0]);
        }
        else if  (validMoves.Count>1)
        {
            int moveIndex = _view.AskMoveToPlay(validMoves);
            HandleMove(validMoves[moveIndex-1]);
        }
        else
        {
            _view.InformThereIsNoPossibleMoves();
            _table.DownCard(chosenCard);
            player.TakeCardOutOfHand(chosenCard);
        }
    }

    private void HandleMove(Move played)
    {
        _players.HandlePlayedMove(_idPlayerTurn, played);
        _table.DrawCardsFromTable(played.PossibleMoves);
        _view.InformMove(played, _idPlayerTurn);
        CheckEscoba();
    }

    private void CheckEscoba()
    {
        if (!_table.IsThereCardsOnTable())
        {
            _view.InformEscoba(_idPlayerTurn);
            _players.SumPointEscoba(_idPlayerTurn);
            
        }
        
    }

    private void ProceedTurn() => _idPlayerTurn = (_idPlayerTurn + 1) % NumOfPlayers;

}