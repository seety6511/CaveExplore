using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Linq;

namespace ConsoleEngine.GameSystems.Factorys
{
    //Item 생성 전용 팩토리.
    public static class ItemFactory
    {
        public static Item CreatePotion(Vector pos, PotionType type)
        {
            return new Potion(type, pos);
        }

        public static Item CreateEquipment(Vector pos, EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.Armor:
                    return new Armor(pos);
                case EquipmentType.Weapon:
                    return new Weapon(pos);
                case EquipmentType.Ring:
                    return new Ring(pos);
                default:
                    return null;
            }
        }
    }
}
