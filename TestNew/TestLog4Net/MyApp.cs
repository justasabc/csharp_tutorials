using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using log4net.Config;

using Com.Module; // MyClass
using System.Reflection;  // MethodBase

// http://logging.apache.org/log4net/release/manual/configuration.html

namespace Test
{
    class MyApp
    {
        // typeof(MyApp)--->Test.MyApp
        //private static readonly ILog log = LogManager.GetLogger(typeof(MyApp));

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            Type t1 = typeof(MyApp); // Test.MyApp
            Type t2 = MethodBase.GetCurrentMethod().DeclaringType; // Test.MyApp
            Console.WriteLine(t1);
            Console.WriteLine(t2);

            // Set up a simple configuration that logs on the console.
            //BasicConfigurator.Configure();

            // BasicConfigurator replaced with XmlConfigurator.
            // "Test.exe.config";
            //XmlConfigurator.Configure(new System.IO.FileInfo(args[0]));

            // or use default XXX.exe.config
            XmlConfigurator.Configure(); 

            log.Info("Entering application.");
            MyClass myclass = new MyClass();
            myclass.Process();
            log.Info("Exiting application.");

            Console.ReadLine();
        }
    }
}
