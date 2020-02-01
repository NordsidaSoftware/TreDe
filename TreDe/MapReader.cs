using Microsoft.Xna.Framework;
using System;

namespace TreDe
{
  
    internal class MapReader
    {
        private int worldWidth;
        private int worldHeight;
        private int worldDepth;
        private byte[,,] Map;

        public MapReader(int worldWidth, int worldHeight, int worldDepth)
        {
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
            this.worldDepth = worldDepth;
            Map = new byte[worldWidth, worldHeight, worldDepth];
        }

        internal byte[,,] ReadMap()
        {
            Random rnd = new Random();
           
            for (int x = 0; x < worldWidth; x++)
            {
                for (int y = 0; y < worldHeight; y++)
                {
                    for (int z = 0; z < worldDepth; z++ )
                    Map[x, y, z] = (byte)TileType.Empty;
                }
            }
            
           for (int i = 0; i < (worldHeight * worldWidth)/100; i++)
            {
                var randX = rnd.Next(0, worldWidth);
                var randY = rnd.Next(0, worldHeight);
                for ( int z = 0; z < 7;z++)
                    Map[randX, randY, z] = (byte)TileType.Tree;
                Map[randX, randY, 7] = (byte)TileType.Crown;
           
            }
            

            Rectangle house = new Rectangle(10, 10, 10, 10);
            foreach (Point p in RectangleExtension.Walls(house))
            {
                for (int z = 0; z < 8; z++)
                    Map[p.X, p.Y, z] = (byte)TileType.Wall;
            }

            //SetupStructureInGrid(15, 10, TileType.Door);

            return Map;
        }

        /*
        private void SetupStructureInGrid(int X, int Y, TileType structure)
        {
            var struc = ConvertFromByteToTiles.MapByteToStructure[(byte)structure];

            for (int z = 0; z < struc.tiles.Length; z++)
            {
                Map[X, Y, z] = (byte)struc.tiles[z];
            }
        }
        */
    }
}