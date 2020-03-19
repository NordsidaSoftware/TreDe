namespace TreDe
{
    public static class LoadItemBlueprint
    {
       public static Item LoadItem(string search, GameObjectManager GOmanager)
        {
            foreach (ItemPrefab prefab in ReadFromRaw.itemList)
            {
                if (prefab.Name == search)
                {
                    Item item = new Item(GOmanager);
                    item.Name = prefab.Name;
                    item.Glyph = prefab.Glyph;
                    item.color = prefab.Color;
                    item.Mass = prefab.Mass;
                    foreach (Component c in prefab.Components)
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