using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBControllerCommunicator
{
    interface RGBComponentInterface
    {
        int cycles
        {
            get;
        }

        float resolution
        {
            get;
        }

        void setColor(float red, float green, float blue);
        void setCycle(int number);
    }
}
