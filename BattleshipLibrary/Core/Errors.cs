using System;

namespace BattleshipLibrary.Core
{
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
        /// "There aren't that many players"
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string TooManyPlayersError()
        {
            throw new ArgumentOutOfRangeException("There aren't that many players");
        }
        /// <summary>
        /// "There is no {inpIndex} ship defined."
        /// </summary>
        /// <param name="inpIndex">Index in the ships list.</param>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string TooManyShipsError(int inpIndex)
        {
            throw new IndexOutOfRangeException($"There is no {inpIndex} ship defined.");
        }
        /// <summary>
        /// "You have not yet taken your {inpIndex} shot."
        /// </summary>
        /// <param name="inpIndex">Index in the shots list.</param>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string TooManyShotsError(int inpIndex)
        {
            throw new IndexOutOfRangeException($"You have not yet taken your {inpIndex} shot.");
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
        /// "You have not entered any co-ordiniates."
        /// </summary>
        /// <returns>Nothing. Could return a message if we don't throw an error instead.</returns>
        public static string BlankCoOrdinateError()
        {
            throw new ArgumentOutOfRangeException($"You have not entered any co-ordiniates.");
        }
    }
}
