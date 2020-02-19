using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Axe : Item, IWield
    {
        bool wielded;
        public Axe(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            Name = "Axe";
            Glyph = 34;
            this.position = position;
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

        void IWield.Attack()
        {
            
        }

        void IWield.Unwield()
        {
            wielded = false;
        }

        void IWield.Wield()
        {
            wielded = true;
        }
    }
}
