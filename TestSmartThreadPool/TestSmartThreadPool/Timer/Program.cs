using System;
using System.Threading;

/*
 * Timers provide another way to run an asynchronous method on a regular, recurring basis.
 * When the timer expires, the system sets up the callback method on a thread from the thread pool, 
 * supplies the state object as its parameter, and starts it running. 
 * */

namespace Timers
{
   class Program
   {
      int TimesCalled = 0;
      
      void Display (object state)
      {
         Console.WriteLine("{0} {1}",(string)state, ++TimesCalled);
      }
 
      static void Main( )
      {
         Program p = new Program(); 
            
         System.Threading.Timer myTimer = new Timer
            (p.Display, "Processing timer event",  // callback and state
               2000,   // First callback at 2 seconds
               1000);  // Repeat every second
         Console.WriteLine("Timer started.");

         Console.WriteLine("Hit key to terminate...");
         Console.ReadLine(); 
      }
   }
}
