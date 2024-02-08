using BattleshipLibrary.Core;
using BattleshipLibrary.Interfaces;

namespace BattleshipLibrary.Models
{
    public class Hit: Square, ISquare
    {
        #region Constructors
        public Hit (string inpProwCoOrds): base(inpProwCoOrds)
        {
        }
        #endregion

        #region Methods
        ///  Return the status image (object class) of the square.
        /// </summary>
        /// <param name="inpGameMode"></param>
        /// <returns>Character to display in grid.</returns>
        new public string GetStatus(Enums.GameMode inpGameMode)
        {
            if (inpGameMode == Enums.GameMode.Setup)
            { return GetSetupStatus(); }
            else
            { return GetPlayStatus(); }
        }
        /// <summary>
        /// Returns the status image (object class) of the square, in Play mode.
        /// </summary>
        /// <returns>Character to display in grid.</returns>
        new public string GetPlayStatus()
        {
            return Configuration.HitMarker;
        }
        /// <summary>
        /// Returns the status image (object class) of the square, in Setup mode.
        /// </summary>
        /// <returns>Character to display in grid.</returns>
        new public string GetSetupStatus()
        {
            return Configuration.BlankMarker;
        }
        #endregion
    }
}
