using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Player:Actor
    { 
        public Player(State state, Point position, Texture2D texture, int TileSize) 
                                              : base((PlayState)state, position, texture, TileSize)
        {  }
    }
}