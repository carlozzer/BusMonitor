using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusMonitor.BLL.Extensions
{
    public static class LongExtensions
    {
        public static DateTime FromUnixFormat(this long dt) {

            DateTime ret = default(DateTime);

            if ( dt < 0 ) { 
                throw new ArgumentOutOfRangeException();
            }

            ret = DateHelper.FromUnixFormat( dt );
            return ret;
        }

    }
}
