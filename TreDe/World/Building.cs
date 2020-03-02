using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TreDe.World
{
    public class Building
    {
        public Actor Owner;
        public Rectangle Rectangle;
        public List<Point> Doors;

        public Building(Actor Owner, Rectangle Rectangle, List<Point> Doors)
        {
            this.Owner = Owner;
            this.Rectangle = Rectangle;
            this.Doors = Doors;
        }
    }
}
