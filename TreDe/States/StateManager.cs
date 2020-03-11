using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TreDe
{

    /// <summary>
    /// State is the controlling class for the current state in the statemanager 
    /// </summary>
    public abstract class State
    {
        internal StateManager Manager;
        public void Initialize(StateManager Manager) { this.Manager = Manager; }
        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch) { }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }

       

    }



    public class StateManager : GameComponent
    {
        private Stack<State> StateStack;


        private bool IsNotEmpty { get { return StateStack.Count > 0; } }
        public bool GameInProgress { get { return StateStack.Count > 1; } } // Stack[0] = MainMenu. 
        
        public StateManager(Game game) : base(game)
        {
            StateStack = new Stack<State>();
        }
        public void Push(State newState)
        {
            newState.Initialize(this);
            newState.OnEnter();
            StateStack.Push(newState);
        }
        public void Pop()
        {
            if (IsNotEmpty)
            {
                StateStack.Peek().OnExit();
                StateStack.Pop();
            }
        }

        public State Peek() { return StateStack.Peek(); }

        public void ClearStack()
        {
            StateStack.Clear();
        }

        public override void Update(GameTime gameTime)
        {
            if (IsNotEmpty)
            {
                StateStack.Peek().Update(gameTime);
            }
            else { Game.Exit(); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsNotEmpty)
            {
                StateStack.Peek().Draw(spriteBatch);
            }
        }

    }
}
