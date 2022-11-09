namespace Server;

public class Table
{
    private List<Card> _cardsOnTable = new List<Card>();
    private Pile _pile;

    public Table()
    {
        _pile = new Pile();
    }
    
    public List<Card> CardsOnTable => _cardsOnTable;

    public Pile PileOfCards => _pile;

    public void AddCardsToTable(){
        for (int i = 0; i < 4; i++)
        {
            Card drawCard = _pile.TakeCardOfPile();
            AddCardToTable(drawCard);
        }
    }
    public void AddCardToTable(Card card) =>_cardsOnTable.Add(card);
    public void DrawCardsFromTable(List<Card> cards)
    {
        _cardsOnTable = _cardsOnTable.Except(cards).ToList();
    }
    
    public bool IsThereCardsOnTable() => _cardsOnTable.Any();
    
    
    public bool IsThereAnyCardOnThePile() => _pile.isThereCardsOnThePile();
    
    
    public void DistributeCardsAgain(Player playerInTurn, Player distributorPlayer)
    {
        _pile.GiveCardsToPlayer(playerInTurn);
        _pile.GiveCardsToPlayer(distributorPlayer);
    }

    

}