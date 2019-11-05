using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine;
using ConsoleEngine.UI;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Enums;
using ConsoleEngine.Textures;
using ConsoleEngine.GameSystems.Factorys;
using ConsoleEngine.Prefabs.Template.Struct;

namespace ConsoleEngine.Prefabs
{
    public class Player : Actor
    {
        Tilemap map;
        MessageUI msUI;
        Inventory inven;
        public Equipment equip;
        StatusWindow statWindow;
        public EquipmentWindow equipWindow;

        public void ReciveMap(Tilemap _map) => map = _map;

        public Player()
        {
            eyeSight = 10;
            Active = true;
            name = "Hero";
            tag = Tag.Player;
            updatePriority = 1;
            movingDistance = 1;
            killCount = 0;
            inven = new Inventory(30);
            equip = new Equipment();
            texture = ActorTextures.GetPlayerTexture();
            stat = ActorFactory.PlayerStatTemplate;
            stat.exp = 0;

            msUI = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
        }

        void KeyCheck(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.NumPad1:
                    Move(-1, 1);
                    break;
                case ConsoleKey.NumPad2:
                    Move(0, 1);
                    break;
                case ConsoleKey.NumPad3:
                    Move(1, 1);
                    break;
                case ConsoleKey.NumPad4:
                    Move(-1, 0);
                    break;
                case ConsoleKey.NumPad5:
                    Move(0, 0);
                    break;
                case ConsoleKey.NumPad6:
                    Move(1, 0);
                    break;
                case ConsoleKey.NumPad7:
                    Move(-1, -1);
                    break;
                case ConsoleKey.NumPad8:
                    Move(0, -1);
                    break;
                case ConsoleKey.NumPad9:
                    Move(1, -1);
                    break;
                case ConsoleKey.Tab:
                    inven.Open();
                    break;
            }
        }

        void Move(int _x, int _y)
        {
            var nextPos = new Vector(position.x + _x, position.y + _y);

            if (NextTileCheck(nextPos))
            {
                map.GetTile(position).isUpdate = true;
                SetPosition(nextPos);
            }
        }

        bool NextTileCheck(Vector pos)
        {
            var tile = map.GetTile(pos);

            if (tile is null)
                return false;

            if (!tile.isPass)
            {
                if (tile.isDistructible)
                    tile.Dig(1);

                return false;
            }
            else
            {
                if (position == pos)
                {
                    if (tile.type == TileType.DownStair)
                    {
                        GameManager.Instance.NextFloor();
                        return false;
                    }
                    var entity = GameManager.Instance.GetEntitys(Tag.Item);
                    foreach(var e in entity)
                    {
                        if(e is Item)
                        {
                            var item = e as Item;
                            if (!item.isBag && e.position == pos)
                            {
                                PickUpItem(e as Item);
                                return false;
                            }
                        }
                    }
                }
            }
            
            var targetEntity = GameManager.Instance.GetEntity(tile.position);
            if (targetEntity != null && targetEntity.tag == Tag.Enemy)
            {
                if (targetEntity.tag == Tag.Enemy)
                    Attack(targetEntity as Actor);

                return false;
            }
            return true;
        }

        void Attack(Actor Target)
        {
            GameManager.Instance.Battle(this, Target);
        }

        public void Sight()
        {
            map.tiles[position.x, position.y].isFog = false;
            int cx = 0;
            int cy = 0;
            for (int i = -eyeSight; i <= eyeSight; ++i)
            {
                for (int k = -eyeSight; k <= eyeSight; ++k)
                {
                    cx = i + position.x;
                    cy = k + position.y;
                    if (cx < 0 || cy < 0 || cx >= map.width || cy >= map.height)
                        continue;

                    int isLine = i * i + k * k;
                    int ra = eyeSight * eyeSight - eyeSight;
                    if (isLine <= ra)
                        map.tiles[cx, cy].isFog = false;
                }
            }
        }

        void StatusCheck()
        {
            if (stat.now == NowStatus.Dead)
                GameManager.Instance.ImDead(this);
        }

        void PickUpItem(Item item)
        {
            inven.Push(item);
        }

        public override void Awake()
        {
            statWindow = UIManager.Instance.GetInGameWindow(WindowType.StatusWindow) as StatusWindow;
            equipWindow = UIManager.Instance.GetInGameWindow(WindowType.EquipmentWindow) as EquipmentWindow;
            equipWindow.Rendering();
            statWindow.Message(stat);

            var a = ItemFactory.CreateEquipment(inven.Cells[inven.emptyBag], EquipmentType.Armor);
            var b = ItemFactory.CreateEquipment(inven.Cells[inven.emptyBag], EquipmentType.Weapon);
            var c = ItemFactory.CreateEquipment(inven.Cells[inven.emptyBag], EquipmentType.Ring);
            SceneManager.nowRunningScene.AddEntity(a);
            SceneManager.nowRunningScene.AddEntity(b);
            SceneManager.nowRunningScene.AddEntity(c);
            PickUpItem(a);
            PickUpItem(b);
            PickUpItem(c);
        }

        public override void Update()
        {
            if (InputManager.KeyBoardEvent.bKeyDown)
                KeyCheck(InputManager.inputConsoleKey);
            Sight();
            StatusCheck();
        }

        public override void Rendering()
        {
            if (stat.isChange)
                statWindow.Message(stat);
            Renderer.Render(position, texture);
            inven.Rendering();
        }
    }
}
