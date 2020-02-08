﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Player:Actor
    {
        
        public Player(GameObjectManager GOmanager, Point3 position) 
                                              : base(GOmanager, position)
        {
         
            color = Color.Yellow;
            Glyph = 2;
        }

    }
}