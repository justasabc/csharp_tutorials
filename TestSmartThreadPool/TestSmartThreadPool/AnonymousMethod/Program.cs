using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonymousMethod
{
    class DelegateDemo
    {
        delegate void GreetingDelegate(string s);

        static void Main(string[] args) 
        {

            // use an anonymous method for the delegate
            GreetingDelegate gd = delegate(string s)
            {
                Console.WriteLine(s);
            };
 
            // invoke the delegate
            gd("Hello");

            Console.ReadLine();
      }


    }

}
