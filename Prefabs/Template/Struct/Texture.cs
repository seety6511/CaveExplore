using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs.Template.Struct
{
    public struct Texture
    {
        char sprite;
        public char Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }
        ConsoleColor tc;
        public ConsoleColor TextColor
        {
            get { return tc; }
            set { tc = value; }
        }
        ConsoleColor bc;
        public ConsoleColor BackgroundColor
        {
            get { return bc; }
            set { bc = value; }
        }
        public Texture(char t, ConsoleColor _tc, ConsoleColor _bc)
        {
            sprite = t;
            tc = _tc;
            bc = _bc;
        }
    }
}
