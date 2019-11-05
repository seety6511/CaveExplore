using ConsoleEngine.Core;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Prefabs.Template.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.UI
{
    public class EquipmentWindow : Window
    {
        int x, y;

        Equipment equip;
        public Vector[] cells;
        public EquipmentWindow()
        {
            Active = true;
            width = 25;
            height = 10;
            x = 51;
            y = 10;
            sp = new Vector(x, y);
            cells = new Vector[3];
            cells[0] = new Vector(sp.x + 4, sp.y + 3);
            cells[1] = new Vector(sp.x + 13, sp.y + 3);
            cells[2] = new Vector(sp.x + 20, sp.y + 3);
        }

        public override void Rendering()
        {
            Renderer.DrawXLine(sp, 25);
            sp.y += 1;
            sp.x += 2;
            Renderer.Write(sp, "Weapon");
            sp.x += 9;
            Renderer.Write(sp, "Armor");
            sp.x += 8;
            Renderer.Write(sp, "Ring");
            sp.x -= 1;
            sp.y += 4;
            sp.x = x;
            Renderer.DrawXLine(sp, 25);
            sp.y =y;

            Renderer.Render(cells[0], 'd');
            Renderer.Render(cells[1], 'd');
            Renderer.Render(cells[2], 'd');
        }

        public void Message(Equipment e)
        {
            equip = e;
        }
    }
}
