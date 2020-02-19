using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class PlayStateRender: Render
    {
        public PlayState renderTarget;
        private Display LowerTextDisplay;

        // PERSPECTIVE EFFECT DATA //
        public float TopScale;      // Scale of top layer
        public FPoint[,] ScaledPoints; // precalculated scaled points

        public Vector2 Origin;      // CAMERA TOP LEFT POSITION

        // PLAYSTATE WORLD DATA //
        public int TilesWidth;
        public int TilesHeight;
        public int TilesDepth;

        
        public PlayStateRender(StateManager Manager, PlayState renderTarget):base(Manager)
        {
            this.renderTarget = renderTarget;
            this.renderTarget.HappeningEvent += RecieveTextFromGame;
           
            Point TileScreen;
            TopScale = 1.2f;

            TileScreen = new Point(Manager.Game.GraphicsDevice.Viewport.Width,
                                  Manager.Game.GraphicsDevice.Viewport.Height-150);

            Settings s = (Settings)Manager.Game.Services.GetService(typeof(ISettings));
            TilesWidth = s.WorldWidth;
            TilesHeight = s.WorldHeight;
            TilesDepth = s.WorldDepth;

            Origin = Vector2.Zero;

            ScreenTilesX = TileScreen.X / TileSize;
            ScreenTilesY = TileScreen.Y / TileSize;

            LowerTextDisplay = new Display(0, 
                Manager.Game.GraphicsDevice.Viewport.Height - 150, 
                Manager.Game.GraphicsDevice.Viewport.Width,
                150, this);


            CalculateScaledPoints();

        }

        private void CalculateScaledPoints()
        {
            // Precalculate the scale offset for each grid on the screen
            // ===========================================================
            ScaledPoints = new FPoint[ScreenTilesX, ScreenTilesY];

            float dx;
            float dy;

            for (int x = -ScreenTilesX / 2; x < ScreenTilesX / 2; x++)
            {
                for (int y = -ScreenTilesY / 2; y < ScreenTilesY / 2; y++)
                {
                    dx = (x) * TopScale;
                    dy = (y) * TopScale;

                    ScaledPoints[x + (ScreenTilesX / 2), y + (ScreenTilesY / 2)] = new FPoint(dx, dy);
                }
            }
        }

        private void RecieveTextFromGame(object sender, HappeningArgs happening)
        {
            LowerTextDisplay.WriteLine(happening.text, Color.Blue) ;
        }

        internal void MoveCamera(int dx, int dy)
        {
            if (Origin.X <= 0 && dx == -1) { return; }
            if (Origin.Y <= 0 && dy == -1) { return; }
            if (Origin.X / TileSize >= TilesWidth && dx == 1) { return; }
            if (Origin.Y / TileSize >= TilesHeight && dy == 1) { return; }

            Origin.X += dx * TileSize;
            Origin.Y += dy * TileSize;

            renderTarget.CameraPosition.X = (Origin.X / TileSize);
            renderTarget.CameraPosition.Y = (Origin.Y / TileSize);

        }

        internal void Update()
        {
            if (renderTarget.GOmanager.player.position.X - renderTarget.CameraPosition.X > ScreenTilesX - 10)
            {
                MoveCamera(1, 0);
            }

            if (renderTarget.GOmanager.player.position.X - renderTarget.CameraPosition.X < 10)
            {
                MoveCamera(-1, 0);
            }

            if (renderTarget.GOmanager.player.position.Y - renderTarget.CameraPosition.Y < 5)
            {
                MoveCamera(0, -1);
            }

            if (renderTarget.GOmanager.player.position.Y - renderTarget.CameraPosition.Y > ScreenTilesY - 5)
            {
                MoveCamera(0, 1);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            float TopX, TopY, perspectiveX, perspectiveY;
            int strX_offset, strY_offset;

            // DRAW TERRAIN //

            Tile tile;
            Rectangle rectangle;

            for (int x = 0; x < ScreenTilesX; x++)
            {
                for (int y = 0; y < ScreenTilesY; y++)
                {
                    var tileX = x + ((int)Origin.X / TileSize);
                    var tileY = y + ((int)Origin.Y / TileSize);
                    if (tileX < 0 || tileY < 0) { continue; }
                    if (tileX >= TilesWidth || tileY >= TilesHeight) { continue; }


                    TopX = ScaledPoints[x, y].X;
                    TopY = ScaledPoints[x, y].Y;

                    rectangle = new Rectangle(x * TileSize, y * TileSize, TileSize, TileSize);

                    for (int z = 0; z < TilesDepth; z++)
                    {
                        tile = TileManager.MapByteToTile[renderTarget.Terrain[tileX, tileY, z]];
                        strX_offset = tile.charPart % TextureTiles * TextureTileSize;
                        strY_offset = tile.charPart / TextureTiles * TextureTileSize;

                        perspectiveX = TopX / TilesDepth;
                        perspectiveY = TopY / TilesDepth;
                        rectangle.Offset(perspectiveX * z, perspectiveY * z);

                        
                        spriteBatch.Draw(texture, rectangle,
                                         new Rectangle(strX_offset, strY_offset, TextureTileSize, TextureTileSize),

                                         new Color(tile.color, (1.0f - z / 8.0f)));



                        if (renderTarget.PhysE.WaterGrid[tileX, tileY, z])
                        {
                            strX_offset = 219 % TextureTiles * TextureTileSize;
                            strY_offset = 219 / TextureTiles * TextureTileSize;

                            spriteBatch.Draw(texture, rectangle,
                                         new Rectangle(strX_offset, strY_offset, TextureTileSize, TextureTileSize),

                                         new Color(Color.LightBlue, (1.0f - z / 8.0f)));
                        }

                        
                        Item item = renderTarget.GOmanager.GetItemAt(tileX, tileY, z);
                        if ( item != null)
                        {
                           
                            
                                strX_offset = item.Glyph % TextureTiles * TextureTileSize;
                                strY_offset = item.Glyph / TextureTiles * TextureTileSize;

                                spriteBatch.Draw(texture, rectangle,
                                             new Rectangle(strX_offset, strY_offset, TextureTileSize, TextureTileSize),

                                             new Color(item.color, (1.0f - z / 8.0f)));
                            
                        }
                        
                        GameObject GO = renderTarget.GOmanager.GetActorAt(tileX, tileY, z);
                        if (GO != null)
                        {
                            strX_offset = GO.Glyph % TextureTiles * TextureTileSize;
                            strY_offset = GO.Glyph / TextureTiles * TextureTileSize;

                            spriteBatch.Draw(texture, rectangle,
                                         new Rectangle(strX_offset, strY_offset, TextureTileSize, TextureTileSize),

                                         new Color(GO.color, (1.0f - z / 8.0f)));
                        }
                        
                    }
                }
            }


            LowerTextDisplay.Draw(spriteBatch);
        }
    }
}

