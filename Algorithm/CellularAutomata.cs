using ConsoleEngine.Core;
using ConsoleEngine.Entitys;
using ConsoleEngine.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Algorithm
{
    public class CellularAutomata
    {
        private int[,] field;
        private char texture = '@';
        int maxX, maxY;

        public CellularAutomata(int[,] _field, int _mx, int _my)
        {
            field = _field;
            maxX = _mx;
            maxY = _my;
        }

        public void DrawField()
        {
            for (int y = 0; y < maxY; ++y)
            {
                for (int x = 0; x < maxX; ++x)
                    Console.Write(field[x, y] == 1 ? texture : ' ');
                Console.WriteLine();
            }
        }

        public void Init()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            for (int x = 0; x < maxX; ++x)
            {
                for (int y = 0; y < maxY; y++)
                    field[x, y] = r.Next(0, 1 + 1);
            }
        }

        public void LifeTime()
        {
            RuleSet rs = new CGOL(field, maxX, maxY);
            for (int i = 0; i < 5000; ++i)
            {
                DrawField();
                rs.Tick();
            }
        }
    }

    public abstract class RuleSet
    {
        protected int _maxX = 0;
        protected int _maxY = 0;
        protected int[,] _field;
        protected abstract int[,] TickAlgorithm();

        public RuleSet(int[,] field, int maxX, int maxY)
        {
            _field = field;
            _maxX = maxX;
            _maxY = maxY;
        }

        protected int GetNumberOfNegibors(int x, int y)
        {
            int n = 0;

            if (x + 1 < _maxX && _field[x + 1, y] == 1)
            {
                n++;
            }

            if (x - 1 >= 0 && _field[x - 1, y] == 1)
                n++;

            if (y + 1 < _maxY && _field[x, y + 1] == 1)
                n++;

            if (y - 1 >= 0 && _field[x, y - 1] == 1)
                n++;

            if (x + 1 < _maxX && y + 1 < _maxY && _field[x + 1, y + 1] == 1)
                n++;

            if (x + 1 < _maxX && y - 1 >= 0 && _field[x + 1, y - 1] == 1)
                n++;

            if (x - 1 >= 0 && y + 1 < _maxY && _field[x - 1, y + 1] == 1)
                n++;

            if (x - 1 >= 0 && y - 1 >= 0 && _field[x - 1, y - 1] == 1)
                n++;
            return n;

        }

        public void Tick()
        {
            int[,] field2 = TickAlgorithm();
            Array.Copy(field2, _field, field2.Length);
        }

    }

    //ConwaysGameOfLife
    public class CGOL : RuleSet
    {
        public CGOL(int[,] field, int maxX, int maxY) : base(field, maxX, maxY) { }

        protected override int[,] TickAlgorithm()
        {
            int[,] field2 = new int[_maxX, _maxY];
            for (int y = 0; y < _maxY; ++y)
            {
                for (int x = 0; x < _maxX; ++x)
                {
                    int n = GetNumberOfNegibors(x, y);
                    if (n == 3)
                    {
                        field2[x, y] = 1;
                        continue;
                    }

                    if (n == 2 || n == 3)
                    {
                        field2[x, y] = _field[x, y];
                        continue;
                    }
                    field2[x, y] = 0;
                }
            }
            return field2;
        }
    }

    public class Rule125d36 : RuleSet
    {
        public Rule125d36(int[,] field, int maxX, int maxY) : base(field, maxX, maxY) { }

        protected override int[,] TickAlgorithm()
        {
            int[,] field2 = new int[_maxX, _maxY];
            for (int y = 0; y < _maxY; ++y)
            {
                for (int x = 0; x < _maxX; ++x)
                {
                    int n = GetNumberOfNegibors(x, y);
                    if (n == 3 || n == 6)
                    {
                        field2[x, y] = 1;
                        continue;
                    }

                    if (n == 1 || n == 2 || n == 5)
                    {
                        field2[x, y] = _field[x, y];
                        continue;
                    }
                    field2[x, y] = 0;
                }
            }
            return field2;
        }

    }

    public class RuleGeneric : RuleSet
    {
        private int a;
        private int b;
        public RuleGeneric(int[,] field, int maxX, int maxY, int _a, int _b) : base(field, maxX, maxY)
        {
            a = _a;
            b = _b;
        }

        protected List<int> ToDigitArray(int digits)
        {
            List<int> result = new List<int>();
            string ds = digits.ToString();
            foreach (var d in ds)
                result.Add(Convert.ToInt32(d.ToString()));
            return result;
        }

        protected override int[,] TickAlgorithm()
        {
            int[,] field2 = new int[_maxX, _maxY];

            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < _maxX; ++x)
                {
                    bool p = false;
                    int n = GetNumberOfNegibors(x, y);

                    List<int> bd = ToDigitArray(b);

                    foreach (var d in bd)
                    {
                        if (n == d)
                        {
                            field2[x, y] = 1;
                            p = true;
                            break;
                        }
                    }

                    if (p)
                        continue;

                    List<int> ld = ToDigitArray(a);
                    foreach (var d in ld)
                    {
                        if (n == d)
                        {
                            field2[x, y] = _field[x, y];
                            p = true;
                            break;
                        }
                    }
                    if (p)
                        continue;
                    field2[x, y] = 0;
                }
            }
            return field2;
        }
    }

    //이 위엣라인 미사용
    //아래는 긁어온 코드 수정본.
    public class MapHandler
    {
        Random rand = new Random();

        public Tile[,] Map;
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public int PercentAreWalls { get; set; }
        public Tilemap tileMap;
        public MapHandler(int width, int height, int WallPercent = 40)
        {
            MapWidth = width;
            MapHeight = height;
            PercentAreWalls = WallPercent;
            Map = new Tile[MapWidth, MapHeight];
            RandomFillMap();
            tileMap = new Tilemap(width, height);
            tileMap.tiles = Map;
            //Renderer.DrawTileMap(Map);
        }

        public void PrintMap()
        {
            for (int x = 0; x < MapWidth; ++x)
            {
                for (int y = 0; y < MapHeight; ++y)
                    Renderer.Render(Map[x, y]);
            }
        }

        public void MakeCaverns()
        {
            // By initilizing column in the outter loop, its only created ONCE
            for (int column = 0, row = 0; row < MapWidth; row++)
            {
                for (column = 0; column < MapHeight; column++)
                    Map[row, column].type = PlaceWall(row, column);
            }
        }

        public TileType PlaceWall(int x, int y)
        {
            int numWalls = GetAdjacentWalls(x, y, 1, 1);
            if (Map[x, y].type == TileType.Wall)
            {
                if (numWalls >= 4)
                    return TileType.Wall;

                if (numWalls < 2)
                    return TileType.Empty;
            }
            else
            {
                if (numWalls >= 5)
                    return TileType.Wall;
            }
            return TileType.Empty;
        }

        public int GetAdjacentWalls(int x, int y, int scopeX, int scopeY)
        {
            int startX = x - scopeX;
            int startY = y - scopeY;
            int endX = x + scopeX;
            int endY = y + scopeY;
            int iX = startX;
            int iY = startY;
            int wallCounter = 0;

            for (iX = startX; iX <= endX; iX++)
            {
                for (iY = startY; iY <= endY; iY++)
                {
                    if (!(iX == x && iY == y) && IsWall(iX, iY))
                        wallCounter += 1;
                }
            }
            return wallCounter;
        }

        bool IsWall(int x, int y)
        {
            // Consider out-of-bound a wall
            if (IsOutOfBounds(x, y) || Map[x, y].type == TileType.Wall)
                return true;

            if (Map[x, y].type != TileType.Wall)
                return false;

            return false;
        }

        bool IsOutOfBounds(int x, int y)
        {
            if (x < 0 || y < 0 || x > MapWidth - 1 || y > MapHeight - 1)
                return true;

            return false;
        }

        public void BlankMap()
        {
            for (int row = 0; row < MapWidth; row++)
            {
                for (int column = 0; column < MapHeight; column++)
                    Map[row, column].type = TileType.Empty;
            }
        }

        public void RandomFillMap()
        {
            int mapMiddle = 0; // Temp variable
            for (int row = 0; row < MapWidth; row++)
            {
                for (int column = 0; column < MapHeight; column++)
                {
                    Map[row, column] = new Tile(row, column);
                    // If coordinants lie on the the edge of the map (creates a border)
                    if (column == 0 || row == 0 || column == MapHeight - 1 || row == MapWidth - 1)
                        Map[row, column].type = TileType.Wall;
                    else
                    {
                        mapMiddle = (MapWidth / 2);

                        if (column == mapMiddle)
                            Map[row, column].type = TileType.Empty;
                        else
                            Map[row, column].type = RandomPercent(PercentAreWalls);
                    }
                }
            }
        }

        TileType RandomPercent(int percent)
        {
            if (percent >= rand.Next(1, 101))
                return TileType.Wall;

            return TileType.Empty;
        }
    }
}
