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
        Texture2D cellTexture;
        private Cell[] cellsEditor;
        private int cellWidth;
        private int cellHeight;
        private Cell currentCell;


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
            cellTexture = mainGame.Content.Load<Texture2D>("bricks");

            oldMouseState = new MouseState();
            oldMouseState = Mouse.GetState();
            mouseState = new MouseState();

            ZoneEditor = new Rectangle(617, 325, 116, 40);

            brickZone = new Rectangle(25, 25, 495, 365);

            cellWidth = 38;
            cellHeight = 20;
            cellsEditor = new Cell[6];
            int x = 0;
            int y = 0;
            for (int i = 0; i < cellsEditor.Length; i++)
            {
                cellsEditor[i] = new Cell(
                    new Texture2D(mainGame.GraphicsDevice, cellWidth, cellHeight),
                    cellWidth,
                    cellHeight,
                    new Vector2(ZoneEditor.X + x, ZoneEditor.Y + y),
                    editorPower[i]);
                if(i == 2)
                {
                    x = 0;
                    y += cellHeight;
                }
                else
                {
                    x += cellWidth;
                }
            }
            currentCell = cellsEditor[5];
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
                for (int i = 0; i < cellsEditor.Length; i++)
                {
                    if (HoverBrick(cellsEditor[i]) == true &&
                        mouseState.LeftButton == ButtonState.Pressed && 
                        mouseState.LeftButton != oldMouseState.LeftButton  )
                    {
                        currentCell = cellsEditor[i];
                        currentCell.Select();
                        Console.WriteLine($"Cell Power : {currentCell.Power} et {currentCell.Selected}");
                        break;
                    }
                }
            } else if (HoverZone(brickZone)== true)
            {
                for (int i = 0; i < bricksGame.Count; i++)
                {
                    if (HoverBrick(bricksGame[i]) == true &&
                        mouseState.LeftButton == ButtonState.Pressed &&
                        mouseState.LeftButton != oldMouseState.LeftButton &&
                        currentCell.Selected == true)
                    {
                        if (bricksGame[i].Power != currentCell.Power)
                        {
                            bricksGame[i].Power = currentCell.Power;


                        }

                        Console.WriteLine($"Brick game : {i} et power : {bricksGame[i].Power}");
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
            foreach (Brick b in bricksGame)
            {
                if (b.Power >= 1)
                {
                    spriteBatch.Draw(b.Texture, b.Pos, b.GetTile(), Color.White);
                }

            }
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
                    theBrick.Add(new Brick(cellTexture, brickGameWidth, brickGameHeight, new Vector2(X, Y), theMap[row, colunm]));
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

        
    }
}
