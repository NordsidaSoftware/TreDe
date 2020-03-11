using System;
using System.Collections.Generic;

namespace TreDe
{
    [Serializable]
    public class GameObjectManager
    {
        [NonSerialized]
        public PlayState playState;
        [NonSerialized]
        public Player player;
        [NonSerialized]
        public List<Actor> ActorsList;
        [NonSerialized]
        public GameObject[,,] ActorsGrid;
        /*
         * FOrt før jeg glemmer det. Hver person kan ha en ID, siden det bare er å lagre 
         * en int. PErsoner som ikke er fokusert lagres kun som en abstraksjon
         * samme for utstyr tenker jeg. Kan bare være et item pr. tile, men flere
         * items sammen kan være en pile
         * 
         * 
         * 
         * */
        [NonSerialized]
        public int[,,] ItemGrid;
        [NonSerialized]
        public Dictionary<int, Item> ItemDictionary;
        [NonSerialized]
        readonly Random rnd;


        public void InitializeItems()
        {
            for (int a = 0; a < 100; a++)
            {
                Item i = LoadItemBlueprint.LoadItem("oks", this);
                i.position = new Point3(rnd.Next(0, 100), rnd.Next(0, 100), 0);
                DropItemOnTerrain(i);

                Item j = LoadItemBlueprint.LoadItem("En sekk av strie", this);
                j.position = new Point3(rnd.Next(0, 100), rnd.Next(0, 100), 0);
                DropItemOnTerrain(j);

                Item k = LoadItemBlueprint.LoadItem("En rar greie", this);
                k.position = new Point3(rnd.Next(0, 100), rnd.Next(0, 100), 0);
                DropItemOnTerrain(k);
            }
        }
        public GameObjectManager(PlayState playState)
        {
            Settings s = (Settings)playState.Manager.Game.Services.GetService(typeof(ISettings));
            this.playState = playState;
            rnd = new Random();

            ItemGrid = new int[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            ItemDictionary = new Dictionary<int, Item>();



            ActorsGrid = new GameObject[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            ActorsList = new List<Actor>();
           
        }

        public Actor CreateRandomNPC(int x, int y, int z)
        {
            Actor actor = new Actor(this, new Point3(x, y, z));
            actor = LoadActorBlueprint.Load("NPC", actor);
            NewActor(actor);
            return actor;
        }

        public Player CreateNewPlayer(int x, int y, int z)
        {
            Player player = new Player(this, new Point3(x, y, z));
            this.player = player;
            NewActor(player);
            return player;
        }

        public void DropItemOnTerrain(Item i) // ---> Drop 'i' on grid.
        {
            Item other = GetItemAt(i.position.X, i.position.Y, i.position.Z);

            if (other != null)
            {
                if (other is Pile p)  // ---> put in existing pile or make new pile
                {
                    p.Container.Add(i);
                    return;
                }
                else
                { //       Making a new pile, adding both items and register
                    Pile newPile = new Pile(this, i.position);
                    newPile.Container.Add(other);
                    newPile.Container.Add(i);

                    ItemDictionary[newPile.ID] = newPile;
                    ItemGrid[newPile.position.X, newPile.position.Y, newPile.position.Z] = newPile.ID;
                    return;
                }

            }

            else   //  ---> empty grid, insert new item
            {
                // Register item on grid. Register in lookuptable
                ItemDictionary[i.ID] = i;
                ItemGrid[i.position.X, i.position.Y, i.position.Z] = (i.ID);
            }
        }

        internal void RemoveItemFromTerrain(Point3 position)
        {
            int ID = ItemGrid[position.X, position.Y, position.Z];
            ItemGrid[position.X, position.Y, position.Z] = 0;
            ItemDictionary.Remove(ID);
        }

        internal bool IsActorsGridOccupied(int x, int y, int z)
        {
            return (ActorsGrid[x, y, z] != null);
        }

        internal bool IsItemAt(int x, int y, int z)
        {
            if (ItemGrid[x, y, z] != 0) { return true; }
            else { return false; }
        }
        /// <summary>
        ///  Method to check the itemGrid for an item,
        /// </summary>
       
        /// <returns>Item or pile object</returns>
        internal Item GetItemAt(int x, int y, int z)
        {
            int ID = ItemGrid[x, y, z];
            if (ID == 0) { return null; }
            return ItemDictionary[ID];
        }

        public void NewActor(Actor actor)
        {
            if (actor is Player p) { ActorsList.Insert(0, p); }
            else { ActorsList.Add(actor); }
            ActorsGrid[actor.position.X, actor.position.Y, actor.position.Z] = actor;
        }

        internal void ActorMove(Point3 position, int dx, int dy, int dz)
        {
            ActorsGrid[position.X + dx, position.Y + dy, position.Z + dz] =
            ActorsGrid[position.X, position.Y, position.Z];
            ActorsGrid[position.X, position.Y, position.Z] = null;

        }

        public GameObject GetActorAt(int x, int y, int z)
        {
            return ActorsGrid[x, y, z];
        }

    }
}