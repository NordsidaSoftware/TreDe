using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe
{
    public static class MapToStructures
    {
        public static Structure Empty = new Structure() { charParts = new char[1] { ' ' } };
        public static Structure Wall = new Structure() { charParts = new char[8] { 'a','b','c','d','e','f','g','h'} };
        public static Structure Door = new Structure() { charParts = new char[8] { 'a', ' ', ' ', ' ', ' ', 'f', 'g', 'h'} };
        public static Structure Tree = new Structure() { charParts = new char[4] { 'A', '.', '.', '*' } };
        public static Structure Water = new Structure() { charParts = new char[1] { (char)219 } };
       
        public static Dictionary<byte, Structure> MapToStructure = new Dictionary<byte, Structure>()
        {
            {(byte)TileType.Empty, Empty },
            {(byte)TileType.Wall, Wall },
            {(byte)TileType.Door, Door },
            {(byte)TileType.Tree, Tree },
            {(byte)TileType.Water, Water }   
        };
    }

        public struct Structure
        {
            public char[] charParts;
        }   
    }


