using BattleshipLibrary.Core;
using System;
using System.Collections.Generic;

namespace BattleShipUI
{
    /// <summary>
    /// Display the grid where the game is played.
    /// </summary>
    class Grid
    {
        /// <summary>
        /// Display the grid on the screen.
        /// </summary>
        /// <param name="inpShipsList">List of ships to display. Pass null when playing the game.</param>
        /// <param name="inpHitList">List of locations of hits. Pass null during setup.</param>
        /// <param name="inpMissLists">List of locations of shots that missed. Pass null during setup.</param>
        public static void DrawGrid(List<string> inpShipsList, List<string> inpHitList, List<string> inpMissLists)
        {
            int GridWidth = Configuration.GridXWidth;
            int GridHeight = Configuration.GridYWidth;

            // Set the list of row labels (first always wants to be blank).
            string[] sideTitles = new string[GridHeight + 1];
            sideTitles[0] = " ";

            for (int i = 1; i <= GridHeight; i++)
            {
                sideTitles[i] = SetYLabel(i);
            }

            // Display the grid, line by line.
            for (int i = 0; i <= GridHeight; i++)
            {
                // The first square always displays the titles for that line.
                string dataLine = $" {sideTitles[i]} |";
                string separatorLine = $"---+";

                // Now work across the lines, square by square.
                // Note that we ignore column 0 as that is set above.
                for (int j = 1; j <= GridWidth; j++)
                {
                    // If on the first line, display the labels.
                    string valueEntry;
                    if (i == 0)
                    {
                        valueEntry = SetXLabel(j);
                    }
                    // Otherewise, show the appropriate marker for the game mode/square type.
                    else
                    {
                        string gridCoOrdinate = sideTitles[i] + SetXLabel(j);
                        valueEntry = SetSquareContents(gridCoOrdinate, inpShipsList, inpHitList, inpMissLists);
                    }

                    dataLine += $" {valueEntry} |";
                    separatorLine += $"---+";
                }

                //Display the line of the grid and the separator beneith it.
                Console.WriteLine(dataLine);
                Console.WriteLine(separatorLine);
            }
        }
        /// <summary>
        /// Determine what to display in each square of the grid.
        /// </summary>
        /// <param name="inpGridCoOrdinate">Grid co-ordinate that we're looking at.</param>
        /// <param name="inpShipsList">List of ships to display. Pass null when playing the game.</param>
        /// <param name="inpHitList">List of locations of hits. Pass null during setup.</param>
        /// <param name="inpMissLists">List of locations of shots that missed. Pass null during setup.</param>
        /// <returns>String to display in square</returns>
        private static string SetSquareContents(string inpGridCoOrdinate, List<string> inpShipsList, List<string> inpHitList, List<string> inpMissLists)
        {
            string valueEntry;
            // In setup, we show where the ships are.
            if (inpShipsList != null && inpShipsList.Contains(inpGridCoOrdinate))
            {
                valueEntry = Configuration.ShipMarker;
            }
            // In play, we show the hits and misses.
            else if (inpHitList != null && inpHitList.Contains(inpGridCoOrdinate))
            {
                valueEntry = Configuration.HitMarker;
            }
            else if (inpMissLists != null && inpMissLists.Contains(inpGridCoOrdinate))
            {
                valueEntry = Configuration.MissMarker;
            }
            // And blank for an empty square.
            else
            {
                valueEntry = " ";
            }

            return valueEntry;
        }

        /// <summary>
        /// Get the appropriate label for this column.
        /// </summary>
        /// <param name="inpLetter">Column number</param>
        /// <returns>Label for column</returns>
        private static string SetXLabel(int inpLetter)
        {
            // We're using a zero based array, so knock the number down by 1.
            inpLetter -= 1;

            return Configuration.ValidXLabels[inpLetter];
        }
        /// <summary>
        /// Get the appropriate label for this row.
        /// </summary>
        /// <param name="inpLetter">Rown number</param>
        /// <returns>Label for row</returns>
        private static string SetYLabel(int inpLetter)
        {
            // We're using a zero based array, so knock the number down by 1.
            inpLetter -= 1;

            return Configuration.ValidYLabels[inpLetter];
        }
    }
}
