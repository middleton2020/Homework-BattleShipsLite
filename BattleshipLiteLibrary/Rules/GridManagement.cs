using BattleshipLibrary.Core;
using BattleshipLibrary.Interfaces;
using BattleshipLibrary.Models;
using System.Collections.Generic;

namespace BattleshipLibrary.Rules
{
    public class GridManagement
    {
        #region AddShipMethods
        /// <summary>
        /// If the ship is more than 1 square in length, we'll need to add it to the other squares... 
        /// </summary>
        /// <param name="inpShipCoOrds">Where the prow of the ship is in the grid.</param>
        /// <param name="inpShip">Ship object we're working with.</param>
        /// <param name="inpPlayer">Player object that we're updating.</param>
        public static void AddLargeShipExtraSquares(string inpShipCoOrds, Ships inpShip, Player inpPlayer)
        {
            string xAxis;
            string yAxis;
            (xAxis, yAxis) = SplitCoOrdinates(inpShipCoOrds);
            int currentIndex;
            switch (inpShip.Facing)
            {
                case Enums.Orientation.Down:
                    currentIndex = CurrentIndex(yAxis, Configuration.ValidYLabels);
                    for (int i = 1; i < inpShip.Size; i++)
                    {
                        if (currentIndex + i >= Configuration.GridYWidth)
                        {
                            Errors.TooCloseToEdgeError();
                        }
                        inpShipCoOrds = Configuration.ValidYLabels[currentIndex + i] + xAxis;
                        inpShipCoOrds = GridValidation.ValidShipLocation(inpShipCoOrds, inpPlayer);
                        inpPlayer.AddShip(inpShip, inpShipCoOrds);
                    }

                    break;
                case Enums.Orientation.Left:
                    currentIndex = CurrentIndex(xAxis, Configuration.ValidXLabels);
                    for (int i = inpShip.Size - 1; i > 0; i--)
                    {
                        if (currentIndex - i < 0)
                        {
                            Errors.TooCloseToEdgeError();
                        }
                        inpShipCoOrds = yAxis + Configuration.ValidXLabels[currentIndex - i];
                        inpShipCoOrds = GridValidation.ValidShipLocation(inpShipCoOrds, inpPlayer);
                        inpPlayer.AddShip(inpShip, inpShipCoOrds);
                    }

                    break;
                case Enums.Orientation.Right:
                    currentIndex = CurrentIndex(xAxis, Configuration.ValidXLabels);
                    for (int i = 1; i < inpShip.Size; i++)
                    {
                        if (currentIndex + i >= Configuration.GridXWidth)
                        {
                            Errors.TooCloseToEdgeError();
                        }
                        inpShipCoOrds = yAxis + Configuration.ValidXLabels[currentIndex + i];
                        inpShipCoOrds = GridValidation.ValidShipLocation(inpShipCoOrds, inpPlayer);
                        inpPlayer.AddShip(inpShip, inpShipCoOrds);
                    }

                    break;
                case Enums.Orientation.Up:
                    currentIndex = CurrentIndex(yAxis, Configuration.ValidYLabels);
                    for (int i = inpShip.Size - 1; i > 0; i--)
                    {
                        if (currentIndex - i < 0)
                        {
                            Errors.TooCloseToEdgeError();
                        }
                        inpShipCoOrds = Configuration.ValidYLabels[currentIndex - i] + xAxis;
                        inpShipCoOrds = GridValidation.ValidShipLocation(inpShipCoOrds, inpPlayer);
                        inpPlayer.AddShip(inpShip, inpShipCoOrds);
                    }

                    break;
            }
        }
        /// <summary>
        /// We've failed to save a ship, so make sure that we've not left any populated squares around.
        /// </summary>
        /// <param name = "inpShip" > The ship object that we're updating.</param>
        /// <param name = "inpPlayer" > Player object that we're updating.</param>
        public static void RemoveShip(Ships inpShip, Player inpPlayer)
        {
            Dictionary<string, ISquare> grid = inpPlayer.PlayersGrid.GridSquareList;

            // Step through the grid and remove the ship instances.
            // We need to step through it using co-ordinates as we
            // cannot update the grid while itterating through it.
            foreach (string locationX in Configuration.ValidXLabels)
            {
                foreach (string locationY in Configuration.ValidYLabels)
                {
                    string coOrdinate = locationY + locationX;
                    if (grid[coOrdinate] == inpShip)
                    {
                        grid[coOrdinate] = new Square(coOrdinate);
                    }
                }
            }
        }
        #endregion

        #region GridTools
        /// <summary>
        /// Get the Player record for the player that we are attacking.
        /// </summary>
        /// <param name="inpCurrentPlayer">Current player, who's target we're seeking.</param>
        /// <param name="inpPlayersList">Game object with all players listed.</param>
        /// <returns>Player who is identified as the target.</returns>
        public static Player GetEnemyPlayer(Player inpCurrentPlayer, Game inpPlayersList)
        {
            Player enemyPlayer = inpPlayersList.Players[inpCurrentPlayer.EnemyPlayer];
            return enemyPlayer;
        }
        /// <summary>
        /// Calculate how many hits were scored for how many shots were taken?
        /// </summary>
        /// <param name="inpCurrentPlayer">Player to calculate the score for.</param>
        /// <returns>Tupple; Number of hits and number of shots taken.</returns>
        public static (int numHits, int numShots) CalculateScore(Player inpCurrentPlayer)
        {
            int numHits = 0;
            int numShots = 0;

            foreach(KeyValuePair <string, ISquare> gridSquare in inpCurrentPlayer.PlayersGrid.GridSquareList)
            {
                if(gridSquare.Value.GetStatus(Enums.GameMode.Play) == Configuration.HitMarker)
                {
                    numHits += 1;
                    numShots += 1;
                }
                else if(gridSquare.Value.GetStatus(Enums.GameMode.Play) == Configuration.MissMarker)
                {
                    numShots += 1;
                }
            }
            //    foreach (KeyValuePair<string, Enums.GridStatus> gridSquare in inpCurrentPlayer.PlayersGrid.GridSquareList)
            //    {
            //        if (gridSquare.Value == Enums.GridStatus.Hit)
            //        {
            //            numHits += 1;
            //            numShots += 1;
            //        }
            //        else if (gridSquare.Value == Enums.GridStatus.Miss)
            //        {
            //            numShots += 1;
            //        }
            //    }

            return (numHits, numShots);
        }
        #endregion

        #region ShipTools
        /// <summary>
        /// Split the co-ordinates into X and Y elements.
        /// </summary>
        /// <param name="inpShipCoOrds">Co-ordinates to split.</param>
        /// <returns>X and Y elements of the co-ordinates, as strings.</returns>
        public static (string xAxis, string yAxis) SplitCoOrdinates(string inpShipCoOrds)
        {
            if (string.IsNullOrWhiteSpace(inpShipCoOrds))
            {
                Errors.BlankCoOrdinateError();
            }

            inpShipCoOrds = inpShipCoOrds.ToUpper();

            string xAxis;
            string yAxis;
            // If the grid is more than 26 rows high, we need to use 2 characters for the vertical co-ordinates.
            if (Configuration.NumShips > 26)
            {
                xAxis = inpShipCoOrds.Substring(2);
                yAxis = inpShipCoOrds.Substring(0, 2);
            }
            // Otherwise, we use 1 character for the Y co-ordinate.
            else
            {
                xAxis = inpShipCoOrds.Substring(1);
                yAxis = inpShipCoOrds.Substring(0, 1);
            }

            return (xAxis, yAxis);
        }
        /// <summary>
        /// Find out how far along the selected axis the current index is.
        /// </summary>
        /// <param name="inpShipCoOrdElement">The X or Y element of the co-ordinates.</param>
        /// <param name="inpList">The X or Y values list to look up in.</param>
        /// <returns>The current index from the list (zero based).</returns>
        private static int CurrentIndex(string inpShipCoOrdElement, List<string> inpList)
        {
            int currentIndex = 0;
            int count = 0;
            foreach (string entry in inpList)
            {
                if (entry == inpShipCoOrdElement)
                {
                    currentIndex = count;
                }
                count += 1;
            }

            return currentIndex;
        }
        #endregion
    }
}
