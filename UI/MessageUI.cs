using System.Collections.Generic;
using ConsoleEngine;
using ConsoleEngine.Prefabs.Template;

namespace ConsoleEngine.Core
{
    public class MessageUI : Window
    {
        int messageLine;
        int messageStartLine;
        int messageEndLine;
        int messageLineLength;
        List<string> messageStack;
        List<string> systemMessage;
        Vector startLinePos;
        Vector endLinePos;
        Vector messageLinePos;
        public MessageUI()
        {
            messageLine = 22;
            messageEndLine = 31;
            messageStartLine = 21;
            messageLineLength = 43;

            messageStack = new List<string>();
            systemMessage = new List<string>();

            startLinePos = new Vector(0, messageStartLine);
            endLinePos = new Vector(0, messageEndLine);
            messageLinePos = new Vector(0, messageLine);
        }

        public void Message(string _text)
        {
            messageStack.Add(_text);
            while (messageStack.Count >= messageEndLine - messageStartLine)
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

        public void Clear()
        {
            messageStack.Clear();
        }
    }
}
