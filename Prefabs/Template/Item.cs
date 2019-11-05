using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.GameSystems.Managers;

namespace ConsoleEngine.Prefabs.Template
{
    //아이템 추상화 클래스.
    public abstract class Item : Entity
    {
        protected ItemType type;
        //상호작용 가능한 종류
        public InteractType possibleInteract;
        GameManager gm;
        bool IsFog => gm.GetTilemap.tiles[position.x, position.y].isFog;
        //가방안에 들어있나?
        public bool isBag;
        public bool isEquiped;

        //떨어졌을때 보이는 스프라이트 
        public char dropSprite;
        //겹치기 가능한가?
        //현재 미사용
        public bool isOverrap
        {
            get
            {
                switch (type)
                {
                    case ItemType.Weapon:
                    case ItemType.Armor:
                    case ItemType.Ring:
                        return false;
                    default:
                        return true;
                }
            }
        }
        protected MessageUI ms;
        public Item(ItemType _type, Vector pos)
        {
            gm = GameManager.Instance;
            renderingPriority = 2;
            updatePriority = 3;
            tag = Tag.Item;
            isBag = false;
            isEquiped = false;
            position = pos;
            dropSprite = '*';
            type = _type;
            isPass = true;
            ms = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
            if (type != ItemType.Etc)
                possibleInteract = InteractType.Read | InteractType.Throw | InteractType.Drop | InteractType.Destroy;

            switch (type)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Ring:
                    possibleInteract |= InteractType.Equip;
                    break;
                case ItemType.Potion:
                case ItemType.Scroll:
                case ItemType.Food:
                    possibleInteract |= InteractType.Use;
                    break;
                default:
                    break;
            }
        }

        public virtual void Interact(InteractType type)
        {
            bool a = (type & possibleInteract) == type;
            if (!a)
                IncorrectUse();

            switch (type)
            {
                case InteractType.Use:
                    Use();
                    break;
                case InteractType.Throw:
                    Throw();
                    break;
                case InteractType.Read:
                    Read();
                    break;
                case InteractType.Equip:
                    Equip();
                    break;
                case InteractType.Drop:
                    Drop();
                    break;
                case InteractType.Destroy:
                    Destroy();
                    break;
            }
        }

        //잘못된 사용법
        public abstract void IncorrectUse();
        //소모(소모품전용, ex:포션, 스크롤등등..)
        public abstract void Use();
        //던지기(전부가능)
        public abstract void Throw();
        //정보(전부가능)
        public abstract void Read();
        //장착(장비전용)
        public abstract void Equip();
        //떨구기(전부가능)
        public abstract void Drop();
        //파괴(특수경우 제외하고 전부 가능)
        public abstract void Destroy();

        public override void Rendering()
        {
            if (isBag || isEquiped)
                Renderer.Render(position, texture);

            if (!isBag && !IsFog)
                Renderer.Render(position, dropSprite);
        }
    }
}
