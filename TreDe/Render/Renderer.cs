using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{

    public abstract class Render
    {
        // TEXTURE FONT DATA //
        public Texture2D texture;   // texture of font
        public int TextureTiles;    // number of glyphs in each row in font texture
        public int TextureTileSize; // size in px. of each glyph
        
        // TILE DATA //
        public int TileSize;       // Size of tile on display

        // SCREEN DATA //
        public int ScreenTilesX;   // NR of tiles on x axis
        public int ScreenTilesY;   // NR of tiles on y axis

        public virtual void Draw(SpriteBatch spriteBatch) { }

        public Render(StateManager Manager)
        {
            Settings s = (Settings)Manager.Game.Services.GetService(typeof(ISettings));
            texture = Manager.Game.Content.Load<Texture2D>(s.Font);

            TileSize = s.TileSize;
            TextureTiles = s.TextureTiles;
            TextureTileSize = s.TextureTileSize;
        }
    }
}

