using Microsoft.Xna.Framework;

namespace TreDe
{
    public class Actor : GameObject
    {
        
        public Actor(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            this.Glyph = 1;
            Name = "NPC";
        }


        public virtual void Move(int dx, int dy, int dz)
        {
            
            if (GOmanager.playState.IsBlocked(position.X + dx, position.Y + dy, position.Z + dz))
            {

                /// HERE IS THE FRONTIER ! //
                GOmanager.playState.Interact(position.X + dx, position.Y + dy, position.Z + dz);
                
                   
                
            }

           else
            {
                if (!GOmanager.IsActorsGridOccupied(position.X + dx, position.Y + dy, position.Z + dz))
                {
                    GOmanager.ActorMove(position, dx, dy, dz);
                    position.X += dx;
                    position.Y += dy;
                    position.Z += dz;

                    GOmanager.playState.Interact(position.X, position.Y, position.Z);
                }
            }

            // TEST EVENT DRIVEN MESSAGE TO RENDERER :
            GOmanager.playState.RaiseHappeningEvent(new HappeningArgs(TypeOfComponent.TEST, "MOVE: " + position.ToString()));
                

            // =============================
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
    }
}
