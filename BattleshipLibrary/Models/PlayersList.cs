using BattleshipLibrary.Core;
using System.Collections.Generic;

namespace BattleshipLibrary.Models
{
    /// <summary>
    /// List of Player Objects for the game.
    /// </summary>
    public class PlayersList
    {
        public List<Player> Players = new List<Player>();

        /// <summary>
        /// Gets the next player in the list. Loops at the end of the list.
        /// </summary>
        /// <param name="inpCurrentPlayerIndex">Index for the current player.</param>
        /// <returns>Index of the next player.</returns>
        public static int GetNextPlayer(int inpCurrentPlayerIndex)
        {
            int nextPlayerIndex = inpCurrentPlayerIndex + 1;
            if (nextPlayerIndex == Configuration.NumPlayers)
            {
                nextPlayerIndex = 0;
            }
            return nextPlayerIndex;
        }
    }
}
