﻿using BattleshipLibrary.Core;
using System;

namespace BattleShipUI
{
    public static class Messages
    {
        #region SetupMessages
        /// <summary>
        /// Welcome the players to the game.
        /// </summary>
        public static void WelcomeMessage()
        {
            // Make sure the screen is clear before we start.
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Welcome to BATTLESHIPS - LIGHT!");
        }

        /// <summary>
        /// Ask the player their name.
        /// </summary>
        /// <param name="playerLabel">Label to discribe the player (e.g. 1, 2, 3)</param>
        public static void AskPlayerName(string playerLabel)
        {
            Console.Clear();
            Console.Write($"Hello player {playerLabel}, please enter your name: ");
        }

        /// <summary>
        /// Ask the player to select where their ships are (header).
        /// </summary>
        /// <param name="inpPlayerName">Name of the player.</param>
        public static void PlaceShips(string inpPlayerName)
        {
            Console.Clear();
            Console.WriteLine($"Thank you {inpPlayerName}. Please place your ships");
        }
        /// <summary>
        /// Ask for the location of each ship individually.
        /// </summary>
        /// <param name="inpShipName">Label to identify the ship (e.g. 1, 2, 3)</param>
        public static void RequestShip(string inpShipName)
        {
            Console.Write($"Please enter the co-ordinates for ship number {inpShipName}: ");
        }
        #endregion

        #region PlayMessages
        /// <summary>
        /// Announce the start of the game.
        /// </summary>
        public static void StartGame()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"LET'S PLAY THE GAME!");
            Messages.WaitToProceed();
        }
        /// <summary>
        /// Top of the screen for player taking a shot.
        /// </summary>
        /// <param name="inpPlayerName">Name of current player.</param>
        public static void WhereToShootHeader(string inpPlayerName)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"        Player: {inpPlayerName}");
            Console.WriteLine();
            Console.WriteLine($"{inpPlayerName} please take your shot:");
            Console.WriteLine($"Hit = {Configuration.HitMarker},    Miss = {Configuration.MissMarker}");
            Console.WriteLine();
        }
        /// <summary>
        /// Ask the user where to shoot.
        /// </summary>
        /// <param name="inpPlayerName">Name of current player.</param>
        public static void WhereToShootRequest(string inpPlayerName)
        {
            Console.Write($"{inpPlayerName} where do you want to shoot?");
        }
        /// <summary>
        /// Announce the shot having hit.
        /// </summary>
        public static void ShotHit()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"        A Hit!");
        }
        /// <summary>
        /// Announce the shot having missed.
        /// </summary>
        public static void ShotMiss()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"        Missed");
        }
        #endregion

        #region ResultsMessages
        /// <summary>
        /// Announce a winner.
        /// </summary>
        /// <param name="inpWinnerName">Name of the winner.</param>
        public static void AnnounceWinner(string inpWinnerName)
        {
            Console.WriteLine($"WE HAVE A WINNER!");
            Console.WriteLine($"WINNER: {inpWinnerName} has destroyed all their target ships.");
            Console.WriteLine();
            Console.WriteLine($"The hit rates of each player are:");
        }
        /// <summary>
        /// "{inpPlayerName} hit {inpNumHits} times out of {inpNumShots} taken."
        /// </summary>
        /// <param name="inpPlayerName">Name of the player.</param>
        /// <param name="inpNumHits">Number of hits by the player.</param>
        /// <param name="inpNumShots">Total numebr of shots taken.</param>
        public static void DisplayResults(string inpPlayerName, int inpNumHits, int inpNumShots)
        {
            Console.WriteLine($"{inpPlayerName} hit {inpNumHits} times out of {inpNumShots} taken.");
        }
        #endregion

        #region FormattingMessages
        /// <summary>
        /// Message to pause before proceeding,
        /// </summary>
        public static void WaitToProceed()
        {
            Console.WriteLine("Press RETURN to proceed.");
            Console.ReadLine();
        }
        /// <summary>
        /// Padding of 4 lines.
        /// </summary>
        public static void PadScreen()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        #endregion
    }
}
