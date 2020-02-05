using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TreDe
{
    public abstract class State
    {
        internal StateManager Manager;
        public void Initialize(StateManager Manager) { this.Manager = Manager; }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }








    public class StateManager : GameComponent
    {
        private Stack<State> StateStack;

        private bool IsNotEmpty { get { return StateStack.Count > 0; } }
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
            if (IsNotEmpty) { StateStack.Pop().OnExit(); }
        }

        public override void Update(GameTime gameTime)
        {
            if (IsNotEmpty)
            {
                StateStack.Peek().Update(gameTime);
            }
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
