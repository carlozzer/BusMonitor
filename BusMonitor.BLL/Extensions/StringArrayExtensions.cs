using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class StringArrayExtensions
    {
        public static string SafeJoin( this string[] arr , string separator ) {

            string ret = string.Empty;

            if ( arr != null ) {

                string accumulator = string.Empty;
                arr.SafeForEach( (item,index) => {

                    bool first = index == 0;
                    accumulator = string.Concat( accumulator , first ? string.Empty : separator , item );

                });

                ret = string.Copy( accumulator );

            }

            return ret;

        }

        public static string SafeIndexer( this string[] arr , int index ) {

            string ret = string.Empty;

            if ( arr != null && arr.Length > index ) {

                ret = arr[index].Safe();

            }

            return ret;

        }

        public static string SafeLast( this string[] arr ) {

            string ret = string.Empty;

            if ( arr != null ) {

                int last = arr.Length - 1;
                if ( last >= 0 ) { 

                    ret = arr.SafeIndexer( last );

                }
            }

            return ret;

        }

        public static string SafeFirst( this string[] arr ) {

            return arr.SafeIndexer( 0 );

        }
    }
}
