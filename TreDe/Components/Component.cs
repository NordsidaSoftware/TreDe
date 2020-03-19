using System;

namespace TreDe
{
    public enum TypeOfComponent
    {
        TextMessage,
        PHYSIC,
        WEAPON,
        CONTAINER,
        BODY
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

    }
}