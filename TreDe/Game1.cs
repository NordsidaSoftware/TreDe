using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TreDe
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandler input;
        public StateManager stateManager;

        public Settings settings;
        FPS fps;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height - 150 ;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2; ;
        }

      
        protected override void Initialize()
        {
            Randomizer.Initialize(1976);

            fps = new FPS(this);

            settings = new Settings();
            Services.AddService(typeof(ISettings), settings);
                
            input = new InputHandler(this);
            Components.Add(input);
            Services.AddService(typeof(IIhandler), input);

            stateManager = new StateManager(this);
            Components.Add(stateManager);

            // PlayState ps = new PlayState();
            MainMenuState sss = new MainMenuState();
            stateManager.Push(sss);


            base.Initialize();
        }

      
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

       
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {

            fps.Draw(gameTime);  
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            stateManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
