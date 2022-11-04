namespace Server;

public class Pile
{
    private string[] _pintas = {"Oro","Espada","Copa","Bastos"}; 
    private List<Card> _pileOfCards;

    public Pile()
    {
        GenerateCards();
    }

    private void GenerateCards()
    {
        _pileOfCards = new List<Card>();
        foreach (var pinta in _pintas )
        {
            foreach(CardValue cardValue in Enum.GetValues(typeof(CardValue)))
            {
                _pileOfCards.Add(new Card(pinta, cardValue));
            }
        }
    }

    public void GiveCardsToPlayer(Player player)
    {
        int cardQuantity = 3;
        for(int i=0; i<cardQuantity; i++) player.AddCardHand(TakeCardOfPile());
    }

    public void ReturnCardsToPile(List<Card> cards)
    {
        _pileOfCards = cards;
    }

    public Card TakeCardOfPile()
    {
        Card cardTaken = _pileOfCards[_pileOfCards.Count-1];
        _pileOfCards.Remove(cardTaken);
        return cardTaken;
    }

    public void MixPile()
    {
        _pileOfCards = _pileOfCards.OrderBy(carta => RandomNumberGenerator.Generate()).ToList();
    }

    public bool isThereCardsOnThePile() => _pileOfCards.Any();
}