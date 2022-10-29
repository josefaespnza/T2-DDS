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
        table.PutPileOnTable(pile);
    }

    public Player GetPlayer(int playerIndex) => _players[playerIndex];
    
}