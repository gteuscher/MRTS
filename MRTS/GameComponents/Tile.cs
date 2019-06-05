using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class Tile
    {
        public Texture2D TileTexture { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int Size { get; set; } = 128;

        public Tile(Texture2D t, int x, int y)
        {
            TileTexture = t;
            this.x = x;
            this.y = y;
        }

        public Rectangle GetCoordinates()
        {
            return new Rectangle(x*Size, y*Size, Size, Size);
        }
    }
}
