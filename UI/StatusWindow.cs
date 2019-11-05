using ConsoleEngine.Core;
using ConsoleEngine.GameSystems.Managers;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.UI
{
    //플레이어 전용 윈도우
    public class StatusWindow : Window
    {
        int sx;
        int Line;
        Stat stat;
        Vector linePos;
        public StatusWindow()
        {
            sx = 50;
            Line = 25;
            Active = true;
            linePos = new Vector(sx, 0);
        }

        public void Message(Stat _stat)
        {
            stat = _stat;
            Rendering();
        }

        public override void Rendering()
        {
            Clear();
            Renderer.DrawXLine(linePos, Line, '=');
            linePos.y++;
            Renderer.WriteLine(linePos, "Status : " + stat.now.ToString());
            linePos.y++;
            Renderer.WriteLine(linePos, "Lv : " + stat.level);
            linePos.y++;
            Renderer.WriteLine(linePos, "Hp : " + stat.hp);
            
            linePos.y++;
            Renderer.WriteLine(linePos, "Str : " + stat.str);

            linePos.x += 15;
            Renderer.WriteLine(linePos, "ATK : " + stat.atk);
            linePos.x -= 15;

            linePos.y++;
            Renderer.WriteLine(linePos, "Dex : " + stat.dex);

            linePos.x += 15;
            Renderer.WriteLine(linePos, "DEF : " + stat.def);
            linePos.x -= 15;

            linePos.y++;
            Renderer.WriteLine(linePos, "Wis : " + stat.wis);
            linePos.y++;
            Renderer.WriteLine(linePos, "NextLv : " + stat.exp + "/" + stat.nextLevel);
            linePos.y++;
            Renderer.WriteLine(linePos, "KillCount : " + GameManager.Instance.player.killCount);
            linePos.y++;
            Renderer.DrawXLine(linePos, Line, '=');

            linePos.y = 0;
            stat.isChange = false;
        }

        void Clear()
        {
            linePos.y++;
            Renderer.DrawXLine(linePos, Line, ' ');
            linePos.y++;
            Renderer.DrawXLine(linePos, Line, ' ');
            linePos.y++;
            Renderer.DrawXLine(linePos, Line, ' ');
            linePos.y++;
            Renderer.DrawXLine(linePos, Line, ' ');
            linePos.y = 0;
        }
    }
}
