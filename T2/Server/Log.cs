namespace Server;

public class Log
{
    private List<int> _escobaLog=new List<int>();

    private int _lastPlayerToTakeCards;

    public List<int> EscobaLog
    {
        get { return _escobaLog; }
    }

    public int LastPlayerToTakeCards
    {
        get { return _lastPlayerToTakeCards; }
    }
    
    public void AddEscoba(int playerId)
    { 
        _escobaLog.Add(playerId);
    }
    
    public void ClearLog()
    {
        _escobaLog.Clear();
    }

    public void UpdateLastPlayerToTakeCards(int playerId)
    {
        _lastPlayerToTakeCards = playerId;
    }
    
    

    
}