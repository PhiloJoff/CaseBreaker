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
    class SceneGameplay : Scene
    {
        private List<Brick> mesBricks;
        //Brick uneBrick;
        int brickWidth = 63;
        int brickHeight = 20;
        string[] map =
        {   "111111111111",
            "111111111111",
            "111111111111",
            "111111111111",
            "111111111111",
            "111000000111",
            "111111111111",
            "111111111111",
            "121111111121",
            "111144441111",
            "111111111111",
            "111113333311",
            "000000000000",
            "000000000000",
            "000000000000"
        };

        Racket uneRacket;
        int racketWidth;
        int racketHeight;

        Ball uneBall;
        int ballSize;
        Vector2 ballPos;
        int ballDirectionX;
        int ballDirectionY;

        bool isRunning;

        KeyboardState keyboardState;
        KeyboardState oldKbState;

        public SceneGameplay(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            racketWidth = 100;
            racketHeight = 15;

            ballDirectionX = 1;
            ballDirectionY = -1;
            ballSize = 15;
            mesBricks = new List<Brick>();
            mesBricks = GenerateMap(map);


            isRunning = false;

            oldKbState = Keyboard.GetState();
            uneRacket = new Racket(racketWidth, racketHeight, new Vector2((WindowWidth / 2) - (racketWidth / 2), WindowHeight - racketHeight - 5), 1, Graphic);
            uneBall = new Ball(ballSize, ballSize, new Vector2((WindowWidth / 2) - (ballSize / 2), uneRacket.Pos.Y - ballSize), 1, Graphic);
            base.Load();
        }

        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                mainGame.Exit();

            keyboardState = Keyboard.GetState();
            ballPos = uneBall.Pos;

            
            if (keyboardState.IsKeyDown(Keys.Space) && oldKbState.IsKeyDown(Keys.Space) != true)
            {
                oldKbState = Keyboard.GetState();
                if (isRunning != true)
                    isRunning = true;
            }

            if (isRunning == true)
            {
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    if (uneRacket.Pos.X <= 1)
                    {
                        //uneRacket.Pos = new Vector2(1, uneRacket.Pos.Y);
                        uneRacket.Pos.X = 1;
                    }
                    else
                    {
                        uneRacket.Pos.X = uneRacket.Pos.X - uneRacket.Speed;
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    if (uneRacket.Pos.X >= WindowWidth - racketWidth - 1)
                    {
                        uneRacket.Pos.X = WindowWidth - racketWidth - 1;
                    }
                    else
                    {
                        uneRacket.Pos.X = uneRacket.Pos.X + uneRacket.Speed;
                    }
                }

                if (ballPos.X >= WindowWidth - ballSize)
                {
                    ballDirectionX = -1;
                }

                if (ballPos.X <= 0)
                {
                    ballDirectionX = 1;
                }

                if (ballPos.Y >= WindowHeight - ballSize)
                {
                    ballDirectionY = -1;
                }

                if (ballPos.Y <= 0)
                {
                    ballDirectionY = 1;
                }

                uneBall.Pos.X = uneBall.Pos.X + uneBall.Speed * ballDirectionX;
                uneBall.Pos.Y = uneBall.Pos.Y + uneBall.Speed * ballDirectionY;
                uneBall.Center.X = uneBall.Pos.X + (uneBall.Width / 2);
                uneBall.Center.Y = uneBall.Pos.Y + (uneBall.Height / 2);


                if (uneBall.IsColide(uneRacket))
                    uneBall.Rebond(uneRacket, ref ballDirectionX, ref ballDirectionY);
                for(int i = 0; i < mesBricks.Count; i++)
                {
                    if (mesBricks[i].Power != 0)
                    {
                        if (uneBall.IsColide(mesBricks[i]))
                        {
                            uneBall.Rebond(mesBricks[i], ref ballDirectionX, ref ballDirectionY);
                        }
                        mesBricks[i].setColor(mesBricks[i], Graphic);
                    }
                    else
                    {
                        mesBricks.Remove(mesBricks[i]);
                    }
                }

            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            foreach (Brick b in mesBricks)
            {
                if (b.Power >= 1)
                {
                    spriteBatch.Draw(b.Rect, b.Pos, Color.White);
                }

            }
            spriteBatch.Draw(uneRacket.Rect, uneRacket.Pos, Color.White);

            if (uneBall.Power >= 1)
                spriteBatch.Draw(uneBall.Rect, uneBall.Pos, Color.White);

            base.Draw(gameTime);
        }


        // Generation de la map de brick
        private List<Brick> GenerateMap(string[] theMap)
        {
            List<Brick> theBrick = new List<Brick>();
            int X = -1;
            int Y = 0;
            string mapConsole = "\n";
            int toNumber;
            bool isNumber;

            for (int i = 0; i < theMap.Length; i++)
            {
                Y += 4;
                X += 4;
                foreach (char c in theMap[i])
                {
                    mapConsole += c;
                    isNumber = Int32.TryParse(c.ToString(), out toNumber);
                    if (isNumber == true)
                    {
                        if (c != 0)
                            theBrick.Add(new Brick(new Vector2(X, Y), toNumber, Graphic));
                    }
                    X += 4 + brickWidth;

                }
                Y += brickHeight;
                X = -1;
                mapConsole += "\n";
            }

            Console.WriteLine(mapConsole);
            return theBrick;
        }
    }
}
