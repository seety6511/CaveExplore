using ConsoleEngine.Algorithm;
using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Factorys;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Textures;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs
{
    public class Enemy : Actor
    {
        public Actor Target;
        EnemyType type;
        MessageUI msUI;
        GameManager gm;

        void TargetChange(Actor change) => Target = change;
        bool IsFog => gm.GetTilemap.tiles[position.x, position.y].isFog;

        public Enemy(Vector sp, EnemyType _type, string _name, Stat _stat)
        {
            name = _name;
            type = _type;
            Active = true;
            tag = Tag.Enemy;
            SetPosition(sp);
            isPass = false;
            gm = GameManager.Instance;
            stat = _stat;
            eyeSight = 6;
            texture = ActorTextures.GetMonsterTexture(type);
        }

        public override void Rendering()
        {
            if (!IsFog)
                Renderer.Render(position, texture);
        }


        public override void Update()
        {
            if (!StatusCheck())
                return;

            if (FOVCheck())
                Chase();
        }

        bool StatusCheck()
        {
            if (stat.now == NowStatus.Dead)
            {
                Dead();
                return false;
            }

            return true;
        }

        void Dead()
        {
            var ui = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
            ui.Message(name +" 이/가 죽었다.");
            GameManager.Instance.ImDead(this);
        }

        public Item Loot()
        {
            Random r = new Random();
            switch (type)
            {
                case EnemyType.Goblin:
                    return ItemFactory.CreatePotion(position, PotionType.Posion);
                case EnemyType.Orc:
                    return ItemFactory.CreateEquipment(position, (EquipmentType)r.Next(0, (int)EquipmentType.Max));
                default:
                    return null;
            }
        }

        //시야체크
        bool FOVCheck()
        {
            if (Target is null)
                Target = GameManager.Instance.GetEntity(Tag.Player) as Actor;

            var distance = Vector.Distance(position, Target.position);
            if (distance <= eyeSight)
                return true;

            return false;
        }

        Entity TargetIsNear
        {
            get
            {
                var thisTile = GameManager.Instance.GetTilemap.GetTile(position);

                for (int i = 0; i < 8; ++i)
                {
                    var e = GameManager.Instance.GetEntity(thisTile.neighbors[i].position);
                    if (e != null && e == Target)
                        return e;
                }
                return null;
            }
        }

        void Chase()
        {
            if (TargetIsNear == Target)
            {
                Attack();
                return;
            }

            var next = AStar.NextNode(Target, this);
            var e = GameManager.Instance.GetEntity(next);
            var t = GameManager.Instance.GetTilemap.GetTile(next);

            if (t.isPass && (e is null || e.isPass) )
            {
                GameManager.Instance.GetTilemap.GetTile(position).isUpdate = true;
                SetPosition(next);
            }
            if (t.isDistructible)
                t.Dig(1);
        }

        void Attack()
        {
            if (msUI == null)
                msUI = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;

            GameManager.Instance.Battle(this, Target);
        }
    }
}
