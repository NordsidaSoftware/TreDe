using Microsoft.Xna.Framework;
using System;

namespace TreDe
{
    public enum TileType { Empty, Wall, Door, Tree, Water }
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
                SetupStructureInGrid(randX, randY, TileType.Tree);
           
            }

            Rectangle house = new Rectangle(10, 10, 10, 10);
            foreach (Point p in RectangleExtension.Walls(house))
            {
                SetupStructureInGrid(p.X, p.Y, TileType.Wall);
            }
            SetupStructureInGrid(15, 10, TileType.Door);

            return Map;
        }

        private void SetupStructureInGrid(int randX, int randY, TileType structure)
        {
            var struc = MapToStructures.MapToStructure[(byte)structure];

            for (int z = 0; z < struc.charParts.Length; z++)
            {
                Map[randX, randY, z] = (byte)struc.charParts[z];
            }
        }
    }
}