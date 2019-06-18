using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class GameObject
    {
        public List<Texture2D> Textures { get; set; }
        public int GraphicIndex { get; set; }
        public int StartingHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int SpawnRate { get; set; }
        public Point Position { get; set; }
        public Point Dimensions { get; set; }

        public GameManager GameManager { get; set; }

        public GameObject()
        {
            GameManager = GameServices.GetService<GameManager>();
        }

        public Texture2D CurrentGraphic => Textures.FirstOrDefault();

        public Rectangle Coordinates => new Rectangle(Position.X, Position.Y, Dimensions.X, Dimensions.Y);
    }
}
