using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    public class Brick
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public Vector2 Pos;// { get; set; }
        public int Power { get; set; }
        public Texture2D Rect{  get; protected set; }

        public Rectangle Box { get; set; }
        protected Color[] data;
        public Brick(Vector2 pos, int power, GraphicsDeviceManager graphics)
        {
            this.Width = 39;
            this.Height = 20;
            this.Pos = pos;
            this.Power = power;
            this.Box = new Rectangle((int)this.Pos.X, (int)this.Pos.Y, this.Width, this.Height);

            data = new Color[Width * Height];
            Rect = new Texture2D(graphics.GraphicsDevice, this.Width, this.Height);
            this.SetColor(this, graphics);
        }

        public Brick(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics)
        {
            this.Width = width;
            this.Height = height;
            this.Pos = pos;
            this.Power = power;
            this.Box = new Rectangle((int)this.Pos.X, (int)this.Pos.Y, this.Width, this.Height);

            data = new Color[width * height];
            Rect = new Texture2D(graphics.GraphicsDevice, this.Width, this.Height);
            this.SetColor(this, graphics);
        }

        

        public void SetColor(Brick b, GraphicsDeviceManager graphics)
        {
            switch (this.Power)
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

            Rect.SetData(data);
        }
        
    }
}
