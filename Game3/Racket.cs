using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
    class Racket : Brick

    {
        public float Speed { get; set; }
        public Racket(int width, int height, Vector2 pos, int power, GraphicsDeviceManager graphics) :base(width, height, pos, power,graphics)
        {
            this.Speed = 5;
            Rect = new Texture2D(graphics.GraphicsDevice, width, height);
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Blue;
            Rect.SetData(data);
        }
    }
}
