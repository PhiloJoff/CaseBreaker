using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBreaker
{
     class Util
     {
        static Random RNG = new Random();

        public static int GetRandom(int min, int max)
        {
            return RNG.Next(min, max + 1);
        }

        public static bool IsColide(Rectangle a, Rectangle b)
        {

            return b.Contains(a);
        }
        public static bool IsColide(int aX, int aY, int aWidth, int aHeight,
            int bX, int bY, int bWidth, int bHeight)
        {

            if (aY + aHeight <= bY ||
                aY >= bY + bHeight ||
                aX + aWidth <= bX ||
                aX >= bX + bWidth)
            {
                return false;
            }
            return true;
        }

        public static bool IsHover(MouseState mouseState, int aX, int aY, int aWidth, int aHeight)
        {
            if (mouseState.Position.X > aX &&
                mouseState.Position.X < aX + aWidth &&
                mouseState.Position.Y > aY &&
                mouseState.Position.Y < aY + aHeight)
            {
                return true;
            }
            return false;
        }
     }
}
