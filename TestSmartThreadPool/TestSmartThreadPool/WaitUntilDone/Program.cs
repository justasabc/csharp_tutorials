using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading; // For Thread.Sleep()

/*
 * The first one we’ll look at is the wait-until-done pattern. 
 * In this pattern, the initial thread initiates an asynchronous method call, 
 * does some additional processing, 
 * and then stops and waits until the spawned thread finishes.
 * */

namespace PatternWaitUntilDone
{// wait until done; polling ; callback
    class Program
    {
        delegate long MyDel(int first, int second); // Declare delegate type

        static long Sum(int x, int y) // Declare method for async
        {
            Console.WriteLine("             Inside Sum");
            Console.WriteLine("Current thread: {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(100);
            return x + y;
        }

        static void Main()
        {
            //AsyncResult represents the state of the asynchronous method
            //IAsyncResult  IsCompleted  AsyncState 
            //AsyncResult   AsyncDelegate

            MyDel del = new MyDel(Sum);

            Console.WriteLine("Main thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("Before BeginInvoke");
            IAsyncResult iar = del.BeginInvoke(3, 5, null, null); // Start async
            Console.WriteLine("After BeginInvoke");
            Console.WriteLine("Doing stuff");
            long result = del.EndInvoke(iar); // Wait for end and get result
            Console.WriteLine("After EndInvoke: {0}", result);

            Console.ReadLine();
        }
    }

}

/*
Before BeginInvoke
After BeginInvoke
Doing stuff
Inside Sum 
After EndInvoke: 8
*/
