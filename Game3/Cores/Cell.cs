using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class Cell : Brick
    {
        public bool Selected { get; private set; }
        public int Row { get; private set; }
        public int Colunm { get; private set; }

        public Cell(Texture2D texture, int width, int height, Vector2 pos, int power) : base(texture, width, height, pos, power)
        {

            Selected = false;
        }

        public Cell(Texture2D texture, int width, int height, Vector2 pos, int power, int row, int colunm) : base(texture, width, height, pos, power)
        {
            Row = row;
            Colunm = colunm;
            Selected = false;
        }

        public void Select()
        {
            Selected = !Selected;
        }
    }
}
