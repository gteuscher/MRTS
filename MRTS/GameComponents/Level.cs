using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MRTS.GameComponents
{
    public class Level
    {
        public List<GameObject> GameObjects { get; set; }
        public Army Army { get; set; }
        private List<Texture2D> TowerGraphics;
        private Texture2D TileGraphic;
        private Point Dimensions { get; set; }

        public Level(int x, int y, List<Texture2D> towerGraphics, Texture2D tileGraphic)
        {
            Dimensions = new Point(x, y);
            GameObjects = new List<GameObject>();
            TowerGraphics = towerGraphics;
            TileGraphic = tileGraphic;
            Initialize();
        }

        public void Initialize()
        {
            Army = new Army();
            
            for(var x = 0; x < Dimensions.X; x++)
            {
                for (var y = 0; y < Dimensions.Y; y++)
                {
                    GameObjects.Add(new Tile(TileGraphic, Tile.SIZE * x, Tile.SIZE * y));
                }
            }
        }

        public void Draw(SpriteBatch s)
        {
            GameObjects.ForEach(go =>
            {
                s.Draw(go.CurrentGraphic, new Vector2(go.Position.X, go.Position.Y),
                    Color.White);
            });

        }

        public void Update(GameTime g, int x, int y)
        {
            var quadrant = GameObjects.GetTiles().FirstOrDefault(t => t.Coordinates.Contains(new Point(x, y)));
            // TODO Null check on quadrant is a hack. Only trigger update if mouseclick was in game window
            if (quadrant != null && !GameObjects.GetTowers().Any(t => t.Coordinates.Contains(new Point(x, y)) && t.CurrentHealth == 0))
            {
                GameObjects.Add(new Tower(TowerGraphics, 100, quadrant.Position.X, quadrant.Position.Y));
            }
        }
    }
}
