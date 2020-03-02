namespace TreDe
{
    /// <summary>
    /// Item is a GameObject with a unique Item ID.
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