namespace Server;

public class Log
{
    private int _lastPlayerIdToMakeAEscoba;

    public int LastPlayerIdToMakeAEscoba
    {
        get { return _lastPlayerIdToMakeAEscoba; }
    }
    public void UpdatePlayerId(int playerId)
    {
        _lastPlayerIdToMakeAEscoba = playerId;
    }
}