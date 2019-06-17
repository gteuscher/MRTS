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

        public Tower(List<Texture2D> towerTextures, int startingHealth, int x, int y, int spawnRate = 3000)
        {
            Textures = towerTextures;
            GraphicIndex = 1;
            StartingHealth = startingHealth;
            CurrentHealth = startingHealth;
            SpawnRate = spawnRate;
            Position = new Point(x, y);
            Dimensions = new Point(SIZE, SIZE);

            Task.Run(() => StartActionLoop());
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

        private async Task SpawnUnit()
        {
            var random = new Random();
            var xPos = Position.X + random.Next(Dimensions.X);
            var yPos = Position.Y + random.Next(Dimensions.Y);
            var army = Army.Instance;
            lock (army)
            {
                army.AddUnit(xPos, yPos);
            }
            await Task.Delay(SpawnRate);
        }

        public async Task StartActionLoop()
        {
            await Task.Run( async () => {
                while (CurrentHealth > 0)
                {
                    await SpawnUnit();
                }
            });
        }
    }
}
