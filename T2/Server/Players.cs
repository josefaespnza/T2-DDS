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
        Pile pile = new Pile();
        pile.MixPile();
        pile.GiveCardsToPlayer(_players[(playerDistributorId + 1) % 2], 3);
        pile.GiveCardsToPlayer(_players[playerDistributorId],3);
        pile.GiveCardsToTable(table, 4);
        //checkear si se armo una escoba
        table.PutPileOnTable(pile);
    }

    public bool IsBothPlayersHandEmpty()
    {
        if (!_players[0].IsThereCardsToPlay() && !_players[1].IsThereCardsToPlay())
        {
            return true;
        }

        return false;
    }
    public void HandlePlayedMove(int playerId, Move move)
    {
        Player player = GetPlayer(playerId);
        player.AddPlayedMove(move);
        player.TakeCardOutOfHand(move.PossibleMoves[0]);
    }

    public void SumPointEscoba(int playerId)
    {
        Player player = GetPlayer(playerId);
        player.AddPoint();
    }
    public Player GetPlayer(int playerIndex) => _players[playerIndex];
    
}