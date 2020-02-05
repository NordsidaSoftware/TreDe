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
                    for (int z = 0; z < worldDepth; z++)
                    {
                        if (z == 0)
                            Map[x, y, z] = (byte)TileType.Grass;
                        else
                            Map[x, y, z] = (byte)TileType.Empty;
                    }
                }
            }
            
           for (int i = 0; i < (worldHeight * worldWidth)/100; i++)
            {
                var randX = rnd.Next(0, worldWidth);
                var randY = rnd.Next(0, worldHeight);

                AddStructure(randX, randY, "Tree");
               
           
            }
            

            Rectangle house = new Rectangle(10, 10, 10, 10);
            foreach (Point p in RectangleExtension.Walls(house))
                AddStructure(p.X, p.Y, "HouseWall");

            foreach (Point p in RectangleExtension.Area(house))
                Map[p.X, p.Y, 0] = (byte)TileType.Empty;

            AddStructure(15, 20, "Door");

            return Map;
        }

        private void AddStructure(int x, int y, string structureAsString)
        {
            if (ConvertFromByteToTiles.StructureDictionary.ContainsKey(structureAsString))
            {
                Structure s = ConvertFromByteToTiles.StructureDictionary[structureAsString];
                for ( int layer = 0; layer < s.tiles.Length; layer++)
                {
                    Map[x, y, layer] = (byte)s.tiles[layer];
                }
            }
        }
    }
}