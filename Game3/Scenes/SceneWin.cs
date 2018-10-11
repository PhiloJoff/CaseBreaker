using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class SceneWin : Scene
    {
        private Texture2D background;
        private Vector2 backgroundPos;
        public SceneWin(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            background = mainGame.Content.Load<Texture2D>("Youwin");
            backgroundPos = new Vector2((mainGame.graphics.PreferredBackBufferWidth / 2) - (background.Width / 2), (mainGame.graphics.PreferredBackBufferHeight / 2) - (background.Height / 2));
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(background, backgroundPos , Color.White);
            base.Draw(gameTime);
        }
    }
}
