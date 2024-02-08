using BattleshipLibrary.Rules;
using BattleshipLibrary.Core;
using System.Collections.Generic;

namespace BattleshipLibrary.Models
{
    public class Miss: Square
    {
        #region Constructors
        public Miss (string inpProwCoOrds): base(inpProwCoOrds)
        {
        }
        #endregion

        #region Methods
        new public string GetStatus()
        {
            return Configuration.MissMarker;
        }
        #endregion
    }
}
