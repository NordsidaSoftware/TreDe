﻿namespace TreDe
{
    public enum TypeOfComponent { TextMessage,
        PHYSIC
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
}