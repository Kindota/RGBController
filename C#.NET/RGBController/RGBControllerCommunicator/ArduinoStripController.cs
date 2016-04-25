using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBControllerCommunicator
{
    class ArduinoStripController : IRGBComponent
    {
        private ArduinoRGBController arduino;
        private int stripIndex;

        public ArduinoStripController(ArduinoRGBController arduino, int stripIndex)
        {
            this.arduino = arduino;
            this.stripIndex = stripIndex;
        }

        public int Cycles
        {
            get
            {
                return arduino.Cycles;
            }
        }

        public float Resolution
        {
            get
            {
                return arduino.Resolution;
            }
        }

        public void setColor(float red, float green, float blue)
        {
            arduino.setColor(stripIndex, Convert.ToInt32(red), Convert.ToInt32(green), Convert.ToInt32(blue));
        }

        public void setCycle(int number)
        {
            arduino.setCycle(stripIndex, number);
        }
    }
}
