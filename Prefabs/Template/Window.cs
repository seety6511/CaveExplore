using ConsoleEngine.Core;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs.Template
{
    public class Window : Entity
    {
        //startPoint
        protected Scene sc;
        protected Vector sp;

        protected int state;
        protected int width;
        protected int height;
        protected List<Button> buttons;

        public Window() { }

        public Window(Scene _sc, Vector _sp, int _width, int _height, int _state = 0)
        {
            sc = _sc;
            sp = _sp;
            Active = false;
            state = _state;
            width = _width;
            height = _height;
            buttons = new List<Button>();
        }

        public void Open()
        {
            Active = true;
            foreach (var e in buttons)
                e.Active = true;
        }

        public virtual void Close()
        {
            Active = false;
            foreach (var e in buttons)
                e.Active = false;
        }
    }
}
