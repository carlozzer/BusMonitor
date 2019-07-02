using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class DoubleExtensions
    {
        #region CHECKS

        public static bool NotZero(this double number)
        {
            return (number != 0);
        }

        public static bool InBounds(this double number, double min, double max)
        {
            return (number >= min && number <= max);
        }

        #endregion
    }
}
