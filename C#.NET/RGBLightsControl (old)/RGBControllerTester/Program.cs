using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArduinoCommunication;


namespace RGBControllerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            IRGBController controller = new ArduinoUno();
            Console.WriteLine("Enter colors");
            while (true)
            {
                string[] numbers = Console.ReadLine().Split(' ');
                controller.SetColor(Int32.Parse(numbers[0]), Int32.Parse(numbers[1]), Int32.Parse(numbers[2]));
            }
            /*SerialPort a = new SerialPort();
            a.PortName = "COM4";
            a.Open();
            a.WriteLine("255, 255, 0\n");
            a.Close();
            Console.ReadKey();*/
        }
    }
}
