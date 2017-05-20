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
            //test array
            string[] arr = new string[5];
            Random rnd = new Random();
            arr[0] = rnd.Next(1, 500).ToString();
            arr[1] = rnd.Next(1, 500).ToString();
            arr[2] = rnd.Next(1, 500).ToString();
            arr[3] = rnd.Next(1, 500).ToString();
            arr[4] = rnd.Next(1, 500).ToString();


            Robot r = new Robot(arr);
            r.StartTest("192.168.117.40");           

            int i = 0;
            while (true)
            {                
                i++;
                Console.WriteLine("Refresh " + i + " prompted");
                r.refreshPrompt();
                Console.ReadKey();
            }


        }
    }
}
