using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe
{
    public class WorldGen
    {
        public int WorldWidth, WorldHeight, WorldDepth;
        public List<Building> Buildings;

        public WorldGen(int WorldWidth, int WorldHeight, int WorldDepth)
        {
            Buildings = new List<Building>();
            this.WorldWidth = WorldWidth;
            this.WorldHeight = WorldHeight;
            this.WorldDepth = WorldDepth;
        }

        public bool GenerateRandomHouse()
        {
            int Width = Randomizer.rnd.Next(3, 10);
            int Height = Randomizer.rnd.Next(3, 10);
            int PositionX = Randomizer.rnd.Next(10, WorldWidth - (Width + 10));
            int PositionY = Randomizer.rnd.Next(10, WorldHeight - (Height + 10));
            int NRDoors = Randomizer.rnd.Next(1, 3);

            Rectangle rectangle = new Rectangle(PositionX,
                                                PositionY,
                                                Width,
                                                Height);

            List<Point> walls = RectangleExtension.Walls(rectangle);
            List<Point> doors = new List<Point>();
            for (int door = 0; door < NRDoors; door++)
            {
                doors.Add(walls[Randomizer.rnd.Next(0, walls.Count - 1)]);
            }

            Building b = new Building(null, rectangle, doors);
            Buildings.Add(b);
            return true;
        }
    }
}
