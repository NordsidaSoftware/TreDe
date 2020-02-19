using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TreDe
{
    /// <summary>
    /// State for picking items from a container
    /// input  : actor and IContainer
    /// </summary>
    internal class ContainerManagerState : State
    {
        Actor actor;
        IContainer container;
        private int index;
        ItemManagerRenderer renderer;
        InputHandler input;

        private Item selected;




        public ContainerManagerState(Actor actor, IContainer container)
        {
            this.actor = actor;
            this.container = container;
        }

        public override void OnEnter()
        {
            renderer = new ItemManagerRenderer(Manager);
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));

            base.OnEnter();
        }

        public override void OnExit()
        {
            renderer = null;
            input = null;
            actor = null;
            base.OnExit();
        }

        public override void Update(GameTime gameTime)
        {

            if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
            if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > container.GetItems().Count - 1) { index = container.GetItems().Count - 1; } } }
            if (input.WasKeyPressed(Keys.Enter)) { if (container.GetItems().Count > 0) ChooseItem(container.GetItems()[index]); }
            if (input.WasKeyPressed(Keys.Escape)) { CheckForEmptyPile(); Manager.Pop(); } // <-- Must be last call!

        }

        private void CheckForEmptyPile()
        {
            if (container.isEmpty()) { actor.GOmanager.RemoveItemFromTerrain(actor.position); }
        }

        private void ChooseItem(Item item)
        {
            actor.Inventory.Add(item);

            container.RemoveItem(item);
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color c;
            renderer.display.ClearText();


            renderer.display.WriteLine("=== Pile ===", Color.Yellow);
            for (int i = 0; i < container.GetItems().Count; i++)
            {
                if (i == index) { c = Color.Red; }
                else { c = Color.White; }
                renderer.display.WriteLine(container.GetItems()[i].ToString(), c);
            }

            renderer.Draw(spriteBatch);
        }
    }
}