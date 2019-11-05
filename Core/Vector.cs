using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Core
{
    public class Vector
    {
        public int x;
        public int y;

        public Vector(int x, int y)
        {
            this.x = Math.Max(0, x);
            this.y = Math.Max(0, y);
        }
        public Vector(Vector v)
        {
            x = Math.Max(0, v.x);
            y = Math.Max(0, v.y);
        }
        public void SetX(int _x) => x = Math.Max(0, x + _x);
        public void SetY(int _y) => y = Math.Max(0, y + _y);

        //이 벡터를 기준점으로 잡고 난 후의 좌표
        public Vector GetLocalPoint(int _x = 0, int _y = 0) => new Vector(x + _x, y + _y);

        public static Vector RandomPoint(int min, int max)
        {
            Random rand = new Random();
            min = Math.Max(0, min);
            max = Math.Max(0, max);

            if (min > max)
            {
                int t = min;
                min = max;
                max = t;
            }

            int x = rand.Next(min, max);
            int y = rand.Next(min, max);
            return new Vector(x, y);
        }
        public static int Distance(Vector A, Vector B)
            => (int)Math.Sqrt((B.x - A.x) * (B.x - A.x) + (B.y - A.y) * (B.y - A.y));

        public void Info()
            => UI.SystemUI.Message("X = " + x + ", Y = " + y);

        public static Vector operator+(Vector a, Vector b)
            => new Vector(a.x + b.x, a.y + b.y);

        public static Vector operator-(Vector A, Vector B)
            => new Vector(A.x - B.x, A.y - B.y);

        public static bool operator ==(Vector a, Vector b)
            => a.x == b.x && a.y == b.y;

        public static bool operator !=(Vector a, Vector b)
            => !(a.x == b.x && a.y == b.y);
        //스칼라곱
        public static int operator *(Vector A, Vector B)
            => A.x * B.x + A.y * B.y;
    }
}
