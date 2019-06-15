using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MRTS.GameComponents
{
    public class Level
    {
        public Tower[,] TileCollection { get; set; }
        private List<Texture2D> TowerGraphics;

        public Level(int x, int y, List<Texture2D> towerGraphics)
        {
            TileCollection = new Tower[x, y];
            TowerGraphics = towerGraphics;
            Initialize();
        }

        public void Initialize()
        {
            for(var i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (var j = 0; j < TileCollection.GetLength(1); j++)
                {
                    var graphic = TowerGraphics.FirstOrDefault();
                    if (graphic != null)
                    {
                        TileCollection[i, j] = new Tower(graphic, i, j);
                    }
                }
            }
        }

        public void Draw(SpriteBatch s)
        {
            for (var i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (var j = 0; j < TileCollection.GetLength(1); j++)
                {
                    var t = TileCollection[i, j];
                    s.Draw(t.Graphics[t.GraphicIndex], new Vector2(i * t.Dimensions.X, j * t.Dimensions.Y), Color.White);
                }
            }
        }

        public void Update(GameTime g, int x, int y)
        {
            for (var i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (var j = 0; j < TileCollection.GetLength(1); j++)
                {
                    var t = TileCollection[i, j];
                    if (t.GetCoordinates().Contains(new Point(x, y)) && t.CurrentHealth == 0)
                    {
                        TileCollection[i, j] = new Tower(TowerGraphics, 100, i, j);
                        TileCollection[i, j].CreateSpawnThread();
                    }
                }
            }
        }
    }
}
