using System.Reflection.Metadata.Ecma335;

namespace Server;

public abstract class View
{
    protected abstract void Write(string message);
    protected abstract string ReadLine();
    public virtual void Close() {}
    public void Pause() => ReadLine();
    public void ShowPlayerInTurn(int playerId) => Write("Juega jugador " + playerId);
    public void Welcome() =>Write("¡Bienvenido a la escoba!");
    public void ShowHandPlayer(Player player)
    {
        string msg = "Mano Jugador: ";
        int i = 1;
        foreach (var cardPlayer in player.Hand)
        {
            msg += "("+i+") " +cardPlayer+", ";
            i++;
        }
        Write(msg);
    }
    public void ShowInformationTable(Table table)
    {
        string msg = "Mesa actual: ";
        int i = 1;
        foreach (var card in table.CardsOnTable)
        {
            msg += "("+i+") " +card+", ";
            i++;
        } 
        Write(msg);
        
    }
    public int AskCardToPlay(int numberOfCards)
    {
        Write("¿Qué carta quieres bajar?");
        Write("Ingresa un número entre 1 y " + numberOfCards);
        int ans = AskValidNumber(1, numberOfCards);
        return ans;
    }

    public int AskMoveToPlay(List<Move> validMoves)
    {
        Write("Hay "+ validMoves.Count+" jugadas en la mesa");
        for (int i = 1; i <= validMoves.Count; i++)
        {
            Write(i+"-"+ validMoves[i]);
        }
        Write("¿Cuál jugada quieres realizar?");
        Write("Ingresa un número entre 1 y" + validMoves.Count);
        int moveId = AskValidNumber(1, validMoves.Count);
        return moveId;
    }

    public void InformThereIsNoPossibleMoves() => Write("No hay posibles combinaciones");

    public void InformMove(Move movePlayed, int playerId)
    {
        Write("Jugador "+ playerId +" se lleva las siguientes cartas " + movePlayed);
    }

    public void InformEscoba(int playerId) => Write("ESCOBA! **** Jugador "+playerId);

    public void InformEscobaSpecial(int playerId, int points) =>
        Write("Jugador " + playerId + " realizó " + points+" **Escobas**");
    private int AskValidNumber(int minValue, int maxValue)
    {
        int number;
        bool isPossibleToTransformString;
        do
        {
            string? inputUser = ReadLine();
            isPossibleToTransformString = int.TryParse(inputUser, out number);
        } while (!isPossibleToTransformString || number < minValue || number > maxValue);

        return number;
    }
    

}