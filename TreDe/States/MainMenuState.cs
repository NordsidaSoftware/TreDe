using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace TreDe
{
    public class MainMenuState : State
    {
        InputHandler input;
        SingleScreenRender renderer;

        Dictionary<int, Choice> InteractionChoices;
        int index;
        bool saved_game_exists = false;
        
       

        public override void OnEnter()
        {
            input = (InputHandler)Manager.Game.Services.GetService(typeof(IIhandler));
            renderer = new SingleScreenRender(Manager);
            InteractionChoices = new Dictionary<int, Choice>();
            InitializeChoices();
        }

        private void InitializeChoices()
        {
            int i = 0;
            InteractionChoices.Clear();

            // First 2 choices are always active :
            InteractionChoices.Add(i++, new Choice() { text = "Start new game", keyword = "new" });
            InteractionChoices.Add(i++, new Choice() { text = "Quit", keyword = "quit" });

            // If game is already in play :
            if (Manager.GameInProgress)
            {
                InteractionChoices.Add(i++, new Choice() { text = "Save the game", keyword = "save" });
                InteractionChoices.Add(i++, new Choice() { text = "Continue", keyword = "continue" });
            }

            // if savegame exists :
            if (saved_game_exists)
            {
                InteractionChoices.Add(i++, new Choice() { text = "Load game", keyword = "load" });
            }
        }

        private void ChooseInteraction(int index)
        {
            Choice choice = InteractionChoices[index];
            switch (choice.keyword)
            {
                case "new":Manager.ClearStack(); Manager.Push(new MainMenuState()); Manager.Push(new PlayState()); break;
                case "quit": Manager.ClearStack(); break;
                case "save": Manager.Pop(); Manager.Push(new SaveState((PlayState)Manager.Peek())); break;
                case "continue":Manager.Pop(); break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            InitializeChoices();
            if (input.WasKeyPressed(Keys.Up)) { index--; { if (index < 0) { index = 0; } } }
            if (input.WasKeyPressed(Keys.Down)) { index++; { if (index > InteractionChoices.Count - 1) { index = InteractionChoices.Count - 1; } } }
            if (input.WasKeyPressed(Keys.Enter)) { ChooseInteraction(index); }

            if (input.WasKeyPressed(Keys.Escape))
            {
                Manager.Pop();
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            Color c = Color.White;
            renderer.display.ClearText();

            for (int i = 0; i < InteractionChoices.Count; i++)
            {
                if (i == index) { c = Color.Red; }
                else { c = Color.White; }
                renderer.display.WriteLine(InteractionChoices[i].text, c);
            }
            renderer.Draw(spriteBatch);
        }
    }
}
