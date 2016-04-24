using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace RGBControllerCommunicator
{
    public class ArduinoRGBController : RGBComponentInterface
    {
        private SerialPort _SerialPort;

        public int cycles
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public float resolution
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void setColor(float red, float green, float blue)
        {
            throw new NotImplementedException();
        }

        public void setCycle(int number)
        {
            throw new NotImplementedException();
        }
    }
}
