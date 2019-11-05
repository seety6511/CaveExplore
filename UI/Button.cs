using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;

namespace ConsoleEngine.UI
{
    public delegate void OnClickEvent();
    public delegate void OnClickEvent2(string name);

    public class Button : Entity
    {
        public int width;
        public int height;
        public string title;
        //선택 되었을때 커서가 위치할 위치. 보통 기본 pos의 x-1값을 가짐. 
        //+ 버튼을 누를때의 기준점.
        public Vector selectPos;
        public ConsoleKey key;
        //void()
        public OnClickEvent oce;
        //void(string)
        public OnClickEvent2 oce2;
        public Entity connectEntity;

        public Button(
            Vector _pos, 
            int _width, int _height, 
            string _title,
            ConsoleKey _key,
            OnClickEvent clickEvent = null)
        {
            Active = true;
            key = _key;
            title = _title;
            width = _width;
            position = _pos;
            height = _height;
            oce = clickEvent;
            selectPos = new Vector(Math.Max(0, _pos.x - 1), _pos.y);
        }
        public Button(
            Vector _pos,
            int _width, int _height,
            string _title,
            ConsoleKey _key = ConsoleKey.ExSel)
        {
            Active = true;
            key = _key;
            title = _title;
            width = _width;
            position = _pos;
            height = _height;
            selectPos = new Vector(Math.Max(0, _pos.x - 1), _pos.y);
        }

        public Button(
            Vector _pos,
            int _width, int _height,
            string _title,
            OnClickEvent2 clickEvent = null,
            ConsoleKey _key = ConsoleKey.ExSel)
        {
            Active = true;
            key = _key;
            title = _title;
            width = _width;
            position = _pos;
            height = _height;
            oce2 = clickEvent;
            selectPos = new Vector(Math.Max(0, _pos.x - 1), _pos.y);
        }
    }
}
