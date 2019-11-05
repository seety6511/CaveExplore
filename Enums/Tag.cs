using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Enums
{
    public enum Tag
    {
        none = 0,
        Object = 1,
        Player = 2,
        Enemy = 3,
        Item = 4,
        //이동불가 엔티티. ex)벽타일.
        Enviro = 5,
    }
}
