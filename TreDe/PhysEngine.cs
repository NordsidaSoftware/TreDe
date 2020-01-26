
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TreDe
{
    public class PhysEngine
    {
        private PlayState playState;
        int Width, Height;
        public byte[,] WaterGrid, WaterTempGrid;

        byte MaxAmount = 7;
        byte MinAmount = 1;
        Random rnd;

        public PhysEngine(PlayState playState)
        {
            this.playState = playState;
            this.Width = playState.Grid.GetLength(0);
            this.Height = playState.Grid.GetLength(1);
            WaterGrid = WaterTempGrid = new byte[Width, Height];
            rnd = new Random();
        }

        public void AddWater(int x, int y, byte amount)
        {
            WaterGrid[x, y] += amount;
            if (WaterGrid[x,y] > MaxAmount) { WaterGrid[x, y] = MaxAmount; }
        }

        public float WaterAmountAt(int x, int y)
        {
            return WaterGrid[x, y];
        }

        internal byte[,,] Draw(byte[,,] grid)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (WaterGrid[x, y] == 0) { continue; }
                    int amount = WaterGrid[x, y];
                    if (amount >= 7) { SetWaterAmount(x, y, 7); }
                    else { SetWaterAmount(x, y, amount); }
                }
            }

            void SetWaterAmount(int x, int y, int amount)
            {
                for (int level = 0; level < amount; level++)
                {
                    grid[x, y, level] = (byte)MapToStructures.Water.charParts[0];
                }
            }

            return grid;
        }


        public void Update(GameTime gameTime)
        {
            // 1. Reset Temp Grid :
            for ( int x = 0; x < Width; x++)
            {
                for ( int y = 0; y < Height; y++ )
                {
                    WaterTempGrid[x, y] = 0;
                }
            }

            // 2. Step through each cell in simulation ( not border ) :
            byte amount = 0;
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height - 1; y++)
                {
                    for (int z = 0; z < 8; z++) {

                        amount = WaterGrid[x, y];
                        if (amount < MinAmount) {  continue; }
                        if (amount > MaxAmount) { WaterGrid[x, y] = MaxAmount; }

                        List<Point> flowPoints = new List<Point>();
                        foreach (Point n in GetNeighbors(x, y))
                        {
                            if (WaterGrid[n.X, n.Y] < WaterGrid[x, y])
                                if (!playState.IsOccupied(n.X, n.Y, z) || playState.Grid[n.X, n.Y, z] == 219)

                                {
                                    flowPoints.Add(n);
                                }
                        }

                        if (flowPoints.Count > 0)
                        {

                            WaterTempGrid[x, y] = (byte)(WaterGrid[x, y] - 1);

                            // Point p = flowPoints[rnd.Next(0, flowPoints.Count)];

                            //WaterGrid[p.X, p.Y] += 1;
                          
                            foreach (Point p in flowPoints)
                            {
                                WaterTempGrid[p.X, p.Y] += 1;
                            }
                            
                        }
                    }
                }
            }

            WaterGrid = WaterTempGrid;
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