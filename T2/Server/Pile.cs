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
            int counter = 1;
            foreach(CardValue cardValue in Enum.GetValues(typeof(CardValue)))
            {
                if(counter<=7) _pileOfCards.Add(new CardNumber(pinta, cardValue));
                else _pileOfCards.Add(new CardString(pinta, cardValue));
                counter++;
            }
        }
    }
    
    public void MixPile()
    {
        _pileOfCards = _pileOfCards.OrderBy(carta => RandomNumberGenerator.Generate()).ToList();
    }
    
    
    public void GiveCardsToPlayer(Player player)
    {
        int cardQuantity = 3;
        for (int i = 0; i < cardQuantity; i++)
        {
            Card removedCard = TakeCardOfPile();
            player.AddCardHand(removedCard);
        }
    }
    public Card TakeCardOfPile()
    {
        Card cardTaken = _pileOfCards[_pileOfCards.Count-1];
        _pileOfCards.Remove(cardTaken);
        return cardTaken;
    }

    public void ReturnCardsToPile(List<Card> cards)
    {
        _pileOfCards = cards;
    }
    
    
    public bool isThereCardsOnThePile() => _pileOfCards.Any();
}