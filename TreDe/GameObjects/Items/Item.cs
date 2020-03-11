using System;

namespace TreDe
{
    /// <summary>
    /// Item is a GameObject with an ID. 
    /// Static method GetID kan assign next id
    /// </summary>
    [Serializable]
    public class Item : GameObject
    {
        public static int IDCounter;
        public int ID { get; set; }
        public Item(GameObjectManager GOmanager) : base(GOmanager){  }

        public static int GetID() { return ++IDCounter; }

    }
}