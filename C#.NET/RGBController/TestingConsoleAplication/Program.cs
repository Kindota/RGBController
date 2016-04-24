using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGBControllerCommunicator;
using System.Management;

namespace TestingConsoleAplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello");

            RGBComponentInterface controller = new ArduinoRGBController();

            while (true)
            {
                Console.WriteLine("Press button to turn on");
                Console.ReadKey();
                controller.setColor(255, 255, 255);
                Console.WriteLine("Press button to turn off");
                Console.ReadKey();
                controller.setColor(0, 0, 0);
            }
        }
    }
}
