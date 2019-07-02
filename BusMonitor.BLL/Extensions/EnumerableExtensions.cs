using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class EnumerableExtensions
    {
        #region SAFE

        public static void SafeForEach<T>(this IEnumerable<T> lst, Action<T> act)
        {
            if (lst != null && lst.Count() > 0) {
                foreach (T element in lst) {
                    act(element);
                }
            }
        }

        public static void SafeForEach<T>(this IEnumerable<T> lst, Action<T,int> act)
        {
            if (lst != null && lst.Count() > 0)
            {
                int cont = 0;
                foreach (T element in lst)
                {
                    act(element, cont);
                    cont++;
                }
            }
        }

        public static int
        SafeCount<T>(this List<T> lst)
        {
            return lst == null ? 0 : lst.Count;
        }

        public static bool 
        SafeHasItems<T>(this List<T> lst)
        {
            return lst != null && lst.Count > 0;
        }

        #endregion
    }
}
