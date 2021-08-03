using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CaseBreaker
{
    public class MainGame : Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        private int windowWidth = 800;
        private int windowHeight = 600;
        public GameState gameState;

        KeyboardState keyboardState;


        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = windowHeight,
                PreferredBackBufferWidth = windowWidth
            };
            Window.Title = "CaseBreaker";
            Content.RootDirectory = "Content";
            gameState = new GameState(this);
        }

        protected override void Initialize()
        {
            gameState.SwitchScene(GameState.SceneType.MainMenu);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            gameState.CurrentScene.Load();
        }

        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            gameState.CurrentScene.Update(gameTime);
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            gameState.CurrentScene.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);
            
        }

        
    }
}
