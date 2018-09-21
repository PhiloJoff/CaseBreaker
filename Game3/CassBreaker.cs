using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace CassBreaker
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class CassBreaker : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        int windowWidth = 800;
        int windowHeight = 600;

        private List<Brick> mesBricks;
        //Brick uneBrick;
        int brickWidth = 62;
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


        public CassBreaker()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = windowHeight,
                PreferredBackBufferWidth = windowWidth
            };
            Window.Title = "Casse-Briques";
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            racketWidth = 100;
            racketHeight = 15;

            ballDirectionX = 1;
            ballDirectionY = -1;
            ballSize = 15;
            mesBricks = GenerateMap(map);
            

            isRunning = false;

            oldKbState = Keyboard.GetState(); 
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //uneBrick = new Brick(new Vector2(3, 280), 2, graphics);

            uneRacket = new Racket(racketWidth, racketHeight, new Vector2((windowWidth / 2) - (racketWidth / 2), windowHeight - racketHeight - 5), 1, graphics);
            uneBall = new Ball(ballSize, ballSize, new Vector2((windowWidth / 2) - (ballSize / 2), uneRacket.Pos.Y - ballSize), 1, graphics);
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            ballPos = uneBall.Pos;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || keyboardState.IsKeyDown(Keys.Escape))
                Exit();

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
                    if (uneRacket.Pos.X >= windowWidth - racketWidth - 1)
                    {
                        uneRacket.Pos.X = windowWidth - racketWidth - 1;
                    }
                    else
                    {
                        uneRacket.Pos.X = uneRacket.Pos.X + uneRacket.Speed;
                    }
                }

                if (ballPos.X >= windowWidth - ballSize)
                {
                    ballDirectionX = -1;
                }

                if (ballPos.X <= 0)
                {
                    ballDirectionX = 1;
                }

                if (ballPos.Y >= windowHeight - ballSize)
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
                        mesBricks[i].setColor(mesBricks[i], graphics);
                    }
                    else
                    {
                        mesBricks.Remove(mesBricks[i]);
                    }
                }

            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            string mapConsole = "\n";
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            foreach (Brick b in mesBricks)
            {
                mapConsole += b.Power + "-";
                if (b.Power >= 1)
                {
                    spriteBatch.Draw(b.Rect, b.Pos, Color.White);
                }

            }
            spriteBatch.Draw(uneRacket.Rect, uneRacket.Pos, Color.White);

            if (uneBall.Power >= 1)
                spriteBatch.Draw(uneBall.Rect, uneBall.Pos, Color.White);
            spriteBatch.End();
            mapConsole += "\n";
            base.Draw(gameTime);
        }

        private List<Brick> GenerateMap(string[] theMap)
        {
            List<Brick> theBrick = new List<Brick>();
            int X = 0;
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
                            theBrick.Add(new Brick(new Vector2(X, Y), toNumber, graphics));
                    }
                    X += 4 + brickWidth;

                }
                Y += brickHeight;
                X = 0;
                mapConsole += "\n";
            }

            Console.WriteLine(mapConsole);
            return theBrick;
        }
    }
}
