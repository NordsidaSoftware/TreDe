using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TreDe
{
    public interface IContainer { void Add(Item i); bool IsFull(); List<Item> GetItems(); }
    public class Bucket : Item, IContainer
    {
        Item Contained;
        public Bucket(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            Name = "bucket";
            Glyph = 3;
            color = Color.Yellow;
        }

        public override string ToString()
        {

            if (Contained != null)
                return Name + " containing a " + Contained.ToString();
            else return base.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        void IContainer.Add(Item i)
        {
            Contained = i;
        }

        List<Item> IContainer.GetItems()
        {
            return new List<Item>() { Contained };
        }

        bool IContainer.IsFull()
        {
            if (Contained != null) { return true; }
            else return false;
        }
    }


}
