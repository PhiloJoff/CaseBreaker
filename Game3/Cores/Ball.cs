using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    public class Ball : Brick
    {
        public float Speed { get; set; }
        public Vector2 Center;
        public float Angle { get; set; }
        public int ballDirectionX;
        public int ballDirectionY;
        public float AngleConstant { get; private set; }

        private float RadiusPow2 { get; set; }

        //public Ball(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) : base(width, height, pos, power, graphics)
        //{
        //    Speed = 4;
        //    Rect = new Texture2D(graphics.GraphicsDevice, width, height);
        //    for (int i = 0; i < data.Length; ++i) data[i] = Color.Red;
        //    Rect.SetData(data);
        //    Center = new Vector2(Pos.X + (Width / 2), Pos.Y + (Height / 2));
        //    AngleConstant = (float)(45 * (Math.PI / 180));
        //    Angle = AngleConstant;
        //    ballDirectionX = 1;
        //    ballDirectionY = -1;
        //    RadiusPow2 = width * width;
        //}

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
            RadiusPow2 = width * width;
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

            //float distX = Math.Abs(Center.X - (b.Pos.X + (b.Width/2)));
            //float distY = Math.Abs(Center.Y - (b.Pos.Y + (b.Height/2)));

            //if (distX > ((b.Width / 2) + (Width / 2))) return false;
            //if (distY > ((b.Height / 2) + (Height / 2))) return false;
            //if (distX <= (b.Width / 2)) return true;
            //if (distY <= (b.Height / 2)) return true;

            //float dx = distX;
        }

        //public void Rebond(Brick b)
        //{
        //    if (Util.IsColide((int)Pos.X, (int)Pos.Y, Width, Height,
        //        (int)b.Pos.X, (int)b.Pos.Y, b.Width, b.Height) == true)
        //    //if (IsColide(b) == true)
        //    //if(Util.IsColide(Box, b.Box) == true)
        //    {

        //        if ((b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width) ||
        //            (b.Pos.X < Pos.X && Pos.X < b.Pos.X + b.Width) ||
        //            (b.Pos.X < Pos.X + Width && Pos.X + Width < b.Pos.X + b.Width))
        //        //if (b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width)
        //        { //TOP OR BOT
        //            ballDirectionY *= -1;
        //            if (Pos.Y <= b.Pos.Y) //TOP
        //            {
        //                Console.WriteLine("collision haut");
        //                Pos = new Vector2(Pos.X,b.Pos.Y - Height);
        //                if (b.GetType() == typeof(Racket))
        //                {
        //                    float midRacket = (b.Width / 2);
        //                    if (ballDirectionX == -1)
        //                    {
        //                        float marge = Center.X - Math.Abs(b.Pos.X);
        //                        Angle = AngleConstant * (marge / midRacket);
        //                    }
        //                    else if (ballDirectionX == 1)
        //                    {

        //                        float marge = Math.Abs((b.Pos.X + b.Width)) - Center.X;
        //                        Angle = AngleConstant * (marge / midRacket);
        //                    }
        //                    Speed += 0.05f;
        //                }
        //            }
        //            else if (Pos.Y + Height >= b.Pos.Y + b.Height) //BOT
        //            {
        //                Console.WriteLine("collision bas");
        //                Pos = new Vector2(Pos.X, b.Pos.Y + b.Height);
        //            }
        //        }
        //        else if ((b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height) ||
        //            (b.Pos.Y < Pos.Y && Pos.Y < b.Pos.Y + b.Height) ||
        //            (b.Pos.Y < Pos.Y + Height && Pos.Y + Height < b.Pos.Y + b.Height))
        //        //else if (b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height)
        //        {   //RIGHT OR LEFT
        //            ballDirectionX *= -1;
        //            if (Pos.X <= b.Pos.X)
        //            {
        //                Console.WriteLine("collision left");
        //                Pos = new Vector2(b.Pos.X - Width, Pos.Y);
        //            }
        //            else if (Pos.X + Height >= b.Pos.X + b.Height)
        //            {
        //                Console.WriteLine("collision left");
        //                Pos = new Vector2(b.Pos.X + b.Width, Pos.Y);
        //            }
        //        }

        //        if (b.GetType() != typeof(Racket))
        //            b.Power += -1;
        //    }


        //}

        public bool Rebond(Brick b)
        {
            if (Util.IsColide((int)Pos.X, (int)Pos.Y, Width, Height,
                (int)b.Pos.X, (int)b.Pos.Y, b.Width, b.Height) == true)
            //if (IsColide(b) == true)
            //if(Util.IsColide(Box, b.Box) == true)
            {
                Console.WriteLine("------------------------------");
                //Console.WriteLine($"X : {b.Pos.X} < {Center.X} < {b.Pos.X + b.Width}");
                //Console.WriteLine($"Y : {b.Pos.Y} < {Center.Y} < {b.Pos.Y + b.Height}");
                //if ((b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height))
                ////else if (b.Pos.Y < Center.Y && Center.Y < b.Pos.Y + b.Height)
                //{   //RIGHT OR LEFT
                //    ballDirectionX *= -1;
                //    if (Center.X <= b.Pos.X)
                //    {
                //        Console.WriteLine($"{Center.X} <= {b.Pos.X}");
                //        Console.WriteLine("collision left");
                //        Pos = new Vector2(b.Pos.X - Width, Pos.Y);
                //    }
                //    else if (Center.X >= b.Pos.X + b.Height)
                //    {
                //        Console.WriteLine($"{Center.X} >= {b.Pos.X + b.Height}");
                //        Console.WriteLine("collision left");
                //        Pos = new Vector2(b.Pos.X + b.Width, Pos.Y);
                //    }
                //    if (b.GetType() != typeof(Racket))
                //        b.Power += -1;
                //}

                //else if ((b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width))
                ////if (b.Pos.X < Center.X && Center.X < b.Pos.X + b.Width)
                //{ //TOP OR BOT
                //    ballDirectionY *= -1;
                //    if (Center.Y <= b.Pos.Y) //TOP
                //    {
                //        Console.WriteLine($"{Center.Y} <= {b.Pos.Y}");
                //        Console.WriteLine("collision haut");
                //        Pos = new Vector2(Pos.X, b.Pos.Y - Height);
                //        if (b.GetType() == typeof(Racket))
                //        {
                //            float midRacket = (b.Width / 2);
                //            if (ballDirectionX == -1)
                //            {
                //                float marge = Center.X - Math.Abs(b.Pos.X);
                //                Angle = AngleConstant * (marge / midRacket);
                //            }
                //            else if (ballDirectionX == 1)
                //            {

                //                float marge = Math.Abs((b.Pos.X + b.Width)) - Center.X;
                //                Angle = AngleConstant * (marge / midRacket);
                //            }
                //            Speed += 0.05f;
                //        }
                //    }
                //    else if (Center.Y >= b.Pos.Y + b.Height) //BOT
                //    {
                //        Console.WriteLine($"{Center.Y} >= {b.Pos.Y + b.Height}");
                //        Console.WriteLine("collision bas");
                //        Pos = new Vector2(Pos.X, b.Pos.Y + b.Height);
                //    }
                //    if (b.GetType() != typeof(Racket))
                //        b.Power += -1;
                //}

                if (((Math.Pow(Center.X - b.Pos.X, 2) + Math.Pow(Center.Y - b.Pos.Y, 2)) <= RadiusPow2) && //LeftTOP
                   (b.GetType() == typeof(Racket)))
                {
                    if (ballDirectionX == 1)
                        ballDirectionX = -1;
                    ballDirectionY *= -1;
                }
                else if(((Math.Pow(Center.X - (b.Pos.X + b.Width), 2) + Math.Pow(Center.Y - b.Pos.Y, 2)) <= RadiusPow2) && //RightTOP
                   (b.GetType() == typeof(Racket)))
                {
                    if (ballDirectionX == -1)
                        ballDirectionX = 1;
                    ballDirectionY *= -1;
                }                //else if (((Math.Pow(Center.X - b.Pos.X, 2) + Math.Pow(Center.Y - (b.Pos.Y + b.Height), 2)) <= RadiusPow2)) //LeftBOT
                //{
                //    ballDirectionX = -1;
                //    ballDirectionY = 1;
                //}
                //else if (((Math.Pow(Center.X - (b.Pos.X + b.Width), 2) + Math.Pow(Center.Y - (b.Pos.Y + b.Height), 2)) <= RadiusPow2)) //RightBOT
                //{
                //    ballDirectionX = 1;
                //    ballDirectionY = 1;
                //}
                else
                {
                    if (b.Pos.Y <= Center.Y && Center.Y <= b.Pos.Y + b.Height &&
                        (b.Pos.X > Center.X || Center.X > b.Pos.X + b.Width))
                    {
                        for (float i = (b.Pos.Y); i <= (b.Pos.Y + b.Height); i++)
                        {
                            if (((Math.Pow(Center.X - b.Pos.X, 2) + Math.Pow(Center.Y - i, 2)) <= RadiusPow2))
                            {
                                Console.WriteLine("LEFT");
                                Console.WriteLine("Y : " +i);
                                ballDirectionX *= -1;
                                Pos = new Vector2(b.Pos.X - Height, Pos.Y);
                                return true;
                            }

                            if (((Math.Pow(Center.X - (b.Pos.X + b.Width), 2) + Math.Pow(Center.Y - i, 2)) <= RadiusPow2))
                            {
                                Console.WriteLine("RIGHT");
                                Console.WriteLine("Y : " + i);
                                ballDirectionX *= -1;
                                Pos = new Vector2(b.Pos.X + b.Width, Pos.Y);
                                return true;
                            }
                        }
                    }
                    
                    else if (b.Pos.X <= Center.X && Center.X <= b.Pos.X + b.Width &&
                        (b.Pos.Y > Center.Y || Center.Y > b.Pos.Y + b.Height))
                    {
                        for (float i = (b.Pos.X); i <= (b.Pos.X + b.Width); i++)
                        {
                            if (((Math.Pow(Center.X - i, 2) + Math.Pow(Center.Y - b.Pos.Y, 2)) <= RadiusPow2))
                            {
                                Console.WriteLine("TOP");
                                Console.WriteLine("X : "+ i);
                                ballDirectionY *= -1;
                                Pos = new Vector2(Pos.X, b.Pos.Y - Width);
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
                                return true;
                            }
                            else if (((Math.Pow(Center.X - i, 2) + Math.Pow(Center.Y - (b.Pos.Y + Height), 2)) <= RadiusPow2))
                            {
                                Console.WriteLine("BOT");
                                Console.WriteLine("X : " + i);
                                ballDirectionY *= -1;
                                Pos = new Vector2(Pos.X, b.Pos.Y + b.Height);
                                return true;
                            }
                        }
                    }

                }

            }
            return false;
        }
    }

}
