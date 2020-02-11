using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Item : GameObject
    {
        public Item(GameObjectManager GOmanager) : base(GOmanager)
        {

        }

        public override void FireEvent(object sender, HappeningArgs args)
        {
            base.FireEvent(sender, args);
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}