using System.Collections.Generic;

namespace TreDe
{
    public enum TypeOfComponent { TextMessage, PHYSIC, WEAPON,
        CONTAINER
    }
    public class Component
    {
        public TypeOfComponent typeOfComponent;
        public GameObject owner;

        public Component(TypeOfComponent typeOfComponent, GameObject owner)
        {
            this.typeOfComponent = typeOfComponent;
            this.owner = owner;
        }
        public virtual void Execute(object sender, HappeningArgs args ) {  }
    }

    public class Attack
    {
        string type;
        int length;
        int penetration;
        public Attack(string type, int length, int penetration)
        {
            this.type = type;
            this.length = length;
            this.penetration = penetration;
        }
    }

    public class WeaponComponent : Component
    {
        public List<Attack> Attacks;
        public bool wielded;
        public WeaponComponent(TypeOfComponent typeOfComponent, GameObject owner) : base(typeOfComponent, owner)
        {
            Attacks = new List<Attack>();
        }

        public override void Execute(object sender, HappeningArgs args)
        {
            base.Execute(sender, args);
        }
    }

    public class ContainerComponent : Component
    {
        public List<Item> Contains; 

        public int MaxCapacity { get; set; }
        public bool IsEmpty { get { return Contains.Count == 0; } }
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