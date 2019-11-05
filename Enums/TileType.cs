using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Enums
{
    public enum TileType
    {
        Empty = '.',
        Wall = '#',
        DamangedWall = ',',
        DestroyedWall = '*',
        UpStair = '<',
        DownStair = '>',
    }
}
