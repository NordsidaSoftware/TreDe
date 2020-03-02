using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TreDe
{
    /// <summary>
    /// State for picking items from a pile
    /// </summary>
    internal class PileManagerState : State
    {
        Actor actor;
        Pile pile;
        private int index;
        ItemManagerRenderer renderer;
        InputHandler input;


        public PileManagerState(Actor actor, Pile pile)
        {
            this.actor = actor;
            this.pile = pile;
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
            if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > pile.AmountInPile() - 1) { index = pile.AmountInPile() - 1; } } }
            if (input.WasKeyPressed(Keys.Enter)) { if (pile.AmountInPile() > 0) ChooseItem(pile.GetItems()[index]); }
            if (input.WasKeyPressed(Keys.Escape)) { CheckForEmptyPile(); Manager.Pop(); } // <-- Must be last call!

        }

        private void CheckForEmptyPile()
        {
            if (pile.IsEmpty()) { actor.GOmanager.RemoveItemFromTerrain(actor.position); }
            
        }

        private void ChooseItem(Item item)
        {
            actor.Inventory.Add(item);

            pile.RemoveItem(item);
           
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color c;
            renderer.display.ClearText();


            renderer.display.WriteLine("=== Pile ===", Color.Yellow);
            for (int i = 0; i < pile.AmountInPile(); i++)
            {
                if (i == index) { c = Color.Red; }
                else { c = Color.White; }
                renderer.display.WriteLine(pile.GetItems()[i].ToString(), c);
            }

            renderer.Draw(spriteBatch);
        }
    }
}