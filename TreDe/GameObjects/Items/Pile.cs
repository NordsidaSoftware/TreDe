using System.Collections.Generic;

namespace TreDe
{
    public class Pile : Item, Container
    {
        public List<Item> Container;
        public Pile(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            Name = "Pile";
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


        void Container.Add(Item i)
        {
            Container.Add(i);
        }


        List<Item> Container.GetItems()
        {
            return Container;
        }

        bool Container.IsFull()
        {
            return false; // <--- infinite pile...
        }
    }


}
