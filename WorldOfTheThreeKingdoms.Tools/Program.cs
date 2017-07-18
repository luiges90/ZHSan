using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTheThreeKingdoms.Tools
{
    class Program
    {
        static string solutionDir = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\";

        static string exeDir = "";

        static int dirsCount = 0;
        static int filesCount = 0;

        static GraphicsDevice graphicDevice = null;

        static Game game = null;

        static void Main(string[] args)
        {
            ProcessTextures();
            Console.ReadLine();
        }

        static void CheckTreasurePic()
        {
            var treas1 = Directory.GetFiles(@"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Textures\Resources\Treasure\").Select(fi => Path.GetFileNameWithoutExtension(fi)).ToArray();
            var treas2 = Directory.GetFiles(@"C:\Users\Leming\Desktop\3\").Select(fi => Path.GetFileNameWithoutExtension(fi)).ToArray();

            List<string> fis1 = new List<string>();
            List<string> fis2 = new List<string>();
            foreach (var tr in treas1)
            {
                //var trNew = tr.Replace(" 拷貝", "");
                //if (tr != trNew)
                //{
                //    File.Move(tr, trNew);
                //}

                //var finame = Path.GetFileNameWithoutExtension(tr);

                if (treas2.Contains(tr))
                {
                    fis1.Add(tr);
                }
                else
                {
                    fis2.Add(tr);
                }
            }

        }

        static string RemoveEmptyLine()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\text.txt";

            string[] text = File.ReadAllLines(path);

            var newlines = text.Where(te => !String.IsNullOrEmpty(te.Trim())).ToArray();

            var newlinesStr = String.Join("\r\n", newlines);

            return newlinesStr;
        }

        //生成資源引用
        static string BuildContentLinkTexturesWin()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Textures\";

            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string sb = "";
            foreach (var file in files)
            {
                string content = file.Replace(path, "");  //Animation\Female\46.xnb
                string include = @"..\Content\Textures\" + content;
                string link = @"Content\Sound\" + content;
                string temp = "<Content Include=\"{0}\">\r\n<Link>{1}</Link>\r\n<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>\r\n</Content>";
                string one = String.Format(temp, include, link);
                sb = sb + one + "\r\n";
            }
            return sb;
        }

        //生成資源引用
        static string BuildContentLinkSoundWin()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\bin\Win\Sound\";

            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            string sb = "";
            foreach (var file in files)
            {
                string content = file.Replace(path, "");  //Animation\Female\46.xnb
                string include = @"..\Content\bin\Win\Sound\" + content;
                string link = @"Content\Sound\" + content;
                string temp = "<Content Include=\"{0}\">\r\n<Link>{1}</Link>\r\n<CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>\r\n</Content>";
                string one = String.Format(temp, include, link);
                sb = sb + one + "\r\n";
            }
            return sb;
        }

        public static void CreateGame()
        {
            game = new Game();
            var GraphicsDeviceManager = new GraphicsDeviceManager(game);            
            GraphicsDeviceManager.ApplyChanges();
            graphicDevice = GraphicsDeviceManager.GraphicsDevice;
        }

        public static void MigrateTwoPics()
        {
            //TexturesAlpha1的JPG与TexturesAlpha2的PNG混合

            var files1 = Directory.GetFiles(solutionDir + @"Content\TexturesAlpha", "*.*", SearchOption.AllDirectories);

            foreach (var file in files1)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".jpg")
                {
                    //檢查文件夾
                    string dir = Path.GetDirectoryName(file);
                    dir = dir.Replace("TexturesAlpha", @"TexturesNew");

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string newFile = file.Replace("TexturesAlpha", @"TexturesNew");

                    File.Copy(file, newFile);
                }
            }

            var files2 = Directory.GetFiles(solutionDir + @"Content\Textures", "*.*", SearchOption.AllDirectories);

            foreach (var file in files2)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".png")
                {
                    //檢查文件夾
                    string dir = Path.GetDirectoryName(file);
                    dir = dir.Replace("Textures", @"TexturesNew");

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string newFile = file.Replace("Textures", @"TexturesNew");

                    File.Copy(file, newFile);
                }
            }

        }

        //處理資源圖片
        public static void ProcessTextures()
        {
            CreateGame();

            var files = Directory.GetFiles(solutionDir + @"Content\Textures", "*.*", SearchOption.AllDirectories);

            string ers = "";

            int i = 0;
            foreach (string file in files)
            {
                string ext = Path.GetExtension(file).ToLower();

                if (ext == ".png")  //".png")  // || ext == ".jpg")  // || ext == ".bmp" || ext == ".gif")
                {
                    //檢查文件夾
                    string dir = Path.GetDirectoryName(file);
                    dir = dir.Replace("Textures", @"TexturesAlpha");

                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    string newFile = file.Replace("Textures", @"TexturesAlpha");
                    if (File.Exists(newFile))
                    {
                        continue;
                    }

                    using (var stream = File.OpenRead(file)) // TitleContainer.OpenStream(name))
                    {
                        try
                        {
                            Texture2D tex = Texture2D.FromStream(graphicDevice, stream);
                            if (tex != null)
                            {
                                File.Delete(newFile);
                                if (ext == ".png")
                                {
                                    PreMultiplyAlphas(tex);
                                    using (var fileStream = File.OpenWrite(newFile) as Stream)
                                    {
                                        tex.SaveAsPng(fileStream, tex.Width, tex.Height);
                                    }
                                }
                                else
                                {
                                    throw new Exception("error");
                                }
                                //else if (ext == ".jpg")
                                //{
                                //    //File.Copy(file, newFile);
                                //    using (var fileStream = File.OpenWrite(newFile))
                                //    {
                                //        tex.SaveAsJpeg(fileStream, tex.Width, tex.Height);
                                //    }
                                //}
                                //else
                                //{
                                //    File.Copy(file, newFile);
                                //}

                                tex.Dispose();
                                tex = null;
                            }
                            else
                            {
                                throw new Exception("none");
                            }
                        }
                        catch (Exception ex)
                        {
                            ers = ers + file + "\r\n";
                        }
                    }

                }

                i++;

                if (i >= 200)
                {
                    i = 0;
                    GC.Collect();
                }
            }

        }

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

        //處理字庫禁止字符
        static void ProcessDenyChars()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Font\Messages.txt";

            string text = File.ReadAllText(path);

            string deny = "叁实喑坂咤堇帻捭汜涂礴糅荥镔雳鞅髯鬣鲭麾黯敎災";

            //炬哙实
            foreach (var de in deny.ToCharArray())
            {
                text = text.Replace(de.ToString(), "");
            }
        }

        //收集己知字符
        static void ProcessCharacter()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\";

            var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            var exts = new string[] { ".xml", ".cs", ".json" };  //".txt", 

            string texts = "";

            foreach (var file in files)
            {
                var ext = Path.GetExtension(file).ToLower();
                if (exts.Contains(ext))
                {
                    var chs = File.ReadAllText(file).ToArray();
                    foreach (var ch in chs)
                    {
                        if (!texts.Contains(ch.ToString()))
                        {
                            texts += ch.ToString();
                        }
                    }
                }
            }
        }

        static void CheckFontCache()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Font\AllText.txt";

            string alltext = System.IO.File.ReadAllText(path);

            var chars = alltext.ToCharArray();

            string texts = "";

            foreach (var ch in chars)
            {
                //if (!Session.Current.Font.Characters.Contains(ch))
                //{
                //    texts += ch.ToString();
                //}
            }
        }

        static void ConvertFontCharacters()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Font\繁简.txt";

            string[] alllines = System.IO.File.ReadAllLines(path);

            var chars1 = alllines[0].ToCharArray();
            var chars2 = alllines[1].ToCharArray();

            var files = Directory.GetFiles(@"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content\Data\Plugins\");

            foreach (var file in files)
            {
                var tes = File.ReadAllText(file);

                for (int i = 0; i < chars1.Length; i++)
                {
                    var ch1 = chars1[i];
                    var ch2 = chars2[i];

                    tes = tes.Replace(ch1, ch2);
                }

                File.WriteAllText(file.Replace("Plugins", "Plugins1"), tes);

            }

        }

        //處理大小寫後綴
        static void ProcessCase()
        {
            string path = @"C:\Projects\zhsan\ZhongHuaSanGuoZhiMobile\Content";

            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                string ext = Path.GetExtension(file);

                string ext2 = ext.ToLower();

                if (ext != ext2)
                {
                    string file2 = file.Replace(ext, ext2);

                    Directory.Move(file, file2 + ".tmp");

                    Directory.Move(file2 + ".tmp", file2);
                }
            }
        }

        static void ProcessOneDir(string path, string platform, bool checkDir, bool recursive)
        {
            Console.WriteLine(String.Format("{0}處理文件夾...{1}", platform, path));
            dirsCount++;

            //檢查對應platform有無此文件夾
            var platformDir = path.Replace("Content", "Platforms\\" + platform);
            if (checkDir)
            {
                if (!Directory.Exists(platformDir))
                {
                    Directory.CreateDirectory(platformDir);
                }
            }

            //處理當前文件夾下的所有文件
            var files = Directory.GetFiles(path);

            if (files != null && files.Length > 0)
            {
                foreach(var file in files)
                {
                    Console.WriteLine(String.Format("{0}處理文件...{1}", platform, path));
                    filesCount++;

                    string platformFile = file.Replace("Content", "Platforms\\" + platform);
                    string ext = Path.GetExtension(file).ToLower();
                    if (ext == ".txt" || ext == ".xml" || ext == ".jpg")
                    {
                        File.Copy(file, platformFile);
                    }
                    else if (ext == ".png")
                    {
                        if (platform == "Win")
                        {
                            File.Copy(file, platformFile);
                        }
                    }
                }
            }

            //遞歸處理當前文件夾下的文件夾
            var dirs = Directory.GetDirectories(path);

            if (recursive)
            {
                foreach (var dir in dirs)
                {
                    ProcessOneDir(dir, platform, true, true);
                }
            }
        }

        static void ProcessResources()
        {
            Console.WriteLine("--------------------中華三國志 跨平台資源處理程序--------------------");

            var platforms = new string[] { "Win", "Android", "iOS", "UWP" };

            string input = Console.ReadLine();
            if (platforms.Contains(input))
            {
                platforms = new string[] { input };
            }

            exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(exeDir);

            var contentDir = exeDir + "\\Content";

            List<string> results = new List<string> { };

            foreach (var platform in platforms)
            {
                ProcessOneDir(contentDir, platform, true, true);

                string result = String.Format("{0}平台處理完成，文件夾{1}個，文件{2}", platform, dirsCount, filesCount);

                results.Add(result);

                dirsCount = 0;
                filesCount = 0;
            }

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }

    }
}
