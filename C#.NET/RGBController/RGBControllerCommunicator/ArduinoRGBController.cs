using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Management;
using System.Collections.Concurrent;

namespace RGBControllerCommunicator
{
    public class ArduinoRGBController : RGBComponentInterface
    {
        //example os string I can send "S0,255,0,255,0E"


        private SerialPort _serialPort;
        private Thread _communicationThread;
        private int _cycles;
        private float _resolution;

        private BlockingCollection<string> buffer;

        public int Cycles
        {
            get
            {
                return _cycles;
            }
        }

        public float Resolution
        {
            get
            {
                return _resolution;
            }
        }

        public ArduinoRGBController()
        {
            _serialPort = null;
            _cycles = 0;
            _resolution = 255;
            buffer = new BlockingCollection<string>();
            _communicationThread = new Thread(commWorker);
            _communicationThread.Start();
        }

        public void setColor(float red, float green, float blue)
        {
            string message = String.Format("S0,{0},{1},{2},0E", red, green, blue);
            buffer.Add(message);
        }

        public void setCycle(int number)
        {
            string message = String.Format("S0,0,0,0,{0}E", number);
            buffer.Add(message);
        }

        private string[] getArduinoPorts()
        {
            IList<string> portNames = new List<String>();
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                string descriptor = item["Description"].ToString();
                string deviceId = item["DeviceID"].ToString();
                if (descriptor.Contains("Arduino"))
                {
                    Console.WriteLine("Arduino found on port: " + deviceId);
                    portNames.Add(deviceId);
                }
            }
            return portNames.ToArray();
        }
        /// <summary>
        /// main worker thread that starts the port and sedn all information
        /// </summary>
        private void commWorker()
        {
            string[] arduinos = getArduinoPorts();
            _serialPort = new SerialPort(arduinos[0], 9600);
            _serialPort.DtrEnable = true;
            Console.WriteLine("Opening");
            _serialPort.Open();
            string initializationMessage = _serialPort.ReadLine();
            Console.WriteLine(initializationMessage);
            string[] segments = initializationMessage.Split(' ');

            if (segments[0].Contains("RGBControllerVersion:") && segments[2].Contains("numberOfStrips:"))
            {
                Console.WriteLine("Found the RGB COntroller");
                Console.WriteLine(String.Format("Version: {0}", segments[3]));
            }

            while (true)
            {
                _serialPort.Write(buffer.Take());
            }
        }
    }
}
