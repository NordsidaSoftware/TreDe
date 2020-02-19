using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe
{
    public class Actor : GameObject
    {
        public List<Item> Inventory;
        public Actor(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            this.Glyph = 1;
            Name = "NPC";
            Inventory = new List<Item>();
        }


        public virtual void Move(int dx, int dy, int dz)
        {
            
            // 1.Check if tile is blocked
            if (GOmanager.playState.IsBlocked(position.X + dx, position.Y + dy, position.Z + dz))
            {  
                // 2. if blocked, can it be interacted with ?
                GOmanager.playState.Interact(position.X + dx, position.Y + dy, position.Z + dz);
            }

           else // 3.tile is not blocked
            {      
            
                // 4. check if tile is occupied by other actor.
                if (!GOmanager.IsActorsGridOccupied(position.X + dx, position.Y + dy, position.Z + dz))
                {   
                    // 5. Not occupied, move into tile
                    GOmanager.ActorMove(position, dx, dy, dz);
                    position.X += dx;
                    position.Y += dy;
                    position.Z += dz;

                    // 6. Interact with tile just exited : (close doors )
                    GOmanager.playState.Interact(position.X, position.Y, position.Z);
                }
            }


            // Finally : Send text to render about items in tile. Just a test.
            // TEST EVENT DRIVEN MESSAGE TO RENDERER :
            if (GOmanager.IsItemAt(position.X, position.Y, position.Z))
            {
            GOmanager.playState.RaiseHappeningEvent(
                new HappeningArgs(
                    TypeOfComponent.TextMessage, "ITEM : " 
                    + GOmanager.GetItemAt(position.X, position.Y, position.Z)));
            }
                
        }

        public void PickupItem()
        {
            // 1.Check if tile contains an Item
            if (GOmanager.IsItemAt(position.X, position.Y, position.Z)){

                // 2. Pickup item, move to inventory, remove from ItemGrid
                // need more advanced algorithm to sort out what to to do.
                Inventory.Add(GOmanager.GetItemAt(position.X, position.Y, position.Z));
                GOmanager.RemoveItemAt(position);
            }

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
