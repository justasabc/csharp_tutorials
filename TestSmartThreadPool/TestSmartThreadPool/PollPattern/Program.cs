using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading; // For Thread.Sleep()

/*
 *In the polling pattern, the initial thread initiates an asynchronous method call, 
 *does some additional processing, 
 *and then uses the IsCompleted method of the IAsyncResult object to check periodically 
 *whether the spawned thread has completed. 
 *If the asynchronous method has completed, the initial thread calls EndInvoke and continues on. 
 *Otherwise, it does some additional processing and checks again later. 
 *The "processing" in this case just consists of counting from 0 to 10,000,000. 
 **/


namespace PatternPolling
{
    class Program
    {
        delegate long MyDel(int first, int second);

        static long Sum(int x, int y)
        {
            Console.WriteLine(" Inside Sum");
            Console.WriteLine("Current thread: {0}", Thread.CurrentThread.ManagedThreadId);

            Thread.Sleep(100);
            return x + y;
        }

        static void Main()
        {
            MyDel del = new MyDel(Sum);

            Console.WriteLine("Main thread: {0}", Thread.CurrentThread.ManagedThreadId);

            IAsyncResult iar =                    // Start async.
               del.BeginInvoke(3, 5, null, null); // Spawn async method.
            Console.WriteLine("After BeginInvoke");
           
           

            /*
             * You can use the "IsCompleted" method if you are polling for example,
             * you could check if the method has finished, and if so, you could invoke EndInvoke.
             * 
             * You can also use the WaitHandle that is returned by the AsyncWaitHandle to monitor async method. 
             * You can use the WaitOne method, WaitAll or WaitAny to get finer control, 
             * they are all essentially the same operation, but it applies to the three potential conditions: 
             * single wait, waiting for all the handles to complete, 
             * or wait for any handles to complete (WaitHandles are not limited to be used with async methods, 
             * you can obtain those from other parts of the framework,
             * like network communications, or from your own thread synchronization operations).
             * */

             // while (!iar.AsyncWaitHandle.WaitOne(1000, true))
            //  while (!iar.IsCompleted)
 
            while (!iar.IsCompleted)            // Check whether async method is done.
            {
                Console.WriteLine("Not Done");
                // Continue processing, even though in this case it's just busywork.
                for (long i = 0; i < 10000000; i++)
                    ;
            }

            Console.WriteLine("Done");
            long result = del.EndInvoke(iar);   // Call EndInvoke to get result and clean up.
            Console.WriteLine("Result: {0}", result);

            Console.ReadLine();
        }
    }

}

/*
After BeginInvoke
Not Done
Inside Sum
Not Done
Not Done
Done
Result: 8 
*/
