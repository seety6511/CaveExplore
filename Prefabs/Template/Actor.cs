using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine;
using ConsoleEngine.Prefabs.Template;
using ConsoleEngine.Enums;

namespace ConsoleEngine.Prefabs
{
    public abstract class Actor : Entity
    {
        public Stat stat;
        protected int movingDistance;
        protected int eyeSight;
        public int killCount;

        public Actor()
        {
            renderingPriority = 8;
            updatePriority = 2;
            movingDistance = 1;
            eyeSight = 3;
        }

        protected virtual void Action() { }
        public virtual void Interact(Entity From, ActionType act)
        {
            switch (act)
            {
                case ActionType.Attack:
                    break;
                case ActionType.Talk:
                    break;
                case ActionType.Touch:
                    break;
            }
        }
        protected virtual void Chase(Entity target) { }
    }
}
