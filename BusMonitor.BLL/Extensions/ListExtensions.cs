using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class ListExtensions
    {
        #region SAFE

        public static void SafeAdd<T>(this List<T> lst, T item)
        {
            if (lst != null) {
                if (item != null) {
                    lst.Add(item);
                }
            }
        }

        public static T SafeFirst<T>(this List<T> lst )
        {
            T ret = default(T);

            if ( lst.SafeCount() > 0 ) {
                ret = (T) lst[0];
            }

            return ret;
        }

        #endregion
    }
}
