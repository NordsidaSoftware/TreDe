using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TreDe
{
    
    public static class LoadItemBlueprint
    {
       public static Item LoadItem(string search)
        {
            foreach (Item i in ReadFromRaw.itemList)
            {
                if (i.Name == search)
                {
                    Item item = new Item(i.GOmanager);
                    int id = item.ID;
                    item = i;
                    item.ID = id;
                    return item;
                }
            }
            return null;
        }
    }

      
}