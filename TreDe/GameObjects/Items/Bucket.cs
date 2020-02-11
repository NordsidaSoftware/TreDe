using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Bucket : Item
    {
        public Bucket(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            Name = "bucket";
            Glyph = 3;
            color = Color.Yellow;
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

    public class OOPDoor : Item
    {
        public OOPDoor(GameObjectManager GOmanager) : base(GOmanager)
        {
            Name = "TestDoor";
            Glyph = '=';
            color = Color.Black;
                 
        }

        public override void FireEvent(object sender, HappeningArgs args)
        {
            base.FireEvent(sender, args);
        }

        public override Component GetComponent(TypeOfComponent type)
        {
            return base.GetComponent(type);
        }

      
    }
}
