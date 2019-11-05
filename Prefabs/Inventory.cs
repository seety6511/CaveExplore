using ConsoleEngine.Core;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleEngine.Prefabs
{
    public class Inventory
    {
        int sx;
        int sy;
        int size;
        int row;
        int column;
        char cursor;
        //현재 인벤토리를 조작중인가?
        bool isOpen;
        //장비가 업데이트 되었는가?
        bool equipUpdate;
        int cellIndex;
        //인벤토리에 새로운 내용이 올라왔는가?
        bool invenUpdate;

        Vector sp;
        Vector cellCursor;
        //아이템이 들어있는 배열
        public Item[] bag;

        //각 아이템이 인벤토리 안에서 가지는 좌표. 렌더링용.
        public Vector[] Cells
        {
            get
            {
                List<Vector> list = new List<Vector>();
                int a = 1;
                for (int i = 0; i < column; ++i)
                {
                    for (int j = 0; j < row; ++j)
                        list.Add(new Vector(sp.x + 3 + j * 5, sp.y + a));

                    a += 2;
                }
                return list.ToArray();
            }
        }

        //비어있는 가방을 찾음. 없으면 -1
        public int emptyBag
        {
            get
            {
                for (int i = 0; i < size; ++i)
                {
                    if (bag[i] == null)
                        return i;
                }
                return -1;
            }
        }

        //인벤토리 조작시 유효한 인덱스를 벗어나지 않도록 하는 프로퍼티
        private int CellIndex
        {
            get { return cellIndex; }
            set
            {
                int temp = cellIndex;
                cellIndex += value;
                if (cellIndex > size - 1 || cellIndex < 0)
                    cellIndex = temp;
            }
        }

        public Inventory(int _size)
        {
            sx = 50;
            sy = 16;
            row = 6;
            column = 5;
            size = _size;
            cursor = '-';
            invenUpdate = true;
            bag = new Item[size];
            sp = new Vector(sx, sy);
        }

        //겹치기 가능한가?
        //가능하다면 이미 가방에 같은 아이템(이름기준)이 존재하는가?
        //존재한다면 현재 들어온 아이템의 카운터를 더함
        //없다면, 빈 백에 넣음.
        //겹치기 불가능하다면, 빈 백에 넣음
        public void Push(Item item)
        {
            int index = emptyBag;
            if (index == -1)
            {
                var ms = UIManager.Instance.GetInGameWindow(Enums.WindowType.MessageUI) as MessageUI;
                ms.Message("가방이 가득찼다.");
                return;
            }
            bag[index] = item;
            item.position = Cells[index];
            item.Active = true;
            item.isBag = true;
            invenUpdate = true;
        }

        public void Open()
        {
            CursorRendering();
            CellIndex = 0;
            isOpen = true;
            while (isOpen)
            {
                InputManager.Input();
                KeyCheck();
                CursorRendering();
            }
            cellCursor = Cells[CellIndex];
            Renderer.Render(cellCursor, ' ');
            cellCursor.x -= 1;
            Renderer.Render(cellCursor, ' ');
        }

        void KeyCheck()
        {
            cellCursor = Cells[CellIndex];
            cellCursor.x -= 1;
            Renderer.Render(cellCursor, ' ');
            Item select = null;
            switch (InputManager.inputConsoleKey)
            {
                case ConsoleKey.LeftArrow:
                    CellIndex = -1;
                    break;
                case ConsoleKey.RightArrow:
                    CellIndex = 1;
                    break;
                case ConsoleKey.DownArrow:
                    CellIndex = 6;
                    break;
                case ConsoleKey.UpArrow:
                    CellIndex = -6;
                    break;
                case ConsoleKey.Enter:
                    select = bag[CellIndex];
                    if (select == null)
                        break;
                    if ((select.possibleInteract & Enums.InteractType.Equip) != 0)
                    {
                        select.Equip();
                        select.isEquiped = true;
                        select.isBag = true;
                        equipUpdate = true;
                    }
                    else if ((select.possibleInteract & Enums.InteractType.Use)!=0)
                        select.Use();
                    bag[CellIndex] = null;
                    isOpen = false;
                    invenUpdate = true;
                    break;
                case ConsoleKey.Spacebar:
                    select = bag[CellIndex];
                    if (select == null)
                        break;
                    select.Interact(Enums.InteractType.Read);
                    select = null;
                    break;
                case ConsoleKey.Tab:
                    isOpen = false;
                    break;
            }
        }
        void CursorRendering()
        {
            cellCursor = Cells[CellIndex];
            cellCursor.x -= 1;
            Renderer.Render(cellCursor, cursor);
        }

        public void Rendering()
        {
            if (invenUpdate)
            {
                Renderer.DrawTable(sp, row, column);
                invenUpdate = false;
            }
        }
    }
}
