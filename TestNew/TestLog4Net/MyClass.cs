using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;
using System.Reflection; // MethodBase

namespace Com.Module
{
    public class MyClass
    {
        // logger name : Com.Module.MyClass
        //private static readonly ILog log = LogManager.GetLogger(typeof(MyClass));
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public void Process()
        {
            Type t1 = typeof(MyClass); // Com.Module.MyClass
            Type t2 = MethodBase.GetCurrentMethod().DeclaringType; // Com.Module.MyClass
            Console.WriteLine(t1);
            Console.WriteLine(t2);

            log.Info("Start");
            log.Debug("Processing...");
            log.Warn("Warning");
            log.Info("Finish");
        }
    }
}
