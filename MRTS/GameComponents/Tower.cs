using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MRTS.GameComponents
{
    class Tower : Tile
    {
        //[0] is default texture [1] is damage texture, [2] is destroyed texture
        private List<Texture2D> TowerGraphics;
        public int TotalHealth{ get; set; }
        public int CurrentHealth { get; set; }
        public int SpawnRate { get; set; }

        public Tower(Texture2D tileTexture, int x, int y): base (tileTexture, x, y)
        {
        }

        public Tower(List<Texture2D> towerGraphics, int totalHealth, int x, int y, int spawnRate = 10000): base (towerGraphics[1], x, y)
        {
            TowerGraphics = towerGraphics;
            TotalHealth = totalHealth;
            CurrentHealth = totalHealth;
            SpawnRate = spawnRate;            
        }

        public int Damage(int damageDealt)
        {
            if (CurrentHealth <= damageDealt)
            {
                TileTexture = TowerGraphics[3];
            }
            else
            {
                TileTexture = TowerGraphics[2];
            }
            
            return CurrentHealth -= damageDealt;
        }
    }
}
