using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MRTS.GameComponents
{
    public class Level
    {
        public Point Dimensions { get; set; }

        public Level(int x, int y)
        {
            Dimensions = new Point(x, y);
        }
    }
}
