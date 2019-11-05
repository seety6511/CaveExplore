using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;

namespace ConsoleEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 32;
            Console.WindowWidth = 120;
            SceneManager.InitScene();
            SceneManager.Running();
        }
    }
}
