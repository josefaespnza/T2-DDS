namespace Server;

public static class PreGameView
{
    public static int AskGameMode()
    {
        WriteOptions();
        int mode = AskValidNumber(1, 2);
        return mode;
    }

    private static void WriteOptions()
    {
        Console.WriteLine("¿Cómo deseas jugar? (Ingresa una opción)");
        Console.WriteLine("1. Modo local");
        Console.WriteLine("2. Modo servidor");
    }
    
    
    private static int AskValidNumber(int minValue, int maxValue)
    {
        int number;
        bool isPossibleToTransformString;
        do
        {
            string? inputUser = Console.ReadLine();
            isPossibleToTransformString = int.TryParse(inputUser, out number);
        } while (!isPossibleToTransformString || number < minValue || number > maxValue);

        return number;
    }
    
    
}