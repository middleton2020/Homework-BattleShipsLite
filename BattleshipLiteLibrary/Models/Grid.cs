using System.Collections.Generic;
using BattleshipLibrary.Core;
using BattleshipLibrary.Interfaces;

namespace BattleshipLibrary.Models
{
    /// <summary>
    /// The grid holding the player's ships and any shots taken at them.
    /// </summary>
    public class Grid
    {
        #region PrivateFields
        private Dictionary<string, ISquare> _gridSquareList;
        #endregion

        #region Properties
        /// <summary>
        /// List of grid squares, holding their type against the co-ordinates.
        /// </summary>
        public Dictionary<string, ISquare> GridSquareList
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
            _gridSquareList = new Dictionary<string, ISquare>();
            foreach (string locationX in Configuration.ValidXLabels)
            {
                foreach (string locationY in Configuration.ValidYLabels)
                {
                    string coOrdinate = locationY + locationX;
                    Square currentSquare = new Square(coOrdinate);
                    _gridSquareList.Add(coOrdinate, currentSquare);
                }
            }
        }
        #endregion
    }
}