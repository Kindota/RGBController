using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGBControllerCommunicator
{
    public interface RGBComponentInterface
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
}
