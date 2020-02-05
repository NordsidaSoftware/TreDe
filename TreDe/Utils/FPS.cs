using Microsoft.Xna.Framework;
using System;

namespace TreDe
{
    internal class FPS
    {
        Game game;
        double totalTime;
        int frameCounter;

        public FPS(Game game) { this.game = game; }

        internal void Draw(GameTime gameTime)
        {
            frameCounter++;
            totalTime += gameTime.ElapsedGameTime.TotalSeconds;
            game.Window.Title = "FPS: " + frameCounter / totalTime;
        }
    }
}