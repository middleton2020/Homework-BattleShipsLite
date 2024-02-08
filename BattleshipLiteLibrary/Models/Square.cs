using BattleshipLibrary.Core;
using BattleshipLibrary.Interfaces;

namespace BattleshipLibrary.Models
{
    public class Square : ISquare
    {
        #region PrivateVariables
        private string _coOrdinates = "";
        #endregion

        #region Properties
        public string CoOrdinates
        {
            get { return _coOrdinates; }
        }
        #endregion

        #region Constructors
        public Square(string inpCoOrdinates)
        {
            _coOrdinates = inpCoOrdinates;
        }
        #endregion

        #region Methods
        /// <summary>
        ///  Return the status (object class) of the square.
        /// </summary>
        /// <param name="inpGameMode"></param>
        /// <returns></returns>
        public virtual string GetStatus(Enums.GameMode inpGameMode)
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
        public virtual string GetPlayStatus()
        {
            return Configuration.BlankMarker;
        }
        /// <summary>
        /// Returns the status image (object class) of the square, in Setup mode.
        /// </summary>
        /// <returns>Character to display in grid.</returns>
        public virtual string GetSetupStatus()
        {
            return Configuration.BlankMarker;
        }
        #endregion
    }
}
