using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;

namespace TreDe
{

    public class Container : Item, IContainer
    {
        List<Item> Contains;
        int Capacity;
        public Container(GameObjectManager GOmanager) : base(GOmanager)
        {
            Contains = new List<Item>();
            Capacity = 3; // default container capacity...
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            if (Contains.Count > 0)
            {
                foreach(Item item in Contains)
                {
                    sb.Append(item.Name + ", ");
                }
            }
            return sb.ToString();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        void IContainer.Add(Item i)
        {
            Contains.Add(i);
        }

        List<Item> IContainer.GetItems()
        {
            return Contains;
        }

        bool IContainer.isEmpty()
        {
            return Contains.Count == 0;
        }

        bool IContainer.IsFull()
        {
            if (Contains.Count >= Capacity) { return true; }
            else return false;
        }

        void IContainer.RemoveItem(Item item)
        {
            Contains.Remove(item);
        }

        void IContainer.SetCapacity(int Capacity)
        {
            this.Capacity = Capacity;
        }
    }


}
