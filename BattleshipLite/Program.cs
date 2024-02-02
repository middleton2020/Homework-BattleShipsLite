using BattleshipLibrary.Models;
using BattleShipUI.GamePlay;

namespace BattleShipUI
{
    class Program
    {
        static void Main(string[] inpArgs)
        {
            // Initialise the list of players for this game.
            Game currentGame = new Game();

            // Setup each player ready to play.
            Rules.SetupGame(currentGame);

            // Start playing the game.
            Rules.RunTheGame(currentGame);
        }
    }
}
