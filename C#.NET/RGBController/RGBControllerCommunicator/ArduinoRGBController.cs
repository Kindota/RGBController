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
    class ArduinoRGBController
    {
        //example os string I can send "S0,255,0,255,0E"


        private SerialPort _serialPort;
        private Thread _communicationThread;
        private int _cycles;
        private int _numberOfStrips;
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

        public int NumberOfStrips
        {
            get
            {
                return _numberOfStrips;
            }
        }

        public ArduinoRGBController()
        {
            _serialPort = null;
            _cycles = 0;
            _numberOfStrips = 0;
            _resolution = 255;
            buffer = new BlockingCollection<string>();
            _communicationThread = new Thread(commWorker);
            if (connect())
            {
                _communicationThread.Start();
            }
        }

        public void setColor(int strip, int red, int green, int blue)
        {
            string message = String.Format("S{0},{1},{2},{3},0E", strip, red, green, blue);
            buffer.Add(message);
        }

        public void setCycle(int strip, int number)
        {
            string message = String.Format("S{0},0,0,0,{1}E", strip, number);
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

        private bool connect()
        {
            string[] arduinos = getArduinoPorts();
            if (arduinos.Length == 0)
            {
                Console.WriteLine("No Ardino's detected!");
                return false;
            }
            if (arduinos.Length > 1)
            {
                Console.WriteLine("Multiple Ardino's detected!");
                return false; // just for safety reasons
            }
            //init serial port
            _serialPort = new SerialPort(arduinos[0]);
            _serialPort.BaudRate = 9600;
            _serialPort.DtrEnable = true;
            try
            {
                _serialPort.Open();
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Serial Port already in use");
                return false;
            }
            string initMessage = _serialPort.ReadLine();
            string[] segments = initMessage.Split(' ');
            if (!(segments[0] == "RGBControllerVersion:" && segments[2] == "numberOfStrips:"))
            {
                Console.WriteLine("Initialization string wrong. Connection failed");
                _serialPort.Close();
                _serialPort = null;
                return false;
            }
            _numberOfStrips = Int32.Parse(segments[3]);
            return true;
        }

        /// <summary>
        /// main worker thread that starts the port and sedn all information
        /// </summary>
        private void commWorker()
        {

            while (true)
            {
                _serialPort.Write(buffer.Take());
            }
        }
    }
}
