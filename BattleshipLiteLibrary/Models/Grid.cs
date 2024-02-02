using System.Collections.Generic;
using BattleshipLibrary.Core;
using BattleshipLibrary.Rules;

namespace BattleshipLibrary.Models
{
    /// <summary>
    /// The grid holding the player's ships and any shots taken at them.
    /// </summary>
    public class Grid
    {
        #region PrivateFields
        private Dictionary<string, Enums.GridStatus> _gridSquareList;
        #endregion

        #region Properties
        /// <summary>
        /// List of the grid squares, holding their status against their co-ordinates.
        /// </summary>
        public Dictionary<string, Enums.GridStatus> GridSquareList
        {
            get { return _gridSquareList; }
            set { _gridSquareList = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Basic constructor. Creates the grid with empty squares.
        /// </summary>
        public Grid()
        {
            _gridSquareList = new Dictionary<string, Enums.GridStatus>();
            foreach (string locationX in Configuration.ValidXLabels)
            {
                foreach (string locationY in Configuration.ValidYLabels)
                {
                    string coOrdinate = locationY + locationX;
                    _gridSquareList.Add(coOrdinate, Enums.GridStatus.Empty);
                }
            }
        }
        #endregion

        #region PseudoPropertyMethods
        /// <summary>
        /// Returns the status of the specified grid square.
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates of the grid square.</param>
        /// <returns>Status of the grid square.</returns>
        public Enums.GridStatus GetGridSquareMode(string inpCoOrdinates)
        {
            inpCoOrdinates = GridValidation.ValidCoOrdinates(inpCoOrdinates);
            return _gridSquareList[inpCoOrdinates];
        }
        /// <summary>
        /// Sets the status of the specific grid square.
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates of the grid square.</param>
        /// <param name="inpNewStatus">Status of the grid square.</param>
        /// <returns>Success of setting the status.</returns>
        public bool SetGridSquareMode(string inpCoOrdinates, Enums.GridStatus inpNewStatus)
        {
            inpCoOrdinates = GridValidation.ValidCoOrdinates(inpCoOrdinates);
            _gridSquareList[inpCoOrdinates] = inpNewStatus;
            return true;
        }
        #endregion
    }
}