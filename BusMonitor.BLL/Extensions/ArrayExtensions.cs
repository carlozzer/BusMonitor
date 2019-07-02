using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{

    public static class ArrayExtensions
    {
        #region SAFE

        public static int SafeLength(this Array arr)
        {
            int ret = 0;

            if (arr != null) {
                ret = arr.Length;
            }

            return ret;
        }

        public static T SafeFirst<T>(this Array arr)
        {
            T ret = default(T);

            if ( arr.SafeLength() > 0 ) {
                ret = (T) arr.GetValue(0);
            }

            return ret;
        }


        /// <summary>
        /// Si el array es null no contiene ningún element, por lo tanto no hay ninguno nulo
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static bool NotASingleNull(this Array arr) {

            bool ok_so_far = true;

            for (int i = 0; i < arr.SafeLength(); i++) {
                ok_so_far = ok_so_far && arr.GetValue(i) != null;
            }

            return ok_so_far;

        }

        #endregion
    }

}
