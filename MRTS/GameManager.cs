using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MRTS.GameComponents;

namespace MRTS
{
    public class GameManager
    {
        private ContentManager ContentManager { get; set; } 
        private List<GameObject> GameObjects { get; set; }
        public Army Army { get; set; }
        
        private List<Texture2D> TowerGraphics;
        private List<Texture2D> UnitGraphics;

        private Texture2D TileGraphic;
        private Level CurrentLevel { get; set; }

        private static GameServiceContainer Container;
        private static ConcurrentQueue<Task> TaskQueue;
        private SynchronizedActionQueue ActionQueue;

        public GameManager()
        {
            ActionQueue = new SynchronizedActionQueue();
            ContentManager = GameServices.GetService<ContentManager>();
            UnitGraphics = GameFunctions.LoadContent("Unit", ContentManager);

            TowerGraphics = GameFunctions.LoadContent("Tower", ContentManager);
            GameObjects = new List<GameObject>();
            // TODO: Fix hack 
            TileGraphic = TowerGraphics.FirstOrDefault();
            TowerGraphics = TowerGraphics.Skip(1).ToList();

            Army = new Army(UnitGraphics);
        }

        public void InitializeLevel(int level = 1)
        {
            // Todo: load levels from json config file
            CurrentLevel = new Level(10, 6);


            for (var x = 0; x < CurrentLevel.Dimensions.X; x++)
            {
                for (var y = 0; y < CurrentLevel.Dimensions.Y; y++)
                {
                    GameObjects.Add(new Tile(TileGraphic, Tile.SIZE * x, Tile.SIZE * y));
                }
            }

            Task.Run(() =>
            {
                ActionQueue.Run();
            });
        }

        public void Draw(SpriteBatch s)
        {
            GameObjects.ToList().ForEach(go =>
            {
                s.Draw(go.CurrentGraphic, new Vector2(go.Position.X, go.Position.Y),
                    Color.White);
            });

            Army.UnitCollection.ToList().ForEach(go =>
            {
                s.Draw(go.CurrentGraphic, new Vector2(go.Position.X, go.Position.Y),
                    Color.White);
            });
        }

        public void RegisterTimeElapsed(GameTime g)
        {
            GameObjects.GetTowers()
                .Where(t => t.CurrentHealth > 0)
                .ToList()
                .ForEach(t =>
                {        
                    var tower = (Tower)t;
                    lock (tower)
                    {
                        var timer = g.TotalGameTime.TotalMilliseconds;
                        if (timer >= tower.NextSpawnTime)
                        {
                            tower.LastSpawnTime = timer;
                            Console.WriteLine($"Spawning Unit {tower.Position.X}, {tower.Position.Y} : {tower.LastSpawnTime}");
                            
                            ActionQueue.Enqueue(() =>
                            {
                                tower.SpawnUnit();
                            });
                        }
                    }
                });
        }

        public void RegisterMouseClick(int x, int y)
        {
            var quadrant = GameObjects.GetTiles().FirstOrDefault(t => t.Coordinates.Contains(new Point(x, y)));
            // TODO Null check on quadrant is a hack. Only trigger update if mouseclick was in game window
            if (quadrant != null && !GameObjects.GetTowers().Any(t => t.Coordinates.Contains(new Point(x, y)) && t.CurrentHealth != 0))
            {
                GameObjects.Add(new Tower(TowerGraphics, 100, quadrant.Position.X, quadrant.Position.Y));
            }
        }
    }
}
