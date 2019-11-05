using ConsoleEngine.Core;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;

namespace ConsoleEngine.UI
{
    public class OptionWindow : Window
    {
        Vector settingButtonPosition;

        public OptionWindow(Scene _sc, Vector _sp, int _width, int _height)
            :base (_sc, _sp, _width,_height)
        {
            updatePriority = 0;
            name = "OptionWindow";
            settingButtonPosition = new Vector(sp.x+5, sp.y + 2);

            var help = UIManager.Instance.GetInGameWindow(Enums.WindowType.HelpWindow) as HelpWindow;
            Button button = new Button(
                new Vector(sp.x + 3, sp.y + 4),
                2, 3,
                "1. 도움말",
                ConsoleKey.D1,
                help.Open);

            SceneManager.nowRunningScene.AddEntity(help);
            Button button1 = new Button(
                new Vector(sp.x + 3, sp.y + 6),
                2, 3,
                "2. 돌아가기",
                ConsoleKey.D2,
                Close);

            Button button2 = new Button(
                new Vector(sp.x + 3, sp.y + 8),
                2, 3,
                "3. 게임 종료",
                ConsoleKey.D3,
                SceneManager.ApplicationQuit);

            SceneManager.nowRunningScene.AddEntity(button);
            SceneManager.nowRunningScene.AddEntity(button1);
            SceneManager.nowRunningScene.AddEntity(button2);

            buttons.Add(button);
            buttons.Add(button1);
            buttons.Add(button2);

            Rendering();
        }

        public override void Update()
        {
            Rendering();
            switch (InputManager.inputConsoleKey)
            {
                case ConsoleKey.D1:
                    //buttons[0].oce();
                    Close();
                    return;
                case ConsoleKey.D2:
                    buttons[1].oce();
                    Close();
                    return;
                case ConsoleKey.D3:
                    buttons[2].oce();
                    return;
            }
        }

        public override void Close()
        {
            base.Close();
            Console.Clear();
            GameManager.Instance.GetTilemap.UpdateALL();
        }

        public override void Rendering()
        {
            Renderer.DrawRect(sp, width, height);
            Renderer.DrawEmptyRect(sp, width, height);
            Renderer.Write(settingButtonPosition, "Settings");
            foreach (var t in buttons)
                Renderer.DrawButton(t , '*');
        }
    }
}
