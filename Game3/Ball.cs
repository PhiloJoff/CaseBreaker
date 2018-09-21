using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class Ball : Brick
    {
        public float Speed { get; set; }
        public Vector2 Center;

        public Ball(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) : base(width, height, pos, power, graphics)
        {
            this.Speed = 3;
            Rect = new Texture2D(graphics.GraphicsDevice, width, height);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            Rect.SetData(data);
            this.Center = new Vector2(this.Pos.X + (this.Width / 2), this.Pos.Y + (this.Height / 2));
        }

        public bool IsColide(Brick b)
        {
            if (this.Pos.Y + this.Height <= b.Pos.Y ||
                this.Pos.Y >= b.Pos.Y + b.Height ||
                this.Pos.X + this.Width <= b.Pos.X ||
                this.Pos.X >= b.Pos.X + b.Width)
            {
                

                return false;
            }
            return true;
        }

        public void Rebond(Brick b, ref int x, ref int y)
        {
            if (b.Pos.X < this.Center.X && this.Center.X < b.Pos.X + b.Width)
            { //TOP OR BOT
                y *= -1;
                if(this.Pos.Y <= b.Pos.Y)
                {
                    this.Pos.Y = b.Pos.Y - this.Height;
                }
                else if (this.Pos.Y >= b.Pos.Y + b.Height)
                {
                    this.Pos.Y = b.Pos.Y + b.Height;
                }
            } else if (b.Pos.Y < this.Center.Y && this.Center.Y < b.Pos.Y + b.Height)
            {   //RIGHT OR LEFT
                x *= -1;
                if (this.Pos.X <= b.Pos.X)
                {
                    this.Pos.X = b.Pos.X - this.Height;
                }
                else if (this.Pos.X >= b.Pos.X + b.Width)
                {
                    this.Pos.X = b.Pos.X + b.Width;
                }
            }
                
           

            if (b.GetType() != typeof(Racket))
                b.Power += -1;
        }
    }
}
