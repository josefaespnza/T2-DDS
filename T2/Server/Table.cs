namespace Server;

public class Table
{
    private List<Card> _cardsOnTable = new List<Card>();
    private Pile _pile;

    public List<Card> CardsOnTable
    {
        get { return _cardsOnTable; }
    }
    public void AddCardsToTable(Card card) =>_cardsOnTable.Add(card);

    public void PutPileOnTable(Pile pileOfCards) => _pile = pileOfCards;

    public bool IsThereAnyCardOnThePile() => _pile.isThereCardsOnThePile();
    
    public void DistributeCardsAgain(Player playerInTurn, Player distributorPlayer)
    {
        _pile.GiveCardsToPlayer(playerInTurn);
        _pile.GiveCardsToPlayer(distributorPlayer);
    }

    public void DownCard(Card card) => _cardsOnTable.Add(card);

    public void DrawCardsFromTable(List<Card> cards)
    {
        _cardsOnTable = _cardsOnTable.Except(cards).ToList();
    }
    public bool IsThereCardsOnTable() => _cardsOnTable.Any();

}