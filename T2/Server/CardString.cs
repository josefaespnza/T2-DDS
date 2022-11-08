namespace Server;

public class CardString : Card
{
    private string _value;
    
    public CardString(string pinta, CardValue valueEnum) : base(pinta, valueEnum)
    {
        _value = valueEnum.ToString();
    }
    
    public override string ToString() => _value + "_" + _pinta;
    
}