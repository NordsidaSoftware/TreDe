using System;

namespace TreDe
{
    [Serializable]
    public class Attack
    {
        public string type;
        public int length;
        public int penetration;
        public Attack(string type, int length, int penetration)
        {
            this.type = type;
            this.length = length;
            this.penetration = penetration;
        }
    }
}