using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace SimpleMultiThreadApp4
{
    class Program
    {
        public class Printer
        {
            public void PrintNumbers()
            {
                // Display Thread info.
                Console.WriteLine("-> {0} is executing PrintNumbers()",
                   Thread.CurrentThread.Name);

                // Print out numbers.
                Console.Write("Your numbers: ");
                for (int i = 0; i < 10; i++)
                {
                    Console.Write("{0}, ", i);
                    Thread.Sleep(2000); // secondary thread
                }
                Console.WriteLine();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***** Background Threads *****\n");
            Printer p = new Printer();
            Thread bgroundThread = new Thread(new ThreadStart(p.PrintNumbers));

            /*
             * Foreground threads have the ability to "prevent the current application from terminating". 
             * The CLR will not shut down an application (which is to say, unload the hosting AppDomain) 
             * until all foreground threads have ended. 
             * 
             * Background threads (sometimes called daemon threads) are viewed by the CLR as expendable paths of execution 
             * that "can be ignored at any point in time" (even if they are currently laboring over some unit of work). 
             * Thus, if all foreground threads have terminated, 
             * any and all background threads are automatically killed when the application domain unloads. 
             * */

            // This is now a background thread.
            bgroundThread.IsBackground = true;
            // By default, every thread you create via the Thread.Start() method is automatically a foreground thread.
           
            bgroundThread.Start();

            // Primary thread is foreground thread by default.
            Console.WriteLine("Main thread finished. Press any key to exit.");
            Console.ReadLine();
        }

    }
}
