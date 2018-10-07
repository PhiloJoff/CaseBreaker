using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class Brick : Sprite
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public int Power { get; set; }
        public Texture2D Rect{  get; protected set; }

        public Rectangle Box { get; set; }
        protected Color[] data;

        public Brick(Texture2D texture, Vector2 pos) : base(texture, pos)
        {
            Texture = texture;
            Pos = pos;
        }

        public Brick(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics)
        {
            Width = width;
            Height = height;
            Pos = pos;
            if (power > 5 || power <= 0)
                Power = 1;
            else
                Power = power;
            Box = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);

            data = new Color[width * height];
            Texture = new Texture2D(graphics.GraphicsDevice, Width, Height);
            SetColor(this, graphics);
        }

        public Brick(Texture2D texture, int width, int height, Vector2 pos, int power)
        {
            Width = width;
            Height = height;
            Pos = pos;
            if (power > 5 || power <= 0)
                Power = 1;
            else
                Power = power;
            Box = new Rectangle((int)Pos.X, (int)Pos.Y, Width, Height);

            Texture = texture;
        }



        public void SetColor(Brick b, GraphicsDeviceManager graphics)
        {
            switch (Power)
            {
                case 1:
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.Orange;
                    break;
                case 2:
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
                    break;
                case 3:
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.Brown;
                    break;
                default:
                    for (int i = 0; i < data.Length; ++i) data[i] = Color.DarkGray;
                    break;

            }

            Texture.SetData(data);
        }

        public Rectangle GetTile()
        {
            return new Rectangle((Power-1) * Width, 0, Width, Height);               
        }

    }
}
