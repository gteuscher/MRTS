using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class Tile : GameObject
    {
        private const int SIZE  = 128;

        public Tile(Texture2D texture, int posX, int posY)
        {
            Graphics = new List<Texture2D> { texture };
            Position = new Point(posX, posY);
            Dimensions = new Point(SIZE, SIZE);
        }
    }
}
