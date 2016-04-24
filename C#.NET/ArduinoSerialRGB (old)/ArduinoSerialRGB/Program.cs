using System;
using System.IO.Ports;

namespace ArduinoSerialRGB
{
    class Program
    {
        static void Main(string[] args)
        {
            SerialPort port = new SerialPort("COM7", 9600);
            port.Open();
            while (true)
            {
                String s = Console.ReadLine();
                if (s.Equals("exit"))
                {
                    break;
                }
                port.Write(s);
            }
            port.Close();
        }
    }
}
