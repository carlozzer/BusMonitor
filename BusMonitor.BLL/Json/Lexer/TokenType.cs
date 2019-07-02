using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Json
{
    public enum TokenType
    {
        Object,
        Colon,
        Comma,
        String,
        Number,
        Array,
        Keyword,
        Unknown
    }
}
