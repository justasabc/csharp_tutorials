using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Windows.Forms;

namespace SimpleMultiThreadApp
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
            Console.WriteLine("***** The Amazing Thread App *****\n");
            Console.Write("Do you want [1] or [2] threads? ");
            string threadCount = Console.ReadLine();

            // Name the current thread.
            Thread primaryThread = Thread.CurrentThread;
            primaryThread.Name = "Primary";

            // Display Thread info.
            Console.WriteLine("-> {0} is executing Main()",
            Thread.CurrentThread.Name);

            // Make worker class.
            Printer p = new Printer();
            switch (threadCount)
            {
                case "2":

                    // Now make the thread.
                    Thread backgroundThread =
                    new Thread(new ThreadStart(p.PrintNumbers));
                    backgroundThread.Name = "Secondary";
                    backgroundThread.Start(); //inform the CLR this thread is ready for processing
                    break;
                case "1":
                    p.PrintNumbers();
                    break;
                default:
                    Console.WriteLine("I don't know what you want...you get 1 thread.");
                    goto case "1";
            }
            // Do some additional work.
            //Console.WriteLine("{0} will sleep for 6 m.", Thread.CurrentThread.Name);
            //Thread.Sleep(6000); // Message box will show when the 4th output.

            MessageBox.Show("I'm busy!", "Work on main thread...");
            Console.ReadLine();
        }

       

    }
}
