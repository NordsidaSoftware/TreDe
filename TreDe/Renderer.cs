using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Renderer
    {
        public Texture2D texture;   // texture of font
        public int TextureTiles;    // number of glyphs in each row in font texture
        public int TextureTileSize; // size in px. of each glyph

        public float TopScale;      // Scale of top layer
        public int TilesWidth;
        public int TilesHeight;
        public int TileSize;       // Size of tile on display

        public int ScreenTilesX;
        public int ScreenTilesY;

        public Vector2 Origin;

        public fPoint[,] ScaledPoints; // precalculated scaled points
        public int Levels;


        private PlayState renderTarget;
        

        public Renderer(StateManager Manager, IRenderable renderTarget,  Settings settings)
        {
            Vector2 Screen;
            texture = Manager.Game.Content.Load<Texture2D>("cp437T");
            this.TileSize = settings.TileSize;
            TextureTiles = 16;
            TextureTileSize = 10;
            TopScale = 1.2f;
            Levels = 8;

            this.renderTarget = (PlayState)renderTarget;
            Screen = new Vector2(Manager.Game.GraphicsDevice.Viewport.Width,
                                  Manager.Game.GraphicsDevice.Viewport.Height);
            TilesWidth = this.renderTarget.Grid.GetLength(0);
            TilesHeight = this.renderTarget.Grid.GetLength(1);

            Origin = Vector2.Zero;

            ScreenTilesX = (int)Screen.X / TileSize;
            ScreenTilesY = (int)Screen.Y / TileSize;
            

            // Precalculate the scale offset for each grid on the screen
            // ===========================================================
            ScaledPoints = new fPoint[ScreenTilesX, ScreenTilesY];

            float dx;
            float dy;

            for (int x = -ScreenTilesX / 2; x < ScreenTilesX / 2; x++)
            {
                for (int y = -ScreenTilesY / 2; y < ScreenTilesY / 2; y++)
                {
                    dx = (x) * TopScale;
                    dy = (y) * TopScale;

                    ScaledPoints[x + (ScreenTilesX / 2), y + (ScreenTilesY / 2)] = new fPoint(dx, dy);
                }
            }

            // =======================================================
        }

        internal void MoveCamera(int dx, int dy)
        {
            if (Origin.X <= 0 && dx == -1) { return; }
            if (Origin.Y <= 0 && dy == -1) { return; }
            if (Origin.X / TileSize >= TilesWidth && dx == 1) { return; }
            if (Origin.Y / TileSize >= TilesHeight && dy == 1) { return; }

            Origin.X += dx * TileSize;
            Origin.Y += dy * TileSize;
           
           renderTarget.CameraPosition.X = (Origin.X/TileSize);
           renderTarget.CameraPosition.Y = (Origin.Y/TileSize);

        }

        internal void Update()
        {
            if (renderTarget.player.position.X - renderTarget.CameraPosition.X > ScreenTilesX-10)
            {
                MoveCamera(1, 0);
            }

            if (renderTarget.player.position.X - renderTarget.CameraPosition.X < 10)
            {
                MoveCamera(-1, 0);
            }

            if (renderTarget.player.position.Y - renderTarget.CameraPosition.Y < 5)
            {
                MoveCamera(0, -1);
            }

            if (renderTarget.player.position.Y - renderTarget.CameraPosition.Y > ScreenTilesY - 5)
            {
                MoveCamera(0, 1);
            }
        }

            public void Draw(SpriteBatch spriteBatch)
        {
            float TopX, TopY, perspectiveX, perspectiveY;
            int strX_offset, strY_offset;
            Tile[] structure = new Tile[8];

            for (int x = 0; x < ScreenTilesX; x++)
            {
                for (int y = 0; y < ScreenTilesY; y++)
                {
                    var tileX = x + ((int)Origin.X /TileSize);
                    var tileY = y + ((int)Origin.Y /TileSize);
                    if (tileX < 0 || tileY < 0) { continue; }
                    if (tileX >= TilesWidth || tileY >= TilesHeight) { continue; }
                

                    TopX = ScaledPoints[x, y].X;
                    TopY = ScaledPoints[x, y].Y;


                    
                    for ( int i = 0; i < Levels; i++)
                    {
                        if (!renderTarget.PhysE.WaterGrid[tileX, tileY, i])
                        structure[i] = ConvertFromByteToTiles.MapByteToTile[renderTarget.Grid[tileX, tileY, i]];
                        else
                        {
                            structure[i] = ConvertFromByteToTiles.Water;
                        }
                    }

                    Rectangle r = new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);

                    for (int layer = 0; layer < 8; layer++)
                    {
                        if (structure[layer].Equals(ConvertFromByteToTiles.Empty)) { continue; }

                        strX_offset = structure[layer].charPart % TextureTiles * TextureTileSize;
                        strY_offset = structure[layer].charPart / TextureTiles * TextureTileSize;

                        perspectiveX = TopX / Levels;
                        perspectiveY = TopY / Levels;
                        r.Offset(perspectiveX * layer, perspectiveY * layer);

                        spriteBatch.Draw(texture, r,
                                         new Rectangle(strX_offset, strY_offset, TextureTileSize, TextureTileSize),

                                         new Color(structure[layer].color, (1.0f-layer/8.0f) ));
                                           
                        

                    }
                }
            }
        }
    }
}

