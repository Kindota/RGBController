using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBControllerCommunicator
{
    public interface IRGBComponent
    {
        int Cycles
        {
            get;
        }

        float Resolution
        {
            get;
        }

        void setColor(float red, float green, float blue);
        void setCycle(int number);
    }

    public static class RGBComponentProvider
    {
        //public delegate void updateRGBComponents(IRGBComponent[] components);

        public static IRGBComponent[] getRGBComponents()
        {
            ArduinoRGBController arduino = new ArduinoRGBController();
            IRGBComponent[] components = new IRGBComponent[arduino.NumberOfStrips];
            for (int i = 0; i < arduino.NumberOfStrips; i++)
            {
                components[i] = new ArduinoStripController(arduino, i);
            }
            return components;
        }
    }
}
