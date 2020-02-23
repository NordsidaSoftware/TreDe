using Microsoft.Xna.Framework;
using System;

namespace TreDe
{
    public static class LoadItemBlueprint
    {
        public static Item Load(string type, GameObjectManager GOmanager)
        {
            switch (type)
            {
                case "AXE": { return LoadWeapon(GOmanager); }
                case "BAG": { return LoadContainer(GOmanager); }

            }
            return new Item(GOmanager) { Name = "ERROR ITEM NOT INITIALIZED..." };
        }
        static Item LoadWeapon(GameObjectManager GOManager)
        {
            Weapon w = new Weapon(GOManager);
            w.Glyph = 'x';
            w.color = Color.Red;
            w.Name = "En ganske liten øks";
            return w;
        }
        static Item LoadContainer(GameObjectManager GOManager)
        {

            Container c = new Container(GOManager);
            c.Glyph = 34;
            c.color = Color.Brown;
            c.Name = "En sekk av strie";
            return c;
        }
    }
}