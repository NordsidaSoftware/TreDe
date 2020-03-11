using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreDe
{
    
    public static class LoadItemBlueprint
    {
       public static Item LoadItem(string search, GameObjectManager GOmanager)
        {
            foreach (ItemPrefab i in ReadFromRaw.itemList)
            {
                if (i.Name == search)
                {
                    Item item = new Item(GOmanager);
                    item.Name = i.Name;
                    item.Glyph = i.Glyph;
                    item.color = i.Color;
                    foreach (Component c in i.Components)
                    {
                        c.owner = item;
                        item.Components.Add(c);
                    }
                    item.ID = Item.GetID();
                    return item;
                }
            }
            return null;
        }
    }

      
}