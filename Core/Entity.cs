using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs.Template.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Core
{
    //Scene상에 등록될 모든 클래스가 상속받아야 할 클래스.
    //이게 상속되어있지 않는다면 Scene에 등록되지 않는다.
    public abstract class Entity
    {
        public string name;
        //false 면 업데이트, 렌더링이 되지않음.
        public bool Active;
        //최초 생성시 true, 이후 Awake() 와 Start() 이후 false로 변함
        public bool isAwake;
        public bool isStart;
        //콘솔 화면 상의 좌표
        public Vector position;
        //통과 가능한가? -> 물리적으로 접촉이 가능한가?
        public bool isPass;

        //우선순위 결정용 변수. 기본값 0(가장먼저)
        public int updatePriority;
        public int renderingPriority;

        //0 = null
        public Tag tag;
        //화면상에 표시될 그래픽
        public Texture texture;

        //현재 자신이 속한 Scene의 상태
        public SceneStatus sceneStatus;
        public Entity()
        {
            //빈텍스쳐.
            texture = new Texture(' ', ConsoleColor.Gray, ConsoleColor.Black);
            tag = 0;
            renderingPriority = 9;
            updatePriority = 9;
            isAwake = true;
            isStart = true;
        }
        //이 엔티티가 생성되면 무조건 한 번 실행되는 메소드
        public virtual void Awake() { }
        //등록후 Active가 true 이면 한 번 실행되는 메소드
        public virtual void Start() { }
        //등록 후 Active가 true이면 반복해서 실행되는 메소드
        public virtual void Update() { }
        //첫 Scene 생성이후 Scene 내부에서 만들어지는 클래스들을 다음번 반복때 포함시키는 메소드.
        public virtual void LateUpdate() { }
        //마지막에 Active 상태인 엔티티들을 순서에 맞게 그려주는 용도
        public virtual void Rendering() { }

        public void SetPosition(Vector v) => position = v;
    }
}
