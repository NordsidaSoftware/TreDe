
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TreDe
{
    public class PhysEngine
    {
        private PlayState playState;
        int Width, Height;

        public bool[,,] WaterGrid;
        public bool[,] WaterGridNeedUpdate, old_WaterGridNeedUpdate;
        private bool WaterIsStatic;

        Random rnd;

        public PhysEngine(PlayState playState)
        {
            this.playState = playState;
            this.Width = playState.Grid.GetLength(0);
            this.Height = playState.Grid.GetLength(1);
            WaterGrid = new bool[Width, Height, 8];
            WaterGridNeedUpdate = old_WaterGridNeedUpdate = new bool[Width, Height];
            rnd = new Random();
        }


        public void AddWaterTop(int x, int y)
        {
            WaterGrid[x, y, 7] = true;
            WaterGridNeedUpdate[x, y] = true;
        }

        public void AddWater(int x, int y, int amount)
        {
            byte level = GetWaterLevelAt(x, y);
            if (amount+level > 7) { amount = 7 - level; }
            for ( int z = level; z < level+amount; z++)
            {
                WaterGrid[x, y, z] = true;
            }
            WaterGridNeedUpdate[x, y] = true;
        }

        private byte GetWaterLevelAt(int x, int y)
        {
            byte level = 0;
            for ( int z = 0; z < 8; z++)
            { if (WaterGrid[x,y,z]) { level++; } }
            return level;
        }

        public void Update(GameTime gameTime)
        {
            if (!WaterIsStatic) { WaterSimulation(); }
        }

        private void WaterSimulation()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (WaterGridNeedUpdate[x, y])
                    {
                        for (int z = 0; z < 7; z++)  // bottom to top - 1
                        {
                            // Flow Down
                            if (WaterGrid[x, y, z + 1] && !WaterGrid[x, y, z])
                            {
                                WaterGrid[x, y, z + 1] = false;
                                WaterGrid[x, y, z] = true;
                            }

                            // Flow Laterally
                            if (WaterGrid[x, y, z + 1] && WaterGrid[x, y, z])
                            {
                                Point3 tile = SearchAndRetrieveEmptyTile(x, y, z);
                                if (tile.X > 0)
                                {
                                    WaterGrid[x, y, z + 1] = false;
                                    WaterGrid[tile.X, tile.Y, tile.Z] = true;
                                }
                            }
                        }
                    }
                }
            }

            old_WaterGridNeedUpdate = WaterGridNeedUpdate;
            
        }

        private Point3 SearchAndRetrieveEmptyTile(int x, int y, int z)
        {
            Point3 EmptyTile = new Point3(-1, -1, -1);
            int lowestLevel = GetLowestLevelFrom(x, y, z);


            for (int level = lowestLevel; level < z; level++)
            {
                Point origin = new Point(x, y);
                Queue<Point> Frontier = new Queue<Point>();
                Dictionary<Point, Point> Came_From = new Dictionary<Point, Point>();
                Frontier.Enqueue(origin);
                Came_From[origin] = origin;
                while (Frontier.Count > 0)
                {
                    Point current = Frontier.Dequeue();
                    if (!WaterGrid[current.X, current.Y, level])
                    { EmptyTile = new Point3(current.X, current.Y, level); return EmptyTile; }

                    foreach (Point p in GetNeighbors(current.X, current.Y))
                    {
                        if (playState.IsOccupied(p.X, p.Y, level)) { continue; }

                        if (!Came_From.ContainsKey(p)) { Came_From[p] = current; Frontier.Enqueue(p); }
                    }
                }

            }
            return EmptyTile;
        }

        private int GetLowestLevelFrom(int x, int y, int z)
        {
            for ( int level = z; level >= 0; level--)
            {
                if (playState.IsOccupied(x, y, level)) { return level + 1; }
            }
            return 0;
        }

        private IEnumerable<Point> GetNeighbors(int x, int y)
        {

            for (int dx = x - 1; dx < x + 2; dx ++)
            {
                for (int dy = y - 1; dy < y + 2; dy++)
                {
                    if (dx == x && dy == y) { continue; }
                    yield return new Point(dx, dy);
                }
            }
        }
    }
}