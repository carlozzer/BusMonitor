using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public class NumberHelper
    {
        public static Nullable<int> ToInt(object num)
        { 
            Nullable<int> ret = null;

            if (num != null)
            { 
                int aux;
                if (Int32.TryParse(num.ToString(), out aux))
                {
                    ret = aux;
                }
            }

            return ret;
        }

        public static bool IsInt(object num)
        {
            bool ret = false;

            Nullable<int> check = ToInt(num);
            ret = check.HasValue;

            return ret;
        }

        public static bool IsNumeric(object Expression)
        {
            bool ret = false;
            double aux;
            ret = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out aux);
            return ret;
        }

        #region RANDOM

        //Function to get random number
        private static readonly Random getrandom = new Random();
        private static readonly object syncLock = new object();
        public static int GetRandomNumber(int min, int max)
        {
            lock (syncLock) { // synchronize
                return getrandom.Next(min, max);
            }
        }

        #endregion
    }
}
