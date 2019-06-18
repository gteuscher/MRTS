using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using MRTS.GameComponents;

namespace MRTS
{
    public static class GameFunctions
    {
        public static List<Texture2D> LoadContent(string folderName, ContentManager content)
        {
            var contentList = new List<Texture2D>();
            var dir = new DirectoryInfo(content.RootDirectory + "\\" + folderName + "\\");
            var files = dir.GetFiles("*.*");
            foreach (var file in files)
            {
                var tempName = (Path.GetFileNameWithoutExtension(file.Name));
                contentList.Add(content.Load<Texture2D>(folderName + "/" + Path.GetFileNameWithoutExtension(file.Name)));
            }
            return contentList;
        }

        public static IEnumerable<GameObject> GetTiles(this List<GameObject> list)
        {
            return list.Where(t => t.GetType() == typeof(Tile));
        }

        public static IEnumerable<GameObject> GetTowers(this List<GameObject> list)
        {
            return list.Where(t => t.GetType() == typeof(Tower));
        }
    }
}
