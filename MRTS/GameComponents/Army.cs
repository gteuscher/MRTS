using System.Collections.Concurrent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace MRTS.GameComponents
{
    public sealed class Army
    {
        public List<Texture2D> UnitTextures;
        public ConcurrentBag<Unit> UnitCollection = new ConcurrentBag<Unit>();
        private static readonly Army instance = new Army();

        public Army()
        {
        }

        public static Army Instance
        {
            get { return instance; }
        }


        public void Draw(SpriteBatch s)
        {
            foreach (var unit in UnitCollection)
            {
                s.Draw(unit.Textures.FirstOrDefault(), new Vector2(unit.Position.X, unit.Position.Y), Color.White);
            }
        }

        public void AddUnitTexture(List<Texture2D> unitTextures)
        {
            UnitTextures = unitTextures;
        }

        public void AddUnit(int x, int y)
        {
            var u = new Unit(UnitTextures[0], x, y);
            UnitCollection.Add(u);
        }

        public List<Unit> GetUnits()
        {
            return UnitCollection.ToList();
        }
    }
}
