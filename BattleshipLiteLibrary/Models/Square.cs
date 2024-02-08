using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipLibrary.Core;

namespace BattleshipLibrary.Models
{
    public class Square
    {
        #region PrivateVariables
        private string _coOrdinates = "";
        #endregion

        #region Properties
        public string CoOrdinates
        {
            get { return _coOrdinates; }
        }
        #endregion

        #region Constructors
        public Square (string inpCoOrdinates)
        {
            _coOrdinates = inpCoOrdinates;
        }
        #endregion

        #region Methods
        public string GetStatus()
        {
            return Configuration.BlankMarker;
        }
        #endregion
    }
}
