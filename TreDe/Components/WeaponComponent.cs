using System;
using System.Collections.Generic;

namespace TreDe
{
    [Serializable]
    public class WeaponComponent : Component
    {
        public List<Attack> Attacks { get; set; }
        public bool wielded { get; set; }

        public WeaponComponent(TypeOfComponent typeOfComponent, GameObject owner) : base(typeOfComponent, owner)
        {
            Attacks = new List<Attack>();
        }

        public override void Execute(object sender, HappeningArgs args)
        {
            base.Execute(sender, args);
        }
    }
}