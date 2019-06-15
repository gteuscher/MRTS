using System;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
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

        public int Damage(int damageDealt)
        {
            GraphicIndex = CurrentHealth <= damageDealt ? 3 : 2;

            return CurrentHealth -= damageDealt;
        }

        public void StartActionLoop()
        {
            Task.Run(() => {
                while (true)
                {
                    Thread.Sleep(SpawnRate);
                    var random = new Random();
                    var xPos = Position.X * Dimensions.X + random.Next(Dimensions.X);
                    var yPos = Position.Y * Dimensions.Y + random.Next(Dimensions.Y);
                    Army.Instance.AddUnit(xPos, yPos);
                }
            });
        }

    }
}
