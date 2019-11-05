using ConsoleEngine.Core;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.UI
{
    //시작시 나오는 윈도우
    public class StartWindow : Window
    {
        public StartWindow(Scene scene, Vector _sp, int _width, int _height)
            :base(scene,_sp,_width,_height)
        {
            buttons.Add(new Button(new Vector(sp.x + 13, sp.y + 5), 2, 3, "1. 시작", SceneManager.LoadSceneName, ConsoleKey.D1));
            buttons.Add(new Button(new Vector(sp.x + 13, sp.y + 7), 2, 3, "2. 옵션", null));
            buttons.Add(new Button(new Vector(sp.x + 13, sp.y + 9), 2, 3, "3. 종료", ConsoleKey.D3, SceneManager.ApplicationQuit));

            foreach (var e in buttons)
                sc.AddEntity(e);
        }

        public override void Update()
        {
            if (InputManager.inputConsoleKey == ConsoleKey.D1)
                buttons.Where(b=>b.key==ConsoleKey.D1).First().oce2("game");
            if (InputManager.inputConsoleKey == ConsoleKey.D3)
                buttons.Where(b=>b.key==ConsoleKey.D3).First().oce();
        }

        public override void Rendering()
        {
            Renderer.DrawEmptyRect(sp, width, height);
            Renderer.Write(new Vector(sp.x + 13, sp.y + 3), "메인메뉴");

            foreach (var t in buttons)
                Renderer.DrawButton(t, '*');
        }
    }
}
