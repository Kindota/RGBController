using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace RGBLightsControl
{
    class SerialCommunicator
    {
        private SerialPort arduino;

        public SerialCommunicator()
        {
            arduino = null;
        }

        public void ConnectTo(string portName)
        {
            if (arduino != null && arduino.IsOpen)
            {
                arduino.Close();
            }
            arduino = new SerialPort(portName, 9600);
            arduino.Open();
        }

        public void SendString(string msg)
        {
            if (arduino == null || !arduino.IsOpen) return;
            if (msg.EndsWith("\n"))
            {
                arduino.Write(msg);
            }
            else
            {
                arduino.Write(msg + "\n");
            }
        }

        public void SetColor(int red, int green, int blue)
        {
            if (arduino == null || !arduino.IsOpen) return;
            arduino.WriteLine($"{red} {green} {blue}");
        }

        public static string[] GetArduinoPort()
        {
            List<string> ports = new List<string>();
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);
            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    string desc = item["Description"].ToString();
                    string deviceId = item["DeviceID"].ToString();
                    if (desc.Contains("Arduino"))
                    {
                        ports.Add(deviceId);
                    }
                }
            }
            catch (Exception)
            {
                /* Do Nothing */
            }
            return ports.Count > 0 ? ports.ToArray() : null;
        }
    }
}
