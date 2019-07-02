using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusMonitor.BLL.Templating
{
    public class ConditionResult<T>
    {
        public bool condition;
        public T result;

        public ConditionResult( bool condition, T result ) {

            this.condition = condition;
            this.result    = result;

        }
    }
}
