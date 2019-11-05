using ConsoleEngine.Algorithm;
using ConsoleEngine.Entitys;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEngine.Core
{
    //Scene들 전체 관리하는 클래스.
    //여기서 Scene 등록, 전환, 삭제, 등이 일어남.
    public static class SceneManager
    {
        public static int sceneId;
        //현재 등록되어있는 SceneList
        public static List<Scene> SceneList;
        //현재 실행중인(루프중인) Scene
        public static Scene nowRunningScene;
        public static SystemStatus systemStatus = SystemStatus.Init;

        static SceneManager()
        {
            SceneList = new List<Scene>();
        }
        //최초 생성할 Scene, 엔티티들은 여기서 생성, 추가해야 한다.
        //시작할 최초의 씬이 무조건 있어야 한다.
        public static void InitScene()
        {
            var lt = new Vector(3, 2);
            var startScene = new Scene(sceneId++, "start");
            startScene.Active = true;
            StartWindow sw = UIManager.Instance.GetInGameWindow(WindowType.StartWindow, startScene, lt, 34, 15) as StartWindow;
            sw.Open();
            startScene.EntityList.Add(sw);

            var nextScene = new Scene(sceneId++, "game");
            nextScene.Active = false;
            var gm = new GameManager(nextScene, 40, 17);
            nextScene.EntityList.Add(gm);

            var gameOverScene = new Scene(sceneId++, "gameover");
            gameOverScene.Active = false;

            SceneList.Add(startScene);
            SceneList.Add(nextScene);
            SceneList.Add(gameOverScene);

            nowRunningScene = startScene;
        }

        //최초 이후 생성될 Scene들은 이 메소드를 통해 SceneList에 합쳐진다.
        public static void AddScene(Scene scene)
            => SceneList.Add(scene);

        public static void CloseScene(Scene scene)
            => scene.Active = false;

        public static void LoadScene(Scene scene)
        {
            systemStatus = SystemStatus.SceneChange;
            nowRunningScene.AllEntityDisable();
            nowRunningScene.Active = false;
            nowRunningScene = scene;
            Console.Clear();
        }
        public static void LoadStartScene()
        {
            systemStatus = SystemStatus.SceneChange;
            nowRunningScene.AllEntityDisable();
            nowRunningScene.Active = false;
            nowRunningScene = SceneList[0];
            Console.Clear();
        }

        public static void LoadSceneName(string name)
        {
            systemStatus = SystemStatus.SceneChange;
            nowRunningScene.AllEntityDisable();
            nowRunningScene.Active = false;
            foreach (var s in SceneList)
            {
                if (s.name == name)
                {
                    nowRunningScene = s;
                    break;
                }
            }
            Console.Clear();
        }

        public static void LoadNextScene()
        {
            systemStatus = SystemStatus.SceneChange;
            nowRunningScene.AllEntityDisable();
            nowRunningScene.Active = false;
            var index = nowRunningScene.sceneId + 1;
            nowRunningScene = SceneList[index];
            Console.Clear();
        }

        public static void RemoveScene(Scene scene)
        {
            SceneList.Remove(scene);
            GC.Collect();
        }

        public static void Running()
        {
            if (nowRunningScene == null)
            {
                Console.WriteLine("RunnigScene is NULL");
                return;
            }
            nowRunningScene.Active = true;
            nowRunningScene.Rendering();

            while (systemStatus != SystemStatus.Exit)
            {
                nowRunningScene.Awake();
                nowRunningScene.Start();
                InputManager.Input();
                InputCheck();
                nowRunningScene.Update();
                if (systemStatus == SystemStatus.SceneChange)
                {
                    InputManager.inputConsoleKey = 0;
                    nowRunningScene.Active = true;
                    nowRunningScene.AllEntityActive();
                    nowRunningScene.Awake();
                    nowRunningScene.Start();
                    nowRunningScene.Update();
                    systemStatus = SystemStatus.Run;
                }
                nowRunningScene.Rendering();
            }
        }
        public static void ApplicationQuit()
        {
            systemStatus = SystemStatus.Exit;
        }

        static void InputCheck()
        {
            if (InputManager.inputConsoleKey == ConsoleKey.Escape)
            {
                var option = UIManager.Instance.GetInGameWindow(WindowType.OptionWindow) as OptionWindow;
                option.Open();
                nowRunningScene.AddEntity(option);
            }
        }
    }
}
