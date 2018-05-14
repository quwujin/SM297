using ESLog.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebFramework.ESLog
{
    public class ESLogMethod
    {
        public static ILogger ESLogInstance;

        static ESLogMethod() {

            if (ESLogInstance == null)
            {
                ESLogInstance = new Logger();
            }

        } 


         
    }
}
