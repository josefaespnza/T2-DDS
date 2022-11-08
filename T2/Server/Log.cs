namespace Server;

public class Log
{
    private List<int> _escobaLog=new List<int>();
    
    public List<int> EscobaLog
    {
        get { return _escobaLog; }
    }
    
    public void AddEscoba(int playerId)
    { 
        _escobaLog.Add(playerId);
    }
    
    public void ClearLog()
    {
        _escobaLog.Clear();
    }
    
    
    public int LastPlayerIdToMakeAEscoba()
    {
        return _escobaLog[^1];
    }
    
    

    
}