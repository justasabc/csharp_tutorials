using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Remoting.Messaging; // AysncResult
using System.Threading; // sleep

/*
 * In the previous two patterns, wait-until-done and polling, 
 * the initial thread continues on with its flow of control only after it knows that the spawned thread has completed. 
 * It then retrieves the results and continues. 
 * The callback pattern is different in that once the initial thread spawns the asynchronous method, 
 * it goes on its way without synchronizing with it again. 
 * When the asynchronous method call completes, the system invokes a user-supplied method to handle its results, 
 * and to call the delegate's EndInvoke method. 
 * This user-defined method is called a callback method, or just callback. 
 * */

namespace PatternCallback
{
    class Program
    {
        delegate long MyDel(int first, int second);
        static long Sum(int x, int y)
        {
            Console.WriteLine(" Inside Sum");
            Thread.Sleep(50);
            return x + y;
        }

        static void CallWhenDone(IAsyncResult iar)
        {
            Console.WriteLine(" Inside CallWhenDone.");
            AsyncResult ar = (AsyncResult)iar;  // get AsyncResult
            MyDel del = (MyDel)ar.AsyncDelegate; // get AsyncDelegate
            string msg = (string)ar.AsyncState; // get AsyncState
            long result = del.EndInvoke(iar); // execute EndInvoke and get result
            Console.WriteLine(" The result is: {0}.", result);
        }

        static void Main()
        {
            MyDel del = new MyDel(Sum);
            Console.WriteLine("Before BeginInvoke");
            IAsyncResult iar = del.BeginInvoke(3, 5, new AsyncCallback(CallWhenDone), "Hello world!");
            // IAsyncResult iar = del.BeginInvoke(3, 5, CallWhenDone, null);

            Console.WriteLine("Doing more work in Main.");
            Thread.Sleep(500);
            Console.WriteLine("Done with Main. Exiting.");

            Console.ReadLine();
        }
    }

}

/*
Before BeginInvoke
Doing more work in Main.
Inside Sum
Inside CallWhenDone.
The result is: 8.
Done with Main. Exiting. 
 * */
