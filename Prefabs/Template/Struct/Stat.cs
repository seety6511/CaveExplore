using ConsoleEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Prefabs.Template
{

    public struct Stat
    {
        public int hp;
        int maxHp;
        public int str;
        public int dex;
        public int wis;
        public int atk => str + str;
        public int def => dex / 2;
        public bool isChange;
        public NowStatus now;
        //지금레벨
        public int level;
        //다음 레벨을 위한 경험치
        public int nextLevel;
        //지금 경험치
        public int exp;
        int nextLevelExp => (level * level * 2);

        public Stat(int _hp, int _str, int _dex, int _wis, int _exp = 1, int _level = 1)
        {
            hp = _hp;
            maxHp = _hp;
            str = _str;
            dex = _dex;
            wis = _wis;
            exp = _exp;
            level = _level;

            isChange = true;
            now = NowStatus.Alive;
            nextLevel = level * level * 2;
        }

        public void StatusChange(StatType type, int value)
        {
            switch (type)
            {
                case StatType.HP:
                    hp = Math.Min(hp + value, maxHp);
                    break;
                case StatType.MAXHP:
                    maxHp += value;
                    break;
                case StatType.STR:
                    str += value;
                    break;
                case StatType.DEX:
                    dex += value;
                    break;
                case StatType.WIS:
                    wis += value;
                    break;
                case StatType.EXP:
                    exp += value;
                    if (exp >= nextLevel)
                    {
                        level++;
                        maxHp += 10;
                        hp = maxHp;
                        str += 2;
                        dex += 2;
                        wis += 2;
                        var t = exp - nextLevel;
                        nextLevel = nextLevelExp;

                        exp = t;
                    }
                    break;
                default:
                    break;
            }
            isChange = true;
        }

        public void StatusChange(Stat _stat)
        {
            hp = _stat.hp;
            str = _stat.str;
            dex = _stat.dex;
            wis = _stat.wis;
            isChange = true;
        }
    }
}
