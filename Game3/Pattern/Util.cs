using Microsoft.Xna.Framework;
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
    }
}
