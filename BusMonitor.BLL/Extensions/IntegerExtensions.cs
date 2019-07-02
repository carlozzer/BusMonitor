using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class IntegerExtensions
    {
        #region CHECKS

        public static bool NotZero(this int number)
        {
            return (number != 0);
        }

        public static bool InBounds(this int number, int min, int max)
        {
            return (number >= min && number <= max);
        }

        #endregion

        #region ROMAN

        public static string ToRoman( this int number ) {

            string ret = "";

            Dictionary<int,string> romanMatrix = new Dictionary<int,string> {
                  {1000, "M" },
                  {900, "CM" },
                  {500, "D" },
                  {400, "CD" },
                  {100, "C" },
                  {90, "XC" },
                  {50, "L" },
                  {40, "XL" },
                  {10, "X" },
                  {9, "IX" },
                  {5, "V" },
                  {4, "IV" },
                  {1, "I" }
            };

            
            if ( number == 0 ) {
                return "";
            }

            foreach ( int key in romanMatrix.Keys ) {

                if ( number >= key ) {
                    return romanMatrix[key] + ToRoman( number - key );
                }
            }

            return ret;
        }

        #endregion

        #region LOOP

        public static void Times( this int number , Action<int> act ) {

            if ( number >= 0 ) { 

                for ( int i=0; i < number; i++ ) { 

                    act( i );

                }

            }

        }

        #endregion

    }
}
