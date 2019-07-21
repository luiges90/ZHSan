using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.IO;
using Microsoft.Xna.Framework.Media;
using SharpCompress.Common;
using SharpCompress.Writer;
using SharpCompress.Reader;
using Platforms;
using GameManager;
using Tools;

namespace Tools
{

    public static class GameTools
    {
        public static string CheckSame()
        { 
            var conDir = Platform.Current.SolutionDir + @"Content\Textures\Resources\Troop";

            var dires = Platform.Current.GetDirectories(conDir, true, true);

            List<string> lens = new List<string>();

            foreach (var dir in dires)
            {
                var files = Platform.Current.GetFiles(dir, false);

                int length = 0;

                foreach (var fi in files)
                {
                    var bytes = Platform.Current.ReadAllBytes(fi);

                    length += bytes.Length;
                }

                lens.Add(dir + " " + length);
            }

            return String.Join("\r\n", lens.OrderBy(le => le.Split(' ')[1]));
        }

        /// <summary>
        /// 生成待編譯資源列表，遍歷Content文件夾
        /// </summary>
        /// <returns></returns>
        public static string GetContentList(string directory, bool android)
        {
            var conDir = Platform.Current.SolutionDir + directory;  // "Content";

            var dires = Platform.Current.GetDirectories(conDir, true, true);

            List<string> lines = new List<string>();

            lines.Add("<ItemGroup>");

            foreach (var dir in dires)
            {

                var di = dir.Split(new string[] { directory }, StringSplitOptions.None)[1];

                if (android)
                {
                    lines.Add($"<Folder Include=\"Assets\\{directory}{di}\" />");
                }
                else
                {
                    lines.Add($"<Folder Include=\"{directory}{di}\" />");
                }

                //win ios
                //<Folder Include="Content\Sound\Troop\29\" />
                //android
                //<Folder Include="Assets\Content\Sound\Animation\Person\493\" />

                var files = Platform.Current.GetFiles(dir, false);

                foreach (var file in files)
                {
                    if (file.ToCharArray().Any(ch => (int)ch > 127))
                    {
                        continue;
                    }

                    string fi = directory + file.Split(new string[] { directory }, StringSplitOptions.None)[1];

                    if (android)
                    {
                        var fi1 = fi;
                        var fi2 = fi;

                        if (fi.Contains(".mp3"))
                        {
                            //<Link>Assets\Attack.mp3</Link>
                            fi2 = fi.Substring(fi.LastIndexOf('\\') + 1);
                        }
                        else if (fi.Contains("ditu"))
                        {
                            fi1 = fi1.Replace("Content", "ContentLite").Replace(@"MODs\Qinghuai", "ContentLite");
                        }

                        lines.Add($"<AndroidAsset Include=\"..\\{fi1}\"><Link>Assets\\{fi2}</Link><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></AndroidAsset>");
                    }
                    else
                    {
                        lines.Add($"<Content Include=\"..\\{fi}\"><Link>{fi}</Link><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></Content>");
                    }
                }

                /* Android
                <AndroidAsset Include="..\Content\Textures\Resources\ditu\neo7\1518.jpg"><Link>Assets\Content\Textures\Resources\ditu\neo7\1518.jpg</Link><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></AndroidAsset>
                 */

                /* Win iOS
                <Content Include="..\Content\Sound\Troop\999\NormalAttack.wav"><Link>Content\Sound\Troop\999\NormalAttack.wav</Link><CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory></Content>
                */
            }

            lines.Add("</ItemGroup>");

            return String.Join("\r\n", lines);
        }

        public static long ZipFile(string zipFile, string[] files)
        {
            using (var zip = Platform.Current.LoadUserFileStream(zipFile, true))
            {
                using (var zipWriter = WriterFactory.Open(zip, SharpCompress.Common.ArchiveType.Zip, SharpCompress.Common.CompressionType.BZip2))
                {
                    foreach (var filePath in files)
                    {
                        string fileName = System.IO.Path.GetFileName(filePath);
                        using (Stream stream = Platform.Current.LoadUserFileStream(fileName, false))
                        {
                            if (stream != null)
                            {
                                zipWriter.Write(filePath, stream);
                            }
                        }
                    }
                }
                return zip.Length;
            }  
        }

        public static string[] UnZipFile(string zipFile)
        {
            List<string> files = new List<string>();
            using (Stream stream = Platform.Current.LoadUserFileStream(zipFile, false))
            {
                using (var reader = ReaderFactory.Open(stream))
                {
                    while (reader.MoveToNextEntry())
                    {
                        if (!reader.Entry.IsDirectory)
                        {
                            files.Add(reader.Entry.FilePath);
                            using (var ms = Platform.Current.LoadUserFileStream(reader.Entry.FilePath, true))
                            {
                                reader.WriteEntryTo(ms);
                            }
                        }
                    }
                }
            }
            return files.NullToEmptyArray();
        }

        public static float AutoSetScale(int width, int height, int newWidth, int newHeight)
        {
            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;
            if (scaleWidth > scaleHeight)
            {
                return scaleHeight;
            }
            else
            {
                return scaleWidth;
            }
        }

        public static string WordsSubString(this string str, int length)
        {
            return str.Length > length ? str.Substring(0, length) + ".." : str;
        }
        public static string WordsSubString2(this string str, int length)
        {
            //return str.Length > length ? str.Substring(0, length) + ".." : str;
            if (str.Length == length)
            {
                return str;
            }
            else if (str.Length > length)
            {
                return str.Substring(0, length);
            }
            else
            {
                int k = str.Length;
                for (int i = 0; i < length- k; i++)
                {
                    str = string.Concat(str, "\t");
                }
                return str;
            }
        }

        public static string RemoveExt(string res)
        {
            return Path.GetFileNameWithoutExtension(res);
        }

        public static bool IsPosInTexture(Vector2 canvasPos, Texture2D tex, Vector2 nowPos)
        {
            return IsPosInArea(canvasPos, tex.Width, tex.Height, nowPos);
        }

        public static bool IsPosInArea(Vector2 canvasPos, int canvasWidth, int canvasHeight, Vector2 nowPos)
        {
            return canvasPos.X <= nowPos.X && nowPos.X <= canvasPos.X + canvasWidth
                && canvasPos.Y <= nowPos.Y && nowPos.Y <= canvasPos.Y + canvasHeight;
        }

        public static Vector2[] ComputeAreaPosition(int canvasWidth, int areaWidth, int spaceWidth, int number, int height)
        {
            List<Vector2> list = new List<Vector2>();
            Vector2 pos = new Vector2((canvasWidth - areaWidth * number - spaceWidth * (number - 1)) / 2, height);
            list.Add(pos);
            for (int i = 0; i < number - 1; i++)
            {
                pos = new Vector2(pos.X + areaWidth + spaceWidth, height);
                list.Add(pos);
            }
            return list.ToArray();
        }

        public static int count = 0;

        private const string des3Key = "WorldOfTheThreeKingdoms";

        public static void PreMultiplyAlphas(Texture2D ret)
        {
            Byte4[] data = new Byte4[ret.Width * ret.Height];
            ret.GetData<Byte4>(data);
            for (int i = 0; i < data.Length; i++)
            {
                Vector4 vec = data[i].ToVector4();
                float alpha = vec.W / 255.0f;
                int a = (int)(vec.W);
                int r = (int)(alpha * vec.X);
                int g = (int)(alpha * vec.Y);
                int b = (int)(alpha * vec.Z);
                uint packed = (uint)(
                    (a << 24) +
                    (b << 16) +
                    (g << 8) +
                    r
                    );
                data[i].PackedValue = packed;
            }
            ret.SetData<Byte4>(data);
        }

        public static int ParseRGB(Color color)
        {
            return (int)(((int)color.B << 16) | (ushort)(((ushort)color.G << 8) | color.R));
        }

        public static Color RGB(int color)
        {
            int r = 0xFF & color;
            int g = 0xFF00 & color;
            g >>= 8;
            int b = 0xFF0000 & color;
            b >>= 16;
            return new Color(r, g, b);
        }

        public static Texture2D CreateShapeTexture(TextureShape shape, Color color, float[] shapeParms)
        {
            Texture2D tex = null;
            if (shape == TextureShape.Circle)
            {
                int radius = Convert.ToInt32(shapeParms[0]);
                int width = radius * 2;
                int height = radius * 2;
                int radius2 = radius * radius;

                Microsoft.Xna.Framework.Color[] bits = new Microsoft.Xna.Framework.Color[width * height];

                Vector2 center = new Vector2(radius, radius);

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        var dis2 = (i - center.X) * (i - center.X) + (j - center.Y) * (j - center.Y);
                        if (dis2 <= radius2)
                        {
                            bits[i + j * width] = color;
                        }
                        else
                        {
                            bits[i + j * width] = Microsoft.Xna.Framework.Color.FromNonPremultiplied(0, 0, 0, 0);
                        }
                    }
                }

                tex = new Texture2D(Platform.GraphicsDevice, width, height);
                tex.SetData(bits);
            }
            return tex;
        }

        public static void ProcessTextureShape(Texture2D tex, TextureShape shape, float[] shapeParms)
        {
            if (shape == TextureShape.Circle)
            {
                Microsoft.Xna.Framework.Color[] bits = new Microsoft.Xna.Framework.Color[tex.Width * tex.Height];
                tex.GetData(bits);

                int radius = tex.Width / 2; // Convert.ToInt32(shapeParms[0]);
                int radius2 = radius * radius;

                Vector2 center = new Vector2(tex.Width / 2, tex.Height / 2);

                for (int i = 0; i < tex.Width; i++)
                {
                    for (int j = 0; j < tex.Height; j++)
                    {
                        var dis2 = (i - center.X) * (i - center.X) + (j - center.Y) * (j - center.Y);
                        if (dis2 <= radius2)
                        {

                        }
                        else
                        {
                            bits[i + j * tex.Width] = Microsoft.Xna.Framework.Color.FromNonPremultiplied(0, 0, 0, 0);
                        }
                    }
                }

                //tex = new Texture2D(tex.GraphicsDevice, tex.Width, tex.Height);
                tex.SetData(bits);
                //for (int i=0; i < bits.Length; i++)
                //{
                //    var color = bits[i];
                //    bits[i] = new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, 0f);  // bits[i] * 0f;
                //}

                //Vector2[] overlapedArea = new Vector2[] { };
                //foreach (Vector2 pixel in overlapedArea)
                //{
                //    int x = (int)(pixel.X);
                //    int y = (int)(pixel.Y);

                //    bits[x + y * tex.Width] = Microsoft.Xna.Framework.Color.FromNonPremultiplied(0, 0, 0, 0);
                //}

                //// Colour the entire texture transparent first.
                //for (int i = 0; i < data.Length; i++)
                //    data[i] = Color.TransparentWhite;

                //// Work out the minimum step necessary using trigonometry + sine approximation.
                //double angleStep = 1f / radius;

                //for (double angle = 0; angle < Math.PI * 2; angle += angleStep)
                //{
                //    // Use the parametric definition of a circle: http://en.wikipedia.org/wiki/Circle#Cartesian_coordinates
                //    int x = (int)Math.Round(radius + radius * Math.Cos(angle));
                //    int y = (int)Math.Round(radius + radius * Math.Sin(angle));

                //    data[y * outerRadius + x + 1] = Color.White;
                //}

                //tex = new Texture2D(tex.GraphicsDevice, tex.Width, tex.Height);
                //tex.SetData(bits);

            }
        }

    }
}
