using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class ItemManagerRenderer:Render
    {
        public Display display;
        public ItemManagerRenderer(StateManager Manager):base(Manager)
        {
            display = new Display(0, 0, Manager.Game.GraphicsDevice.Viewport.Width,
                Manager.Game.GraphicsDevice.Viewport.Height, this);
        }
       
        public override void Draw(SpriteBatch spriteBatch)
        {
            display.Draw(spriteBatch);
        }
    }
}

