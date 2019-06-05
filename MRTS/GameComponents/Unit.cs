using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MRTS.GameComponents
{
    public class Unit
    {
        public Texture2D UnitTexture { get; set; }
        public int StartingHealth { get; set; }
        public int CurrentHealth { get; set; }
        public int Size { get; set; } = 10;
        public Tower SelectedTower { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }

        public Unit(Texture2D unitTexture, int x, int y)
        {
            UnitTexture = unitTexture;
            //account for centering
            SpawnX = x-Size/2;
            SpawnY = y-Size/2;
        }
    }
}
