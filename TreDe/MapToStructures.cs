using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe
{
    public enum TileType { Empty, Wall, Door, Tree, Crown, Water }
    public struct Tile
    {
        public char charPart;
        public bool blocked;
        public Color color;
    }

    public struct Structure
    {
        public TileType[] tiles;
    }

    public static class ConvertFromByteToTiles
    {
        //  ========================  T I L E S ====================

        public static Tile Empty = new Tile()
        {
            charPart = ' ',
            blocked = false,
            color = Color.Transparent
        };



        public static Tile Wall = new Tile()
        {
            charPart =  '#',
            blocked = true,
            color = Color.Yellow
        };


        public static Tile Tree = new Tile()
        {
            charPart = '*',
            blocked = true,
            color = Color.Brown
        };
        public static Tile Crown = new Tile()
        {
            charPart = '&',
            blocked = true,
            color = Color.Green
        };

        public static Tile Water = new Tile()
        {
            charPart = (char)219,
            blocked = false ,
            color =  Color.DarkBlue
        };
       

    
        public static Dictionary<byte, Tile> MapByteToTile = new Dictionary<byte, Tile>()
        {
            {(byte)TileType.Empty, Empty },   
            {(byte)TileType.Water, Water },
            {(byte)TileType.Wall, Wall },
             {(byte)TileType.Crown, Crown },
            {(byte)TileType.Tree, Tree },
        };

      
      
    }
}


