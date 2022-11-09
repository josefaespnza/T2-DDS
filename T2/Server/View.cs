using System.Reflection.Metadata.Ecma335;

namespace Server;

public abstract class View
{
    protected abstract void WriteByPlayer(string message, int playerId);
    protected abstract void WriteForAll(string message);
    protected void WriteLine() => WriteForAll("");
    protected void WriteSeparator() => WriteForAll("-------------------------------");
   
    protected abstract string ReadLine(int playerId);
    
    public virtual void Close() {}
    
    public void Welcome() =>WriteForAll("¡Bienvenido a la escoba!");
    
    public void ShowPlayerInTurn(int playerId)
    {
        WriteSeparator();
        WriteForAll("Juega jugador " + playerId);
    }
    
    public void ShowInformationTable(Table table, int playerId)
    {
        string msg = "Mesa actual: ";
        int i = 1;
        foreach (var card in table.CardsOnTable)
        {
            msg += "("+i+") " +card+", ";
            i++;
        } 
        WriteByPlayer(msg,playerId);
        
    }
    
    
    public void ShowHandPlayer(Player player, int playerId)
    {
        string msg = "Mano Jugador: ";
        int i = 1;
        foreach (var cardPlayer in player.Hand)
        {
            msg += "("+i+") " +cardPlayer+", ";
            i++;
        }
        WriteByPlayer(msg, playerId);
    }
    
    public int AskCardToPlay(int numberOfCards, int playerId)
    {
        WriteByPlayer("¿Qué carta quieres bajar?", playerId);
        WriteByPlayer("Ingresa un número entre 1 y " + numberOfCards, playerId);
        int ans = AskValidNumber(1, numberOfCards, playerId);
        return ans;
    }

    public void ShowValidMoves(List<Move> validMoves,  int playerId)
    {
        WriteByPlayer("Hay "+ validMoves.Count+" jugadas en la mesa", playerId);
        for (int i = 1; i <= validMoves.Count; i++)
        {
            WriteByPlayer(i+"-"+ validMoves[i-1],playerId);
        }
    }
    public int AskMoveToPlay(int numberOfValidMoves, int playerId)
    {
        
        WriteByPlayer("¿Cuál jugada quieres realizar?",playerId);
        WriteByPlayer("Ingresa un número entre 1 y" + numberOfValidMoves, playerId);
        int moveId = AskValidNumber(1, numberOfValidMoves, playerId);
        return moveId;
    }
    
    private int AskValidNumber(int minValue, int maxValue, int playerId)
    {
        int number;
        bool isPossibleToTransformString;
        do
        {
            string? inputUser = ReadLine(playerId);
            isPossibleToTransformString = int.TryParse(inputUser, out number);
        } while (!isPossibleToTransformString || number < minValue || number > maxValue);

        return number;
    }

    public void InformMove(Move movePlayed, int playerId)
    {
        WriteForAll("Jugador "+ playerId +" se lleva las siguientes cartas " + movePlayed);
        WriteLine();
    }
    
    public void InformThereIsNoPossibleMoves(int playerId) => WriteByPlayer("No hay posibles combinaciones",playerId);
    
    public void InformEscoba(int playerId) => WriteForAll("ESCOBA! **** Jugador "+playerId);

    public void InformEscobaSpecial(int playerId, int points) =>
        WriteForAll("Jugador " + playerId + " realizó " + points+" **Escobas** al repartir las cartas");


    
    public void CardsWinAtRound(string[] winCards)
    {
        WriteSeparator();
        WriteForAll("Cartas ganadas en esta ronda");
        for (int i = 0; i < winCards.Length; i++)
        {
            WriteForAll("Jugador " +i +":" +winCards[i]);
        }
        
    }
    public void PointsWinAtRound(int[] points)
    {
        WriteSeparator();
        WriteForAll("Total puntos ganados");
        for (int i = 0; i < points.Length; i++)
        {
            WriteForAll("Jugador " +i +": " +points[i]);
        }
        WriteSeparator();
    }

    
    public void ShowTieMessage()
    {
        WriteSeparator();
        WriteForAll("Hubo un empate..¡Felicidades a los jugadores!\n" +
              "Vuelvan pronto :)");
    }

    public void ShowCongratsWinner(int winnerId)
    {
        WriteSeparator();
        WriteForAll("Ganador: "+ winnerId+" ¡Felicidades!");
    }
    
    
    

}