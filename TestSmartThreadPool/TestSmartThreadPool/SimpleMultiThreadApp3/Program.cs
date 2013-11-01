using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;


namespace SimpleMultiThreadApp3
{
    class Program
    {
        class AddParams
        {
            public int a, b;

            public AddParams(int numb1, int numb2)
            {
                a = numb1;
                b = numb2;
            }
        }

        private static AutoResetEvent waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            Console.WriteLine("***** Adding with Thread objects *****");
            Console.WriteLine("ID of thread in Main(): {0}",
               Thread.CurrentThread.ManagedThreadId);

            AddParams ap = new AddParams(10, 10);
            Thread t = new Thread(new ParameterizedThreadStart(Add));
            t.Start(ap);

            //inform the primary thread to wait until the secondary thread has completed

            // Wait here until you are notified!
            Console.WriteLine("Main thread begin to wait.");
            waitHandle.WaitOne(); // wait until waitHandle set true
            // after waitone is called, AutoResetEvent be set false again.
            Console.WriteLine("Other thread is done!");
            Console.ReadLine();
        }

        static void Add(object data)
        {
            if (data is AddParams)
            {
                Console.WriteLine("ID of thread in Add(): {0}",
                   Thread.CurrentThread.ManagedThreadId);

                AddParams ap = (AddParams)data; // get params
                Console.WriteLine("{0} + {1} is {2}",
                   ap.a, ap.b, ap.a + ap.b);

                // Tell other thread we are done.
                waitHandle.Set(); //[true] 
            }
        }
    }

}
