namespace Server;

public class Pile
{
    private string[] _pintas = {"oro","espada","copa","bastos"}; 
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

    public void GiveCardsToPlayer(Player player, int cardsQuantity)
    {
        for(int i=0; i<cardsQuantity; i++) player.AddCardHand(TakeCardOfPile());
    }

    public void GiveCardsToTable(Table table, int cardsQuantity)
    {
        for(int i=0; i<cardsQuantity; i++) table.AddCardsToTable(TakeCardOfPile());
    }

    public Card TakeCardOfPile()
    {
        Card cardTaken = _pileOfCards[_pileOfCards.Count-1];
        _pileOfCards.Remove(cardTaken);
        return cardTaken;
    }

    public void MixPile()
    {
        for (int i = 0; i < 40; i++)
        {
            int rndPosition =(int) RandomNumberGenerator.Generate();
            (_pileOfCards[rndPosition], _pileOfCards[i]) = (_pileOfCards[i], _pileOfCards[rndPosition]);
        }
    }
}