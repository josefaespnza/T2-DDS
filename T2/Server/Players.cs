namespace Server;

public class Players
{
    private List<Player> _players;

    public Players(int numOfPlayers)
    {
        _players = new List<Player>();
        for (int i = 0; i < numOfPlayers; i++) _players.Add(new Player());
    }

    public void DistributeCards(Table table, int playerDistributorId)
    {
        table.PileOfCards.MixPile();
        table.PileOfCards.GiveCardsToPlayer(_players[(playerDistributorId + 1) % 2]);
        table.PileOfCards.GiveCardsToPlayer(_players[playerDistributorId]);
        table.AddCardsToTable();
        
    }

    public void SumEscobaSpecialCase(int points, int playerDistributorId)
    {
        for(int numEscobas=0; numEscobas<points; numEscobas++) SumPoint(playerDistributorId);
    }
   
    
    public bool IsThereCardsOnBothPlayers()
    {
        if (!_players[0].IsThereCardsToPlay() && !_players[1].IsThereCardsToPlay())
        {
            return false;
        }

        return true;
    }
    public void HandlePlayedMove(int playerId, Move move)
    {
        Player player = GetPlayer(playerId);
        player.AddPlayedMove(move.PossibleMoves);
        player.TakeCardOutOfHand(move.PossibleMoves[0]);
    }

    public void SumPoint(int playerId)
    {
        Player player = GetPlayer(playerId);
        player.AddPoint();
    }

    public int[] CountPointsPlayers()
    {
        int[] points = new int[_players.Count] ;
        for(int i=0; i<_players.Count; i++)
        {
            _players[i].CalculateMyPoints();
            points[i]=_players[i].Score;
        }

        return points;
    }

    public string[] WinCards()
    {
        string[] winCards = new string[_players.Count];
        int i = 0;
        foreach (var player in _players)
        {
            winCards[i] = player.EarnCards();
            i++;
        }
        return winCards;
    }
    
    public void ReturnCardsToPile(Table table)
    {
        List<Card> cards = new List<Card>();
        foreach (var player in _players)
        {
            cards = cards.Concat(player.ReturnEarnCards()).ToList();
            player.ClearEarnCardsAfterReturn();
            
        }
        table.PileOfCards.ReturnCardsToPile(cards);
        
    }

    public bool CheckIfPlayerHas16Points()
    {
        if (_players[0].Score == 16 || _players[1].Score == 16) return true;
        return false;
    }
    

    public int[] WinnerId()
    {
        int[] winnerId = new int[1];
        for (int i = 0; i < _players.Count; i++)
        {
            if (_players[i].Score == 16) winnerId[0]=i;
        }
        return winnerId;

    }
    
    public bool IsATie()
    {
        bool isATie = _players[0].Score == _players[1].Score;
        return isATie;
    }
    

    public Player GetPlayer(int playerIndex) => _players[playerIndex];
    
}