using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    public class Racket : Brick

    {
        public float Speed { get; set; }
        //public Racket(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) :base(width, height, pos, power,graphics)
        //{
        //    this.Speed = 5;
        //    Rect = new Texture2D(graphics.GraphicsDevice, width, height);
        //    for (int i = 0; i < data.Length; ++i) data[i] = Color.Green;
        //    Rect.SetData(data);
        //}

        public Racket(Texture2D texture, Vector2 pos, int width, int height) : base(texture, pos)
        {
            Speed = 4;
            Texture = texture;
            Pos = pos;
            Width = width;
            Height = height;
            Power = 1;
        }
    }
}
