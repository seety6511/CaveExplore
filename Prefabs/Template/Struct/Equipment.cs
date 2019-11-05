using ConsoleEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs.Template.Struct
{
    public struct Equipment
    {
        Armor armor;
        Weapon weapon;
        Ring ring;

        public bool isUpdate;
        public Equipment(bool update = true)
        {
            isUpdate = true;
            armor = null;
            weapon = null;
            ring = null;
        }

        public void EquipmentChange(EquipmentType type, Item item)
        {
            switch (type)
            {
                case EquipmentType.Armor:
                    armor = item as Armor;
                    break;
                case EquipmentType.Weapon:
                    weapon = item as Weapon;
                    break;
                case EquipmentType.Ring:
                    ring = item as Ring;
                    break;
            }

            isUpdate = true;
        }
    }
}
