using System;
using BattleshipLibrary.Models;
using BattleshipLibrary.Core;

namespace BattleShipUI.Display
{
    class GridShow
    {
        /// <summary>
        /// Display the grid on the screen.
        /// </summary>
        /// <param name="inpDisplayGrid">The grid object that we're displaying.</param>
        /// <param name="inpMode">Are we in setup mode (show ships) or game mode (show shots).</param>
        public static void DrawGrid(Grid inpDisplayGrid, Enums.GameMode inpMode)
        {
            int gridWidth = Configuration.GridXWidth;
            int gridHeight = Configuration.GridYWidth;

            // Set the list of row labels (first always wants to be blank).
            string[] sideTitles = new string[gridHeight + 1];
            sideTitles[0] = " ";

            for (int i = 1; i <= gridHeight; i++)
            {
                sideTitles[i] = SetYLabel(i);
            }

            // Display the grid, line by line.
            for (int i = 0; i <= gridHeight; i++)
            {
                // The first square always displays the titles for that line.
                string dataLine = $" {sideTitles[i]} |";
                string separatorLine = $"---+";

                // Now work across the lines, square by square.
                // Note that we ignore column 0 as that is set above.
                for (int j = 1; j <= gridWidth; j++)
                {
                    // If on the first line, display the labels.
                    string valueEntry;
                    if (i == 0)
                    {
                        valueEntry = SetXLabel(j);
                    }
                    // Otherwise, show the appropriate marker for the game mode/square type.
                    else
                    {
                        string gridCoOrdinate = sideTitles[i] + SetXLabel(j);
                        valueEntry = SetSquareContents(gridCoOrdinate, inpDisplayGrid, inpMode);
                    }

                    dataLine += $" {valueEntry} |";
                    separatorLine += $"---+";
                }

                //Display the line of the grid and the separator beneath it.
                Console.WriteLine(dataLine);
                Console.WriteLine(separatorLine);
            }
        }
        /// <summary>
        /// Determine what to display in each square of the grid.
        /// </summary>
        /// <param name="inpGridCoOrdinate">Grid co-ordinate that we're looking at.</param>
        /// <param name="inpDisplayGrid">The grid that we're displaying</param>
        /// <param name="inpMode">Are we in Setup or Play mode?</param>
        /// <returns>String to display in square</returns>
        private static string SetSquareContents(string inpGridCoOrdinate, Grid inpDisplayGrid, Enums.GameMode inpMode)
        {
            string valueEntry = Configuration.BlankMarker;
            switch (inpDisplayGrid.GetGridSquareMode(inpGridCoOrdinate))
            {
                case Enums.GridStatus.Empty:
                    // And blank for an empty square.
                    valueEntry = Configuration.BlankMarker;
                    break;
                case Enums.GridStatus.Hit:
                    if (inpMode == Enums.GameMode.Setup)
                    { valueEntry = Configuration.BlankMarker; }
                    else
                    { valueEntry = Configuration.HitMarker; }
                    break;
                case Enums.GridStatus.Miss:
                    if (inpMode == Enums.GameMode.Setup)
                    { valueEntry = Configuration.BlankMarker; }
                    else
                    { valueEntry = Configuration.MissMarker; }
                    break;
                case Enums.GridStatus.Ship:
                    if (inpMode == Enums.GameMode.Setup)
                    { valueEntry = Configuration.ShipMarker; }
                    else
                    { valueEntry = Configuration.BlankMarker; }
                    break;
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
