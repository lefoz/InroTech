using Inrotech.Domain.Components.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot r = new Robot();
            r.StartTest("192.168.117.40");
            Console.ReadKey();
        }
    }
}
