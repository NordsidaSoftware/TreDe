using System;

namespace TreDe
{
    [Serializable]
    public class Player:Actor
    {
        
        public Player(GameObjectManager GOmanager, Point3 position) 
                                              : base(GOmanager, position)
        {
            Name = "Hiero Protagonist";
            color = new int[3] { 240, 50, 100 };
            Glyph = 2;

            ID = 1;   // Special assignment for the player
            Mass = 70;
        }

    }
}