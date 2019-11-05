using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleEngine.Core;
using ConsoleEngine.Enums;

namespace ConsoleEngine.Entitys
{
    public class Tile : Entity
    {
        //안개에 가려져있나
        public bool isFog;
        public TileType type;
        public Tile[] neighbors;
        //파괴 가능한가?
        public bool isDistructible;
        //내구도
        public int durablity;

        //타일의 상태변화
        public bool isUpdate;
        public Tile(int x, int y)
        {
            isUpdate = true;
            isPass = true;
            isDistructible = false;
            neighbors = new Tile[8];
            position = new Vector(x, y);
        }

        public void Dig(int power)
        {
            if (isDistructible)
                durablity -= power;
            switch (durablity)
            {
                case 2:
                    texture = Textures.TileTextures.GetTileTexture(TileType.DamangedWall);
                    type = TileType.DamangedWall;
                    break;
                case 1:
                    texture = Textures.TileTextures.GetTileTexture(TileType.DestroyedWall);
                    type = TileType.DestroyedWall;
                    break;
                case 0:
                    texture = Textures.TileTextures.GetTileTexture(TileType.Empty);
                    type = TileType.Empty;
                    isDistructible = false;
                    isPass = true;
                    break;
            }
            isUpdate = true;
        }
    }
}
