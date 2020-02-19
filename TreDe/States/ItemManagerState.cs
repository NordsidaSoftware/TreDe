using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TreDe
{
    internal class ItemManagerState : State
    {
        Actor actor;
        private int index;
        ItemManagerRenderer renderer;
        InputHandler input;
        public ItemManagerState(Actor actor)
        {
            this.actor = actor;    
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
            if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > actor.Inventory.Count-1) { index = actor.Inventory.Count-1; } } }

            if (input.WasKeyPressed(Keys.Escape)) { Manager.Pop(); } // <-- Must be last call!
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            renderer.display.ClearText();

            renderer.display.WriteLine("=== INVENTORY ===", Color.Yellow);
            for ( int i = 0; i < actor.Inventory.Count; i++)
            {
                Color c = Color.White;
                if (i == index) { c = Color.Red; }
                renderer.display.WriteLine(actor.Inventory[i].ToString(), c);
            }
            renderer.Draw(spriteBatch);
        }
    }
}