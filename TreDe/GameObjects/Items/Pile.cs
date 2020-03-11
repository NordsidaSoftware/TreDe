using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TreDe
{
    /// <summary>
    /// A special Item called a pile for more than one item on same grid
    /// </summary>
    [Serializable]
    public class Pile : Item
    {
        public List<Item> Container;
        public Pile(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            ID = Item.GetID();
            Name = "Pile";
            Glyph = 239;
            color = new int[3] { 100, 100, 50 };
            this.position = position;
            Container = new List<Item>();
        }

        public override void FireEvent(object sender, HappeningArgs args)
        {
            base.FireEvent(sender, args);
        }

        public override Component GetComponent(TypeOfComponent type)
        {
            return base.GetComponent(type);
        }

        public override string ToString()
        {
            if (Container.Count > 0) // <--- Can a pile ever be empty ? :)
            {
                return Name + " containing " + Container.Count.ToString() + " items";
            }
            return Name;
        }


        public void Add(Item i)
        {
            Container.Add(i);
        }


        public List<Item> GetItems()
        {
            return Container;
        }

        public bool IsEmpty()
        {
            return Container.Count == 0;
        }

        public int AmountInPile()
        {
            return Container.Count;
        }

        public void RemoveItem(Item item)
        {
            Container.Remove(item);
        }
    }


}
