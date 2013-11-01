using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;

namespace Concurrency2
{
    class Program
    {
        public class Printer
        {
            //must be private object lock token.
            private object threadLock = new object();

            /*
             * you have effectively designed a method that will allow the current thread to complete its task. 
             * Once a thread enters into a lock scope, the lock token (in this case, a reference to the current object)
             * is inaccessible by other threads until the lock is released once the lock scope has exited. 
             * Thus, if thread A has obtained the lock token, other threads are unable to enter any scope 
             * that uses the same lock token until thread A relinquishes the lock token. 

             * Note If you are attempting to lock down code in a static method, 
             * simply declare a [private static] object member variable to serve as the lock token. 
             * */

            public void PrintNumbers()
            {
                // Use the private object lock token.
                lock (threadLock)
                {// shared resource is Console
                    Console.WriteLine("->{0} is executing PrintNumbers.", Thread.CurrentThread.Name);
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
            Console.WriteLine("*****Synchronizing Threads *****\n");
            Printer p = new Printer();

            // Make 10 threads that are all pointing to the same
            // method on the same object.
            Thread[] threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(new ThreadStart(p.PrintNumbers));
                threads[i].Name = string.Format("Worker thread #{0}", i);
            }

            // Now start each one.
            foreach (Thread t in threads)
                t.Start();
            Console.ReadLine();
        }
    }


}
