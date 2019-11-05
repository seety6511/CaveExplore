using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.GameSystems.Managers
{
    //싱글턴
    //UI전체 관리 클래스.
    //여기서 모든 UI를 받아온다.
    //특수 윈도우 외에는 각 하나의 Instance를 가진다.
    public class UIManager
    {
        Window[] InGameWindows;
        private static UIManager m_Instance;
        public static UIManager Instance
        {
            get
            {
                CreateInstance();
                return m_Instance;
            }
        }

        public static void CreateInstance()
        {
            if (m_Instance == null)
                m_Instance = new UIManager();
        }

        UIManager()
        {
            InGameWindows = new Window[(int)WindowType.Max];
        }

        public Window GetInGameWindow(WindowType type, Scene scene, Vector sp,int width, int height)
        {
            if (InGameWindows[(int)type] == null)
            {
                switch (type)
                {
                    case WindowType.OptionWindow:
                        InGameWindows[0] = new OptionWindow(scene, sp, width, height);
                        break;
                    case WindowType.StatusWindow:
                        InGameWindows[1] = new StatusWindow();
                        break;
                    case WindowType.MessageUI:
                        InGameWindows[2] = new MessageUI();
                        break;
                    case WindowType.GameOverWindowWin:
                        InGameWindows[3] = new GameOverWindow(scene, sp, width, height,0);
                        break;
                    case WindowType.GameOverWindowLose:
                        InGameWindows[4] = new GameOverWindow(scene, sp, width, height, 1);
                        break;
                    case WindowType.StartWindow:
                        InGameWindows[5] = new StartWindow(scene, sp, width, height);
                        break;
                    case WindowType.HelpWindow:
                        InGameWindows[6] = new StartWindow(scene, sp, width, height);
                        break;
                    case WindowType.EquipmentWindow:
                        InGameWindows[7] = new EquipmentWindow();
                        break;
                }
            }
            return InGameWindows[(int)type];
        }

        public Entity GetInGameWindow(WindowType type)
        {
            if (InGameWindows[(int)type] == null)
            {
                switch (type)
                {
                    case WindowType.OptionWindow:
                        InGameWindows[0] = new OptionWindow(SceneManager.nowRunningScene, new Vector(10,5), 19, 11);
                        break;
                    case WindowType.StatusWindow:
                        InGameWindows[1] = new StatusWindow();
                        break;
                    case WindowType.MessageUI:
                        InGameWindows[2] = new MessageUI();
                        break;
                    case WindowType.GameOverWindowWin:
                        InGameWindows[3] = new GameOverWindow(SceneManager.nowRunningScene, new Vector(5, 2), 40, 17,0);
                        break;
                    case WindowType.GameOverWindowLose:
                        InGameWindows[4] = new GameOverWindow(SceneManager.nowRunningScene, new Vector(5, 2), 40, 17,1);
                        break;
                    case WindowType.StartWindow:
                        InGameWindows[5] = new StartWindow(SceneManager.nowRunningScene, new Vector(10, 5), 10, 10);
                        break;
                    case WindowType.HelpWindow:
                        InGameWindows[6] = new HelpWindow(SceneManager.nowRunningScene, new Vector(10, 5), 10, 10, 1);
                        break;
                    case WindowType.EquipmentWindow:
                        InGameWindows[7] = new EquipmentWindow();
                        break;
                }
            }
            return InGameWindows[(int)type];
        }
    }
}
