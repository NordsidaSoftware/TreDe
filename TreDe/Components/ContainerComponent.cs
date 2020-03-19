using System;
using System.Collections.Generic;

namespace TreDe
{
    [Serializable]
    public class ContainerComponent : Component
    {
        public List<Item> Contains { get; set; }

        public int MaxCapacity { get; set; }
        public bool IsEmpty { get { return Contains.Count == 0; }  }
        public bool IsFull { get { return Contains.Count >= MaxCapacity; } }

        public List<Item> GetItems { get { return Contains; } }

        public ContainerComponent(TypeOfComponent typeOfComponent, GameObject owner) : base(typeOfComponent, owner)
        {
            Contains = new List<Item>();
        }


        public override void Execute(object sender, HappeningArgs args)
        {
            base.Execute(sender, args);
        }

        public override string ToString()
        {
            if (Contains.Count > 0)
            {
                return " som inneholder " + Contains.Count.ToString() + " gjenstander";
            }
            else return "";
        }
    }
}