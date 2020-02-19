using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe
{
    public interface IContainer { void Add(Item i); bool IsFull(); bool isEmpty();  List<Item> GetItems(); void RemoveItem(Item item); }
    public interface IWield { void Wield(); void Unwield(); void Attack(); }
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