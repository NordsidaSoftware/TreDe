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

        
        private Point Cursor;
       
        public int Width { get { return Window.Width; } }
        public int Height { get { return Window.Height; }  }
        public int X { get { return Window.X; } }
        public int Y {  get { return Window.Y; } }
        public Point Center { get { return Window.Center; } }

        public int MinLine { get { return 1; } }
        public int MaxLine {  get { return Height - 1; } }


        
        public Display(int Display_X, int Display_Y, int width, int height, Render r)
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

            SetDefaulBackgroundColor(Color.Black);
            SetDefaulForegroundColor(Color.White);

            Cursor = new Point(0, MinLine);
        }

        public void SetDefaulBackgroundColor(Color color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    BackgroundColor[x, y] = color;
                }
            }
        }

        public void SetDefaulForegroundColor(Color color)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ForegroundColor[x, y] = color;
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
                        ForegroundColor[x,y] );
                }
            }
        }

        //  ===================== TEXT DISPLAY CODE =====================

        internal void Write(string text)
        {
            Write(text, Color.White);
        }
        internal void Write(string text, Color color)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Grid[Cursor.X, Cursor.Y] = (byte)text[i];
                ForegroundColor[Cursor.X, Cursor.Y] = color;
                Cursor.X++;
                if (Cursor.X > Width) { LineShift(); }
            }
        }
        internal void WriteLine(string text, Color color)
        {

            Write(text, color);
            LineShift();
        }

        internal void WriteLine(string text)
        {
            WriteLine(text, Color.White);
        }

        private void LineShift()
        {
            Cursor.Y++;
            Cursor.X = 0;
            if (Cursor.Y > MaxLine) { FlushText(); }
        }

        private void FlushText()
        {
            for ( int x = 0; x < Width; x++)
            {
                for ( int i = 0; i <= MaxLine; i++)
                {
                    if ( i == MaxLine)
                    {
                        Grid[x, MinLine] = Grid[x, i];
                        ForegroundColor[x, MinLine] = ForegroundColor[x, i];
                        BackgroundColor[x, MinLine] = BackgroundColor[x, i];
                    }
                    ClearGrid(x, i);
                }
            }
            Cursor = new Point(0, MinLine+1);
        }

        public void ClearText()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y <= MaxLine; y++)
                {
                    ClearGrid(x, y);
                }
            }
            Cursor = new Point(0, MinLine);
        }

        private void ClearGrid(int x, int y)
        {
            Grid[x, y] = 0;
            ForegroundColor[x, y] = Color.White;
            BackgroundColor[x, y] = Color.Black;
        }
    }
}