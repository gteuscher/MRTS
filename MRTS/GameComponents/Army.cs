using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MRTS.GameComponents
{
    public sealed class Army
    {
        public List<Texture2D> UnitTexture;
        public List<Unit> UnitCollection = new List<Unit>();
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
                s.Draw(unit.UnitTexture, new Vector2(unit.SpawnX, unit.SpawnY), Color.White);
            }
        }

        public void AddUnitTexture(List<Texture2D> unitTexture)
        {
            UnitTexture = unitTexture;
        }

        public void AddUnit(int x, int y)
        {
            var u = new Unit(UnitTexture[0], x, y);
            UnitCollection.Add(u);
        }

        public List<Unit> GetUnits()
        {
            return UnitCollection;
        }
    }
}
