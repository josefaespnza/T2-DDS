
namespace Server;

public static class SubsetSum
{
    private const int Target = 15;

    public static void subset_sum(List<Card> cards, List<Card> partial, List<Move> moves)
    {
        //obtenido de:
        //https://stackoverflow.com/questions/4632322/finding-all-possible-combinations-of-numbers-to-reach-a-given-sum
        int s = Sum(partial);
        if (s == Target)
        {
            moves.Add(new Move(partial));
        }
        if (s >= Target)
        {
            return;
        }
        RemainingSet(cards, partial, moves);

    }
    
    private static int Sum(List<Card> cardsArray)
    {
        int sum = 0;
        foreach (var card in cardsArray)
        {
            sum += card.GetIntValue();
        }

        return sum;
    }

    private static void RemainingSet(List<Card> cards, List<Card> partial, List<Move> moves)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            List<Card> remaining = new List<Card>();
            for (int j = i + 1; j < cards.Count; j++) remaining.Add(cards[j]);
            List<Card> partialRec = new List<Card>(partial) {cards[i]};
            subset_sum(remaining, partialRec, moves);
        }
    }
    
    
    public static int PartitionSet(List<Card> cardsArray)
    {
        int numOfPartition = 0;
        int sum = Sum(cardsArray);
        if (IsSubsetSum15(sum)) numOfPartition = 1;
        else if (IsSubsetSum30(numOfPartition)) numOfPartition = 2;
        return numOfPartition;
    }
    
    

    private static bool IsSubsetSum15(int sum) => sum == 15;
    private static bool IsSubsetSum30(int sum) => sum == 30;



}

   