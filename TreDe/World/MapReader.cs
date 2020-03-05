using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TreDe
{
  
    internal class MapReader
    {
        private int worldWidth;
        private int worldHeight;
        private int worldDepth;
        private byte[,,] Map;

        private List<Building> buildings;

        public MapReader(WorldGen WG)
        {
            this.worldWidth = WG.WorldWidth;
            this.worldHeight = WG.WorldHeight;
            this.worldDepth = WG.WorldDepth;

            Map = new byte[worldWidth, worldHeight, worldDepth];
            buildings = WG.Buildings;
            
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
            
            foreach (Building house in buildings)
            {
                foreach (Point p in RectangleExtension.Walls(house.Rectangle))
                    AddStructure(p.X, p.Y, "HouseWall");

                foreach (Point p in RectangleExtension.Area(house.Rectangle))
                    Map[p.X, p.Y, 0] = (byte)TileType.Empty;
                
                foreach (Point p in house.Doors)
                    AddStructure(p.X, p.Y, "DoorClosed");
            }
            
            

            

           

            return Map;
        }

        private void AddStructure(int x, int y, string structureAsString)
        {
            if (TileManager.MapStringToStructure.ContainsKey(structureAsString))
            {
                Structure s = TileManager.MapStringToStructure[structureAsString];
                for ( int layer = 0; layer < s.tiles.Length; layer++)
                {
                    Map[x, y, layer] = (byte)s.tiles[layer];
                }
            }
        }
    }
}