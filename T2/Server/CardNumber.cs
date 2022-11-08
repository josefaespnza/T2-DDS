namespace Server;

public class CardNumber:Card
{
    private int _value;
    
    public CardNumber(string pinta, CardValue valueEnum) : base(pinta, valueEnum)
    {
        _value =(int)valueEnum ;
    }
    
    public override string ToString() => _value + "_" + _pinta;
}