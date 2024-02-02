using System.Collections.Generic;
using BattleshipLibrary.Core;

namespace BattleshipLibrary.Models
{
    /// <summary>
    /// The Game object itself, lists of players, etc.
    /// </summary>
    public class Game
    {
        #region PublicFields
        public List<Player> Players = new List<Player>();
        #endregion

        #region Methods
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
        #endregion
    }
}
