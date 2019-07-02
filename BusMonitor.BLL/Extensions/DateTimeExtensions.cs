using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusMonitor.BLL.Extensions
{
    public static class DateTimeExtensions
    {
        public static string SafeToString(this Nullable<DateTime> dt, string format="")
        {
            string ret = string.Empty;

            if (dt.HasValue) {
                ret = string.IsNullOrWhiteSpace(format) ? dt.Value.ToString() : dt.Value.ToString(format);
            }

            return ret;
        }

        public static DateTime SafeValue(this Nullable<DateTime> dt , DateTime IfNull )
        {
            DateTime ret = default(DateTime);

            if (dt.HasValue) {
                ret = dt.Value;
            } else {
                ret = IfNull;
            }

            return ret;
        }

        public static long ToUnixFormat( this DateTime dt ) {

            long ret = DateHelper.ToUnixFormat( dt );
            return ret;
        }


    }
}
