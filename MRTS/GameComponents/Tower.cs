using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MRTS.GameComponents
{
    public class Tower : GameObject
    {
        //[0] is default texture [1] is damage texture, [2] is destroyed texture
        AutoResetEvent autoEvent = new AutoResetEvent(false);
        private const int SIZE = 128;
        public double LastSpawnTime = 0;

        public double NextSpawnTime => LastSpawnTime + SpawnRate;

        public Tower(List<Texture2D> towerTextures, int startingHealth, int x, int y, int spawnRate = 3000)
        {
            Textures = towerTextures;
            GraphicIndex = 1;
            StartingHealth = startingHealth;
            CurrentHealth = startingHealth;
            SpawnRate = spawnRate;
            Position = new Point(x, y);
            Dimensions = new Point(SIZE, SIZE);
        }

        public new Texture2D CurrentGraphic => CurrentHealth / StartingHealth > (2 / 3)
            ? Textures.FirstOrDefault()
            : CurrentHealth / StartingHealth > (1 / 3)
                ? Textures.ElementAtOrDefault(1) ?? Textures.FirstOrDefault()
                : Textures.ElementAtOrDefault(2) ?? Textures.FirstOrDefault();

        public int Damage(int damageDealt)
        {
            return CurrentHealth -= damageDealt;
        }


        public void SpawnUnit()
        {
            var random = new Random();
            var xPos = Position.X + random.Next(Dimensions.X);
            var yPos = Position.Y + random.Next(Dimensions.Y);
            lock (GameManager.Army)
            {
                GameManager.Army.AddUnit(xPos, yPos);
            }
        }
    }
}
