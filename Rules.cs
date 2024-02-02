using BattleshipLibrary;
using BattleshipLibrary.Core;
using BattleshipLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BattleShipUI
{
    class Rules
    {
        #region SetupMethods
        /// <summary>
        /// Prepare the game to play.
        /// </summary>
        /// <param name="inpPlayersList">List of players to add to.</param>
        public static void SetupGame(PlayersList inpPlayersList)
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
                        newPlayer = new Player(playerName, inpPlayersList.Players.Count);
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.ParamName);
                        playerName = "";
                    }
                } while (playerName == "");
                inpPlayersList.Players.Add(newPlayer);

                // Place their ships.
                GetShipLocations(inpPlayersList.Players[playerNum - 1]);
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
            // Get location of each ship in turn, upto the limit.
            for (int shipNum = 1; shipNum <= Configuration.NumShips; shipNum++)
            {
                // Make the request and display the grid for reference.
                Messages.PlaceShips(inpPlayer.PlayerName);

                Grid.DrawGrid(inpPlayer.ShipLocations, null, null);

                // Loop round until we get a valid co-ordinates.
                string shipCoOrds;
                do
                {
                    // Request the location for the ship.
                    Messages.RequestShip(shipNum.ToString());
                    shipCoOrds = Console.ReadLine().ToUpper();
                    try
                    {
                        // Validate the location as we save it.
                        inpPlayer.ShipLocation(shipNum - 1, shipCoOrds);
                        shipCoOrds = inpPlayer.ShipLocation(shipNum - 1);
                    }
                    // If the location isn't good, display the messages to screen and loop.
                    catch (ArgumentOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.ParamName);
                        shipCoOrds = "";
                    }
                    catch (ArgumentException ex)
                    {
                        Console.WriteLine(ex.Message);
                        shipCoOrds = "";
                    }
                } while (shipCoOrds == "");
            }

            // Clear the screen and display the settings before we continue.
            Console.Clear();
            Console.WriteLine();
            Grid.DrawGrid(inpPlayer.ShipLocations, null, null);
            Messages.WaitToProceed();
        }
        #endregion

        #region PlayRegion
        /// <summary>
        /// Manage the running of the game.
        /// </summary>
        /// <param name="inpPlayersList">List of players</param>
        public static void RunTheGame(PlayersList inpPlayersList)
        {
            // Anounce the start of the name.
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
                        Grid.DrawGrid(null, player.ShotsHit, player.ShotsMissed);

                        // Get target co-ordinates from user.
                        string targetCoOrds = PromptForShot(player);

                        // Check if it is a hit.
                        CheckIfHit(targetCoOrds, player, inpPlayersList);

                        Messages.PadScreen();
                        Grid.DrawGrid(null, player.ShotsHit, player.ShotsMissed);
                        Messages.WaitToProceed();

                        winnerName = WhoHasWon(player);
                    }
                }
            } while (winnerName == "");

            DisplayResults(winnerName, inpPlayersList.Players);
        }
        /// <summary>
        /// Prompt the user for a target for your shot.
        /// </summary>
        /// <param name="inpCurrentPlayer">Current player record.</param>
        /// <returns></returns>
        private static string PromptForShot(Player inpCurrentPlayer)
        {
            string targetCoOrds;

            // Keep going until we have a valid target.
            do
            {
                Messages.WhereToShootRequest(inpCurrentPlayer.PlayerName);
                targetCoOrds = Console.ReadLine().ToUpper();
                try
                {
                    if (!GridValidation.ValidShotTaken(targetCoOrds, inpCurrentPlayer))
                    {
                        targetCoOrds = "";
                    }
                }
                // Handle if we've already shot there.
                catch (ArgumentException ex)
                {
                    if (ex.ParamName == "")
                    {
                        Console.WriteLine(ex.Message);
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                    }
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
        private static void CheckIfHit(string inpTargetCoOrds, Player inpCurrentPlayer, PlayersList inpPlayersList)
        {
            if (GridValidation.IsShipThere(inpTargetCoOrds, inpPlayersList.Players[inpCurrentPlayer.EnemyPlayer]))
            {
                inpCurrentPlayer.ShotHit(inpCurrentPlayer.ShotsHit.Count, inpTargetCoOrds);
                Messages.ShotHit();
            }
            else
            {
                inpCurrentPlayer.ShotMissed(inpCurrentPlayer.ShotsMissed.Count, inpTargetCoOrds);
                Messages.ShotMiss();
            }
        }
        /// <summary>
        /// Have we a winner yet? If so, who is it?
        /// </summary>
        /// <param name="inpCurrentPlayer">Current player record.</param>
        /// <returns></returns>
        private static string WhoHasWon(Player inpCurrentPlayer)
        {
            string winnerName = "";

            if (inpCurrentPlayer.ShotsHit.Count >= Configuration.NumShips)
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
        private static void DisplayResults(string inpWinnerName, List<Player> inpPlayersList)
        {
            Messages.AnnounceWinner(inpWinnerName);
            // List everyone's hit rate.
            foreach (Player player in inpPlayersList)
            {
                int numHits = player.ShotsHit.Count;
                int numShots = player.ShotsMissed.Count + numHits;
                Messages.DisplayResults(player.PlayerName, numHits, numShots);
            }
        }
        #endregion
    }
}