using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TreDe
{
    public class SplashScreenState : State
    {
        InputHandler input;
        ItemManagerRenderer renderer;
        
        public SplashScreenState()
        {
           
        }

        public override void OnEnter()
        {
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));
            renderer = new ItemManagerRenderer(Manager);
            
        }

        public override void Update(GameTime gameTime)
        {
            if (input.WasKeyPressed(Keys.Enter))
            {
                Manager.Push(new PlayState());

            }
            if (input.WasKeyPressed(Keys.Escape))
            {
                Manager.Pop();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.Draw(spriteBatch);
        }
    }
}
