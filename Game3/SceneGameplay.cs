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
        {   "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000001",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000",
            "000000000000"
        };

        Racket uneRacket;
        int racketWidth;
        int racketHeight;

        Ball uneBall;
        int ballSize;

        bool isRunning;

        KeyboardState keyboardState;
        KeyboardState oldKbState;

        public SceneGameplay(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            mainGame.IsMouseVisible = false;
            racketWidth = 100;
            racketHeight = 15;

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


            if (keyboardState.IsKeyDown(Keys.Space) && oldKbState.IsKeyDown(Keys.Space) != true)
            {
                oldKbState = Keyboard.GetState();
                if (isRunning != true)
                    isRunning = true;
            }

            if (isRunning == true)
            {
                uneBall.Box = new Rectangle((int)uneBall.Pos.X, (int)uneBall.Pos.Y, uneBall.Width, uneBall.Height);

                uneRacket.Box = new Rectangle((int)uneRacket.Pos.X, (int)uneRacket.Pos.Y, uneRacket.Width, uneRacket.Height);
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    if (uneRacket.Pos.X <= 1)
                    {
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

                if (uneBall.Pos.X > WindowWidth - ballSize)
                {
                    uneBall.Pos.X = WindowWidth - ballSize;
                    uneBall.ballDirectionX = -1;
                }

                if (uneBall.Pos.X < 0)
                {
                    uneBall.Pos.X = 0;
                    uneBall.ballDirectionX = 1;
                }

                if (uneBall.Pos.Y >= WindowHeight - ballSize)
                {
                    mainGame.gameState.SwitchScene(GameState.SceneType.Gameplay);
                }

                if (uneBall.Pos.Y < 0)
                {
                    uneBall.Pos.Y = 0;
                    uneBall.ballDirectionY = 1;
                }

                uneBall.Pos.X += uneBall.Speed * uneBall.ballDirectionX * (float)Math.Abs(Math.Cos(uneBall.Angle));
                uneBall.Pos.Y += uneBall.Speed * uneBall.ballDirectionY * (float)Math.Abs(Math.Sin(uneBall.AngleConstant));
                uneBall.Center.X = Math.Abs(uneBall.Pos.X + (uneBall.Width / 2));
                uneBall.Center.Y = Math.Abs(uneBall.Pos.Y + (uneBall.Height / 2));


                uneBall.Rebond(uneRacket);
                for (int i = 0; i < mesBricks.Count; i++)
                {
                    if (mesBricks[i].Power != 0)
                    {
                        uneBall.Rebond(mesBricks[i]);
                        mesBricks[i].SetColor(mesBricks[i], Graphic);
                    }
                    else
                    {
                        mesBricks.Remove(mesBricks[i]);
                    }
                }

                if (mesBricks.Count <= 0)
                {
                    mainGame.gameState.SwitchScene(GameState.SceneType.Win);
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

            //Console.WriteLine(mapConsole);
            return theBrick;
        }
    }
}
