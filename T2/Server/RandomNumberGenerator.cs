namespace Server;

public static class RandomNumberGenerator
{
    private const int RandomSeed = 10;
    private static Random rnd = new Random(RandomSeed);
    public static double Generate() => rnd.Next(40);
}