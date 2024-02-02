using System.Collections.Generic;

namespace BattleshipLibrary.Core
{
    public static class Configuration
    {
        #region NumberSettings
        /// <summary>
        /// Number of players in the game.
        /// </summary>
        public static int NumPlayers
        {
            get { return 2; }
        }
        /// <summary>
        /// Number of ships each player has.
        /// </summary>
        public static int NumShips
        {
            get { return 5; }
        }
        /// <summary>
        /// How wide the grid is.
        /// </summary>
        public static int GridXWidth
        {
            get { return ValidXLabels.Count; }
        }
        /// <summary>
        /// How tall the grid is.
        /// </summary>
        public static int GridYWidth
        {
            get { return ValidYLabels.Count; }
        }
        #endregion

        #region DisplayCharacters
        /// <summary>
        /// List of labels for X axis.
        /// </summary>
        public static List<string> ValidXLabels
        {
            get
            {
                List<string> validLetters = new List<string>
                { "1", "2", "3", "4", "5"};
                return validLetters;
            }
        }
        /// <summary>
        /// List of labels for Y axis.
        /// </summary>
        public static List<string> ValidYLabels
        {
            get
            {
                List<string> validLetters = new List<string>
                { "A", "B", "C", "D", "E"};
                return validLetters;
            }
        }
        /// <summary>
        /// What to show when a ship is in that square.
        /// </summary>
        public static string ShipMarker
        {
            get { return "X"; }
        }
        /// <summary>
        /// What to show what a ship in that square has been hit.
        /// </summary>
        public static string HitMarker
        {
            get { return "X"; }
        }
        /// <summary>
        /// What to show when a shot has hit nothing in that square.
        /// </summary>
        public static string MissMarker
        {
            get { return "O"; }
        }
        #endregion
    }
}
