using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Enums
{
    [Flags]
    public enum InteractType
    {
        Read = 0,
        Throw = 1 <<0,
        Drop = 2 <<1,
        Use = 3 <<2,
        Equip = 4 <<3,
        Destroy = 5<<4
    }
}
