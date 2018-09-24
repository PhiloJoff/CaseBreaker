using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{ 
    abstract class Scene
    {
        protected MainGame mainGame;
        protected GraphicsDeviceManager Graphic;
        protected SpriteBatch spriteBatch;
        protected int WindowWidth;
        protected int WindowHeight;

        public Scene(MainGame mainGame)
        {
            this.mainGame = mainGame;
            Graphic = mainGame.graphics;
            WindowWidth = Graphic.PreferredBackBufferWidth;
            WindowHeight = Graphic.PreferredBackBufferHeight;
        }

        public virtual void Load()
        {
            spriteBatch = mainGame.spriteBatch;

        }

        public virtual void Unload()
        {

        }

        public virtual void Update(GameTime gametime)
        {

        }

        public virtual void Draw(GameTime gameTime)
        {

        }

    }
}
