using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace Test_ThreadPool
{
    class Program
    {
        public class Printer
        {
            //must be private object lock token.
            private object threadLock = new object();

            public void PrintNumbers()
            {
                // Use the private object lock token.
                lock (threadLock)
                {// shared resource is Console
                    Console.WriteLine("->{0} is executing PrintNumbers.", Thread.CurrentThread.ManagedThreadId);
                    Console.WriteLine("Your numbers:");
                    for (int i = 0; i < 10; i++)
                    {
                        // Put thread to sleep for a random amount of time.
                        Random r = new Random();
                        Thread.Sleep(1000 * r.Next(5));
                        Console.Write("{0}, ", i);
                    }
                    Console.WriteLine();
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("***** Fun with the CLR Thread Pool *****\n");
            Console.WriteLine("Main thread started. ThreadID = {0}",
            Thread.CurrentThread.ManagedThreadId);
            Printer p = new Printer();
            WaitCallback workItem = new WaitCallback(PrintTheNumbers);

            // Queue the method ten times.
            for (int i = 0; i < 10; i++)
            {
                ThreadPool.QueueUserWorkItem(workItem, p);
            }
            Console.WriteLine("All tasks queued");
            Console.ReadLine();
        }

        static void PrintTheNumbers(object state)
        {
            Printer task = (Printer)state;
            task.PrintNumbers();
        }

    }
}
