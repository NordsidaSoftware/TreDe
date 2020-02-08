using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Display
    {
        private Rectangle Window;           // Size and position of display
        public Texture2D texture;          // Font texture to be used
        public int TextureGlyphsPrRow;     // number of glyphs in each row in font texture
        public int TextureGlyphSize;       // size in px. of each glyph
        public int FontSize;               // size of font on display

        public byte[,] Grid;
        public Color[,] ForegroundColor;
        public Color[,] BackgroundColor;
       
        public int Width { get { return Window.Width; } }
        public int Height { get { return Window.Height; }  }
        public int X { get { return Window.X; } }
        public int Y {  get { return Window.Y; } }
        public Point Center { get { return Window.Center; } }

        public Display(int Display_X, int Display_Y, int width, int height, Renderer r)
        {
          
            texture = r.texture;
            TextureGlyphSize = r.TextureTileSize;
            FontSize = 10;
            TextureGlyphsPrRow = r.TextureTiles;

            int W  = width / FontSize;
            int H = height / FontSize;

            Window = new Rectangle(Display_X, Display_Y, W, H);

            Grid = new byte[Width, Height];
            ForegroundColor = new Color[W, H];
            BackgroundColor = new Color[W, H];
        }

        public void SetBackground(Color color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    BackgroundColor[x, y] = color;
                }
            }
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            int x_offset;
            int y_offset;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (BackgroundColor[x,y] != Color.Black || BackgroundColor[x, y] != null)
                    {
                        x_offset = 219 % TextureGlyphsPrRow * TextureGlyphSize;
                        y_offset = 219 / TextureGlyphsPrRow * TextureGlyphSize;

                        spriteBatch.Draw(texture,
                        new Rectangle((x * FontSize )+ this.X, (y * FontSize) + this.Y, FontSize, FontSize),
                        new Rectangle(x_offset, y_offset, TextureGlyphSize, TextureGlyphSize),
                        BackgroundColor[x,y]);
                    }

                    if (Grid[x,y] == 0) { continue; }

                    x_offset = Grid[x, y] % TextureGlyphsPrRow * TextureGlyphSize;
                    y_offset = Grid[x, y] / TextureGlyphsPrRow * TextureGlyphSize;

                    spriteBatch.Draw( texture,
                        new Rectangle((x * FontSize) + this.X, (y * FontSize)+this.Y, FontSize, FontSize),
                        new Rectangle(x_offset, y_offset, TextureGlyphSize, TextureGlyphSize), 
                        Color.White );
                }
            }
        }

        internal void SetMessage(string text)
        {
            for ( int i = 0; i < text.Length; i++)
            {
                Grid[i, 5] = (byte)text[i];
            }
        }
    }
}