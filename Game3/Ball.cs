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
        public float AngleX;
        public float AngleY;
        public int ballDirectionX;
        public int ballDirectionY;

        public Ball(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) : base(width, height, pos, power, graphics)
        {
            this.Speed = 4;
            Rect = new Texture2D(graphics.GraphicsDevice, width, height);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            Rect.SetData(data);
            this.Center = new Vector2(this.Pos.X + (this.Width / 2), this.Pos.Y + (this.Height / 2));
            this.AngleX = 45;
            this.AngleY = 45;
            ballDirectionX = 1;
            ballDirectionY = -1;
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
            if (Util.IsColide((int)this.Pos.X, (int)this.Pos.Y,this.Width, this.Height,
                (int)b.Pos.X, (int)b.Pos.Y, b.Width, b.Height) == true)
                //if (this.IsColide(b) == true)
            //if(Util.IsColide(this.Box, b.Box) == true)
            {
                if (b.Pos.X < this.Center.X && this.Center.X < b.Pos.X + b.Width)
                { //TOP OR BOT
                    y *= -1;
                    if (this.Pos.Y <= b.Pos.Y) //TOP
                    {
                        this.Pos.Y = b.Pos.Y - this.Height;
                        if(b.GetType() == typeof(Racket))
                        {
                            float midRacket = (b.Width / 2);
                            if (this.ballDirectionX == 1)
                            {
                                float marge = this.Center.X - b.Pos.X;
                                this.AngleX *= (marge / midRacket);
                                if (this.AngleX >= 90)
                                {
                                    this.AngleX = 90;
                                }
                                else if (this.AngleX <= 1)
                                {
                                    this.AngleX = 1;
                                }
                            } else if (this.ballDirectionX == -1)
                            {
                                float marge = (b.Pos.X + b.Height) - this.Center.X ;
                                this.AngleX *= (marge / midRacket);
                                if (this.AngleX >= 90)
                                {
                                    this.AngleX = 90;
                                }
                                else if (this.AngleX <= 1)
                                {
                                    this.AngleX = 1;
                                }
                            }
                           
                            
                            Console.WriteLine(this.AngleX);
                        }
                    }
                    else if (this.Pos.Y + this.Height >= b.Pos.Y + b.Height) //BOT
                    {
                        this.Pos.Y = b.Pos.Y + b.Height;
                    }
                }
                else if (b.Pos.Y < this.Center.Y && this.Center.Y < b.Pos.Y + b.Height)
                {   //RIGHT OR LEFT
                    x *= -1;
                    if (this.Pos.X <= b.Pos.X)
                    {
                        this.Pos.X = b.Pos.X - this.Height;
                    }
                    else if (this.Pos.X + this.Height >= b.Pos.X + b.Height)
                    {
                        this.Pos.X = b.Pos.X + b.Height;
                    }
                }

                if (b.GetType() != typeof(Racket))
                    b.Power += -1;
            }
                      
        }
    }
}
