using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;

namespace GameManager
{
    public struct TextureRecs
    {
        public string CacheType;
        public string Ext;
        public int Width;
        public int Height;
        public Rectangle[] Recs;
    }

    public static class TextureRecsManager
    {
        public static string ReadTextureMaps()
        {
            return Platform.Current.LoadText("Content/Data/TextureRecs.txt");
        }

        public static Rectangle[] SplitRectangle(this Texture2D texture, bool isHorizontal)
        {
            if (isHorizontal)
            {
                int width = texture.Width / 3; int height = texture.Height;
                return new Rectangle[] { new Rectangle(0, 0, width, height), new Rectangle(width, 0, width, height), new Rectangle(width * 2, 0, width, height) };
            }
            else
            {
                int width = texture.Width; int height = texture.Height / 3;
                return new Rectangle[] { new Rectangle(0, 0, width, height), new Rectangle(0, height, width, height), new Rectangle(0, height * 2, width, height) };
            }
        }

        public static Rectangle[] FindOneTexRectangles(int index, int repeat, int width, int imageWidth, int height)
        {
            int alreadyWidth = index * repeat * width;
            List<Rectangle> recs = new List<Rectangle>();
            for (int i = 0; i < repeat; i++)
            {
                int totalWidth = alreadyWidth + i * width;
                int row = totalWidth / imageWidth;
                int col = totalWidth % imageWidth;
                Rectangle rec = new Rectangle(col, height * row, width, height);
                recs.Add(rec);
            }
            return recs.ToArray();
        }

        public static void FindTextureRectangles(Dictionary<string, TextureRecs> dic, string[] oneMetas)
        {
            string name = oneMetas[0].Trim();
            string ext = oneMetas[1].Trim();
            string[] texts = oneMetas[2].Trim().Split(new string[] { "," }, StringSplitOptions.None);
            //總寬度
            int imageWidth = int.Parse(oneMetas[3].Trim());
            //總高度
            int imageHeight = int.Parse(oneMetas[4].Trim());
            //單寬度
            int width = int.Parse(oneMetas[5].Trim());
            //單高度
            int height = int.Parse(oneMetas[6].Trim());
            //重復
            int repeat = int.Parse(oneMetas[7].Trim());
            for (int index = 0; index < texts.Length; index++)
            {
                Rectangle[] recs = FindOneTexRectangles(index, repeat, width, imageWidth, height);
                dic.Add(name + "#" + texts[index], new TextureRecs() { Width = imageWidth, Height = imageHeight, CacheType = oneMetas[8], Ext = ext, Recs = recs });
            }
        }

        public static Dictionary<string, TextureRecs> FindTextureRectangles(Dictionary<string, TextureRecs> dic, string texture)
        {
            Dictionary<string, TextureRecs> textureDic = new Dictionary<string, TextureRecs>();
            foreach (var di in dic)
            {
                if (di.Key.StartsWith(texture)) textureDic.Add(di.Key.Replace(texture + "#", ""), di.Value);
            }
            return textureDic;
        }

        public static Dictionary<string, TextureRecs> AllTextureRectangles()
        {
            string[] oneTexs = ReadTextureMaps().Split(new string[] { ";" }, StringSplitOptions.None);
            Dictionary<string, TextureRecs> dic = new Dictionary<string, TextureRecs>();
            foreach (string oneTex in oneTexs)
            {
                FindTextureRectangles(dic, oneTex.Trim().Split(new string[] { ":" }, StringSplitOptions.None));
            }
            return dic;
        }
    }
}
