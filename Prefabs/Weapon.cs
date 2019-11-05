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
    public class Weapon : Item
    {
        public Weapon(Vector pos) : base(Enums.ItemType.Weapon, pos)
        {
            dropSprite = 'W';
            position = pos;
            texture = ItemTextures.GetEquipmentTexture(Enums.EquipmentType.Weapon);
        }
        public override void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Drop()
        {
        }

        public override void Equip()
        {
            ms.Message("검을 들었다.");
            GameManager.Instance.player.stat.StatusChange(StatType.STR, 10);
            GameManager.Instance.player.equip.EquipmentChange(EquipmentType.Weapon, this);
            position = GameManager.Instance.player.equipWindow.cells[0];
        }

        public override void IncorrectUse()
        {
        }

        public override void Read()
        {
            ms.Message("날카로운 검이다.");
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
