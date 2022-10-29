namespace Server;

public class Card
{
    private string _pinta;
    private CardValue _value;

    public Card(string pinta, CardValue value)
    {
        _pinta = pinta;
        _value = value;
    }

    public string pinta
    {
        get { return _pinta; }
    }

    public CardValue value
    {
        get { return _value; }
    }

}

