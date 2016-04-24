using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ArduinoCommunication
{
    public class ArduinoUno : IRGBController
    {
        private SerialPort port;
        private object myLock;
        private BlockingCollection<string> buffer;
        private Thread consumerThread;
        
        public ArduinoUno()
        {
            this.port = new SerialPort();
            this.myLock = new object();
            this.buffer = new BlockingCollection<string>(new ConcurrentQueue<string>());
            consumerThread = null;
            Start();
        }

        public void Start()
        {
            if (consumerThread != null && consumerThread.IsAlive)
            {
                consumerThread.Abort();
            }
            consumerThread = new Thread(new ThreadStart(Run));
            consumerThread.Start();
        }

        private void Connect()
        {
            string portName = getPortName();
            if (port.IsOpen)
            {
                port.Close();
            }
            port.PortName = portName;
            port.BaudRate = 9600;
            port.Open();
        }

        private string getPortName()
        {
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                try
                {
                    string description = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();
                    //Console.WriteLine("   " + deviceId);
                    //Console.WriteLine("   " + description);
                    if (description.Contains("Arduino"))
                    {
                        return deviceId;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception: " + e.ToString());
                }
               
            }
            return null;
        }

        public void Run()
        {
            try
            {
                Connect();
                while (true)
                {
                    port.Write(buffer.Take());
                }
            }
            catch (ThreadAbortException)
            {
                
                throw;
            }
        }

        public void SetColor(int red, int green, int blue)
        {
            buffer.Add($"{red}, {green}, {blue}\n");
        }

        public void SetSequence(int sequence)
        {
            buffer.Add($"{sequence}, {sequence}, {sequence}\n");
        }
    }
}
