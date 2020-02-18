using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Item : GameObject
    {
        public static int IDCounter;
        public int ID;
        public Item(GameObjectManager GOmanager) : base(GOmanager)
        {
            ID = GetID();
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

        public static int GetID() {return ++IDCounter; }
    }
}