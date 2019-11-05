using ConsoleEngine.Core;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.UI
{
    public class HelpWindow : Window
    {
        public HelpWindow(Scene _sc, Vector _sp, int _width, int _heigth, int _state=0)
            :base(_sc, _sp, _width, _heigth, _state) { }

        public override void Rendering()
        {
            Renderer.DrawEmptyRect(sp, width, height);
        }
    }

}
