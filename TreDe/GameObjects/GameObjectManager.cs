using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TreDe
{
    public class GameObjectManager
    {
        public PlayState playState;
        public Player player;
        public List<Actor> ActorsList;
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


        public int [,,] ItemGrid;
        public Dictionary<int, Item> ItemDictionary;
        
        Random rnd;
     
     
        public GameObjectManager(PlayState playState)
        {
            Settings s = (Settings)playState.Manager.Game.Services.GetService(typeof(ISettings));
            this.playState = playState;
            rnd = new Random();

            ItemGrid = new int[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            ItemDictionary = new Dictionary<int, Item>();
           
            // BSP OR WHAT ???¤%?¤%?
            for (int i = 0; i < 10000; i++)
            {
                Bucket b = new Bucket(this, new Point3(rnd.Next(0, 100), rnd.Next(0,100), 0));
                DropNewItemOnTerrain(b);
            }

            ActorsGrid = new GameObject[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            ActorsList = new List<Actor>();
            player = new Player(this, new Point3(28, 9, 0));
            NewActor(player);

            for (int i = 0; i < 10; i++)
                NewActor(new Actor(this, new Point3(rnd.Next(0, 50),
                                                    rnd.Next(0, 50),
                                                                 0)));
        }

        
        public void DropNewItemOnTerrain(Item i) // ---> Drop 'i' on grid.
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

        internal bool IsActorsGridOccupied(int x, int y, int z)
        {
            return (ActorsGrid[x, y, z] != null);
        }

        internal bool IsItemAt(int x, int y, int z)
        {
            if (ItemGrid[x,y,z] != 0) { return true; }
            else { return false; }
        }
        internal Item GetItemAt(int x, int y, int z)
        {
            int ID = ItemGrid[x, y, z];
            if (ID == 0) { return null; }
            return ItemDictionary[ID];
        }

        private void NewActor(Actor actor)
        {
            ActorsList.Add(actor);
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