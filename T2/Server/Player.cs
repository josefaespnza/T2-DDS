namespace Server;

public class Player
{
    private List<Card> _hand = new List<Card>();
    private List<Card> _movesPlayed = new List<Card>();
    private int _score = 0;

    public List<Card> Hand
    {
        get { return _hand; }
    }

    public int Score
    {
        get { return _score; }
    }

    public string EarnCards()
    {
        return _movesPlayed.Aggregate("", (current, card) => current + (card + ", "));
    }

    public List<Card> ReturnEarnCards()=>_movesPlayed;
    
    public void ClearEarnCardsAfterReturn()=>_movesPlayed.Clear();
    
    public void AddCardHand(Card card)=>_hand.Add(card);
    
    public bool IsThereCardsToPlay() => _hand.Any();

    public List<Move> GetPossibleMoves(Card dropCard, List<Card> cardsOnTable)
    {
        List<Move> validMoves = new List<Move>();
        List<Card> partial = new List<Card>(){dropCard};
        SubsetSum.subset_sum(cardsOnTable, partial,validMoves);
        return validMoves;
    }

    public void TakeCardOutOfHand(Card card)=>_hand.Remove(card);
    

    public void AddPlayedMove(List<Card> movePlayed)
    {
        foreach (var card in movePlayed)
        {
            _movesPlayed.Add(card);
        }
    }

    public void CalculateMyPoints()
    {
        int nGoldSevens = CountGoldSeven();
        for(int i=0; i<nGoldSevens;i++ ) AddPoint();
        if(NumberOfEarnCards()>=20) AddPoint();
        if(NumberOfSeven()>=2) AddPoint();
        if(NumberOfGold()>=5) AddPoint();
    }

    private int CountGoldSeven()
    {
        int goldSeven = 0;
        foreach (var card in _movesPlayed)
        {
            if (card.Pinta == "Oro" && card.GetIntValue()==7) goldSeven += 1;
        }
        return goldSeven;
    }

    private int NumberOfEarnCards()
    {
        return _movesPlayed.Count;
    }

    private int NumberOfSeven()
    {
        int numSevens = 0;
        foreach (var card in _movesPlayed)
        {
            if (card.GetIntValue()==7) numSevens += 1;
        }
        return numSevens;
    }

    private int NumberOfGold()
    {
        int goldCards = 0;
        foreach (var card in _movesPlayed)
        {
            if (card.Pinta == "Oro" ) goldCards+= 1;
        }
        return goldCards;
    }
    public void AddPoint()
    {
        _score += 1;
    }
    
    
}