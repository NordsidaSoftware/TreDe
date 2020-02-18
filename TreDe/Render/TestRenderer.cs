using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TreDe
{
    internal class TestRenderer : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Rectangle Window;           // Size and position of display

        public Texture2D texture;          // Font texture to be used
        public int TextureGlyphsPrRow;     // number of glyphs in each row in font texture
        public int TextureGlyphSize;       // size in px. of each glyph
        public int FontSize;               // size of font on display

        public byte[,] Grid;
        public Color[,] ForegroundColor;
        public Color[,] BackgroundColor;

        private const int MinLine = 3;
        private const int MaxLine = 8;
        private Point Cursor;

        private StateManager Manager;

        public int Width { get { return Window.Width; } }
        public int Height { get { return Window.Height; } }
        public int X { get { return Window.X; } }
        public int Y { get { return Window.Y; } }
        public Point Center { get { return Window.Center; } }
        public TestRenderer(Game game) : base(game)
        {
            texture = game.Content.Load<Texture2D>("cp437T");
        }

        public override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (GameComponent c in Game.Components)
            {
                if (c is StateManager) { int index = Game.Components.IndexOf(c);
                    Manager = (StateManager)Game.Components[index];
                }
            }
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
           if (Manager.currentState is PlayState)
            {
            spriteBatch.Begin();
                spriteBatch.Draw(texture, Vector2.Zero, Color.White);
            spriteBatch.End();

            }
        }
    }
}