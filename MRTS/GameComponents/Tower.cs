using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MRTS.GameComponents
{
    public class Tower : GameObject
    {
        //[0] is default texture [1] is damage texture, [2] is destroyed texture
        AutoResetEvent autoEvent = new AutoResetEvent(false);

        public Tower(Texture2D texture, int posX, int posY)
        {
            Graphics = new List<Texture2D> { texture };
            Position = new Point(posX, posY);
        }

        public Tower(List<Texture2D> towerGraphics, int startingHealth, int x, int y, int spawnRate = 3000)
        {
            Graphics = towerGraphics;
            StartingHealth = startingHealth;
            CurrentHealth = startingHealth;
            SpawnRate = spawnRate;
        }

        public int Damage(int damageDealt)
        {
            GraphicIndex = CurrentHealth <= damageDealt ? 3 : 2;

            return CurrentHealth -= damageDealt;
        }

        public void CreateSpawnThread()
        {
            var unitSpawner = new Thread(SpawnUnits);
            unitSpawner.IsBackground = true;
            unitSpawner.Start();
        }

        private void SpawnUnits()
        {
            Thread.Sleep(SpawnRate);
            Army.Instance.AddUnit( Position.X * Dimensions.X + Dimensions.X / 2 , Position.Y * Dimensions.Y + Dimensions.Y/2 );
            CreateSpawnThread();
        }
    }
}
