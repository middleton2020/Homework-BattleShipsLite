using System.Collections.Generic;
using BattleshipLibrary.Core;

namespace BattleshipLibrary.Models
{
    public class Player
    {
        #region PrivateVariables
        private readonly string _playerName;
        private int _enemyPlayerIndex;
        private Grid _playersGrid;
        private List<Ships> _playersShips;
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
        /// The Grid object holding this player's ships.
        /// </summary>
        public Grid PlayersGrid
        {
            get { return _playersGrid; }
            set { _playersGrid = value; }
        }

        /// <summary>
        /// The list of ships that this player has.
        /// </summary>
        public List<Ships> PlayersShips
        {
            get { return _playersShips; }
            set { _playersShips = value; }
        }

        /// <summary>
        /// Calculates how many of the enemy's ships have been sunk.
        /// </summary>
        public int SunkShips
        {
            get
            {
                int sunkShips = 0;
                foreach (Ships ship in PlayersShips)
                {
                    if (ship.Status == Enums.ShipStatus.Sunk)
                    {
                        sunkShips += 1;
                    }
                }

                return sunkShips;
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

            _playersGrid = new Grid();
            _playersShips = new List<Ships>();
        }
        /// <summary>
        /// Create a new player record, sets the name and who you're fighting.
        /// </summary>
        /// <param name="inpName">Name the player is to be known by.</param>
        /// <param name="inpCurrentPlayerIndex">Where the player will be added in the list of players.</param>
        public Player(string inpName, int inpCurrentPlayerIndex) : this(inpName)
        {
            // We know where this player will fall in the lists, so set who they will be attacking.
            _enemyPlayerIndex = Game.GetNextPlayer(inpCurrentPlayerIndex);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add a ship to a square of the grid.
        /// </summary>
        /// <param name="inpShip">Ship record to add or add square to.</param>
        public void AddShipSquare(Ships inpShip)
        {
            /// Have we already added the ship?
            int existingShip = -1;
            for (int i = 0; i < PlayersShips.Count; i++)
            {
                if (PlayersShips[i].ShipName == inpShip.ShipName)
                {
                    existingShip = i;
                }
            }

            // If the ship hasn't been added, then add it here.
            if (existingShip == -1)
            {
                PlayersShips.Add(inpShip);
            }
            // Otherwise, add another co-ordinate to the existing ship.
            else
            {
                Ships testShip = PlayersShips[existingShip];
                string newCoOrds = inpShip.CoOrdinates[0];
                if (testShip.CoOrdinates.Contains(newCoOrds) == false)
                {
                    testShip.CoOrdinates.Add(newCoOrds);
                }
            }
        }
        #endregion
    }
}
