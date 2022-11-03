namespace Server;

public class Table
{
    private List<Card> _cardsOnTable=new List<Card>();
    private Pile _pile;

    public List<Card> CardsOnTable
    {
        get { return _cardsOnTable; }
    }
    public void AddCardsToTable(Card card) =>_cardsOnTable.Add(card);

    public void PutPileOnTable(Pile pileOfCards) => _pile = pileOfCards;

    public bool IsThereAnyCardOnThePile() => _pile.isThereCardsOnThePile();

    public void DownCard(Card card) => _cardsOnTable.Add(card);

    public void DrawCardsFromTable(List<Card> cards)
    {
        foreach (var card in cards)
        {
            DrawCardFromTable(card);
        }
    }
    private void DrawCardFromTable(Card card) => _cardsOnTable.Remove(card);
    public bool IsThereCardsOnTable() => _cardsOnTable.Any();

}