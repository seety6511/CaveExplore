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
    public class Armor : Item
    {
        public Armor(Vector pos) : base(ItemType.Armor, pos)
        {
            dropSprite = 'A';
            position = pos;
            texture = ItemTextures.GetEquipmentTexture(EquipmentType.Armor);
        }
        public override void Destroy()
        {
        }

        public override void Drop()
        {
        }

        public override void Equip()
        {
            ms.Message("갑옷을 입었다.");
            GameManager.Instance.player.stat.StatusChange(StatType.MAXHP, 10);
            GameManager.Instance.player.stat.StatusChange(StatType.STR, 2);
            GameManager.Instance.player.equip.EquipmentChange(EquipmentType.Armor, this);
            position = GameManager.Instance.player.equipWindow.cells[1];
        }

        public override void IncorrectUse()
        {
        }

        public override void Read()
        {
            ms.Message("튼튼해 보이는 가죽갑옷이다.");
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
