using System;
using System.Collections.Generic;
using System.Text;

namespace TreDe
{
    public enum TypeOfComponent
    {
        TextMessage,
        PHYSIC,
        WEAPON,
        CONTAINER
    }
    [Serializable]
    public class Component
    {
        public TypeOfComponent typeOfComponent { get; set; }
        public GameObject owner { get; set; }


        public Component(TypeOfComponent typeOfComponent, GameObject owner)
        {
            this.typeOfComponent = typeOfComponent;
            this.owner = owner;
        }
        public virtual void Execute(object sender, HappeningArgs args) { }

        public virtual string ToFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(typeOfComponent.ToString());
            sb.Append(":");
            return sb.ToString();
        }
    }

    [Serializable]
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

        public string ToFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(type);
            sb.Append(":");
            sb.Append(length.ToString());
            sb.Append(":");
            sb.Append(penetration.ToString());
            return sb.ToString();
        }
    }

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

        public override string ToFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(wielded.ToString());
            sb.Append(":");
            foreach(Attack attack in Attacks)
            {
                sb.Append(attack.ToFile());
                sb.Append(":");
            }
            return base.ToFile() + sb.ToString();
        }
    }

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

        public override string ToFile()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MaxCapacity.ToString());
            sb.Append(":");
            foreach (Item item in Contains)
            {
                sb.Append(item.ToFile());
                sb.Append(":");
            }
            return base.ToFile() + sb.ToString();
        }
    }
}