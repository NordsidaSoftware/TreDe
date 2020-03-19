using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreDe
{
    /* PLAN : reading a raw text file with data, an empty actor instance is filled
     * with data from raw.
     * extract from raw file the relevant data, a human or a troll will be filled with 
     * different stats. Further specification can be made after the basic data is aquired
     * 
     * */
    public static class LoadActorBlueprint
    {
        private static Random rnd = new Random();
        // a test static list of names:
        private static List<string> RandomNames = new List<string>()
        {
            "Paul", "Ringo", "John", "Harrison"
        };
        public static Actor Load(string type, Actor actor)
        {
            actor.Name = RandomNames[rnd.Next(0, RandomNames.Count - 1)];
            actor.Mass = 70 + rnd.Next(-30, 30);

            BodyComponent bc = new BodyComponent(TypeOfComponent.BODY, actor);
            bc.BodyPlanID = 0;  // HUMANOID BODYPLAN = 0
            actor.Components.Add(bc);


            return actor;
        }
    }
}
