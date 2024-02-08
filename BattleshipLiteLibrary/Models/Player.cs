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
            get { return _playerName; }
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
        /// Add a ship to the list of those against the player.
        /// </summary>
        /// <param name="inpShip">Ship object to add.</param>
        /// <param name="inpCoOrdinates">Where the ship is in the grid.</param>
        public void AddShip(Ships inpShip, string inpCoOrdinates)
        {
            _playersGrid.GridSquareList[inpCoOrdinates] = inpShip;
            if (_playersShips.Contains(inpShip) == false)
            {
                _playersShips.Add(inpShip);
            }
            if (inpShip.CoOrdinates.Contains(inpCoOrdinates) == false)
            {
                inpShip.CoOrdinates.Add(inpCoOrdinates);
            }
        }
        #endregion
    }
}
