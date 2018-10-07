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
        public float Angle { get; set; }
        public int ballDirectionX;
        public int ballDirectionY;
        public float AngleConstant { get; private set; }

        public Ball(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) : base(width, height, pos, power, graphics)
        {
            Speed = 4;
            Rect = new Texture2D(graphics.GraphicsDevice, width, height);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
            Rect.SetData(data);
            Center = new Vector2(Pos.X + (Width / 2), Pos.Y + (Height / 2));
            AngleConstant = (float)(45 * (Math.PI / 180));
            Angle = AngleConstant;
            ballDirectionX = 1;
            ballDirectionY = -1;
        }

        public Ball(Texture2D texture, Vector2 pos, int width, int height) : base(texture, pos)
        {
            Speed = 4;
            Texture = texture;
            Pos = pos;
            Width = width;
            Height = height;
            Power = 1;
            Center = new Vector2(Pos.X + (Width / 2), Pos.Y + (Height / 2));
            AngleConstant = (float)(45 * (Math.PI / 180));
            Angle = AngleConstant;
            ballDirectionX = 1;
            ballDirectionY = -1;
        }

        public bool IsColide(Brick b)
        {
            if (Pos.Y + Height <= b.Pos.Y ||
                Pos.Y >= b.Pos.Y + b.Height ||
                Pos.X + Width <= b.Pos.X ||
                Pos.X >= b.Pos.X + b.Width)
            {


                return false;
            }
            return true;
        }

        public void Rebond(Brick b)
        {
            if (Util.IsColide((int)Pos.X, (int)Pos.Y, Width, Height,
                (int)b.Pos.X, (int)b.Pos.Y, b.Width, b.Height) == true)
            //if (IsColide(b) == true)
            //if(Util.IsColide(Box, b.Box) == true)
            {

                if ((b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width) ||
                    (b.Pos.X < Pos.X && Pos.X < b.Pos.X + b.Width) ||
                    (b.Pos.X < Pos.X + Width && Pos.X + Width < b.Pos.X + b.Width))
                //if (b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width)
                { //TOP OR BOT
                    ballDirectionY *= -1;
                    if (Pos.Y <= b.Pos.Y) //TOP
                    {
                        Console.WriteLine("collision haut");
                        Pos = new Vector2(Pos.X,b.Pos.Y - Height);
                        if (b.GetType() == typeof(Racket))
                        {
                            float midRacket = (b.Width / 2);
                            if (ballDirectionX == -1)
                            {
                                float marge = Center.X - Math.Abs(b.Pos.X);
                                Angle = AngleConstant * (marge / midRacket);
                            }
                            else if (ballDirectionX == 1)
                            {

                                float marge = Math.Abs((b.Pos.X + b.Width)) - Center.X;
                                Angle = AngleConstant * (marge / midRacket);
                            }
                            Speed += 0.05f;
                        }
                    }
                    else if (Pos.Y + Height >= b.Pos.Y + b.Height) //BOT
                    {
                        Console.WriteLine("collision bas");
                        Pos = new Vector2(Pos.X, b.Pos.Y + b.Height);
                    }
                }
                else if ((b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height) ||
                    (b.Pos.Y < Pos.Y && Pos.Y < b.Pos.Y + b.Height) ||
                    (b.Pos.Y < Pos.Y + Height && Pos.Y + Height < b.Pos.Y + b.Height))
                //else if (b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height)
                {   //RIGHT OR LEFT
                    ballDirectionX *= -1;
                    if (Pos.X <= b.Pos.X)
                    {
                        Console.WriteLine("collision left");
                        Pos = new Vector2(b.Pos.X - Width, Pos.Y);
                    }
                    else if (Pos.X + Height >= b.Pos.X + b.Height)
                    {
                        Console.WriteLine("collision left");
                        Pos = new Vector2(b.Pos.X + b.Width, Pos.Y);
                    }
                }

                if (b.GetType() != typeof(Racket))
                    b.Power += -1;
            }


        }
    }
}
