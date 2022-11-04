namespace Server;

public class Game
{
    private int _playerDistributor = 0;
    private const int NumOfPlayers = 2;

    private int _idPlayerTurn;
    
    private Players _players;
    private Table _table;
    
    //private View _view = new ConsoleView();
    private View _view;
    private Log _log = new Log();
    public Game(int mode)
    {
        ActivateMode(mode);
        _view.Welcome();
        EmptyTable();
        InitializePlayers();
        
    }

    private void ActivateMode(int mode)
    {
        if (mode == 1) _view = new ConsoleView();
        else if (mode == 2) _view = new SocketView();
    }
    private void EmptyTable() => _table = new Table();
    private void InitializePlayers() => _players = new Players(NumOfPlayers);
    private void DistributeCards() => _players.DistributeCards(_table, _playerDistributor);
    private void DecideWhoStars() => _idPlayerTurn = (_playerDistributor + 1) % NumOfPlayers;
    private void NextDistributor() => _playerDistributor = (_playerDistributor + 1) % NumOfPlayers;
    private void SpecialCaseEscoba()
    {
        int points = SubsetSum.PartitionSet(_table.CardsOnTable);
        if (points!=0)
        {
            Player player = _players.GetPlayer(_playerDistributor);
            _log.UpdatePlayerId(_playerDistributor);
            player.AddPlayedMove(_table.CardsOnTable);
            _table.DrawCardsFromTable(_table.CardsOnTable);
            _players.SumEscobaSpecialCase(points, _playerDistributor);
            _view.InformEscobaSpecial(_playerDistributor,points);
        }
    }

    public void Play()
    {
        while (!IsGameOver())
        {
            DistributeCards();
            SpecialCaseEscoba();
            DecideWhoStars();
            while (CardsAvailable())
            {
                PlayTurn();
            }
            
            EndRound();
            PrepareNextRound();
        }
        HandleGameOver();
        _view.Close();
        
    }

    private bool CardsAvailable()
    {
        return _table.IsThereAnyCardOnThePile() || _players.IsThereCardsOnBothPlayers();
    }

    private void PrepareNextRound()
    {
        NextDistributor();
        _players.ReturnCardsToPile(_table);
    }

    private void EndRound()
    {
        if(_table.IsThereCardsOnTable()) SpecialMove();
        int[] points = _players.CountPointsPlayers();
        _view.CardsWinAtRound(_players.WinCards());
        _view.PointsWinAtRound(points);
    }

    private void SpecialMove()
    {
        Move specialMove = new Move(_table.CardsOnTable);
        HandleMove(specialMove);
    }
    private bool IsGameOver()
    {
        return _players.CheckIfPlayerHas16Points();
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
        if (!_players.IsThereCardsOnBothPlayers() && _table.IsThereAnyCardOnThePile())
        {
            _table.DistributeCardsAgain(player, 
                _players.GetPlayer(_playerDistributor));
        }
        _view.ShowHandPlayer(player);
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
            CheckEscoba();
        }
        else if  (validMoves.Count>1)
        {
            int moveIndex = _view.AskMoveToPlay(validMoves);
            HandleMove(validMoves[moveIndex-1]);
            CheckEscoba();
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
        
    }

    private void CheckEscoba()
    {
        if (!_table.IsThereCardsOnTable())
        {
            _log.UpdatePlayerId(_idPlayerTurn);
            _view.InformEscoba(_idPlayerTurn);
            _players.SumPoint(_idPlayerTurn);//quizas esto deberia guardarse en log y al final contar los puntos
            
        }
        
    }

    private void HandleGameOver()
    {
        if (_players.IsATie())
        {
            _view.ShowTieMessage();
        }
        CongratsSingleWinner();
    }

    private void CongratsSingleWinner()
    {
        int winnerId = _players.WinnerId()[0];
        _view.ShowCongratsWinner(winnerId);
        
    }

    private void ProceedTurn() => _idPlayerTurn = (_idPlayerTurn + 1) % NumOfPlayers;

}