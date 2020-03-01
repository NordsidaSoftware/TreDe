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
        ItemManagerRenderer renderer;
        InputHandler input;
        private InventoryState displayState;

        private int index;
        private Item selected;
        private Item selectedContainer;
        
        private Dictionary<int, Choice> InteractionChoices;
        private List<Item> ContainerList;
        
        internal enum InventoryState
        {
            InventoryDisplay,
            ItemInteraction,
            ContainerEnterDisplay,
            ContainerExitDisplay
        };
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
            ContainerList = new List<Item>();
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
                case "put": { IdentifyContainers(); break; }
                case "drop":
                    {
                        actor.DropItem(selected);
                        SwitchDisplayState(InventoryState.InventoryDisplay);
                        break;
                    }
                case "wield": { actor.Wield(selected); break; }
                case "remove": { SelectContainerToManipulate(); break; }
            }
        }

        private void SelectContainerToManipulate()
        {
            selectedContainer = selected;
            SwitchDisplayState(InventoryState.ContainerExitDisplay);
        }

        private void IdentifyContainers()
        {
            ContainerList.Clear();
            foreach (Item item in actor.Inventory )
            {
                if (item.GetComponent(TypeOfComponent.CONTAINER) != null)
                {
                    ContainerComponent cc = (ContainerComponent)item.GetComponent(TypeOfComponent.CONTAINER);
                    if (!cc.IsFull && item != selected)
                    ContainerList.Add(item);
                }
            }
            if (ContainerList.Count > 0)
                SwitchDisplayState(InventoryState.ContainerEnterDisplay);
        }
        private void PutIntoContainer(int index)
        {
            if (ContainerList[index].GetComponent(TypeOfComponent.CONTAINER )!= null)
            {
                ContainerComponent cc = (ContainerComponent)ContainerList[index].GetComponent(TypeOfComponent.CONTAINER);
                if (selected != ContainerList[index])
                {
                    if (!cc.IsFull)
                    {
                        cc.Contains.Add(selected);
                        actor.Inventory.Remove(selected);
                    }
                }
            }
            SwitchDisplayState(InventoryState.InventoryDisplay);
        }

        private void ChooseItem(Item item)
        {
            selected = item;
            int i = 0;
            InteractionChoices.Clear();
            InteractionChoices.Add(i++, new Choice() { text = "Drop", keyword = "drop" });
            InteractionChoices.Add(i++, new Choice() { text = "Put in container", keyword = "put" });

            if (selected.GetComponent(TypeOfComponent.WEAPON) != null)
            {
                InteractionChoices.Add(i++, new Choice() { text = "Wield", keyword = "wield" });
            }
            if (selected.GetComponent(TypeOfComponent.CONTAINER) != null)
            {
                ContainerComponent cc = (ContainerComponent)selected.GetComponent(TypeOfComponent.CONTAINER);
                if (!cc.IsEmpty)
                InteractionChoices.Add(i++, new Choice() { text = "Remove item", keyword = "remove" });
            }

            SwitchDisplayState(InventoryState.ItemInteraction);

        }
        private void RemoveFromContainer(int index)
        {
            ContainerComponent cc = (ContainerComponent)selectedContainer.GetComponent(TypeOfComponent.CONTAINER);
            actor.Inventory.Add(cc.GetItems[index]);
            cc.Contains.Remove(cc.GetItems[index]);
            if (cc.IsEmpty)
            { SwitchDisplayState(InventoryState.InventoryDisplay); }
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
                case InventoryState.ContainerEnterDisplay:
                    {
                        if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
                        if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > ContainerList.Count - 1) { index = ContainerList.Count - 1; } } }
                        if (input.WasKeyPressed(Keys.Enter)) { PutIntoContainer(index); }
                        if (input.WasKeyPressed(Keys.Escape)) { SwitchDisplayState(InventoryState.InventoryDisplay); }
                        break;
                    }
                case InventoryState.ContainerExitDisplay:
                    {
                        if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
                        if (input.WasKeyPressed(Keys.Down)) { index++; }  // TODO : max inventory for selected container
                        if (input.WasKeyPressed(Keys.Enter)) { RemoveFromContainer(index); }
                        if (input.WasKeyPressed(Keys.Escape)) { SwitchDisplayState(InventoryState.InventoryDisplay); }
                        break;
                    }
            }
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
                        renderer.display.WriteLine("===   I T E M   ===", Color.Yellow);
                        renderer.display.WriteLine(" ");
                        renderer.display.WriteLine(selected.ToString().PadLeft(5));
                        renderer.display.WriteLine("-----------------------");

                        if (selected.GetComponent(TypeOfComponent.WEAPON)!= null)
                        {
                            WeaponComponent wc = (WeaponComponent)selected.GetComponent(TypeOfComponent.WEAPON);
                            renderer.display.WriteLine("Wielded : " + wc.wielded.ToString());
                        }
                        renderer.display.WriteLine("---------------------------");

                       for ( int i = 0; i < InteractionChoices.Count; i++)
                        {
                            if ( i == index) { c = Color.Red; }
                            else { c = Color.White; }
                            renderer.display.WriteLine(InteractionChoices[i].text, c);
                        }
                        break;
                    }

                case InventoryState.ContainerEnterDisplay:
                    {
                        renderer.display.WriteLine("=== AVAILABLE CONTAINERS ===", Color.Yellow);
                        renderer.display.WriteLine(" ");
                       
                        for (int i = 0; i < ContainerList.Count; i++)
                        {
                            if (i == index) { c = Color.Red; }
                            else { c = Color.White; }
                            renderer.display.WriteLine(ContainerList[i].Name, c);
                        }
                        break;
                    }
                case InventoryState.ContainerExitDisplay:
                    {
                        renderer.display.WriteLine("=== ITEMS IN CONTAINER ===", Color.Yellow);
                        renderer.display.WriteLine(" ");
                        ContainerComponent cc = (ContainerComponent)selectedContainer.GetComponent(TypeOfComponent.CONTAINER);
                        for (int i = 0; i < cc.GetItems.Count; i++)
                        {

                            if (i == index) { c = Color.Red; }
                            else { c = Color.White; }
                            renderer.display.WriteLine(cc.GetItems[i].Name, c);
                        }
                        break;
                    }
            }
            renderer.Draw(spriteBatch);
        }
    }
}