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

    public string Pinta
    {
        get { return _pinta; }
    }

    public CardValue Value
    {
        get { return _value; }
    }

    public int GetIntValue()
    {
        return (int)_value;
    }

    public override string ToString() => _value + "_" + _pinta;


}

