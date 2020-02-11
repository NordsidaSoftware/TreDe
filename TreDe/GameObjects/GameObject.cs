using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe
{
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

            color = Color.Red;
            position = new Point3(10, 10, 4);
            Glyph = 45;
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

        public virtual void Update(GameTime gameTime)
        { }

        public override string ToString()
        {
            return Name;
        }
    }
}