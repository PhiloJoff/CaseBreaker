using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CaseBreaker
{
    public class Sprite : IActor
    {
        public Vector2 Pos { get; set; }
        public Texture2D Texture { get; set; }

        public Sprite()
        {
        }

        public Sprite(Texture2D texture, Vector2 pos)
        {
            Texture = texture;
            Pos = pos;
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Pos, Color.White);
        }
    }
}
