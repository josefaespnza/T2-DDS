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

    public void AddCardHand(Card card)
    {
        _hand.Add(card);
    }
    public bool IsThereCardsToPlay() => _hand.Count > 0;

    public List<Move> GetPossibleMoves(Card dropCard)
    {
        List<Move> validMoves = new List<Move>();
        
        return validMoves;
    }

    private List<Card> GetPossibleTrio(List<Card> trio)
    {
        
        return trio;
    }
}