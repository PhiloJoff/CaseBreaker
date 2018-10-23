using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XInput = Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Json;

namespace CaseBreaker
{
    class SceneGameEditor : Scene
    {
        
        private Texture2D background;
        
        private int[] editorPower = { 1, 2, 3, 4, 5, 0 };
        private Rectangle ZoneEditor;
        private Texture2D emptyTexture2D;
        private Texture2D cellTexture;
        private Cell[] cellsEditor;
        private int cellWidth;
        private int cellHeight;
        private Cell currentCell;


        private List<Cell> bricksGame;
        private int brickGameWidth;
        private int brickGameHeight;
        private Rectangle brickZone;
        private int[,] mapInt = { //int[20,15]
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

        private MouseState oldMouseState;
        private MouseState mouseState;

        private SaveFileDialog saveFileDialog;
        
        private Rectangle saveRect;


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

            saveRect = new Rectangle(617, 388, 20, 20);

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
                    editorPower[i]
                    );
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
                        mouseState.LeftButton == XInput.ButtonState.Pressed && 
                        mouseState.LeftButton != oldMouseState.LeftButton  )
                    {
                        currentCell = cellsEditor[i];
                        currentCell.Select();
                        break;
                    }
                }
            }
            else if (HoverZone(brickZone)== true)
            {
                for (int i = 0; i < bricksGame.Count; i++)
                {
                    if (HoverBrick(bricksGame[i]) == true &&
                        mouseState.LeftButton == XInput.ButtonState.Pressed &&
                        mouseState.LeftButton != oldMouseState.LeftButton &&
                        currentCell.Selected == true)
                    {
                        if (bricksGame[i].Power != currentCell.Power)
                        {
                            bricksGame[i].Power = currentCell.Power;
                            mapInt[bricksGame[i].Row, bricksGame[i].Colunm] = currentCell.Power;
                        }

                        Console.WriteLine($"Brick game : {i} et power : {bricksGame[i].Power}");
                        break;
                    }
                }
                

            }
            else if (HoverZone(saveRect) == true)
            {
                if (mouseState.LeftButton == XInput.ButtonState.Pressed &&
                    mouseState.LeftButton != oldMouseState.LeftButton)
                {
                    saveFileDialog = new SaveFileDialog();
                    saveFileDialog.AddExtension = true;
                    saveFileDialog.DefaultExt = ".lvl";
                    saveFileDialog.Filter = "Stage File|*.lvl";
                    saveFileDialog.Title = "Save a stage file";
                    if(saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        Console.WriteLine("Save ok");
                        if (saveFileDialog.FileName.Equals("") != true)
                        {
                            saveFileDialog.FileName = "StageEditor";
                        }
                        SaveEditor saveEditor = new SaveEditor();
                        saveEditor.ConvertToWrite(mapInt);
                        MemoryStream stream = new MemoryStream();
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SaveEditor));
                        serializer.WriteObject(stream, saveEditor);
                        stream.Position = 0;
                        StreamReader streamReader = new StreamReader(stream);
                        string strMap = streamReader.ReadToEnd();
                        File.WriteAllText(saveFileDialog.FileName, strMap);

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
        private List<Cell> GenerateMap(int[,] theMap)
        {
            List<Cell> theBrick = new List<Cell>();
            float X = 25;
            float Y = 25;
            //string mapConsole = "\n";
            for (int row = 0; row <= theMap.GetUpperBound(0); row++)
            {
                for (int colunm = 0; colunm < theMap.GetUpperBound(1); colunm++)
                {
                    Y += 0f;
                    X += 0f;
                    theBrick.Add(new Cell(cellTexture, brickGameWidth, brickGameHeight, new Vector2(X, Y), theMap[row, colunm], row, colunm));
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
