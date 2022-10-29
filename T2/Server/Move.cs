namespace Server;

public class Move
{
    private List<Card> _trioOfCards;

    public List<Card> TrioOfCards
    {
        get { return _trioOfCards; }
    }

    public Move(List<Card> possibleTrio)
    {
        _trioOfCards = possibleTrio;
    }

    public override string ToString()
    {
        string msg = "";
        //aca voy
        return msg;
    }
}