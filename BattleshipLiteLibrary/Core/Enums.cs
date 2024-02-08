
namespace BattleshipLibrary.Core
{
    /// <summary>
    /// Put all the enums into the same location.
    /// </summary>
    public class Enums
    {
        /// <summary>
        /// Is the game in setup or play mode.
        /// </summary>
        public enum GameMode
        {
            Play,
            Setup
        }

        /// <summary>
        /// Which way is the ship facing.
        /// </summary>
        public enum Orientation
        {
            Down,
            Left,
            Right,
            Up
        }

        /// <summary>
        /// Is the ship still floating?
        /// </summary>
        public enum ShipStatus
        {
            Safe,
            Sunk
        }
    }
}
