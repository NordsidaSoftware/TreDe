using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe
{
    public enum TileType:byte { Empty = 0x0, Grass, Wall, DoorClosed, Tree, Crown, Water,
        DoorOpen
    }
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

    public static class TileManager
    {
        //  ========================  T I L E S ====================

        public static Tile EmptyTile = new Tile()
        {
            charPart = ' ',
            blocked = false,
            color = Color.Transparent
        };

        public static Tile GrassTile = new Tile()
        {
            charPart = '.',
            blocked = false,
            color = Color.Green
        };



        public static Tile WallTile = new Tile()
        {
            charPart =  '#',
            blocked = true,
            color = Color.Yellow
        };


        public static Tile TreeTile = new Tile()
        {
            charPart = '*',
            blocked = true,
            color = Color.Brown
        };
        public static Tile TreeTopTile = new Tile()
        {
            charPart = '&',
            blocked = true,
            color = Color.Green
        };


        public static Tile DoorClosedTile = new Tile()
        {
            charPart = '-',
            blocked = true,
            color = Color.Red
        };

        public static Tile DoorOpenTile = new Tile()
        {
            charPart = '_',
            blocked = false,
            color = Color.Yellow
        };


        //  ========================  S T R U C T U R E S ====================


        public static Structure TreeStruct = new Structure()
        {
            tiles = new TileType[]{TileType.Tree, TileType.Tree,
            TileType.Tree, TileType.Tree, TileType.Tree, TileType.Tree, TileType.Tree,
            TileType.Crown}
        };

        public static Structure WallStruct = new Structure()
        {
            tiles = new TileType[]{TileType.Wall, TileType.Wall,
            TileType.Wall, TileType.Wall, TileType.Wall}
        };

        public static Structure DoorClosedStruct = new Structure()
        {
            tiles = new TileType[]{TileType.DoorClosed, TileType.DoorClosed,
            TileType.DoorClosed, TileType.DoorClosed, TileType.DoorClosed}
        };

        public static Structure DoorOpenStruct = new Structure()
        {
            tiles = new TileType[]{TileType.DoorOpen, TileType.DoorOpen,
            TileType.DoorOpen, TileType.DoorOpen, TileType.DoorOpen}
        };


        public static Dictionary<byte, Tile> MapByteToTile = new Dictionary<byte, Tile>()
        {
            {(byte)TileType.Empty, EmptyTile },
            {(byte)TileType.Grass, GrassTile },
            {(byte)TileType.Wall,  WallTile  },
            {(byte)TileType.Crown, TreeTopTile },
            {(byte)TileType.Tree,  TreeTile  },
            {(byte)TileType.DoorClosed, DoorClosedTile },
            {(byte)TileType.DoorOpen, DoorOpenTile }
        };


        public static Dictionary<string, Structure> MapStringToStructure = new Dictionary<string, Structure>()
        {
             {"Tree", TreeStruct},

             {"HouseWall", WallStruct },

             {"DoorClosed",  DoorClosedStruct},

             {"DoorOpen", DoorOpenStruct }

        };
      
      
    }
}


