using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Util
{
    public static class Utils
    {
        static Random r = new Random();
        static StringBuilder sb = new StringBuilder();

        public static T[] ArrayShuffle<T>(T[] arr)
        {
            for(int i = 0; i < arr.Length; ++i)
            {
                var temp = arr[i];
                var index = r.Next(0, arr.Length - 1);
                arr[i] = arr[index];
                arr[index] = temp;
            }

            return arr;
        }

        public static string StringCreate(params string[] arr)
        {
            for (int i = 0; i < arr.Length; ++i)
                sb.Append(arr[i]);

            var result = sb.ToString();
            sb.Clear();
            return result;
        }
    }
}
