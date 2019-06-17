using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class Tile : GameObject
    {
        public const int SIZE  = 128;

        public Tile(Texture2D texture, int posX, int posY)
        {
            Textures = new List<Texture2D> { texture };
            Position = new Point(posX, posY);
            Dimensions = new Point(SIZE, SIZE);
        }
    }
}
