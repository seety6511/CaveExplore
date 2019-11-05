using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Textures;
using System;

namespace ConsoleEngine.Prefabs
{


    public class Potion : Item
    {
        PotionType pType;
        public Potion(PotionType _pType, Vector pos) 
            : base( ItemType.Potion, pos)
        {
            Active = true;
            pType = _pType;
            name = _pType.ToString();
            texture = ItemTextures.GetPotionTexture(pType);
            ms = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
        }

        public override void Destroy()
        {
            ms.Message(name + " is Destroy..");
        }

        public override void Drop()
        {
        }

        public override void Read()
        {
            switch (pType)
            {
                case PotionType.Healing:
                    ms.Message("선명한 붉은색의 액체가 든 유리병이다.");
                    ms.Message("마시면 상처가 나을 것 같다.");
                    break;
                case PotionType.Posion:
                    ms.Message("조금 위험해보이는 색의 액체가 들어있다.");
                    ms.Message("그리고 거품이 부글거린다.");
                    break;
            }
        }

        public override void Throw()
        {
        }

        public override void Use()
        {
            switch (pType)
            {
                case PotionType.Healing:
                    ms.Message("상처가 낫는 것 같다.");
                    GameManager.Instance.player.stat.StatusChange(StatType.HP, 10);
                    break;
                case PotionType.Posion:
                    ms.Message("목이 타들어 가는것 같다!");
                    GameManager.Instance.player.stat.StatusChange(StatType.HP, -10);
                    break;
            }
            Active = false;
            isBag = false;
        }

        public override void Equip()
        {
            IncorrectUse();
        }

        public override void IncorrectUse()
        {
            ms.Message("Wrong behavior!");
        }
    }
}
