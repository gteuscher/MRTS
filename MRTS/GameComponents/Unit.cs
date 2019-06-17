using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class Unit : GameObject
    {
        public const int SIZE = 10;

        public Unit(Texture2D texture, int x, int y)
        {
            Textures = new List<Texture2D> { texture };
            Position = new Point(x, y);
            Dimensions = new Point(SIZE, SIZE);
        }
    }
}
