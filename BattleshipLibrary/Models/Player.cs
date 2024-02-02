using BattleshipLibrary.Core;
using System.Collections.Generic;

namespace BattleshipLibrary.Models
{
    public class Player
    {
        #region PrivateVariables
        private readonly string _playerName;
        private int _enemyPlayerIndex;
        private List<string> listShipLocations = new List<string>();
        private List<string> listShotsMissed = new List<string>();
        private List<string> listShotsHit = new List<string>();
        #endregion

        #region Properties
        /// <summary>
        /// Name of this player. Read-only, set in the constructor.
        /// </summary>
        public string PlayerName
        {
            get
            {
                return _playerName;
            }
        }

        /// <summary>
        /// The player (index) that they are attacking.
        /// </summary>
        public int EnemyPlayer
        {
            get { return _enemyPlayerIndex; }
            set
            {
                // Can't attack players that don't exist.
                if (value >= Configuration.NumPlayers)
                {
                    // "There aren't that many players"
                    Errors.TooManyPlayersError();
                }
                _enemyPlayerIndex = value;
            }
        }

        /// <summary>
        /// List of ship locations for this player.
        /// </summary>
        public List<string> ShipLocations
        {
            get { return listShipLocations; }
            set { listShipLocations = value; }
        }

        /// <summary>
        /// List of shots that this player took, that hit nothing.
        /// </summary>
        public List<string> ShotsMissed
        {
            get { return listShotsMissed; }
            set { listShotsMissed = value; }
        }

        /// <summary>
        /// List of shots that this player took, that hit a ship.
        /// </summary>
        public List<string> ShotsHit
        {
            get { return listShotsHit; }
            set { listShotsHit = value; }
        }
        #endregion

        #region Pseudo-Properties
        // These are methods that are pretending to be properties,
        // mirroring the Get and Set. I've had to do it this way,
        // as I can't find out how to pass the index number.

        /// <summary>
        /// Get - Co-ordinates for a given ship.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <returns>Co-ordinate value.</returns>
        public string ShipLocation(int inpIndex)
        {
            if (inpIndex >= listShipLocations.Count)
            {
                // "There is no {inpIndex} ship defined."
                Errors.TooManyShipsError(inpIndex);
            }

            return listShipLocations[inpIndex];
        }
        /// <summary>
        /// Set - Co-ordinates for a given ship.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <param name="inpCoOrdinates">Co-ordinate value to store.</param>
        public void ShipLocation(int inpIndex, string inpCoOrdinates)
        {
            // Have we got a valid co-ordinate and not over-writing an existing ship.
            inpCoOrdinates = GridValidation.ValidShipLocation(inpCoOrdinates, this);

            // Passed validation.
            if (inpCoOrdinates != "")
            {
                // Add a new entry to the list.
                if (inpIndex >= listShipLocations.Count)
                {
                    listShipLocations.Add(inpCoOrdinates);
                }
                // Update an existing entry in the list.
                else
                {
                    listShipLocations[inpIndex] = inpCoOrdinates;
                }
            }
        }

        /// <summary>
        /// Get - Co-ordinates for a shot that didn't hit anything.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <returns>Co-ordinate value.</returns>
        public string ShotMissed(int inpIndex)
        {
            if (inpIndex >= listShotsMissed.Count)
            {
                // "You have not yet taken your {inpIndex} shot."
                Errors.TooManyShotsError(inpIndex);
            }

            return listShotsMissed[inpIndex];
        }
        /// <summary>
        /// Set - Co-ordinates for a shot that didn't hit anything.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <param name="inpCoOrdinates">Co-ordinate value to store.</param>
        public void ShotMissed(int inpIndex, string inpCoOrdinates)
        {
            // Have we got a valid co-ordinate and not targeted this location already.
            if (GridValidation.ValidShotTaken(inpCoOrdinates, this))
            {
                // Add a new entry to the list.
                if (inpIndex >= listShotsMissed.Count)
                {
                    listShotsMissed.Add(inpCoOrdinates);
                }
                // Update an existing entry in the list.
                else
                {
                    listShotsMissed[inpIndex] = inpCoOrdinates;
                }
            }
        }

        /// <summary>
        /// Get - Co-ordinates for a shot that hit something.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <returns>Co-ordinate value.</returns>
        public string ShotHit(int inpIndex)
        {
            if (inpIndex >= listShotsHit.Count)
            {
                // "You have not yet taken your {inpIndex} shot."
                Errors.TooManyShotsError(inpIndex);
            }

            return listShotsHit[inpIndex];
        }
        /// <summary>
        /// Set - Co-ordinates for a shot that hit something.
        /// </summary>
        /// <param name="inpIndex">Index in the list that we're looking at.</param>
        /// <param name="inpCoOrdinates">Co-ordinate value to store.</param>
        public void ShotHit(int inpIndex, string inpCoOrdinates)
        {
            // Have we got a valid co-ordinate and not targeted this location already.
            if (GridValidation.ValidShotTaken(inpCoOrdinates, this))
            {
                // Add a new entry to the list.
                if (inpIndex >= listShotsHit.Count)
                {
                    listShotsHit.Add(inpCoOrdinates);
                }
                // Update an existing entry in the list.
                else
                {
                    listShotsHit[inpIndex] = inpCoOrdinates;
                }
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Create a new player record, sets just the name.
        /// </summary>
        /// <param name="inpName">Name the player is to be known by.</param>
        public Player(string inpName)
        {
            if (inpName == "")
            {
                // "You must provide a player name."
                Errors.BlankNameError();
            }

            _playerName = inpName;
        }
        /// <summary>
        /// Create a new player record, sets the name and who you're fighting.
        /// </summary>
        /// <param name="inpName">Name the player is to be known by.</param>
        /// <param name="inpCurrentPlayerIndex">Where the player will be added in the list of players.</param>
        public Player(string inpName, int inpCurrentPlayerIndex) : this(inpName)
        {
            // We know where this player will fall in the lists, so set who they will be attacking.
            _enemyPlayerIndex = PlayersList.GetNextPlayer(inpCurrentPlayerIndex);
        }
        #endregion
    }
}
