using BattleshipLibrary.Rules;
using BattleshipLibrary.Core;
using System.Collections.Generic;

namespace BattleshipLibrary.Models
{
    public class Hit: Square
    {
        #region Constructors
        public Hit (string inpProwCoOrds): base(inpProwCoOrds)
        {
        }
        #endregion

        #region Methods
        new public string GetStatus()
        {
            return Configuration.HitMarker;
        }
        #endregion
    }
}
