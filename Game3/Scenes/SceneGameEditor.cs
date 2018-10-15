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
    class SceneGameEditor : Scene
    {
        
        private Texture2D background;

        private int[] editorPower = { 1, 2, 3, 4, 5, 0 };
        private Rectangle ZoneEditor;
        Texture2D emptyTexture2D;
        Texture2D brick;
        private Brick[] bricksEditor;
        private int brickEditorWidth;
        private int brickEditorHeight;


        private List<Brick> bricksGame;
        int brickGameWidth;
        int brickGameHeight;
        private Rectangle brickZone;
        int[,] mapInt = { //int[20,15]
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        };

        MouseState oldMouseState;
        MouseState mouseState;


        public SceneGameEditor(MainGame mainGame) : base(mainGame)
        {
        }

        public override void Load()
        {
            background = mainGame.Content.Load<Texture2D>("backgroundEditor");
            brick = mainGame.Content.Load<Texture2D>("bricks");

            oldMouseState = new MouseState();
            oldMouseState = Mouse.GetState();
            mouseState = new MouseState();

            ZoneEditor = new Rectangle(617, 325, 116, 40);

            brickZone = new Rectangle(25, 25, 495, 365);

            brickEditorWidth = 38;
            brickEditorHeight = 20;
            bricksEditor = new Brick[6];
            int x = 0;
            int y = 0;
            for (int i = 0; i < bricksEditor.Length; i++)
            {
                bricksEditor[i] = new Brick(
                    new Texture2D(mainGame.GraphicsDevice, brickEditorWidth, brickEditorHeight),
                    brickEditorWidth,
                    brickEditorHeight,
                    new Vector2(ZoneEditor.X + x, ZoneEditor.Y + y),
                    editorPower[i]);
                if(i == 2)
                {
                    x = 0;
                    y += brickEditorHeight;
                }
                else
                {
                    x += brickEditorWidth;
                }
            }
            brickGameWidth = 33;
            brickGameHeight = 17;
            emptyTexture2D = new Texture2D(mainGame.GraphicsDevice, brickGameWidth, brickGameHeight);
            bricksGame = GenerateMap(mapInt);

            base.Load();
        }
         
        public override void Unload()
        {
            base.Unload();
        }

        public override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if(HoverZone(ZoneEditor) == true)
            {
                for (int i = 0; i < bricksEditor.Length; i++)
                {
                    if (HoverBrick(bricksEditor[i]) == true &&
                        mouseState.LeftButton == ButtonState.Pressed && 
                        mouseState.LeftButton != oldMouseState.LeftButton  )
                    {
                        Console.WriteLine($"Brick Editor : {i}");
                        break;
                    }
                }
            } else if (HoverZone(brickZone)== true)
            {
                for (int i = 0; i < bricksGame.Count; i++)
                {
                    if (HoverBrick(bricksGame[i]) == true &&
                        mouseState.LeftButton == ButtonState.Pressed &&
                        mouseState.LeftButton != oldMouseState.LeftButton)
                    {
                        Console.WriteLine($"Brick game : {i}");
                        break;
                    }
                }

            }

            oldMouseState = mouseState;
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Draw(background, Vector2.Zero, Color.White);
            base.Draw(gameTime);
        }



        //Generation de la map de brick
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
                    theBrick.Add(new Brick(emptyTexture2D, brickGameWidth, brickGameHeight, new Vector2(X, Y), theMap[row, colunm]));
                    X += 0f + brickGameWidth;

                }
                Y += brickGameHeight;
                X = 25f;
            }
            
            return theBrick;
        }

        private bool HoverZone(Rectangle zone)
        {
            return Util.IsHover(mouseState, (int)zone.X, (int)zone.Y, zone.Width, zone.Height) ;
        }

        private bool HoverBrick(Brick b)
        {
            return Util.IsHover(mouseState, (int)b.Pos.X, (int)b.Pos.Y, b.Width, b.Height);
        }

        
        public Rectangle GetTile(int power)
        {
            return new Rectangle((power - 1) * brickEditorWidth, 0, brickEditorWidth, brickEditorWidth);
        }
    }
}
