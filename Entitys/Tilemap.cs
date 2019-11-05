using System;
using System.Linq;
using ConsoleEngine.Core;
using ConsoleEngine.Enums;
using ConsoleEngine.Textures;

namespace ConsoleEngine.Entitys
{
    //콘솔창 전용 타일맵
    //StartPoint (sp) 를 0,0 으로 놓고 좌표계산이 된다.
    //CellulaAutomata 로 생성된 맵을 받아서 여기서 관리한다.
    public class Tilemap : Entity
    {
        public int width;
        public int height;
        public Tile[,] tiles;
        public static int level = 0;

        public Tilemap(int _width, int _height)
        {
            width = _width;
            height = _height;
            renderingPriority = 0;
            Active = true;
            level++;
        }

        public Tile[,] GetMap() => tiles;
        public Tile[] EmptyTiles
            => tiles.Cast<Tile>().Where(i => i.type == TileType.Empty && i.type != TileType.Wall).ToArray();
        void AttachTag(Tile t, Tag tag) => t.tag = tag;
        bool IsOutRange(Vector v)
            => v.x < 0 || v.y < 0 || v.x >= width || v.y >= height;

        //해당 좌표의 타일
        public Tile GetTile(Vector v)
        {
            if (v.x >= width || v.y >= height || v.x < 0 || v.y < 0)
                return null;

            return tiles[v.x, v.y];
        }

        //통과 설정
        public void PassSetting()
        {
            foreach (var t in tiles)
            {
                t.isFog = true;
                if (t.type == TileType.Wall)
                {
                    t.isPass = false;
                    AttachTag(t, Tag.Enviro);
                }
                t.texture = TileTextures.GetTileTexture(t.type);
            }
        }
        
        //내려가는 계단 설정
        public void SetDownStair()
        {
            RandomEmptyTile().type = TileType.DownStair;
        }

        //타일 온 , 내구도 설정
        public void TileActiveAndDurablitySetting()
        {
            foreach(var t in tiles)
            {
                t.Active = true;
                if (!isEdge(t) && t.type == TileType.Wall)
                {
                    t.isDistructible = true;
                    t.durablity = 3;
                }
            }
        }

        //초기화
        public void Clear()
        {
            for(int i =0;i<width; ++i)
            {
                for(int j = 0; j < height; ++j)
                    tiles[i, j] = null;
            }
            Renderer.DrawRect(new Vector(0, 0), new Vector(width, height), ' ');
        }

        //비어있는 랜덤 타일
        public Tile RandomEmptyTile()
        {
            Random r = new Random();
            var arr = tiles.Cast<Tile>().Where(i => i.type == TileType.Empty).ToArray();
            return arr[r.Next(0, arr.Length - 1)];
        }

        //모서리 판단
        bool isEdge(Tile t)
        {
            int x = t.position.x;
            int y = t.position.y;
            return x == 0 || y == 0 || x == width - 1 || y == height - 1;
        }

        //모든 타일의 8방향으로 이웃 타일을 추가
        //0:상단, 1: 우상단, 2:우측, 3:우하단, 4: 하단, 5:좌하단, 6:좌측, 7:좌상단
        public void FindNeighbors()
        {
            int x = 0;
            int y = 0;
            foreach (var t in tiles)
            {
                x = t.position.x;
                y = t.position.y;
                var v = new Vector(0, 0);

                v.x = x;
                v.y = y+1; 
                if (!IsOutRange(v))
                    t.neighbors[0] = GetTile(v);

                v.x = x + 1;
                v.y = y - 1;
                if (!IsOutRange(v))
                    t.neighbors[1] = GetTile(v);

                v.x = x + 1;
                v.y = y;
                if (!IsOutRange(v))
                    t.neighbors[2] = GetTile(v);

                v.x = x + 1;
                v.y = y + 1;
                if (!IsOutRange(v))
                    t.neighbors[3] = GetTile(v);

                v.x = x;
                v.y = y + 1;
                if (!IsOutRange(v))
                    t.neighbors[4] = GetTile(v);

                v.x = x - 1;
                v.y = y + 1;
                if (!IsOutRange(v))
                    t.neighbors[5] = GetTile(v);

                v.x = x - 1;
                v.y = y;
                if (!IsOutRange(v))
                    t.neighbors[6] = GetTile(v);

                v.x = x - 1;
                v.y = y - 1;
                if (!IsOutRange(v))
                    t.neighbors[7] = GetTile(v);
            }
        }

        public override void Rendering()
        {
            Renderer.DrawTileMap(this);
        }

        //모든 타일을 업데이트한다.
        public void UpdateALL()
        {
            foreach (var t in tiles)
                t.isUpdate = true;
        }
    }
}
