using System.Collections.Generic;

namespace TreDe
{
    public interface IContainer { void Add(Item i); bool IsFull(); bool isEmpty();  List<Item> GetItems(); void RemoveItem(Item item); }
    public interface IWield { void Wield(); void Unwield(); void Attack(); bool Wielded(); }

    /// <summary>
    /// Item is a GameObject with a unike Item ID.
    /// </summary>
    public class Item : GameObject
    {
        public static int IDCounter;
        public int ID;
        public Item(GameObjectManager GOmanager) : base(GOmanager)
        {
            ID = GetID();
        }

        public static int GetID() { return ++IDCounter; }
    }
}