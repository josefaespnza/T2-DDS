namespace Server;

public class Game
{
    private int _playerDistributor = 0;
    private const int NumOfPlayers = 2;

    private int _idPlayerTurn;
    private Players _players;
    private Table _table;
   
    private View _view;
    
    private Log _log = new Log();
    
    public Game(int mode)
    {
        ActivateMode(mode);
        WelcomeToTheGame();
        EmptyTable();
        InitializePlayers();
        
    }

    private void ActivateMode(int mode)
    {
        if (mode == 1) _view = new ConsoleView();
        else if (mode == 2) _view = new SocketView();
    }

    private void WelcomeToTheGame() => _view.Welcome();
    
    private void EmptyTable() => _table = new Table();
    private void InitializePlayers() => _players = new Players(NumOfPlayers);

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
    
    private bool IsGameOver()
    {
        return _players.CheckIfPlayerHas16Points();
    }
    
    private void DistributeCards() => _players.DistributeCards(_table, _playerDistributor);
    private void SpecialCaseEscoba()
    {
        int points = SubsetSum.PartitionSet(_table.CardsOnTable);
        if (points!=0)
        {
            Player player = _players.GetPlayer(_playerDistributor);
            for(int i=0; i<points;i++) _log.AddEscoba(_playerDistributor);
            player.AddPlayedMove(_table.CardsOnTable);
            _table.DrawCardsFromTable(_table.CardsOnTable);
            _view.InformEscobaSpecial(_playerDistributor,points);
        }
    }
    
    private void DecideWhoStars() => _idPlayerTurn = (_playerDistributor + 1) % NumOfPlayers;
    
    private bool CardsAvailable()
    {
        return _table.IsThereAnyCardOnThePile() || _players.IsThereCardsOnBothPlayers();
    }
    
    private void PlayTurn()
    {
        _view.ShowPlayerInTurn(_idPlayerTurn);
        _view.ShowInformationTable(_table,_idPlayerTurn);
        PlayerPlayTurn();
        ProceedTurn();
        
    }
    private void PlayerPlayTurn()
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        GiveCardsToPlayer();
        _view.ShowHandPlayer(player, _idPlayerTurn);
        if (player.IsThereCardsToPlay())
        {
            int cardIndexToPlay = _view.AskCardToPlay(player.Hand.Count,_idPlayerTurn);
            PossibleMovesOnTable(player.Hand[cardIndexToPlay - 1]);
        }
    }

    private void GiveCardsToPlayer()
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        if (!_players.IsThereCardsOnBothPlayers() && _table.IsThereAnyCardOnThePile())
        {
            _table.DistributeCardsAgain(player, 
                _players.GetPlayer(_playerDistributor));
        }
    }
    
    private void PossibleMovesOnTable(Card chosenCard)
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        List<Move> validMoves = player.GetPossibleMoves(chosenCard, _table.CardsOnTable);
        if (validMoves.Count == 1)
        {
            PlayMove(validMoves[0]);
        }
        else if  (validMoves.Count>1)
        {
            int moveIndex = _view.AskMoveToPlay(validMoves, _idPlayerTurn);
            PlayMove(validMoves[moveIndex-1]);
        }
        else
        {
           NoMovesAvailable(chosenCard);
        }
    }
    private void PlayMove(Move chosenMove)
    {
        HandleMove(chosenMove);
        CheckEscoba();
    }
    private void HandleMove(Move played)
    {
        _log.UpdateLastPlayerToTakeCards(_idPlayerTurn);
        _players.HandlePlayedMove(_idPlayerTurn, played);
        _table.DrawCardsFromTable(played.PossibleMoves);
        _view.InformMove(played, _idPlayerTurn);
        
    }
    private void CheckEscoba()
    {
        if (!_table.IsThereCardsOnTable())
        {
            _log.AddEscoba(_idPlayerTurn);
            _view.InformEscoba(_idPlayerTurn);
        }
        
    }
    
    private void NoMovesAvailable(Card chosenCard)
    {
        Player player = _players.GetPlayer(_idPlayerTurn);
        _view.InformThereIsNoPossibleMoves(_idPlayerTurn);
        _table.AddCardToTable(chosenCard);
        player.TakeCardOutOfHand(chosenCard);
    }
    
    private void ProceedTurn() => _idPlayerTurn = (_idPlayerTurn + 1) % NumOfPlayers;
    
    private void EndRound()
    {
        if (_table.IsThereCardsOnTable())
        {
            SpecialMove();
        }
        int[] points = _players.CountPointsPlayers(_log.EscobaLog);
        _view.CardsWinAtRound(_players.WinCards());
        _view.PointsWinAtRound(points);
    }
    
    private void SpecialMove()
    {
        Move specialMove = new Move(_table.CardsOnTable);
        _players.HandlePlayedMove(_log.LastPlayerToTakeCards, specialMove);
        _table.DrawCardsFromTable(specialMove.PossibleMoves);
        _view.InformMove(specialMove, _log.LastPlayerToTakeCards);
    }
    
    private void PrepareNextRound()
    {
        NextDistributor();
        _players.ReturnCardsToPile(_table);
        _log.ClearLog();
    }
    private void NextDistributor() => _playerDistributor = (_playerDistributor + 1) % NumOfPlayers;
    
    private void HandleGameOver()
    {
        if (_players.CheckIsATie())
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
    
    
    
    
    
    
    

}