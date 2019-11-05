using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleEngine.GameSystems.Factorys
{
    //actor에서 파생되는 모든 클래스 들을 여기서 생성한다.
    //ex) Monsters, NPCs, etc...
    public class ActorFactory
    {
        public static Stat PlayerStatTemplate
            => new Stat(100, 5, 5, 5);

        public Actor CreateEnemy(Vector pos, EnemyType type)
        {
            Stat stat;
            switch (type)
            {
                case EnemyType.Goblin:
                    stat = new Stat(10, 2, 2, 2);
                    return new Enemy(pos, type, "Goblin", stat);
                case EnemyType.Orc:
                    stat = new Stat(20, 4, 4, 4);
                    return new Enemy(pos, type, "Orc", stat);
                default:
                    return null;
            }
        }
    }
}
