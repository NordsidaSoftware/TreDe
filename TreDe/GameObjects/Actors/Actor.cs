using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Actor : GameObject
    {
        public string Name;
        public Actor(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            Name = "NPC";
        }


        public virtual void Move(int dx, int dy, int dz)
        {
            if (!GOmanager.playState.IsOccupied(position.X + dx, position.Y + dy, position.Z + dz))
            {
                if (!GOmanager.IsActorsGridOccupied(position.X + dx, position.Y + dy, position.Z + dz))
                {
                    GOmanager.ActorMove(position, dx, dy);
                    position.X += dx;
                    position.Y += dy;
                }
            }
            // if (state.GetGameObjectAt(position.X, position.Y, position.Z) != null) { }
        }

        public override string ToString()
        {
            return Name;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
