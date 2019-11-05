using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs;
using ConsoleEngine.Prefabs.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.GameSystems.Managers
{
    //배틀 순서
    //1. 공격자가 게임매니저에게 누구를 공격할 것인지 메시지를 보냄
    //1-1.게임 매니저는 이 정보가 지금 유효한지 판단하고, 유효하다면 배틀매니저의 StartBattle메소드를 호출
    //2. 배틀매니저는 호출받은 메소드로 넘어온 Actor에서 각 공격자와 방어자의 스탯을 읽어옴.
    //3. 스탯 계산함
    //4. 계산된 값을 Actor들에게 할당.
    //5. 각자에게 배틀 결과 전달.
    //6. 각자 자신의 상태를 점검
    //7. 점검 결과를 게임매니저에게 전달
    //8. 게임매니저는 전달받은 정보를 바탕으로 EntityList 수정.
    public class BattleManager
    {
        MessageUI msUI;
        public BattleManager()
        {
            msUI = UIManager.Instance.GetInGameWindow(WindowType.MessageUI) as MessageUI;
        }

        public void StartBattle(Actor attacker, Actor defender)
        {
            if (attacker == null || defender == null)
                return;

            int damage = defender.stat.def - attacker.stat.atk;
            defender.stat.StatusChange(StatType.HP, damage);
            msUI.Message(Util.Utils.StringCreate(attacker.name, " 이/가 ", defender.name," 를 공격, ", Math.Abs(damage).ToString(), " 피해를 입혔다."));
            BattleResult(attacker, defender);
        }

        void BattleResult(Actor attacker, Actor defender)
        {
            if (defender.stat.hp <= 0)
            {
                defender.stat.now = NowStatus.Dead;
                attacker.stat.StatusChange(StatType.EXP, defender.stat.exp);
                attacker.killCount++;
            }
        }
    }
}
