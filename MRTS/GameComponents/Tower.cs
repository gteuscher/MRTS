using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MRTS.GameComponents
{
    public class Tower : Tile
    {
        //[0] is default texture [1] is damage texture, [2] is destroyed texture
        private List<Texture2D> TowerGraphics;
        public int StartingHealth{ get; set; }
        public int CurrentHealth { get; set; }
        public int SpawnRate { get; set; }
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        public Tower(Texture2D tileTexture, int x, int y): base (tileTexture, x, y)
        {
        }

        public Tower(List<Texture2D> towerGraphics, int startingHealth, int x, int y, int spawnRate = 3000): base (towerGraphics[1], x, y)
        {
            TowerGraphics = towerGraphics;
            StartingHealth = startingHealth;
            CurrentHealth = startingHealth;
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

        public void CreateSpawnThread()
        {

            var unitSpawner = new Thread(new ThreadStart(SpawnUnits));
            unitSpawner.IsBackground = true;
            unitSpawner.Start();
        }

        private void SpawnUnits()
        {
            Thread.Sleep(SpawnRate);
            Army.Instance.AddUnit((x*Size)+(Size/2), (y*Size)+(Size/2));
            CreateSpawnThread();
        }

      
    }
}
