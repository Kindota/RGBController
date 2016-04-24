using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Management;

namespace RGBControllerCommunicator
{
    public class ArduinoRGBController : RGBComponentInterface
    {
        //example os string I can send "S0,255,0,255,0E"


        private SerialPort _serialPort;
        private Thread _communicationThread;
        private int _cycles;
        private float _resolution;

        private int _red;
        private int _green;
        private int _blue;
        private int _mode;

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

        }

        public void setColor(float red, float green, float blue)
        {
            throw new NotImplementedException();
        }

        public void setCycle(int number)
        {
            throw new NotImplementedException();
        }

        private string[] getArduinoPorts()
        {
            IList<string> portNames = new List<String>();
            ManagementScope connectionScope = new ManagementScope();
            SelectQuery serialQuery = new SelectQuery("SELECT * FROM Win32_SerialPort");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(connectionScope, serialQuery);

            foreach (ManagementObject item in searcher.Get())
            {
                string descriptor = item["Descriptor"].ToString();
                string deviceId = item["DeviceID"].ToString();
                if (descriptor.Contains("Arduino"))
                {
                    portNames.Add(deviceId);
                }
            }
            return portNames.ToArray();
        }
    }
}
