using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace TreDe
{
    public enum ItemTypes
    {
        WEAPON, CONTAINER
    }
    public static class ReadFromRaw
    {
        public static List<Item> itemList = new List<Item>();
        public static void Read(GameObjectManager GOmanager)
        {
            string filename = @"C: \Users\kroll\source\repos\TreDe\TreDe\Items.txt";
            string[] rawsource = File.ReadAllLines(filename);
            string[] scannedsource = Scan(rawsource);
            Dictionary<int, string[]> splitItems = SplitItems(scannedsource);

            itemList = CreateItems(splitItems, GOmanager);

        }

        private static List<Item> CreateItems(Dictionary<int, string[]> dict, GameObjectManager GOmanager)
        {
            List<Item> itemPrefabs = new List<Item>();
            for (int i = 0; i < dict.Count; i++)
            {
                Item Prefab = new Item(GOmanager);
                foreach (string s in dict[i])
                {
                    string[] tokens = s.Split(':');
                    if (tokens[0].Trim() == "NAME") { Prefab.Name = tokens[1].Trim(); }
                    if (tokens[0].Trim() == "GLYPH") { Prefab.Glyph = Convert.ToInt32(tokens[1].Trim()); }
                    if (tokens[0].Trim() == "COLOR")
                    {
                        string[] values = tokens[1].Split(',');
                        Prefab.color = new Color(
                            Convert.ToInt32(values[0].Trim()),
                            Convert.ToInt32(values[1].Trim()),
                            Convert.ToInt32(values[2].Trim())
                            );
                    }

                    if (tokens[0].Trim() == "WEAPON")
                    {
                        WeaponComponent WC = new WeaponComponent(TypeOfComponent.WEAPON, Prefab);
                        foreach (string weaponString in dict[i])
                        {
                            string[] weaponTokens = weaponString.Split(':');
                            if (weaponTokens[0].Trim() == "EDGE")
                            {
                                string[] weaponvalues = weaponTokens[1].Split(',');
                                WC.Attacks.Add(
                                         new Attack("EDGE",
                                         Convert.ToInt32(weaponvalues[0].Trim()),
                                         Convert.ToInt32(weaponvalues[1].Trim())
                                         )
                                     );
                            }
                        }
                        Prefab.Components.Add(WC);
                    }
                    if (tokens[0].Trim() == "CONTAINER")
                    {
                        ContainerComponent CC = new ContainerComponent(TypeOfComponent.CONTAINER, Prefab);
                        foreach (string containerString in dict[i])
                        {
                            string[] containerTokens = containerString.Split(':');
                            if (containerTokens[0].Trim() == "CAPACITY")
                            {
                                CC.MaxCapacity = Convert.ToInt32(containerTokens[1]);
                              
                            }
                        }

                        Prefab.Components.Add(CC);
                    }

                }
                itemPrefabs.Add(Prefab);
            }
            return itemPrefabs;
        }

        private static Dictionary<int, string[]> SplitItems(string[] scannedsource)
        {
            Dictionary<int, string[]> splitted = new Dictionary<int, string[]>();
            int itemnr = 0;
            bool Record = false;
            List<string> recorded = new List<string>();
            for (int i = 0; i < scannedsource.Length; i++)
            {
                if (scannedsource[i].Contains("["))
                {
                    Record = true;
                }
                if (scannedsource[i].Contains("]"))
                {
                    Record = false;
                    splitted[itemnr++] = recorded.ToArray();
                    recorded.Clear();
                }
                if (Record) { recorded.Add(scannedsource[i]); }

            }
            return splitted;
        }

        public static string[] Scan(string[] rawsource)
        {
            List<string> scannedSource = new List<string>();
            foreach (string s in rawsource)
            {
                if (s.Trim().StartsWith("#")) { continue; }
                if (s.Trim().Length > 0) { scannedSource.Add(s.Trim()); }
            }

            return scannedSource.ToArray();
        }
    }
}
