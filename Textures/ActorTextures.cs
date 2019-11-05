using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs.Template.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Textures
{
    public static class ActorTextures
    {
        public static Texture GetMonsterTexture(EnemyType type)
        {
            switch (type)
            {
                case EnemyType.Goblin:
                    return new Texture('g', ConsoleColor.DarkGreen, ConsoleColor.Black);
                case EnemyType.Orc:
                    return new Texture('O', ConsoleColor.DarkGreen, ConsoleColor.Black);
            }
            return new Texture('M', ConsoleColor.Gray, ConsoleColor.Black);
        }

        public static Texture GetPlayerTexture()
        {
            return new Texture('@', ConsoleColor.Gray, ConsoleColor.Black);
        }
    }
}
