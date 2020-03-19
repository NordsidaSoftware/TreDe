using System;
using System.Collections.Generic;
using System.Linq;

namespace TreDe
{
    [Serializable]
    internal class BodyComponent : Component
    {
        public int BodyPlanID { get; set; }

        public BodyPlan body { get { return BodyPlans.Plans[BodyPlanID]; } }
        public BodyComponent(TypeOfComponent typeOfComponent, GameObject owner) : base(typeOfComponent, owner)
        { }

        internal BodyPart GetRandomPart()
        {
            int rnd = Randomizer.rnd.Next(body.BodyParts.Count);
            int index = 0;
            foreach (KeyValuePair<Point3, BodyPart> kvp in body.BodyParts)
            {
                if (index == rnd) { return kvp.Value; }
                index++;
            }
            return null;
        }

        internal List<BodyPart> GetPartsFromDirection(Directions direction)
        {
            List<BodyPart> returnList = new List<BodyPart>();
            switch (direction)
            {
                case Directions.BACKWARD:

                    {// for now,  check a 10 units deep structure
                        for ( int z = 5; z > -5; z--)
                        {
                            // LINQ Query. Guess it can be optimized...
                            List<Point3> keysList = body.BodyParts.Keys.Where(key => key.Z == z).ToList();
                            foreach (Point3 p in keysList)
                            {
                                returnList.Add(body.BodyParts[p]);
                            }
                        }

                        break; }

                case Directions.LEFT:

                    {// for now,  check a 10 units deep structure
                        for (int x = 5; x > -5; x--)
                        {
                            // LINQ Query. Guess it can be optimized...
                            List<Point3> keysList = body.BodyParts.Keys.Where(key => key.X == x).ToList();
                            foreach (Point3 p in keysList)
                            {
                                returnList.Add(body.BodyParts[p]);
                            }
                        }

                        break;
                    }
            }
            return returnList;
        }
    }
}