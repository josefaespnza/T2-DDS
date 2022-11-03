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
    public bool IsThereCardsToPlay() => _hand.Any();

    public List<Move> GetPossibleMoves(Card dropCard, List<Card> cardsOnTable)
    {
        List<Move> validMoves = new List<Move>();
        List<Card> partial = new List<Card>(){dropCard};
        SubsetSum.subset_sum(cardsOnTable, partial,validMoves);
        return validMoves;
    }

    public void TakeCardOutOfHand(Card card)
    {
        _hand.Remove(card);
    }

    public void AddPlayedMove(Move played)
    {
        foreach (var card in played.PossibleMoves)
        {
            _movesPlayed.Add(card);
        }
    }

    public void AddPoint()
    {
        _score += 1;
    }
    
    
}