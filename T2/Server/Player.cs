namespace Server;

public class Player
{
    private List<Card> _hand = new List<Card>();

    public List<Card> Hand
    {
        get { return _hand; }
    }
}