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
        pile.GiveCardsToPlayer(_players[(playerDistributorId + 1) % 2]);
        pile.GiveCardsToPlayer(_players[playerDistributorId]);
        pile.GiveCardsToTable(table);
        table.PutPileOnTable(pile);
    }

    public void SumEscobaSpecialCase(int points, int playerDistributorId)
    {
        for(int numEscobas=0; numEscobas<points; numEscobas++) SumPoint(playerDistributorId);
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
        player.AddPlayedMove(move.PossibleMoves);
        player.TakeCardOutOfHand(move.PossibleMoves[0]);
    }

    public void SumPoint(int playerId)
    {
        Player player = GetPlayer(playerId);
        player.AddPoint();
    }
    public Player GetPlayer(int playerIndex) => _players[playerIndex];
    
}