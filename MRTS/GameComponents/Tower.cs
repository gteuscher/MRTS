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

        public Tower(Texture2D texture, int posX, int posY)
        {
            Graphics = new List<Texture2D> { texture };
            Position = new Point(posX, posY);
            Dimensions = new Point(SIZE, SIZE);
        }

        public Tower(List<Texture2D> towerGraphics, int startingHealth, int x, int y, int spawnRate = 3000)
        {
            Graphics = towerGraphics;
            GraphicIndex = 1;
            StartingHealth = startingHealth;
            CurrentHealth = startingHealth;
            SpawnRate = spawnRate;
            Position = new Point(x, y);
            Dimensions = new Point(SIZE, SIZE);

            StartActionLoop();
        }

        public new Texture2D CurrentGraphic => CurrentHealth / StartingHealth > (2 / 3)
            ? Graphics.FirstOrDefault()
            : CurrentHealth / StartingHealth > (1 / 3)
                ? Graphics.ElementAtOrDefault(1) ?? Graphics.FirstOrDefault()
                : Graphics.ElementAtOrDefault(2) ?? Graphics.FirstOrDefault();

        public int Damage(int damageDealt)
        {
            return CurrentHealth -= damageDealt;
        }

        private void SpawnUnit()
        {
            var random = new Random();
            var xPos = Position.X + random.Next(Dimensions.X);
            var yPos = Position.Y + random.Next(Dimensions.Y);
            Army.Instance.AddUnit(xPos, yPos);
        }

        public void StartActionLoop()
        {
            Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(SpawnRate);
                    SpawnUnit();
                }
            });
        }

    }
}
