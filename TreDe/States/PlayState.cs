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
        public byte[,,] Terrain;        // 3D map grid of the gameworld terrain

        public GameObjectManager GOmanager;

        public InputHandler input;

        public Vector2 CameraPosition;

        public PhysEngine PhysE;

        // %%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //  EVENT TESTING :

        public delegate void HappeningEventHandler(object sender, HappeningArgs e); //1.declare delegate
        public event HappeningEventHandler HappeningEvent;  //2.declare event

        public void RaiseHappeingEvent(HappeningArgs args)          // 3.method to raise event
        {
            HappeningEvent?.Invoke(this, args);
        }
        // %%%%%%%%%%%%%%%%%%%%%%%%%%%


        

        public override void OnEnter()
        {
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));
            GOmanager = new GameObjectManager(this);
            PhysE = new PhysEngine(this);

            CameraPosition = Vector2.Zero;
           
         
            // World generation 
            // ================

            Terrain = new byte[WorldWidth, WorldHeight, WorldDepth];

            MapReader mr = new MapReader(WorldWidth, WorldHeight, WorldDepth);
            Terrain = mr.ReadMap();
        
        }

        internal bool IsOccupied(int x, int y, int z)
        {
            return ConvertFromByteToTiles.MapByteToTile[Terrain[x, y, z]].blocked;
        }
 
        public override void Update(GameTime gameTime)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                Manager.Pop();
            }

            if (input.WasKeyPressed(Keys.Up)) { GOmanager.player.Move(0, -1, 0); }
            if (input.WasKeyPressed(Keys.Down)) { GOmanager.player.Move(0, 1, 0); }
            if (input.WasKeyPressed(Keys.Left)) { GOmanager.player.Move(-1, 0, 0); }
            if (input.WasKeyPressed(Keys.Right)) { GOmanager.player.Move(1, 0, 0); }

            if (input.IsKeyPressed(Keys.Space)) { PhysE.AddWaterTop(GOmanager.player.position.X + 3,
                                                                    GOmanager.player.position.Y); }

            PhysE.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw agents and world here. The renderer will
            // take the display class ( by IRenderable interface ) and draw to the screen.
        }
    }
}
