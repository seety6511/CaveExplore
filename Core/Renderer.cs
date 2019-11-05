using ConsoleEngine.Entitys;
using ConsoleEngine.Prefabs.Template.Struct;
using ConsoleEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Core
{
    public enum CircleType
    {
        EMPTY,
        FULL
    }

    public static class Renderer
    {
        //current cursor positions
        static int currentX;
        static int currentY;

        //기본 문자
        static char charTile = '#';

        public static void SetColor(ConsoleColor tc, ConsoleColor bc)
        {
            Console.ForegroundColor = tc;
            Console.BackgroundColor = bc;
        }

        public static void DefalutColor()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        public static void SetCursor(int x, int y)
        {
            if (x < 0 || y < 0)
                return;

            Console.SetCursorPosition(x, y);
            GetCursorPostion();
        }

        public static void SetCursor(Vector point)
        {
            SetCursor(point.x, point.y);
            GetCursorPostion();
        }

        public static void GetCursorPostion()
        {
            currentX = Console.CursorLeft;
            currentY = Console.CursorTop;
        }

        public static void SetCharTile(char c)
        {
            charTile = c;
        }

        public static void Render()
        {
            Console.Write(charTile);
        }

        public static void Render(char c)
        {
            Console.Write(c);
        }

        public static void Render(string c)
        {
            Console.Write(c);
        }

        public static void Render(int c)
        {
            Console.Write(c);
        }

        public static void Render(Vector v, Texture t)
        {
            SetColor(t.TextColor, t.BackgroundColor);
            SetCursor(v);
            Render(t.Sprite);
            DefalutColor();
        }

        public static void Render(Tile t)
        {
            SetColor(t.texture.TextColor, t.texture.BackgroundColor);
            SetCursor(t.position);
            Console.Write((char)t.type);
            DefalutColor();
        }

        public static void Render(Vector point, char c)
        {
            SetCursor(point);
            Render(c);
        }

        public static void Render(int x, int y, char c)
        {
            SetCursor(x, y);
            Render(c);
        }

        public static void Render(Vector point, char c, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            SetCursor(point);
            Render(c);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void DrawXLine(Vector startPoint, int endX)
        {
            SetCursor(startPoint.x, startPoint.y);
            for (int i = startPoint.x; i < startPoint.x + endX; ++i)
                Render();
        }

        public static void DrawYLine(Vector startPoint, int endY)
        {
            SetCursor(startPoint);
            for (int i = startPoint.y; i < endY; ++i)
            {
                SetCursor(startPoint.x, i);
                Render();
            }
        }

        public static void DrawXLine(Vector startPoint, int length, char texture)
        {
            SetCursor(startPoint.x, startPoint.y);
            for (int i = startPoint.x; i < startPoint.x + length; ++i)
                Render(texture);
        }

        public static void DrawYLine(Vector startPoint, int length, char texture)
        {
            for (int i = startPoint.y; i < startPoint.y + length; ++i)
            {
                SetCursor(startPoint.x, i);
                Render(texture);
            }
        }

        public static void Write(Vector start, string text)
        {
            SetCursor(start);
            Console.Write(text);
        }

        public static void WriteLine(Vector start, string text)
        {
            SetCursor(start);
            Console.Write(text);
        }
        //LT = LeftTop, RB = RightBottom
        public static void DrawRect(Vector LT, Vector RB)
        {
            SetCursor(LT);
            int ex = RB.x;
            int ey = RB.y;
            int sy = LT.y;
            for (int sx = LT.x; sx <= ex; ++sx)
            {
                sy = LT.y;
                for (; sy <= ey; ++sy)
                {
                    SetCursor(sx, sy);
                    Render();
                }
            }
        }
        public static void DrawRect(Vector LT, Vector RB, char t)
        {
            SetCursor(LT.x, LT.y);
            int sy = LT.y;
            int ex = RB.x;
            int ey = RB.y;

            for (int sx = LT.x; sx <= ex; ++sx)
            {
                sy = LT.y;
                for (; sy <= ey; ++sy)
                {
                    SetCursor(sx, sy);
                    Render(t);
                }
            }
        }

        public static void DrawRect(Vector lt, int width, int height)
        {
            SetCursor(lt);
            int w = lt.x + width;
            int h = lt.y + height;
            for (int i = lt.x; i < w; ++i)
            {
                for (int j = lt.y; j < h; ++j)
                {
                    SetCursor(i, j);
                    Render(' ');
                }
            }
        }

        public static void DrawEmptyRect(Vector lt, int width, int height)
        {
            SetCursor(lt);
            int w = lt.x + width;
            int h = lt.y + height;
            for (int i = lt.x; i < w; ++i)
            {
                for (int j = lt.y; j < h; ++j)
                {
                    if (!(i == lt.x || i == w - 1 || j == lt.y || j == h - 1))
                        continue;
                    SetCursor(i, j);
                    Render();
                }
            }
        }
        public static void ClearWindow(int width, int height)
        {
            for (int i = 0; i < width; ++i)
            {
                for (int q = 0; q < height; ++q)
                {
                    SetCursor(i, q);
                    Render(' ');
                }
            }
        }

        public static void ClearWindow(Vector start, int width, int height)
        {
            for (int i = start.x; i < start.x + width; ++i)
            {
                for (int q = start.y; q < start.y + height; ++q)
                {
                    SetCursor(i, q);
                    Render(' ');
                }
            }
        }
        public static void DrawCircle(Vector Center, int radius, CircleType type = CircleType.FULL)
        {
            int cx = 0;
            int cy = 0;
            for (int i = -radius; i <= radius; ++i)
            {
                for (int k = -radius; k <= radius; ++k)
                {
                    cx = i + Center.x;
                    cy = k + Center.y;
                    if (cx < 0 || cy < 0)
                        continue;
                    SetCursor(cx, cy);

                    int isLine = i * i + k * k;
                    int ra = radius * radius - radius;
                    if (type == CircleType.EMPTY)
                    {
                        if (isLine >= ra)
                            Render();
                    }
                    else if (type == CircleType.FULL)
                    {
                        if (isLine <= ra)
                            Render();
                    }
                }
            }
        }
        public static void DrawTileMap(Tilemap tilemap)
        {
            var map = tilemap.tiles;
            for (int x = 0; x < tilemap.width; ++x)
            {
                for (int y = 0; y < tilemap.height; ++y)
                {
                    if (map[x, y].isFog || !map[x,y].isUpdate)
                        continue;
                    Render(map[x, y]);
                    map[x, y].isUpdate = false;
                }
            }
        }

        public static void DrawButton(Button button, char texture)
        {
            var np = new Vector(button.position.x, button.position.y);
            var text = texture + button.title;
            Write(np, text);
        }

        public static Vector[] DrawTable(Vector sp, int row, int column)
        {
            int sy = sp.y;
            int sx = sp.x;
            List<Vector> list = new List<Vector>();

            for (int i = 0; i < column + 2; ++i)
            {
                DrawYLine(sp, 10, '│');
                sp.x += 5;
            }
            sp.x = sx;

            for (int i = 0; i < row; ++i)
            {
                DrawXLine(sp, 31, '─');
                sp.y += 2;
            }

            sp.x = sx;
            sp.y = sy;
            
            DrawYLine(sp, 10, '│');
            sp.x += 30;
            DrawYLine(sp, 10, '│');

            sp.x = sx;
            sp.y = sy;

            Render(sp, '┌');
            sp.x += 30;
            Render(sp, '┐');
            sp.x -= 30;
            sp.y += 10;
            Render(sp, '└');
            sp.x += 30;
            Render(sp, '┘');
            sp.x = sx;
            sp.y = sy;
            return list.ToArray();
        }
    }
}
