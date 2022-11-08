namespace Server;

public class Card
{
    protected string _pinta;
    private CardValue _valueEnum;
    
    public Card(string pinta, CardValue value)
    {
        _pinta = pinta;
        _valueEnum = value;
    }

    public string Pinta
    {
        get { return _pinta; }
    }

    public CardValue Value
    {
        get { return _valueEnum; }
    }

    public int GetIntValue()
    {
        return (int)_valueEnum;
    }
    


}



