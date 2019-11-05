using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.UI
{
    public class GameOverWindow : Window
    {
        string endingMessage;
        public GameOverWindow(Scene _sc, Vector _sp, int _width, int _height, int _state = 0)
            : base(_sc, _sp, _width, _height, _state)
        {
            switch (state)
            {
                case 0:
                    endingMessage = "Clear!";
                    break;
                case 1:
                    endingMessage = "You die..";
                    break;
            }

            var button = new Button(new Vector(sp.x + 13, sp.y + 5), 2, 3, "메인 메뉴로", SceneManager.LoadSceneName, ConsoleKey.D1);
            SceneManager.nowRunningScene.AddEntity(button);
            buttons.Add(button);
            var um = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
            um.Clear();
        }

        public override void Update()
        {
            if (InputManager.inputConsoleKey == ConsoleKey.D1)
                buttons[0].oce2("start");
        }

        public override void Rendering()
        {
            Renderer.DrawEmptyRect(sp, width, height);
            Renderer.Write(new Vector(sp.x + 13, sp.y + 3), endingMessage);
            foreach (var b in buttons)
                Renderer.DrawButton(b, '*');
        }
    }
}
