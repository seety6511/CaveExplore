using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Textures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs
{
    public class Ring : Item
    {
        public Ring(Vector pos) : base(ItemType.Ring, pos)
        {
            dropSprite = 'o';
            position = pos;
            texture = ItemTextures.GetEquipmentTexture(EquipmentType.Ring);
        }

        public override void Destroy()
        {
        }

        public override void Drop()
        {
        }

        public override void Equip()
        {
            ms.Message("반지를 착용했다.");
            GameManager.Instance.player.stat.StatusChange(StatType.WIS, 5);
            GameManager.Instance.player.equip.EquipmentChange(EquipmentType.Ring, this);
            position = GameManager.Instance.player.equipWindow.cells[2];
        }

        public override void IncorrectUse()
        {
        }

        public override void Read()
        {
            ms.Message("밋밋한 은색 반지이다.");
        }

        public override void Throw()
        {
        }

        public override void Use()
        {
            Read();
        }
    }
}
