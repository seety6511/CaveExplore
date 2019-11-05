using ConsoleEngine.Enums;
using ConsoleEngine.Prefabs.Template.Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Textures
{
    public static class TileTextures
    {
        public static Texture GetTileTexture(TileType type)
        {
            switch (type)
            {
                case TileType.Wall:
                    return new Texture((char)type, ConsoleColor.DarkGray, ConsoleColor.DarkGray);
                case TileType.DamangedWall:
                    return new Texture((char)type, ConsoleColor.Gray, ConsoleColor.DarkGray);
                case TileType.DestroyedWall:
                    return new Texture((char)type, ConsoleColor.Gray, ConsoleColor.DarkGray);
                case TileType.Empty:
                    return new Texture((char)type, ConsoleColor.Gray, ConsoleColor.Black);
            }
            return new Texture((char)type, ConsoleColor.Gray, ConsoleColor.Black);
        }
    }
}
