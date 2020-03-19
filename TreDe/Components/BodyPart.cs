using System;
using System.Collections.Generic;

namespace TreDe
{
    [Serializable]
    public class BodyPart
    {
        public string Name;
        public Dictionary<Point3, BodyPart> subParts;
        public BodyPart(string Name)
        {
            this.Name = Name;
            subParts = new Dictionary<Point3, BodyPart>();

        }
        public void ApplyDamage(float impact)
        {
            
        }
    }
}