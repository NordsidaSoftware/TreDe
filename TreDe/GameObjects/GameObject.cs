using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Text;

namespace TreDe
{
/// <summary>
/// The base class for Items and Actors
/// Fields : position, glyph, color and name
/// Contains a component system
/// </summary>
    public class GameObject
    {
        public GameObjectManager GOmanager;
        public Point3 position;
        public int Glyph;
        public Color color;
        public string Name;
        public List<Component> Components;

        public GameObject(GameObjectManager GOmanager)
        {
            this.GOmanager = GOmanager;
            GOmanager.playState.HappeningEvent += FireEvent;
            Components = new List<Component>();
        }


        public virtual void FireEvent(object sender, HappeningArgs args)
        {
            foreach (Component c in Components)
            {
                if (args.requires == c.typeOfComponent) { c.Execute(sender, args); }
            }

        }

        public virtual Component GetComponent(TypeOfComponent type)
        {
            foreach (Component c in Components)
            {
                if (c.typeOfComponent == type) { return c; }
            }
            return null;
        }

        public virtual void Update(GameTime gameTime) { }

        public override string ToString()
        {
            return Name;
        }
    }
}