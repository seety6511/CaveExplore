using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template.Struct;
using System;

namespace ConsoleEngine.Textures
{
    public static class ItemTextures
    {
        public static Texture GetPotionTexture(PotionType type)
        {
            switch (type)
            {
                case PotionType.Healing:
                    return new Texture('*', ConsoleColor.DarkRed, ConsoleColor.Black);
                case PotionType.Posion:
                    return new Texture('*', ConsoleColor.DarkGreen, ConsoleColor.Black);
            }
            return new Texture('*', ConsoleColor.DarkGray, ConsoleColor.Black);
        }

        public static Texture GetEquipmentTexture(EquipmentType type)
        {
            switch (type)
            {
                case EquipmentType.Armor:
                    return new Texture('A', ConsoleColor.DarkRed, ConsoleColor.Black);
                case EquipmentType.Weapon:
                    return new Texture('W', ConsoleColor.DarkYellow, ConsoleColor.Black);
                case EquipmentType.Ring:
                    return new Texture('o', ConsoleColor.Cyan, ConsoleColor.Black);
                default:
                    break;
            }
            return new Texture('*', ConsoleColor.DarkGray, ConsoleColor.Black);
        }
    }
}
