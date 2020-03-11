using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TreDe
{
/// <summary>
/// The base class for Items and Actors
/// Fields : position, glyph, color and name
/// Contains a component system
/// </summary>
[Serializable]
    public class GameObject
    {
        public GameObjectManager GOmanager;
        public Point3 position { get; set; }
        public int Glyph { get; set; }
        public int[] color { get; set; }
        public string Name { get; set; }
        public List<Component> Components { get; set; }

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

        public virtual string ToFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(":");
            sb.Append(Glyph.ToString());
            sb.Append(":");
            sb.Append(color);
            sb.Append(":");
            sb.Append(position.ToString());
            sb.Append(":");
            foreach(Component c in Components)
            {
                sb.Append(c.ToFile());
                sb.Append(":");
            }

            return sb.ToString();
        }
    }
}