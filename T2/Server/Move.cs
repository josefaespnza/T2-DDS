namespace Server;

public class Move
{
    private List<Card> _possibleMove;
    
    public Move(List<Card> possibleMove)
    {
        _possibleMove = possibleMove;
    }

    public List<Card> PossibleMoves => _possibleMove;


    public override string ToString()
    {
        string msg = "";
        foreach (var card in _possibleMove)
        {
            msg += card + ", ";
        }
        return msg;
    }
}