using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;

namespace MRTS
{
    public static class GameFunctions
    {
        public static List<Texture2D> LoadContent(string folderName, ContentManager content)
        {
            var contentList = new List<Texture2D>();
            DirectoryInfo dir = new DirectoryInfo(content.RootDirectory + "\\" + folderName + "\\");
            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files)
            {
                var tempName = (Path.GetFileNameWithoutExtension(file.Name));
                contentList.Add(content.Load<Texture2D>(folderName + "/" + Path.GetFileNameWithoutExtension(file.Name)));
            }
            return contentList;
        }
    }
}
