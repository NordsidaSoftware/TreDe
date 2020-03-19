using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace TreDe
{
    /// <summary>
    /// Actor inherits from GameObject. Fields : Inventory.
    /// </summary>
    [Serializable]
    public class Actor : GameObject
    {
        public static int IDCounter = 1;   // ID = 1 is reserved for the player

        public static int GetID() { return ++IDCounter; }
        public List<Item> Inventory { get; set; }
        public int ID { get; set; }

        public Actor(GameObjectManager GOmanager, Point3 position) : base(GOmanager)
        {
            this.position = position;
            Glyph = 1;
            color = new int[3] { 100, 100, 100 };
            Name = "Name not initialized";

            ID = GetID();

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
                if (GOmanager.IsActorsGridOccupied(position.X + dx, position.Y + dy, position.Z + dz))
                {
                    // 5. grid occupied by other actor. BUMP! For now all bumps
                    // is treated as an attack. Later, allies should just switch
                    // place with moving actor.
                    if (true) // = enemy
                    {  // 6. Attack !
                        Actor opponent = GOmanager.GetActorAt(position.X + dx, position.Y + dy, position.Z + dz);
                        Attack(opponent);
                    }
                    else { } // =  ally, switch place.
                }

                else
                {
                    // 7. Not occupied, move into tile
                    GOmanager.ActorMove(position, dx, dy, dz);
                    Point3 newPoint = position;
                    newPoint.X += dx;
                    newPoint.Y += dy;
                    newPoint.Z += dz;
                    position = newPoint;

                    // 8. Interact with tile just exited : (close doors )
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

        private void Attack(Actor opponent)
        {
            GOmanager.playState.RaiseHappeningEvent(
                new HappeningArgs(
                TypeOfComponent.TextMessage, "Attack registered  : "
                + opponent.ToString()));

            foreach (Item item in Inventory)
            {
                // pr now : all available attacks are launched...
                WeaponComponent wc = (WeaponComponent)item.GetComponent(TypeOfComponent.WEAPON);
                if (wc != null && wc.wielded)
                {
                    foreach (Attack attack in wc.Attacks)
                    {
                        GOmanager.playState.RaiseHappeningEvent(
                            new HappeningArgs(
                            TypeOfComponent.TextMessage, attack.type));
                        // ==================================
                        // IMPACT IS CALCULATED FROM THE FORMULA
                        // p = mV   ( m - mass(Kg), V - velocity(m/s) )
                        // V = 66 m/s - (str - m(Kg) ) - rnd(15)
                        // 66 m/s max. velocity 
                        // str - actor mass(Kg) / 10 ( avg for human : 7 )

                        float VelocityAtImpact = 66 - (Mass / 10 - wc.owner.Mass / 1000.0f);
                        float impact = (wc.owner.Mass/1000.0f) * VelocityAtImpact;
                        GOmanager.playState.RaiseHappeningEvent(
                           new HappeningArgs(
                           TypeOfComponent.TextMessage, "impact="+ impact.ToString()+" Kg m/s"));

                        opponent.RecieveDamage(impact);
                    }
                }
            }
        }

        private void RecieveDamage(float impact)
        {
            BodyComponent bc = (BodyComponent)GetComponent(TypeOfComponent.BODY);
            if (bc != null)
            {
                BodyPart bodypart = bc.GetRandomPart();
                bodypart.ApplyDamage(impact);

                GOmanager.playState.RaiseHappeningEvent(
                          new HappeningArgs(
                          TypeOfComponent.TextMessage, " -> " + bodypart.Name));

                List<BodyPart> test = bc.GetPartsFromDirection(Directions.BACKWARD);
                List<BodyPart> test2 = bc.GetPartsFromDirection(Directions.LEFT);

            }
        }

        public void PickupItem()
        {
            // 1.Check if tile contains an Item
            if (GOmanager.IsItemAt(position.X, position.Y, position.Z))
            {
                // 2. If item is a pile enter new container manager state :
                if (GOmanager.GetItemAt(position.X, position.Y, position.Z) is Pile p)
                {
                    GOmanager.playState.Manager.Push(new PileManagerState(this, p));
                    return;  //<--- break from method here
                }
                // 3. if item is a pile and not empty enter new pile manager state:
                if (GOmanager.GetItemAt(position.X, position.Y, position.Z) is Pile pile)
                {
                    if (!pile.IsEmpty())
                    {
                        GOmanager.playState.Manager.Push(new PileManagerState(this, pile));
                        return;  // <--- break from method here
                    }
                }

                // 4. Pickup item, move to inventory, remove from ItemGrid
                // need more advanced algorithm to sort out what to to do.
                Inventory.Add(GOmanager.GetItemAt(position.X, position.Y, position.Z));
                GOmanager.RemoveItemFromTerrain(position);

            }

        }
        internal void DropItem(Item selected)
        {
            selected.position = position;
            GOmanager.DropItemOnTerrain(selected);
            Inventory.Remove(selected);
        }
        internal void Wield(Item item)
        {
            if (item.GetComponent(TypeOfComponent.WEAPON) != null)
            {
                WeaponComponent wc = (WeaponComponent)item.GetComponent(TypeOfComponent.WEAPON);
                wc.wielded = !wc.wielded;
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
