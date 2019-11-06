using ConsoleEngine.Algorithm;
using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Factorys;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.UI;
using ConsoleEngine.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEngine.GameSystems.Managers
{
    //싱글턴
    //게임 흐름을 제어하는 매니저
    //각 엔티티 생성 및 관리를 담당.
    //대부분 각 엔티티의 메소드를 호출해오는 방식으로 작동.
    public class GameManager : Entity
    {
        Random r;
        static Scene scene;
        Tilemap map;
        public Player player;
        ActorFactory AF;
        BattleManager bm;
        List<Actor> Enemies;
        List<Entity> entities;
        MapHandler mapHandler;
        private static GameManager mInstance;
        private GameManager() { }

        public Tilemap GetTilemap => map;
        public static GameManager Instance => mInstance;
        public void Battle(Actor From, Actor Target) => bm.StartBattle(From, Target);
        public Entity GetEntity(Tag tag) => entities.Where(e => e.tag == tag).First();
        public Entity[] GetEntitys(Tag tag) => entities.Where(e => e.tag == tag).ToArray();

        int wallPercent;
        int mapWidth;
        int mapHeight;
        EnemyType nextEnemy;

        public GameManager(Scene _scene, int width, int height)
        {
            Active = true;
            mInstance = this;
            updatePriority = -1;

            r = new Random();
            AF = new ActorFactory();
            bm = new BattleManager();
            Enemies = new List<Actor>();
            entities = new List<Entity>();

            scene = _scene;
            mapWidth = width;
            mapHeight = height;
            wallPercent = 40;
            nextEnemy = EnemyType.Goblin;

            CreateMap(width, height, wallPercent);
            CreatePlayer();
            CreateEnemy(5, nextEnemy);
            CreateItems();
            player.Sight();
        }

        public void SceneStatusChange(SceneStatus status)
        {
            switch (status)
            {
                case SceneStatus.Quit:
                    Clear();
                    break;
                case SceneStatus.Run:
                    break;
                case SceneStatus.Sleep:
                    break;
            }

            sceneStatus = status;
        }

        void CreatePlayer()
        {
            player = null;
            player = new Player();
            player.ReciveMap(map);
            PlayerPositioning();

            entities.Add(player);
            scene.AddEntity(player);
        }

        void CreateMap(int width, int height, int wallP)
        {
            mapHandler = new MapHandler(width, height, wallP);
            mapHandler.MakeCaverns();
            map = mapHandler.tileMap;
            map.PassSetting();
            map.FindNeighbors();
            map.SetDownStair();
            map.TileActiveAndDurablitySetting();
            scene.AddEntity(map);
        }
        
        public void NextFloor()
        {
            foreach (var e in Enemies)
                e.Active = false;

            map.Active = false;
            map.Clear();
            scene.RemoveEntity(map);
            map = null;
            mapHandler = null;
            EnemyClear();
            EntityClear();
            CreateMap(mapWidth, mapHeight, wallPercent);
            PlayerPositioning();
            CreateEnemy(5, nextEnemy);
            CreateItems();
            player.Sight();
            map.Rendering();
            foreach (var e in entities)
                e.Rendering();
        }

        void EntityClear()
        {
            var removeEntities = entities.Where(i => i.tag != Tag.Player).ToList();
            foreach (var e in removeEntities)
            {
                entities.Remove(e);
                scene.RemoveEntity(e);
            }
        }

        void PlayerPositioning()
        {
            player.ReciveMap(map);
            var emptys = Utils.ArrayShuffle(map.EmptyTiles);
            var pos = emptys[0].position;

            var tile = map.GetTile(pos);
            while (!tile.isPass && tile.neighbors.Where(t => t.type == TileType.Wall).Count() != 8)
                pos = emptys[r.Next(0, emptys.Length - 1)].position;

            player.SetPosition(pos);
        }

        void EnemyClear()
        {
            foreach(var e in Enemies)
            {
                scene.RemoveEntity(e);
                entities.Remove(e);
            }
            Enemies.Clear();
        }

        void CreateEnemy(int enemyCount, EnemyType type)
        {
            var emptys = Utils.ArrayShuffle(map.EmptyTiles);
            for (int i = 0; i < enemyCount; ++i)
            {
                if (emptys.Count() <= i)
                    return;

                var pos = emptys[i].position;

                while (GetEntity(pos) != null || !map.GetTile(pos).isPass)
                    pos = emptys[r.Next(0, emptys.Length - 1)].position;

                Enemies.Add(AF.CreateEnemy(pos, type));
            }

            foreach (var e in Enemies)
            {
                scene.AddEntity(e);
                entities.Add(e);
            }
        }

        void CreateItems()
        {
            var emptys = Utils.ArrayShuffle(map.EmptyTiles);
            Random r = new Random();

            for (int i = 0; i < 3; ++i)
            {
                if (emptys.Count() <= i)
                    return;

                var pos = emptys[i].position;

                while (GetEntity(pos) != null || !map.GetTile(pos).isPass)
                    pos = emptys[r.Next(0, emptys.Length - 1)].position;

                var item = ItemFactory.CreatePotion(pos, PotionType.Healing);
                item.Active = true;
                entities.Add(item);
                scene.addList.Add(item);
            }
        }

        public Entity GetEntity(Vector v)
        {
            var t = entities.Where(e => e.position == v).ToArray();

            if (t.Length == 0)
                return null;

            return t[0];
        }

        public void ImDead(Actor From)
        {
            if (From == player)
                GameOver(EndingType.Lose);

            var act = From as Enemy;
            Item loot = act.Loot();
            loot.Active = true;
            entities.Add(loot);
            scene.AddEntity(loot);

            From.Active = false;
            entities.Remove(From);
            scene.RemoveEntity(From);
            Renderer.Render(loot.position, loot.dropSprite);
        }

        //승리조건
        public override void Update()
        {
            if (player.killCount >= 5)
                nextEnemy = EnemyType.Orc;

            if (player.killCount / 5 == 1)
                GameOver(EndingType.Win);
        }

        void GameOver(EndingType type)
        {
            Clear();
            GameOverWindow window = null;
            SceneManager.LoadSceneName("gameover");
            var tempScene = SceneManager.nowRunningScene;

            switch (type)
            {
                case EndingType.Win:
                    window = 
                        UIManager.Instance.GetInGameWindow(WindowType.GameOverWindowWin) as GameOverWindow;
                    break;
                case EndingType.Lose:
                    window = 
                        UIManager.Instance.GetInGameWindow(WindowType.GameOverWindowLose) as GameOverWindow;
                    break; 
            }
            window.Open();
            tempScene.AddEntity(window);
        }

        void Clear()
        {
            Enemies.Clear();
            entities.Clear();
            scene.EntityList.Clear();
            scene.EntityList.Add(this);
            Active = false;
            CreateMap(mapWidth, mapHeight, wallPercent);
            CreatePlayer();
            CreateEnemy(5, nextEnemy);
            CreateItems();
            player.Sight();
        }
    }
}
