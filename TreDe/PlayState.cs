using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TreDe
{
    public interface IRenderable  {   }
  
    public class PlayState : State, IRenderable
    {

        int WorldWidth = 400;
        int WorldHeight = 200;
        int WorldDepth = 8;
        public byte[,,] Grid;        // 3D map grid of the gameworld

        public Player player;
        public List<Actor> actors;

        public InputHandler input;

        public Vector2 CameraPosition;

        public PhysEngine PhysE;

        public override void Draw(SpriteBatch spriteBatch)
        {
            player.Draw(spriteBatch);

            // draw agents and world here. The renderer will
            // take the grid ( by IRenderable interface ) and draw to the screen.
        }

        internal bool IsOccupied(int x, int y, int z)
        {
            return ConvertFromByteToTiles.MapByteToTile[Grid[x, y, z]].blocked;
        }


        public override void OnEnter()
        {

            actors = new List<Actor>();
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));
         
            // World generation 
            // ================

          
            Grid = new byte[WorldWidth, WorldHeight, WorldDepth];
            CameraPosition = Vector2.Zero;

            MapReader mr = new MapReader(WorldWidth, WorldHeight, 8);
            Grid = mr.ReadMap();
          

            // Player generation
            //=====================
            // TODO : if grid movement, no need for texture grab...
            // extracts the correct glyph and makes a new texture.
            Texture2D tex = new Texture2D(Manager.Game.GraphicsDevice, 10, 10);
            Texture2D origin = Manager.Game.Content.Load<Texture2D>("cp437T");
            Color[] data = new Color[10 * 10];
            Rectangle extractRectangle = new Rectangle(10, 0, 10, 10);
            origin.GetData(0, extractRectangle, data, 0, data.Length);
            tex.SetData(data);

            Settings s = (Settings)Manager.Game.Services.GetService(typeof(ISettings));
            player = new Player(this, new Point(28, 9), tex, s.TileSize);

            PhysE = new PhysEngine(this);
            
           
        }

       
        
        public override void Update(GameTime gameTime)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                Manager.Pop();
            }

            if (input.WasKeyPressed(Keys.Up)) { player.Move(0, -1); }
            if (input.WasKeyPressed(Keys.Down)) { player.Move(0, 1); }
            if (input.WasKeyPressed(Keys.Left)) { player.Move(-1, 0); }
            if (input.WasKeyPressed(Keys.Right)) { player.Move(1, 0); }

            if (input.WasKeyPressed(Keys.Space)) { PhysE.AddWaterTop(player.position.X + 3, player.position.Y); }

            PhysE.Update(gameTime);
        }
    }
}
