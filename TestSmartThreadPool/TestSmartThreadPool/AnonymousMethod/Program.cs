using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonymousMethod
{
    /*
     * delegate 
     * 1) named method
     * 2) anonymous method
     * http://msdn.microsoft.com/en-us/library/0yw3tz5k.aspx
     * 3) lambda expressions
     *  http://msdn.microsoft.com/en-us/library/bb397687.aspx
     *  x => x*x   (x,y)=>x+y  ()=> Console.WriteLine("hello")
     */

    class DelegateDemo
    {
        delegate void GreetingDelegate(string s);

        static void Main(string[] args) 
        {
            // 1) named method
            GreetingDelegate gd1 = new GreetingDelegate(DelegateDemo.DoWork);
            gd1("named method");

            // 2) anonymous method
            GreetingDelegate gd2 = delegate(string s)
            {
                System.Console.WriteLine(s);
            };
            gd2("anonymous methods");

            // 3) lambda expressions
            GreetingDelegate gd3 = (s)=>
            {
                System.Console.WriteLine(s);
            };
            gd3("lambda expressions");
            Console.ReadLine();
      }

        // The method associated with the named delegate. 
        static void DoWork(string s)
        {
            System.Console.WriteLine(s);
        }

    }

}
