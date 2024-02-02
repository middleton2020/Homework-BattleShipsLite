using BattleshipLibrary.Core;
using BattleshipLibrary.Models;

namespace BattleshipLibrary
{
    /// <summary>
    /// Tools to control the grid and it's inputs.
    /// </summary>
    public static class GridValidation
    {
        /// <summary>
        /// Are the specified co-ordinates in the correct format and within the grid?
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates to check.</param>
        /// <returns>The co-ordinates, if they are valid, blank if they are not.</returns>
        private static string ValidCoOrdinates(string inpCoOrdinates)
        {
            if (string.IsNullOrWhiteSpace(inpCoOrdinates))
            {
                // You have not entered any co-ordiniates.
                Errors.BlankCoOrdinateError();
            }

            string coOrdX;
            string coOrdY;
            string example;

            // If the grid is more than 26 rows high, we need to use 2 characters for the vertical co-ordinates.
            if (Configuration.GridYWidth > 26)
            {
                coOrdX = inpCoOrdinates.Substring(2);
                coOrdY = inpCoOrdinates.Substring(0, 2);
                example = "AC24";
            }
            // Otherwise, we use 1 character for the Y co-ordinate.
            else
            {
                coOrdX = inpCoOrdinates.Substring(1);
                coOrdY = inpCoOrdinates.Substring(0, 1);
                example = "D3";
            }

            // Force to co-ordinates to upper case.
            coOrdY = coOrdY.ToUpper();

            // Check that the co-ordinates are valid.
            if (!Configuration.ValidXLabels.Contains(coOrdX) ||
                !Configuration.ValidYLabels.Contains(coOrdY))
            {
                // "That is not a valid co-ordinate in the grid. Please select a co-ordinate in the form of '{inpExample}'"
                Errors.InvalidCoOrdinateError(example);
            }

            return coOrdY + coOrdX;
        }
        /// <summary>
        /// If co-ordinates are valid, check we don't have a ship here already.
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates to check.</param>
        /// <param name="inpPlayer">The player record to check against.</param>
        /// <returns>The co-ordinates, if they are valid, blank if they are not.</returns>
        public static string ValidShipLocation(string inpCoOrdinates, Player inpPlayer)
        {
            inpCoOrdinates = ValidCoOrdinates(inpCoOrdinates);

            // Do we already have a ship there.
            if (IsShipThere(inpCoOrdinates, inpPlayer))
            {
                // "You already have a ship in that location. Please select another one."
                Errors.AlreadyFilledError();
            }

            return inpCoOrdinates;
        }
        /// <summary>
        /// If co-ordinates aare valid, check that we haven't already shot here.
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates to check.</param>
        /// <param name="inpPlayer">The player record to check against.</param>
        /// <returns>Are the co-ordinates valid?</returns>
        public static bool ValidShotTaken(string inpCoOrdinates, Player inpPlayer)
        {
            inpCoOrdinates = ValidCoOrdinates(inpCoOrdinates);

            // Have we already shot this location?
            if (inpPlayer.ShotsMissed.Contains(inpCoOrdinates) ||
                inpPlayer.ShotsHit.Contains(inpCoOrdinates))
            {
                // "You have already targeted this location. Please select another one."
                Errors.AlreadyShotError();
                inpCoOrdinates = "";
            }

            return (inpCoOrdinates != "");
        }

        /// <summary>
        /// Does this square contain one of this player's ships?
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates to check.</param>
        /// <param name="inpPlayer">Player who's ship we're checking for.</param>
        /// <returns>Is there a ship in this square?</returns>
        public static bool IsShipThere(string inpCoOrdinates, Player inpPlayer)
        {
            return inpPlayer.ShipLocations.Contains(inpCoOrdinates);
        }
    }
}