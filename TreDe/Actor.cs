using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TreDe
{
    public class Actor
    {
        PlayState state;
        public Point position;
        public int TileSize;
        public Texture2D texture;

        public List<Component> components;

        public Actor(PlayState state, Point position, Texture2D texture, int TileSize)
        {
            this.state = state;
            this.texture = texture;
            this.TileSize = TileSize;
            components = new List<Component>();
            this.position = position;
        }

        public virtual void Move(int dx, int dy)
        {
            if (!state.IsOccupied(position.X + dx, position.Y + dy, 0))
            {
                position.X += dx;
                position.Y += dy;
            }
        }


        public virtual void Update(GameTime gameTime)
        { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((position.X*TileSize)- (int)state.CameraPosition.X*TileSize, position.Y*TileSize-(int)state.CameraPosition.Y*TileSize,
                                                      TileSize, TileSize), Color.Gray);

        }
    }
}