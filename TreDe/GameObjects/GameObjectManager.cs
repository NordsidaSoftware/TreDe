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
        Random rnd;
     
     
        public GameObjectManager(PlayState playState)
        {
            Settings s = (Settings)playState.Manager.Game.Services.GetService(typeof(ISettings));
            this.playState = playState;
            rnd = new Random();

            ActorsGrid = new GameObject[s.WorldWidth, s.WorldHeight, s.WorldDepth];
            ActorsList = new List<Actor>();
            player = new Player(this, new Point3(28, 9, 0));
            AddActor(player);
            for (int i = 0; i < 10; i++)
                AddActor(new Actor(this, new Point3(rnd.Next(0, 50),
                                                    rnd.Next(0, 50),
                                                                 0)));
        }

        internal bool IsActorsGridOccupied(int x, int y, int z)
        {
            return (ActorsGrid[x, y, z] != null);
        }

        private void AddActor(Actor actor)
        {
            ActorsList.Add(actor);
            ActorsGrid[actor.position.X, actor.position.Y, actor.position.Z] = actor;
        }

        internal void ActorMove(Point3 position, int dx, int dy)
        {
            ActorsGrid[position.X + dx, position.Y + dy, position.Z] =
            ActorsGrid[position.X, position.Y, position.Z];
            ActorsGrid[position.X, position.Y, position.Z] = null;

        }

        public GameObject getGOAt(int x, int y, int z)
        {
            return ActorsGrid[x, y, z];
        }

    }
}