using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TreDe
{
    /// <summary>
/// State for all inventory interactions
/// input  : actor
/// </summary>
    internal class ItemManagerState : State
    {
        Actor actor;
        private int index;
        ItemManagerRenderer renderer;
        InputHandler input;
        private InventoryState displayState;
        private Item selected;
        
        private Dictionary<int, Choice> InteractionChoices;
        
        internal enum InventoryState { InventoryDisplay, ItemInteraction};
        internal struct Choice
        {
            internal string text;
            internal string keyword;
        }
        public ItemManagerState(Actor actor)
        {
            this.actor = actor;
            displayState = InventoryState.InventoryDisplay;
            InteractionChoices = new Dictionary<int, Choice>();
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
        { // switching between the different states of display
            switch (displayState)
            {
                case InventoryState.InventoryDisplay:
                    {
                        if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
                        if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > actor.Inventory.Count - 1) { index = actor.Inventory.Count - 1; } } }
                        if (input.WasKeyPressed(Keys.Enter)) { if (actor.Inventory.Count > 0) ChooseItem(actor.Inventory[index]); }
                        if (input.WasKeyPressed(Keys.Escape)) { Manager.Pop(); } // <-- Must be last call!
                        break;
                    }
                case InventoryState.ItemInteraction:
                    {
                        if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
                        if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > InteractionChoices.Count - 1) { index = InteractionChoices.Count - 1; } } }
                        if (input.WasKeyPressed(Keys.Enter)) { ChooseInteraction(index); }
                        if (input.WasKeyPressed(Keys.Escape)) { SwitchDisplayState(InventoryState.InventoryDisplay); }
                        break;
                    }
            }
        }

        private void SwitchDisplayState(InventoryState newState)
        {
            displayState = newState;
            index = 0;
        }

        private void ChooseInteraction(int index)
        {
            Choice choice = InteractionChoices[index];
            switch (choice.keyword)
            {
                case "put": { break; }
                case "drop": { actor.DropItem(selected); break; }
                case "wield": { break; }
            }
        }

        private void ChooseItem(Item item)
        {
            selected = item;
            int i = 0;
            InteractionChoices.Clear();
            InteractionChoices.Add(i++, new Choice() { text = "Drop", keyword = "drop" });
            InteractionChoices.Add(i++, new Choice() { text = "Put in container", keyword = "put" });

            if (selected is IWield w)
            {
                InteractionChoices.Add(i++, new Choice() { text = "Wield", keyword = "wield" });
            }

            SwitchDisplayState(InventoryState.ItemInteraction);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Color c;
            renderer.display.ClearText();

            // switching between the different states of display
            switch (displayState)
            {
                case InventoryState.InventoryDisplay:
                    {
                        renderer.display.WriteLine("=== INVENTORY ===", Color.Yellow);
                        for (int i = 0; i < actor.Inventory.Count; i++)
                        {
                            if (i == index) { c = Color.Red; }
                            else { c = Color.White; }
                            renderer.display.Write(actor.Inventory[i].ToString(), c);
                            renderer.display.WriteLine("   " + actor.Inventory[i].Glyph.ToString());
                        }
                        break;
                    }
                case InventoryState.ItemInteraction:
                    {
                        renderer.display.WriteLine("=== ITEM DISPLAY ===", Color.Yellow);
                        renderer.display.Write(" ");
                        renderer.display.WriteLine(selected.ToString());

                       for ( int i = 0; i < InteractionChoices.Count; i++)
                        {
                            if ( i == index) { c = Color.Red; }
                            else { c = Color.White; }
                            renderer.display.WriteLine(InteractionChoices[i].text, c);
                        }
                        break;
                    }
            }
            renderer.Draw(spriteBatch);
        }
    }
}