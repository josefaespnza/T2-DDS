namespace Server;

public class Players
{
    private List<Player> _players;

    public Players()
    {
        _players = new List<Player>();
        for (int i = 0; i < 2; i++)
        {
            _players.Add(new Player());
        }
    }
}