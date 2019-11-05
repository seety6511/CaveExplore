using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine.GameSystems.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEngine.Algorithm
{
    public static class AStar
    {
        public static Tilemap map => GameManager.Instance.GetTilemap;
        public static List<Entity> list = new List<Entity>();
        public static List<Tile> tileList = new List<Tile>();

        //자기 자신의 주변타일중, 타겟의 좌표와 가장 가까운 타일 하나를 고름
        //이 타일이 통과 가능하다면 그대로 통과, 부술수 있다면 부수고, 
        //그렇지도 않으면 다음타일,
        //거리가 멀어진다면 그대로.
        public static Vector NextNode(Entity target, Entity chaser)
        {
            var myNear = map.GetTile(chaser.position).neighbors;
            var targetNear = map.GetTile(target.position).neighbors;

            myNear = myNear.OrderBy(t => Vector.Distance(t.position, target.position)).ToArray();

            var distance = Vector.Distance(chaser.position, target.position);
            foreach (var t in myNear)
            {
                if (t.isPass || t.isDistructible)
                    return t.position;
                if (distance < Vector.Distance(t.position, target.position))
                    break;
            }

            return chaser.position;
        }

    }
}
