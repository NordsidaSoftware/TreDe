using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Weapon : Item, IWield
    {
        bool wielded;
        public Weapon(GameObjectManager GOmanager) : base(GOmanager)
        {
            Name = "Weapon not initialized";
            Glyph = 'x';
            color = Color.Red;
        }

        public override void FireEvent(object sender, HappeningArgs args)
        {
            base.FireEvent(sender, args);
        }

        public override Component GetComponent(TypeOfComponent type)
        {
            return base.GetComponent(type);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Attack()
        {
            
        }

       public void Unwield()
        {
            wielded = false;
        }

       public void Wield()
        {
            wielded = true;
        }

        public bool Wielded()
        {
            return wielded;
        }
    }
}
