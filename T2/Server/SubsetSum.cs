//obtenido de:https://stackoverflow.com/questions/4632322/finding-all-possible-combinations-of-numbers-to-reach-a-given-sum
namespace Server;

public static class SubsetSum
{
    private const int Target = 15;
    public static void subset_sum(List<Card> cards,List<Card> partial,List<Move> moves)
    {
        
        int s = 0;
        foreach (var card in partial)
        {
            s += card.GetIntValue();
        }
        if (s == Target)
        {
            moves.Add(new Move(partial));
        }
        if (s >= Target) 
        {
            return;
        }
        for (int i = 0; i < cards.Count; i++)
        {
            List<Card> remaining = new List<Card>();
            Card n = cards[i];
            for(int j=i+1; j<cards.Count; j++) remaining.Add(cards[j]);
            List<Card> partialRec = new List<Card>(partial) { n };
            subset_sum(remaining, partialRec, moves);
        }

    }
}