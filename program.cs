using BattleshipLibrary.Models;

namespace BattleShipUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise the list of players for this game.
            PlayersList playersList = new PlayersList();

            // Setup each player readsy to play.
            Rules.SetupGame(playersList);

            // Start playing the game.
            Rules.RunTheGame(playersList);
        }
    }
}
