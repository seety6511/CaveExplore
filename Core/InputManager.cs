using System;
using Microsoft.Win32.SafeHandles;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleEngine.UI;

namespace ConsoleEngine.Core
{
    public class InputManager
    {
        public static InputEvent.MOUSE_EVENT_RECORD MouseEvent;
        public static InputEvent.KEY_EVENT_RECORD KeyBoardEvent;
        public static Vector mousePosition;
        public static ConsoleKey inputConsoleKey;
        static Vector nullVector = new Vector(-1, -1);
        public static void Input()
        {
            var handle = InputEvent.GetStdHandle(InputEvent.STD_INPUT_HANDLE);

            int mode = 0;
            if (!(InputEvent.GetConsoleMode(handle, ref mode))) { throw new Win32Exception(); }

            mode |= InputEvent.ENABLE_MOUSE_INPUT;
            mode &= ~InputEvent.ENABLE_QUICK_EDIT_MODE;
            mode |= InputEvent.ENABLE_EXTENDED_FLAGS;

            if (!(InputEvent.SetConsoleMode(handle, mode))) { throw new Win32Exception(); }

            var record = new InputEvent.INPUT_RECORD();
            uint recordLen = 0;
            while (true)
            {
                if (!(InputEvent.ReadConsoleInput(handle, ref record, 1, ref recordLen))) { throw new Win32Exception(); }
                Console.SetCursorPosition(0, 0);

                MouseEvent = record.MouseEvent;
                SystemUI.MessageClear();
                SystemUI.Message("Mouse X = " + record.MouseEvent.dwMousePosition.X);
                SystemUI.Message("Mouse Y = " + record.MouseEvent.dwMousePosition.Y);
                SystemUI.Message("Mouse Down = " + record.MouseEvent.dwButtonState);
                SystemUI.Message("KeyDown = " + ((ConsoleKey)record.KeyEvent.wVirtualKeyCode).ToString());
                SystemUI.Message("KeyDown = " + record.KeyEvent.bKeyDown);
                switch (record.EventType)
                {
                    case InputEvent.MOUSE_EVENT:
                        mousePosition = new Vector(record.MouseEvent.dwMousePosition.X, record.MouseEvent.dwMousePosition.Y);
                        break;
                    case InputEvent.KEY_EVENT:
                            KeyBoardEvent = record.KeyEvent;
                            inputConsoleKey = (ConsoleKey)record.KeyEvent.wVirtualKeyCode;
                        if (KeyBoardEvent.bKeyDown)
                            return;
                        break;
                }
            }
        }
        public class InputEvent
        {
            public const int STD_INPUT_HANDLE = -10;

            public const int ENABLE_MOUSE_INPUT = 0x0010;
            public const int ENABLE_QUICK_EDIT_MODE = 0x0040;
            public const int ENABLE_EXTENDED_FLAGS = 0x0080;

            public const int KEY_EVENT = 1;
            public const int MOUSE_EVENT = 2;

            //입력이 마우스인지 키보드인지
            [DebuggerDisplay("EventType: {EventType}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct INPUT_RECORD
            {
                [FieldOffset(0)]
                public short EventType;
                [FieldOffset(4)]
                public KEY_EVENT_RECORD KeyEvent;
                [FieldOffset(4)]
                public MOUSE_EVENT_RECORD MouseEvent;
            }

            //마우스 입력 상태
            [DebuggerDisplay("{dwMousePosition.X}, {dwMousePosition.Y}")]
            public struct MOUSE_EVENT_RECORD
            {
                public COORD dwMousePosition;
                public int dwButtonState;
                public int dwControlKeyState;
                public int dwEventFlags;
            }

            [DebuggerDisplay("{X}, {Y}")]
            public struct COORD
            {
                public ushort X;
                public ushort Y;
            }

            //키보드 입력상태
            [DebuggerDisplay("KeyCode: {wVirtualKeyCode}")]
            [StructLayout(LayoutKind.Explicit)]
            public struct KEY_EVENT_RECORD
            {
                [FieldOffset(0)]
                [MarshalAs(UnmanagedType.Bool)]
                public bool bKeyDown;
                [FieldOffset(4)]
                public ushort wRepeatCount;
                [FieldOffset(6)]
                public ushort wVirtualKeyCode;
                [FieldOffset(8)]
                public ushort wVirtualScanCode;
                [FieldOffset(10)]
                public char UnicodeChar;
                [FieldOffset(10)]
                public byte AsciiChar;
                [FieldOffset(12)]
                public int dwControlKeyState;
            };


            public class ConsoleHandle : SafeHandleMinusOneIsInvalid
            {
                public ConsoleHandle() : base(false) { }

                protected override bool ReleaseHandle()
                {
                    return true; //releasing console handle is not our business
                }
            }

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetConsoleMode(ConsoleHandle hConsoleHandle, ref int lpMode);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern ConsoleHandle GetStdHandle(int nStdHandle);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ReadConsoleInput(ConsoleHandle hConsoleInput, ref INPUT_RECORD lpBuffer, uint nLength, ref uint lpNumberOfEventsRead);

            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetConsoleMode(ConsoleHandle hConsoleHandle, int dwMode);
        }
    }
}
