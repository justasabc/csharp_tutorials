using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using Amib.Threading;
using Amib.Threading.Internal;

using System.Threading;

namespace TestSmartThreadPool
{
    class Program
    {
        private static AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            STPStartInfo startInfo = new STPStartInfo();
            startInfo.ThreadPoolName = "KZL Thread";
            startInfo.IdleTimeout = 2000;
            startInfo.MaxWorkerThreads = 15;
            startInfo.MinWorkerThreads = 2;

            SmartThreadPool stp = new SmartThreadPool(startInfo);
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Console.WriteLine("In Main thread...");

            IWorkItemResult iwir = stp.QueueWorkItem(new WorkItemCallback(Callback),
                               new object[] { numbers }
                               );

            // do some other work
            waitHandle.WaitOne(); 
            //System.Threading.Thread.Sleep(6000); // suspend main thread
           
            Console.WriteLine("Secondary thread finished.");
            double result = (double)iwir.Result;

            stp.Shutdown();

            Console.WriteLine("Result is {0}", result.ToString());
            Console.ReadLine();
        }

        public static object Callback(object state)
        {
            object[] parms = (object[])state;
            int[] numbers = (int[])parms[0];

            // do real work
            Console.WriteLine("In Secondary thread...");
            double value =  getAverage(numbers);
            waitHandle.Set(); // set true
            return value;
        }
        
        // Do the real work 
        private static double getAverage(int[] numbers)
        {
            double sum = 0.0;
            for (int i = 0; i < numbers.Length;i++ )
            {
                sum += numbers[i];
            }
            double average = sum / numbers.Length;
            return average;
        }

    }
}
