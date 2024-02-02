using BattleshipLibrary.Core;
using BattleshipLibrary.Models;

namespace BattleshipLibrary.Rules
{
    /// <summary>
    /// Tools to control the grid and it's inputs.
    /// </summary>
    public static class GridValidation
    {
        #region GridValitaionTools
        /// <summary>
        /// Are the specified co-ordinates in the correct format and within the grid?
        /// </summary>
        /// <param name="inpCoOrdinates">Co-ordinates to check.</param>
        /// <returns>The co-ordinates, if they are valid, blank if they are not.</returns>
        public static string ValidCoOrdinates(string inpCoOrdinates)
        {
            if (string.IsNullOrWhiteSpace(inpCoOrdinates))
            {
                // You have not entered any co-ordinates.
                Errors.BlankCoOrdinateError();
            }

            string coOrdX;
            string coOrdY;
            string example;

            (coOrdX, coOrdY) = GridManagement.SplitCoOrdinates(inpCoOrdinates);
            if (coOrdY.Length > 1)
            {
                example = "AC24";
            }
            else
            {
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
        #endregion

        #region SetupValidataionTools
        /// <summary>
        /// Check that the facing entered is a valid facing.
        /// </summary>
        /// <param name="inpFacingText">The text entered to define the facing.</param>
        /// <param name="inpCurrentShip">The current ship facing.</param>
        /// <returns>The facing of the ship, from the enumeration.</returns>
        public static Enums.Orientation ValidShipFacing(string inpFacingText, Ships inpCurrentShip)
        {
            Enums.Orientation currentFacing = Enums.Orientation.Down;

            // If the input's blank, don't try the substring.
            if (inpFacingText.Length > 0)
            {
                inpFacingText = inpFacingText.ToLower().Substring(0, 1);
            }

            switch (inpFacingText)
            {
                case "d":
                    currentFacing = Enums.Orientation.Down;
                    break;
                case "l":
                    currentFacing = Enums.Orientation.Left;
                    break;
                case "r":
                    currentFacing = Enums.Orientation.Right;
                    break;
                case "u":
                    currentFacing = Enums.Orientation.Up;
                    break;
                case "":
                    currentFacing = inpCurrentShip.Facing;
                    break;
                default:
                    Errors.InvalidFacingError();
                    break;
            }

            return currentFacing;
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
                Errors.AlreadyFilledError(inpCoOrdinates);
            }

            return inpCoOrdinates;
        }
        #endregion

        #region PlayValidationTools
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
            if (HaveAlreadyShot(inpCoOrdinates, inpPlayer))
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
            Enums.GridStatus squareStatus = inpPlayer.PlayersGrid.GetGridSquareMode(inpCoOrdinates);
            return (squareStatus == Enums.GridStatus.Ship);
        }
        /// <summary>
        /// Have we shot at this square before?
        /// </summary>
        /// <param name="inpCoOrdinates"></param>
        /// <param name="inpPlayer"></param>
        /// <returns></returns>
        public static bool HaveAlreadyShot(string inpCoOrdinates, Player inpPlayer)
        {
            Enums.GridStatus squareStatus = inpPlayer.PlayersGrid.GetGridSquareMode(inpCoOrdinates);
            return (squareStatus == Enums.GridStatus.Hit ||
                    squareStatus == Enums.GridStatus.Miss);
        }
        #endregion
    }
}
