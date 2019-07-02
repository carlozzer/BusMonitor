using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Extensions
{
    public static class StringBuilderExtensions
    {
        #region FORMAT

        public static void AppendFormatLine(this StringBuilder sb, string formatted, params object[] args)
        {
            string line = formatted.ApplyFormat(args);
            sb.AppendLine(line);
        }

        // short alias Add

        public static void 
        Add ( this StringBuilder sb, string formatted, params object[] args ) {
            string line = formatted.ApplyFormat(args);
            sb.AppendLine(line);
        }

        #endregion
    }
}
