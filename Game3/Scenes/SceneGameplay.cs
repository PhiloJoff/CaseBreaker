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
        private int BrickWidth { get; set; }
        private int BrickHeight { get; set; }
        public Rectangle GameZone { get; set; }
        private Texture2D background;
        private Texture2D tilesBrick;

        int[,] mapInt = { //int[20,15]
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 4, 4, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 3, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 1},
            {1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        //string[] map = {
        //    "000000040000000",
        //    "000000414000000",
        //    "000000414000000",
        //    "000004111400000",
        //    "000004111400000",
        //    "444441111144444",
        //    "041111414111140",
        //    "004111414111400",
        //    "000411111114000",
        //    "004111111111400",
        //    "004111111111400",
        //    "042111141111240",
        //    "042224404422240",
        //    "422440000044224",
        //    "444000000000444",
        //    "055555000555550"
        //};

        Racket uneRacket;
        int racketWidth;
        int racketHeight;
        private Texture2D textureRacket;

        Ball uneBall;
        int ballSize;
        private Texture2D textureBall;

        bool isRunning;

        KeyboardState keyboardState;
        KeyboardState oldKbState;

        Brick uneBrick;

        public SceneGameplay(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            mainGame.IsMouseVisible = false;
            isRunning = false;

            GameZone = new Rectangle(25, 25, 495, 555);
            background = mainGame.Content.Load<Texture2D>("backgroundGame");

            racketWidth = 85;
            racketHeight = 15;
            textureRacket = mainGame.Content.Load<Texture2D>("racket");
            //uneRacket = new Racket(racketWidth, racketHeight, new Vector2(((GameZone.Right) / 2) - (racketWidth / 2), 550), 1, Graphic);
            uneRacket = new Racket(textureRacket, new Vector2(((GameZone.Right) / 2) - (racketWidth / 2), 550), racketWidth, racketHeight);

            textureBall = mainGame.Content.Load<Texture2D>("ball");
            ballSize = 12;
            //uneBall = new Ball(ballSize, ballSize, new Vector2(((GameZone.Right) / 2) - (ballSize / 2), uneRacket.Pos.Y - ballSize), 1, Graphic);
            uneBall = new Ball(textureBall, new Vector2(((GameZone.Right) / 2) - (ballSize / 2), uneRacket.Pos.Y - ballSize),ballSize, ballSize);

            mesBricks = new List<Brick>();
            tilesBrick = mainGame.Content.Load<Texture2D>("bricks");
            BrickWidth = 33;
            BrickHeight = 17;
            mesBricks = GenerateMap(mapInt);

            uneBrick = new Brick(tilesBrick, BrickWidth, BrickHeight, new Vector2(GameZone.X + 100, GameZone.Y + 200), 5);

            oldKbState = Keyboard.GetState();
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
                //oldKbState = Keyboard.GetState();
                if (isRunning != true)
                    isRunning = true;
            }

            if (isRunning == true)
            {
                uneBall.Box = new Rectangle((int)uneBall.Pos.X, (int)uneBall.Pos.Y, uneBall.Width, uneBall.Height);

                uneRacket.Box = new Rectangle((int)uneRacket.Pos.X, (int)uneRacket.Pos.Y, uneRacket.Width, uneRacket.Height);
                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    if (uneRacket.Pos.X <= GameZone.X)
                    {
                        uneRacket.Pos = new Vector2(GameZone.X, uneRacket.Pos.Y);
                    }
                    else
                    {
                        uneRacket.Pos = new Vector2(uneRacket.Pos.X - uneRacket.Speed, uneRacket.Pos.Y);
                    }
                }

                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    if (uneRacket.Pos.X >= GameZone.Right - racketWidth)
                    {
                        uneRacket.Pos = new Vector2(GameZone.Right - racketWidth, uneRacket.Pos.Y);
                    }
                    else
                    {
                        uneRacket.Pos = new Vector2(uneRacket.Pos.X + uneRacket.Speed, uneRacket.Pos.Y);
                    }
                }

                if (uneBall.Pos.X > GameZone.Right - ballSize)
                {
                    uneBall.Pos = new Vector2(GameZone.Right - ballSize,uneBall.Pos.Y);
                    uneBall.ballDirectionX = -1;
                }

                if (uneBall.Pos.X < GameZone.X)
                {
                    uneBall.Pos = new Vector2(GameZone.X, uneBall.Pos.Y);
                    uneBall.ballDirectionX = 1;
                }

                if (uneBall.Pos.Y >= GameZone.Bottom - ballSize)
                {
                    mainGame.gameState.SwitchScene(GameState.SceneType.Gameplay);
                }

                if (uneBall.Pos.Y < GameZone.Y)
                {
                    uneBall.Pos = new Vector2(uneBall.Pos.X, GameZone.Y);
                    uneBall.ballDirectionY = 1;
                }
                uneBall.Pos = new Vector2(uneBall.Pos.X + uneBall.Speed * uneBall.ballDirectionX * (float)Math.Abs(Math.Cos(uneBall.Angle)),
                uneBall.Pos.Y + uneBall.Speed * uneBall.ballDirectionY * (float)Math.Abs(Math.Sin(uneBall.AngleConstant)));
                uneBall.Center.X = Math.Abs(uneBall.Pos.X + (uneBall.Width / 2));
                uneBall.Center.Y = Math.Abs(uneBall.Pos.Y + (uneBall.Height / 2));


                uneBall.Rebond(uneRacket);
                for (int i = 0; i < mesBricks.Count; i++)
                {
                    if (mesBricks[i].Power != 0)
                    {
                        if (Util.IsColide((int)uneBall.Pos.X, (int)uneBall.Pos.Y, uneBall.Width, uneBall.Height,
                (int)mesBricks[i].Pos.X, (int)mesBricks[i].Pos.Y, mesBricks[i].Width, mesBricks[i].Height) == true)
                        {
                            if (uneBall.Rebond(mesBricks[i]) == true)
                            {
                                mesBricks[i].Power -= 1;
                                break;
                            }
                            //isRunning = false;
                        }
                        //mesBricks[i].SetColor(mesBricks[i], Graphic);
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


            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            foreach (Brick b in mesBricks)
            {
                if (b.Power >= 1)
                {
                    //b.Draw(spriteBatch);
                    spriteBatch.Draw(b.Texture, b.Pos, b.GetTile(), Color.White);
                }

            }
            spriteBatch.Draw(uneRacket.Texture, uneRacket.Pos, Color.White);

            if (uneBall.Power >= 1)
                //spriteBatch.Draw(uneBall.Rect, uneBall.Pos, Color.White);
                //spriteBatch.Draw(uneBall.Texture, uneBall.Pos, null, Color.White, 0, Vector2.Zero, new Vector2(0.1f, 0.1f), 0, 0);
                spriteBatch.Draw(uneBall.Texture, uneBall.Pos, Color.White);

            base.Draw(gameTime);
        }


        // Generation de la map de brick
        //private List<Brick> GenerateMap(string[] theMap)
        //{
        //    List<Brick> theBrick = new List<Brick>();
        //    float X = 25;
        //    float Y = 25;
        //    //string mapConsole = "\n";
        //    int toNumber;
        //    bool isNumber;
        //    for (int i = 0; i < theMap.Length; i++)
        //    {
        //        Y += 0f;
        //        X += 0f;
        //        foreach (char c in theMap[i])
        //        {
        //            //mapConsole += c;
        //            isNumber = Int32.TryParse(c.ToString(), out toNumber);
        //            if (isNumber == true)
        //            {
        //                if (toNumber > 0)
        //                    theBrick.Add(new Brick(tilesBrick, BrickWidth, BrickHeight, new Vector2(X, Y), toNumber));
        //            }
        //            X += 0f + BrickWidth;

        //        }
        //        Y += BrickHeight;
        //        X = 25f;
        //        //mapConsole += "\n";
        //    }

        //    //Console.WriteLine(mapConsole);
        //    return theBrick;
        //}

        // Generation de la map de brick
        private List<Brick> GenerateMap(int[,] theMap)
        {
            List<Brick> theBrick = new List<Brick>();
            float X = 25;
            float Y = 25;
            //string mapConsole = "\n";
            for (int row = 0; row <= theMap.GetUpperBound(0); row++)
            {
                for (int colunm = 0; colunm < theMap.GetUpperBound(1); colunm++)
                {
                    Y += 0f;
                    X += 0f;
                    //mapConsole += c;
                    if (theMap[row, colunm] > 0)
                        theBrick.Add(new Brick(tilesBrick, BrickWidth, BrickHeight, new Vector2(X, Y), theMap[row, colunm]));
                    X += 0f + BrickWidth;

                }
                Y += BrickHeight;
                X = 25f;
                //mapConsole += "\n";
            }

            //Console.WriteLine(mapConsole);
            return theBrick;
        }
    }
}
