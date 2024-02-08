using BattleshipLibrary.Rules;
using BattleshipLibrary.Core;
using System.Collections.Generic;
using BattleshipLibrary.Interfaces;

namespace BattleshipLibrary.Models
{
    public class Ships: Square, ISquare
    {
        #region PrivateFields
        private string _shipName;
        private Enums.Orientation _facing = Enums.Orientation.Down;
        private readonly List<string> _coOrdinates;
        private int _hits = 0;
        private Enums.ShipStatus _status = Enums.ShipStatus.Safe;
        #endregion

        #region Properties
        /// <summary>
        /// Reference name for the ship.
        /// </summary>
        public string ShipName
        {
            get { return _shipName; }
            set
            {
                if (value == "")
                { Errors.BlankNameError(); }
                else
                { _shipName = value; }
            }
        }
        /// <summary>
        /// Number of grid squares occupied by this ship.
        /// </summary>
        public int Size
        {
            get
            {
                return Configuration.ShipsList[_shipName];
            }
        }
        /// <summary>
        /// Which way this ship is facing.
        /// </summary>
        public Enums.Orientation Facing
        {
            get { return _facing; }
            set { _facing = value; }
        }
        /// <summary>
        /// Where the prow of the ship can be found in the grid.
        /// </summary>
        new public List<string> CoOrdinates
        {
            get { return _coOrdinates; }
            set
            {
                _coOrdinates.Clear();
                foreach (string coOrdinate in value)
                {
                    string tempCoOrd = GridValidation.ValidCoOrdinates(coOrdinate);
                    _coOrdinates.Add(tempCoOrd);
                }
            }
        }
        #endregion

        #region Constructors
       /// <summary>
        /// Set the name of the ship at creation.
        /// </summary>
        /// <param name="inpName">Ship's name.</param>
        public Ships(string inpCoOrdinates, string inpName): base(inpCoOrdinates)
        {
            ShipName = inpName;
            _coOrdinates = new List<string>();
            _coOrdinates.Add(inpCoOrdinates);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add the ship to a given square.
        /// </summary>
        /// <param name="inpCoOrdinate">Co-ordinate we're placing the ship in.</param>
        public void AddCoOrdinate(string inpCoOrdinate)
        {
            inpCoOrdinate = GridValidation.ValidCoOrdinates(inpCoOrdinate);
            _coOrdinates.Add(inpCoOrdinate);
        }
        /// <summary>
        /// Call this when a ship it hit to see if it sank.
        /// </summary>
        public void ShipHit()
        {
            _hits += 1;
            if (_hits == Size)
            {
                _status = Enums.ShipStatus.Sunk;
            }
        }

        /// <summary>
        ///  Return the status (object class) of the square.
        /// </summary>
        /// <param name="inpGameMode"></param>
        /// <returns></returns>
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
            return Configuration.BlankMarker;
        }
        /// <summary>
        /// Returns the status image (object class) of the square, in Setup mode.
        /// </summary>
        /// <returns>Character to display in grid.</returns>
        new public string GetSetupStatus()
        {
            return Configuration.ShipMarker;
        }
        #endregion
    }
}
