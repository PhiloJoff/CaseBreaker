using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class SceneMainMenu : Scene
    {
        private Texture2D playButton;
        private Vector2 buttonPosition;
        private MouseState oldMouseState;
        private MouseState mouseState;
        public SceneMainMenu(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            playButton = mainGame.Content.Load<Texture2D>("PlayOnly");
            buttonPosition = new Vector2((mainGame.graphics.PreferredBackBufferWidth / 2) - (playButton.Width / 2), (mainGame.graphics.PreferredBackBufferHeight / 2) - (playButton.Height / 2));
            mainGame.IsMouseVisible = true;

            oldMouseState = new MouseState();
            oldMouseState = Mouse.GetState();
            mouseState = new MouseState();
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (Util.IsHover(mouseState, (int)buttonPosition.X, (int)buttonPosition.Y, playButton.Width, playButton.Height) == true)
            {
                if(mouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton != ButtonState.Pressed)
                {
                    oldMouseState = mouseState;
                }
                if(mouseState.LeftButton == ButtonState.Released && oldMouseState.LeftButton == ButtonState.Pressed)
                {
                    mainGame.gameState.SwitchScene(GameState.SceneType.Gameplay);
                }

            }


            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(playButton, buttonPosition, Color.White);   
            base.Draw(gameTime);
        }
    }
}
