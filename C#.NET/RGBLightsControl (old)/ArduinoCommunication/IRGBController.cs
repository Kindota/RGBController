using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunication
{
    public interface IRGBController
    {
        void SetColor(int red, int green, int blue);
        void SetSequence(int sequence);
    }
}
