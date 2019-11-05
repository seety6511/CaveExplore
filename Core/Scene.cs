using System.Collections.Generic;
using System.Linq;
using ConsoleEngine.Enums;

namespace ConsoleEngine.Core
{
    //Scene Template.
    //각 씬마다 고유의 Instance를 갖고 자기만의 Enitity 들을 관리한다.
    public class Scene : Entity
    {
        public int sceneId;
        public int entityId;
        //Update용. Scene 최초 생성 이후에는 사용하지 말것.
        public List<Entity> EntityList;

        //AddEntity. Scene최초 생성 이후 사용.
        public List<Entity> addList;

        //Entity Remove용
        public List<Entity> removeList;

        public Scene(int _id, string _name)
        {
            name = _name;
            entityId = 0;
            sceneId = _id;
            Active = false;
            addList = new List<Entity>();
            removeList = new List<Entity>();
            EntityList = new List<Entity>();
            sceneStatus = SceneStatus.Sleep;
        }

        public override void Awake()
        {
            EntityRemove();
            UpdatePriority();
            foreach (var o in EntityList)
            {
                if (o.isAwake)
                {
                    o.Awake();
                    o.isAwake = false;
                }
            }
        }

        public override void Start()
        {
            foreach (var o in EntityList)
            {
                if (o.isStart)
                {
                    o.Start();
                    o.isStart = false;
                }
            }
        }

        public override void Update()
        {
            foreach (var o in EntityList)
            {
                if (o.Active)
                    o.Update();

                if (SceneManager.systemStatus== SystemStatus.SceneChange)
                    break;
            }
        }

        //업데이트 우선순위 정렬
        void UpdatePriority()
        {
            foreach (var t in addList)
                EntityList.Add(t);

            addList.Clear();
            EntityList = EntityList.OrderBy(e => e.updatePriority).ToList();
        }

        public override void Rendering()
        {
            RenderingPriority();
            foreach (var o in EntityList)
            {
                if (o.Active)
                    o.Rendering();
            }
        }

        //렌더링 우선순위 정렬
        void RenderingPriority()
            => EntityList = EntityList.OrderBy(e => e.renderingPriority).ToList();

        //scene이 running 중일때 추가되는 엔티티들은 이경로를 통해서 EntityList에 들어가야 한다.
        public void AddEntity(Entity e)
            => addList.Add(e);

        //내부적으로 삭제할 엔티티들이 이곳을 통해 삭제해야한다.
        public void RemoveEntity(Entity e)
            => removeList.Add(e);

        void EntityRemove()
        {
            foreach(var e in removeList)
                EntityList.Remove(e);
            removeList.Clear();
        }

        public void AllEntityDisable()
        {
            foreach(var t in EntityList)
                t.Active = false;
        }

        public void AllEntityActive()
        {
            foreach (var e in EntityList)
                e.Active = true;
        }
    }
}
