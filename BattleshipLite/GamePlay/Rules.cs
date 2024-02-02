using System;
using System.Collections.Generic;
using BattleshipLibrary.Core;
using BattleshipLibrary.Models;
using BattleshipLibrary.Rules;
using BattleShipUI.Display;

namespace BattleShipUI.GamePlay
{
    class Rules
    {
        #region SetupMethods
        /// <summary>
        /// Prepare the game to play.
        /// </summary>
        /// <param name="inpCurrentGame">List of players to add to.</param>
        public static void SetupGame(Game inpCurrentGame)
        {
            // Welcome the players
            Messages.WelcomeMessage();
            Messages.WaitToProceed();

            // Get each player's details in turn.
            for (int playerNum = 1; playerNum <= Configuration.NumPlayers; playerNum++)
            {
                string playerName;
                Player newPlayer = null;
                do
                {
                    try
                    {
                        // Get the player's name.
                        playerName = GetPlayerName(playerNum);
                        newPlayer = new Player(playerName, inpCurrentGame.Players.Count);
                    }
                    catch (ArgumentException ex)
                    {
                        MessageOutErrors(ex);
                        playerName = "";
                    }
                } while (playerName == "");
                inpCurrentGame.Players.Add(newPlayer);

                // Place their ships.
                GetShipLocations(inpCurrentGame.Players[playerNum - 1]);
            }
        }

        /// <summary>
        /// Ask the player for their name.
        /// </summary>
        /// <param name="inpPlayerNum">Index number of the player to get.</param>
        /// <returns>Return the name.</returns>
        private static string GetPlayerName(int inpPlayerNum)
        {
            Messages.AskPlayerName(inpPlayerNum.ToString());
            string playerName = Console.ReadLine();
            return playerName;
        }
        /// <summary>
        /// Ask the player where they want their ships.
        /// </summary>
        /// <param name="inpPlayer">Player record</param>
        private static void GetShipLocations(Player inpPlayer)
        {
            // Set the location for each ship in the grid.
            foreach (KeyValuePair<string, int> ship in Configuration.ShipsList)
            {
                Ships currentShip = new Ships(ship.Key);

                // Make the request and display the grid for reference.
                Messages.PlaceShips(inpPlayer.PlayerName);

                GridShow.DrawGrid(inpPlayer.PlayersGrid, Enums.GameMode.Setup);

                // Loop round until we get a valid co-ordinates.
                string shipCoOrds;
                do
                {
                    Enums.Orientation shipFacing = GetShipFacing(currentShip);

                    // Request the location for the ship.
                    Messages.RequestShip(currentShip.ShipName);
                    shipCoOrds = Console.ReadLine().ToUpper();
                    try
                    {
                        GridManagement.SaveShip(shipCoOrds, shipFacing, currentShip, inpPlayer);
                    }
                    // If the location isn't good, display the messages to screen and loop.
                    catch (ArgumentOutOfRangeException ex)
                    {
                        MessageOutErrors(ex);
                        GridManagement.RemoveShip(currentShip, inpPlayer);
                        shipCoOrds = "";
                    }
                    catch (ArgumentException ex)
                    {
                        MessageOutErrors(ex);
                        GridManagement.RemoveShip(currentShip, inpPlayer);
                        shipCoOrds = "";
                    }
                } while (shipCoOrds == "");
            }

            // Clear the screen and display the settings before we continue.
            Console.Clear();
            Console.WriteLine();
            GridShow.DrawGrid(inpPlayer.PlayersGrid, Enums.GameMode.Setup);
            Messages.WaitToProceed();
        }
        /// <summary>
        /// Get the valid facing for the ship
        /// </summary>
        /// <param name="inpCurrentShip">The current ship that we're trying to get a facing for.</param>
        /// <returns>The facing of the ship.</returns>
        private static Enums.Orientation GetShipFacing(Ships inpCurrentShip)
        {
            // If the ship only occupies 1 square, we don't care what it's facing is, so use the default.
            Enums.Orientation shipFacing = inpCurrentShip.Facing;
            if (inpCurrentShip.Size > 1)
            {
                bool hasValidFacing = false;
                while (hasValidFacing == false)
                {
                    try
                    {
                        // Request which way the ship is facing.
                        Messages.RequestFacing(inpCurrentShip.ShipName);
                        string facingText = Console.ReadLine();

                        shipFacing = GridValidation.ValidShipFacing(facingText, inpCurrentShip);
                        hasValidFacing = true;
                    }
                    catch (ArgumentException ex)
                    {
                        MessageOutErrors(ex);
                    }
                }
            }
            Messages.ShowShipFacing(shipFacing, inpCurrentShip.Size);
            return shipFacing;
        }
        #endregion

        #region PlayRegion
        /// <summary>
        /// Manage the running of the game.
        /// </summary>
        /// <param name="inpPlayersList">List of players</param>
        public static void RunTheGame(Game inpPlayersList)
        {
            // Announce the start of the name.
            Messages.StartGame();

            // Keep taking turns until we get a winner.
            string winnerName = "";
            do
            {
                // In each round, the players take turns to shoot.
                foreach (Player player in inpPlayersList.Players)
                {
                    // If we've got a winner, no-more shots are allowed.
                    if (winnerName == "")
                    {
                        // Ask for where to shoot
                        Messages.WhereToShootHeader(player.PlayerName);
                        Player enemyPlayer = GridManagement.GetEnemyPlayer(player, inpPlayersList);
                        GridShow.DrawGrid(enemyPlayer.PlayersGrid, Enums.GameMode.Play);

                        // Get target co-ordinates from user.
                        string targetCoOrds = PromptForShot(player, inpPlayersList);

                        // Check if it is a hit.
                        CheckIfHit(targetCoOrds, player, inpPlayersList);

                        Messages.PadScreen();
                        GridShow.DrawGrid(enemyPlayer.PlayersGrid, Enums.GameMode.Play);
                        Messages.WaitToProceed();

                        winnerName = WhoHasWon(player, inpPlayersList);
                    }
                }
            } while (winnerName == "");

            DisplayResults(winnerName, inpPlayersList);
            Messages.WaitToProceed();
        }
        /// <summary>
        /// Prompt the user for a target for your shot.
        /// </summary>
        /// <param name="inpCurrentPlayer">Current player record.</param>
        /// <param name="inpPlayersList">Game object, used to find enemy player.</param>
        /// <returns></returns>
        private static string PromptForShot(Player inpCurrentPlayer, Game inpPlayersList)
        {
            string targetCoOrds;

            // Keep going until we have a valid target.
            do
            {
                Messages.WhereToShootRequest(inpCurrentPlayer.PlayerName);
                targetCoOrds = Console.ReadLine().ToUpper();
                try
                {
                    Player enemyPlayer = GridManagement.GetEnemyPlayer(inpCurrentPlayer, inpPlayersList);
                    if (!GridValidation.ValidShotTaken(targetCoOrds, enemyPlayer))
                    {
                        targetCoOrds = "";
                    }
                }
                // Handle if we've already shot there.
                catch (ArgumentException ex)
                {
                    MessageOutErrors(ex);
                    targetCoOrds = "";
                }
            } while (targetCoOrds == "");

            return targetCoOrds;
        }
        /// <summary>
        /// Are those co-ordinates a hit or a miss?
        /// </summary>
        /// <param name="inpTargetCoOrds">Target co-ordinates specified.</param>
        /// <param name="inpCurrentPlayer">Current player record.</param>
        /// <param name="inpPlayersList">List of players.</param>
        private static void CheckIfHit(string inpTargetCoOrds, Player inpCurrentPlayer, Game inpPlayersList)
        {
            // Player whos grid we're shooting at.
            Player enemyPlayer = GridManagement.GetEnemyPlayer(inpCurrentPlayer, inpPlayersList);
            if (GridValidation.IsShipThere(inpTargetCoOrds, enemyPlayer))
            {
                enemyPlayer.PlayersGrid.SetGridSquareMode(inpTargetCoOrds, Enums.GridStatus.Hit);
                foreach (Ships ship in enemyPlayer.PlayersShips)
                {
                    ship.ShipHit(inpTargetCoOrds);
                }
                Messages.ShotHit();
            }
            else
            {
                enemyPlayer.PlayersGrid.SetGridSquareMode(inpTargetCoOrds, Enums.GridStatus.Miss);
                Messages.ShotMiss();
            }
        }
        /// <summary>
        /// Have we a winner yet? If so, who is it?
        /// </summary>
        /// <param name="inpCurrentPlayer">Current player record.</param>
        /// <returns></returns>
        private static string WhoHasWon(Player inpCurrentPlayer, Game inpPlayersList)
        {
            string winnerName = "";

            Player enemyPlayer = GridManagement.GetEnemyPlayer(inpCurrentPlayer, inpPlayersList);

            if (enemyPlayer.SunkShips == Configuration.NumShips)
            {
                winnerName = inpCurrentPlayer.PlayerName;
            }

            return winnerName;
        }
        /// <summary>
        /// Display the results of the game.
        /// </summary>
        /// <param name="inpWinnerName">Name of the winner.</param>
        /// <param name="inpPlayersList">List of players</param>
        private static void DisplayResults(string inpWinnerName, Game inpPlayersList)
        {
            Messages.AnnounceWinner(inpWinnerName);
            // List everyone's hit rate.
            foreach (Player player in inpPlayersList.Players)
            {
                int numHits;
                int numShots;
                Player enemyPlayer = GridManagement.GetEnemyPlayer(player, inpPlayersList);
                (numHits, numShots) = GridManagement.CalculateScore(enemyPlayer);

                Messages.DisplayResults(player.PlayerName, numHits, numShots);
            }
        }
        #endregion

        #region ErrorHandling
        /// <summary>
        /// Display an exception as a screen message instead of an error.
        /// </summary>
        /// <param name="inpError">The exception to display.</param>
        public static void MessageOutErrors(ArgumentException inpError)
        {
            if (inpError.Message.Contains("(Parameter"))
            {
                Console.WriteLine(inpError.ParamName);
            }
            else
            {
                Console.WriteLine(inpError.Message);
            }
        }
        /// <summary>
        /// Display an exception as a screen message instead of an error.
        /// </summary>
        /// <param name="inpError">The exception to display.</param>
        public static void MessageOutErrors(ArgumentOutOfRangeException inpError)
        {
            if (inpError.Message.Contains("(Parameter"))
            {
                Console.WriteLine(inpError.ParamName);
            }
            else
            {
                Console.WriteLine(inpError.Message);
            }
        }
        #endregion
    }
}