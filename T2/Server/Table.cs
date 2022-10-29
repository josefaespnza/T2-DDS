namespace Server;

public class Table
{
    private List<Card> _cardsOnTable=new List<Card>();
    private Pile _pile;
    public void AddCardsToTable(Card card) =>_cardsOnTable.Add(card);

    public void PutPileOnTable(Pile pileOfCards) => _pile = pileOfCards;
    
}