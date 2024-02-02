using System;

namespace BattleshipLibrary.Core
{
    /// <summary>
    /// How to handle various errors.
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// "You must provide a player name."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string BlankNameError()
        {
            throw new ArgumentException("You must provide a player name.");
        }

        /// <summary>
        /// That does not represent a valid facing. Please enter D(own), U(p), L(eft) or R(right), or leave blank to accept the current setting.
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string InvalidFacingError()
        {
            throw new ArgumentException("That does not represent a valid facing. Please enter D(own), U(p), L(eft) or R(right), or leave blank to accept the current setting.");
        }

        /// <summary>
        /// "There aren't that many players"
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string TooManyPlayersError()
        {
            throw new ArgumentOutOfRangeException("There aren't that many players");
        }

        /// <summary>
        /// "You already have a ship in that location. Please select another one."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string AlreadyFilledError()
        {
            throw new ArgumentException($"You already have a ship in that location. Please select another one.");
        }
        /// <summary>
        /// "You already have a ship at {inpCoOrdinates}. Please select another one."
        /// </summary>
        /// <param name="inpCoOrdinates">The location causing problems.</param>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string AlreadyFilledError(string inpCoOrdinates)
        {
            throw new ArgumentException($"You already have a ship at {inpCoOrdinates}. Please select another one.");
        }

        /// <summary>
        /// "You have already targeted this location. Please select another one."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string AlreadyShotError()
        {
            throw new ArgumentException($"You have already targeted this location. Please select another one.");
        }

        /// <summary>
        /// "That is not a valid co-ordinate in the grid. Please select a co-ordinate in the form of '{inpExample}'"
        /// </summary>
        /// <param name="inpExample">Example of format, e.g. AD53 or A3.</param>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string InvalidCoOrdinateError(string inpExample)
        {
            throw new ArgumentOutOfRangeException($"That is not a valid co-ordinate in the grid. Please select a co-ordinate in the form of '{inpExample}'");
        }

        /// <summary>
        /// "You have not entered any co-ordinates."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string BlankCoOrdinateError()
        {
            throw new ArgumentOutOfRangeException($"You have not entered any co-ordiniates.");
        }

        /// <summary>
        /// "The ship cannot fit in the grid at that location and orientation."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string TooCloseToEdgeError()
        {
            throw new ArgumentOutOfRangeException($"The ship cannot fit in the grid at that location and orientation.");
        }
    }
}