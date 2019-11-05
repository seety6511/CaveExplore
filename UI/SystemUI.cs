using ConsoleEngine.Core;
using System.Collections.Generic;

namespace ConsoleEngine.UI
{
    //백그라운드/디버그용 데이터 표시용
    public static class SystemUI
    {
        static int sx;
        static int messageLine;
        static int messageStartLine;
        static int messageEndLine;
        static int messageLineLength;

        static List<string> messageStack;
        static List<string> systemMessage;

        static Vector startLinePos;
        static Vector endLinePos;
        static Vector messageLinePos;
        static SystemUI()
        {
            sx = 90;
            messageLine = 1;
            messageStartLine = 0;
            messageEndLine = 16;
            messageLineLength = 30;

            messageStack = new List<string>();
            systemMessage = new List<string>();

            startLinePos = new Vector(sx, messageStartLine);
            endLinePos = new Vector(sx, messageEndLine);
            messageLinePos = new Vector(sx, messageLine);
        }

        public static void Message(string _text)
        {
            messageStack.Add(_text);
            while (messageStack.Count >= messageEndLine- messageStartLine)
                messageStack.RemoveAt(0);

            Renderer.DrawXLine(startLinePos, messageLineLength, '=');
            for (int i = 0; i < messageStack.Count; ++i)
            {
                messageLinePos.y += i;
                Renderer.DrawXLine(messageLinePos, messageLineLength, ' ');
                Renderer.WriteLine(messageLinePos, messageStack[i]);
                messageLinePos.y -= i;
            }

            Renderer.DrawXLine(endLinePos, messageLineLength, '=');
        }

        public static void MessageClear()
        {
            messageStack.Clear();
        }
    }
}
