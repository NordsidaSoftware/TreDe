using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TreDe
{

    public interface IIhandler { }
    public class InputHandler : GameComponent, IIhandler
    {
        KeyboardState keyboardState;
        KeyboardState old_keyboardState;
        MouseState mouseState;
        MouseState old_mouseState;

        public Point MousePosition { get { return mouseState.Position; } }

        public InputHandler(Game game) : base(game)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public override void Update(GameTime gameTime)
        {
            old_keyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            old_mouseState = mouseState;
            mouseState = Mouse.GetState();
        }

        internal bool WasKeyPressed(Keys key)
        {
            return old_keyboardState.IsKeyDown(key) && keyboardState.IsKeyUp(key);
        }

        internal bool IsKeyPressed(Keys key)
        {
            return keyboardState.IsKeyDown(key);
        }

        internal bool IsLeftButtonPressed()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }
        internal bool IsRightButtonPressed()
        {
            return mouseState.RightButton == ButtonState.Pressed;
        }

        internal bool WasLeftButtonPressed()
        {
            return old_mouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released;
        }

        internal bool WasRightButtonPressed()
        {
            return old_mouseState.RightButton == ButtonState.Pressed &&
                mouseState.RightButton == ButtonState.Released;
        }
    }
}
