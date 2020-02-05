using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Display
    {
        public int Width;
        public int Height;
        public int Depth;

        public byte[,,] Grid;
        public Color[,,] ColorGrid;
       

        public Display(int worldWidth, int worldHeight, int worldDepth)
        {
            this.Width = worldWidth;
            this.Height = worldHeight;
            this.Depth = worldDepth;

            Grid = new byte[Width, Height, Depth];
            ColorGrid = new Color[Width, Height, Depth];
        }
    }
}