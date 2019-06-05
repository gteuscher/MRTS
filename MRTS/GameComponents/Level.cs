using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MRTS.GameComponents
{
    class Level
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
            for(int i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (int j = 0; j < TileCollection.GetLength(1); j++)
                {
                    TileCollection[i, j] = new Tower(TowerGraphics[0], i, j);
                }
            }
        }

        public void Draw(SpriteBatch s)
        {
            for (int i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (int j = 0; j < TileCollection.GetLength(1); j++)
                {
                    var t = TileCollection[i, j];
                    s.Draw(t.TileTexture, new Vector2(i * t.Size, j * t.Size), Color.White);
                }
            }
        }

        public void Update(GameTime g, int x, int y)
        {
            for (int i = 0; i < TileCollection.GetLength(0); i++)
            {
                for (int j = 0; j < TileCollection.GetLength(1); j++)
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
