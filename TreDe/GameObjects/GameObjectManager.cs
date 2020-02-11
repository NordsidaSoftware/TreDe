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
        public List<Item>[,,] ItemGrid;
     

        Random rnd;
     
     
        public GameObjectManager(PlayState playState)
        {
            Settings s = (Settings)playState.Manager.Game.Services.GetService(typeof(ISettings));
            this.playState = playState;
            rnd = new Random();

         
            ItemGrid = new List<Item>[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            for ( int x = 0; x < s.WorldWidth;x++)

            {
                for (int y = 0; y < s.WorldHeight; y++)
                {
                    for (int z = 0; z < s.WorldDepth; z++)
                    {
                        ItemGrid[x, y,z] = new List<Item>();
                    }
                }
            }

            

            // BSP OR WHAT ???¤%?¤%?
            for (int i = 0; i < 1000; i++)
            {
                Bucket b = new Bucket(this, new Point3(rnd.Next(0, s.WorldWidth),
                    rnd.Next(0, s.WorldHeight), rnd.Next(0, s.WorldDepth)));
                NewItem(b);
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

        public void NewItem(Item i)
        {
         
            ItemGrid[i.position.X, i.position.Y, i.position.Z].Add(i);
        }

        internal bool IsActorsGridOccupied(int x, int y, int z)
        {
            return (ActorsGrid[x, y, z] != null);
        }

        internal List<Item> GetItemsAt(int x, int y, int z)
        {
            return ItemGrid[x, y, z];
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