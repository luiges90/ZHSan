using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Android.Widget;
using GameManager;
using Java.Lang;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools;
using Matrix = Android.Graphics.Matrix;

namespace Platforms
{
    /// <summary>
    /// 各平台不同的實現
    /// </summary>
    public partial class Platform : PlatformBase
    {
        public static new PlatFormType PlatFormType = PlatFormType.Android;

        public static new bool IsMobilePlatForm = true;

        public new string Channel = "";  //"PlayStore";

        public bool LoadFromOBB
        {
            get
            {
                if (System.String.IsNullOrEmpty(Channel))
                {
                    if (Setting.Current != null && 
                        (System.String.IsNullOrEmpty(Setting.Current.MODRuntime) || Setting.Current.MODRuntime == "Qinghuai"))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool IsActive
        {
            get
            {
                return MainGame != null ? MainGame.IsActive : false;
            }
        }

        public new bool IsGuideVisible
        {
            get
            {
                return Guide.IsVisible;
            }
        }

        ///// <summary>
        ///// 內存使用占用
        ///// </summary>
        //public new string MemoryUsage
        //{
        //    get
        //    {
        //        return (System.GC.GetTotalMemory(false) / 1024).ToString();
        //        //Android.Activity1.os.Debug.getNativeHeapAllocatedSize()
        //        //return "";
        //    }
        //}

        public static AndroidGameActivity Activity1;

        public void SetActivity(AndroidGameActivity activity)
        {
            Activity1 = activity;
        }

        public static bool IsPhone;

        public static new string PreferResolution = "925*520";

        public new bool KeyBoardAvailable = false;

        static GraphicsDeviceManager GraphicsDeviceManager = null;

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return GraphicsDeviceManager != null ? GraphicsDeviceManager.GraphicsDevice : null;
            }
        }

        public static void InitGraphicsDeviceManager()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(MainGame);

            GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        public static void SetGraphicsWidthHeight(int width, int height)
        {
            //GraphicsDeviceManager.PreferredBackBufferWidth = width;  //1024;
            //GraphicsDeviceManager.PreferredBackBufferHeight = height; //680;
        }

        public static void GraphicsApplyChanges()
        {
            GraphicsDeviceManager.ApplyChanges();
        }

        public override void SetTimerDisabled(bool timerDisabled)
        {

        }

        public override void PreparePhone()
        {
            Display d = Activity1.WindowManager.DefaultDisplay;
            DisplayMetrics dm = new DisplayMetrics();
            d.GetRealMetrics(dm);  //.GetMetrics(dm);
            if (dm.WidthPixels >= dm.HeightPixels)
            {
                //PreferResolution = dm.WidthPixels + "*" + dm.HeightPixels;
                Session.MainGame.fullScreenDestination = new Rectangle(0, 0, dm.WidthPixels, dm.HeightPixels);
            }
            else
            {
                //PreferResolution = dm.HeightPixels + "*" + dm.WidthPixels;
                Session.MainGame.fullScreenDestination = new Rectangle(0, 0, dm.HeightPixels, dm.WidthPixels);
            }

            AndroidContentManager.Init();

            //if (System.String.IsNullOrEmpty(Platform.Current.Channel))
            //{

            //}
            //else
            //{
                
            //}

            //var stream = AndroidContentManager.OpenStream("Setting.png");

            //CoreGame.Current.view = PreferResolution;
            //double x = Math.Pow(Convert.ToSingle(dm.WidthPixels) / dm.Xdpi, 2);
            //double y = Math.Pow(Convert.ToSingle(dm.HeightPixels) / dm.Ydpi, 2);
            //double screenSizes = Math.Sqrt(x + y);
            //double diagonalPixels = Math.Sqrt(Math.Pow(Convert.ToDouble(dm.WidthPixels), 2) + Math.Pow(dm.HeightPixels, 2));
            //double screenSizes = diagonalPixels / (160 * dm.Density);
            //IsPhone = screenSizes <= 6.0;
        }

        public override void SetFullScreen(bool full)
        {
            //SystemTray.IsVisible = !full;
            GraphicsDeviceManager.IsFullScreen = full;
        }

        public override void SetOrientations()
        {
            //CoreGame.Current.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        public override string GetDeviceInfo()
        {
            return Build.Manufacturer + " " + Build.Model + " " + Build.VERSION.Sdk.ToString() + " " + Build.VERSION.Release.ToString();
        }

        public override string GetSystemInfo()
        {
            return System.Environment.OSVersion.Platform + " " + System.Environment.OSVersion.VersionString;
        }

        #region 加載資源文件

        public Stream LoadStream(string res)
        {
            res = res.Replace("\\", "/");

            Stream stream = null;
            if (LoadFromOBB)
            {
                if (FileExists(res))
                {
                    stream = AndroidContentManager.OpenStream(res);
                }
                else
                {
                    stream = Game.Activity.Assets.Open(res);
                }
            }
            else
            {
                if (FileExists(res))
                {
                    stream = Game.Activity.Assets.Open(res);
                }
                else
                {
                    stream = AndroidContentManager.OpenStream(res);
                }
            }
            return stream;
        }
        
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string LoadText(string res)
        {
            res = res.Replace("\\", "/");

            res = base.GetMODFile(res);

            lock (Platform.IoLock)
            {
                using (var stream = LoadStream(res))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
            }
        }
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string[] LoadTexts(string res)
        {
            res = res.Replace("\\", "/");

            res = base.GetMODFile(res);

            lock (Platform.IoLock)
            {
                using (var stream = LoadStream(res))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        List<string> texts = new List<string>();
                        while (!streamReader.EndOfStream)
                        {
                            texts.Add(streamReader.ReadLine());
                        }
                        return texts.ToArray();
                    }
                }
            }
        }
        /// <summary>
        /// 加載資源文件
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public byte[] LoadFile(string res)
        {
            res = res.Replace("\\", "/");

            res = base.GetMODFile(res);

            lock (Platform.IoLock)
            {
                using (var dest = new MemoryStream())
                {
                    using (var stream = LoadStream(res))
                    {
                        stream.CopyTo(dest);
                        return dest.ToArray();
                    }
                }
            }
            //return null;
            //Byte[] bytes = new Byte[stream.Length];
            //stream.Position = 0;
            //int length = Convert.ToInt32(stream.Length);
            //stream.Read(bytes, 0, length);
            //return bytes;
        }
        /// <summary>
        /// 加載資源材質
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public Texture2D LoadTexture(string res, bool isUser)
        {
            try
            {
                res = res.Replace("\\", "/");
                if (isUser)
                {
                    if (!UserFileExist(new string[] { res })[0])
                    {
                        //暫時沒有文件
                        return null;
                    }
                }
                else
                {
                    res = base.GetMODFile(res);
                }

                //lock (Platform.IoLock)
                //{
                    using (var stream = isUser ? LoadUserFileStream(res) : LoadStream(res))
                    {
                        //Texture tex = SharpDX.
                        Texture2D tex = Texture2D.FromStream(GraphicsDevice, stream);
                        //if (tex != null && Path.GetExtension(res).ToLower() == ".png")
                        //{
                        //    try
                        //    {
                        //        Season.Current.PreMultiplyAlphas(tex);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        WebTools.TakeWarnMsg("处理透明层级失败:" + res, "PreMultiplyAlphas:" + UserApplicationDataPath + res, ex);
                        //    }
                        //}
                        return tex;
                    }
                //}
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("加载游戏材质失败:" + res, "LoadTexture:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }

        public override string[] ReadAllLines(string file)
        {
            file = file.Replace("\\", "/");

            string[] lines = null;

            lock (Platform.IoLock)
            {
                using (var stream = LoadStream(file))
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        List<string> texts = new List<string>();
                        while (!streamReader.EndOfStream)
                        {
                            texts.Add(streamReader.ReadLine());
                        }
                        lines = texts.ToArray();
                    }
                }
            }

            return lines;
        }

        public override string[] GetDirectories(string dir, bool all, bool full)
        {
            dir = dir.Replace("\\", "/");

            if (!dir.EndsWith("/"))
            {
                dir = dir + "/";
            }

            if (LoadFromOBB)
            {
                return GetDirectoriesExpan(dir, all, full);
            }
            else
            {
                return GetDirectoriesBasic(dir, all, full);
            }
        }

        public override string[] GetDirectoriesBasic(string dir, bool all, bool full)
        {
            string[] dirs = null;

            dir = dir.Replace("\\", "/");
            if (!dir.EndsWith("/"))
            {
                dir += "/";
            }

            dirs = Game.Activity.Assets.List(dir).NullToEmptyArray();

            if (full)
            {
                dirs = dirs.Select(fi => dir + fi).NullToEmptyArray();
            }

            return dirs;
        }

        public override string[] GetDirectoriesExpan(string dir, bool all, bool full)
        {
            var directories = new List<string>();

            var allFiles = AndroidContentManager.entries.Where(en => en.StartsWith(dir)).NullToEmptyArray().Select(en => en.Replace(dir + "/", "")).NullToEmptyArray();

            if (full)
            {

            }
            else
            {
                foreach (var file in allFiles)
                {
                    if (file.Contains("/"))
                    {
                        var di = file.Substring(0, file.IndexOf('/'));
                        if (!System.String.IsNullOrEmpty(di) && !directories.Contains(di))
                        {
                            directories.Add(di);
                        }
                    }
                }
            }

            return directories.NullToEmptyArray();
        }

        public override string[] GetFiles(string dir, bool all = false)
        {
            string[] list = null;

            if (dir.Contains("/"))
            {
                dir = dir.Substring(0, dir.LastIndexOf('/'));
            }

            if (LoadFromOBB)
            {
                list = AndroidContentManager.entries.Where(en => en.StartsWith(dir)).NullToEmptyArray().Select(en => en.Contains("/") ? en.Substring(en.LastIndexOf('/') + 1) : en).NullToEmptyArray();
            }
            else
            {
                dir = dir.Replace("\\", "/");
                list = Game.Activity.Assets.List(dir);
                if (all)
                {
                    if (dir.EndsWith("/"))
                    {
                        dir = dir.Substring(0, dir.Length - 1);
                    }
                    list = list.Select(fi => dir + "/" + fi).NullToEmptyArray();
                }
            }

            return list;
        }

        public override string[] GetFilesBasic(string dir, bool all = false)
        {
            string[] files = null;
            dir = dir.Replace("\\", "/"); 
            files = Game.Activity.Assets.List(dir);
            if (all)
            {
                files = files.Select(fi => dir + fi).NullToEmptyArray();
            }
            return files;
        }

        public override bool DirectoryExists(string dir)
        {
            string[] list = null;

            if (LoadFromOBB)
            {
                list = AndroidContentManager.entries.Where(en => en.StartsWith(dir)).NullToEmptyArray().Select(en => en.Contains("/") ? en.Substring(en.LastIndexOf('/') + 1) : en).NullToEmptyArray();
            }
            else
            {
                list = Game.Activity.Assets.List(dir);
            }

            if (list == null || list.Length <= 0)
            {
                return false;
            }
            return true;
        }

        public override bool FileExists(string file)
        {
            var list = GetFiles(file, false);

            if (list != null && list.Length > 0)
            {
                if (file.Contains('/'))
                {
                    file = file.Substring(file.LastIndexOf('/') + 1);
                }
                return list.Contains(file);
            }

            return false;
            //var uri = new System.Uri(file);
            //var real = uri.LocalPath;
            //return System.IO.File.Exists(real);
        }

        //public override bool DirectoryExists(string dir)
        //{
        //    return false;
        //}

        #endregion

        #region 處理用戶文件

        public bool UserDirectoryExist(string path)
        {
            try
            {
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        return storage.DirectoryExists(path);
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("判斷用戶文件夾失敗:" + path, "UserDirectoryExist:" + UserApplicationDataPath + path, ex);
                return false;
            }
        }

        public void UserDirectoryCreate(string path)
        {
            try
            {
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        storage.CreateDirectory(path);
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("創建用戶文件夾失敗:" + path, "UserCreateDirectory:" + UserApplicationDataPath + path, ex);
            }
        }

        /// <summary>
        /// 獲得用戶文件
        /// </summary>
        /// <param name="searchPatterns"></param>
        /// <returns></returns>
        public string[] GetUserFileNames(string searchPattern)
        {
            try
            {
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        var files = storage.GetFileNames(searchPattern);
                        if (files != null)
                        {
                            files = files.Select(fi => System.IO.Path.GetFileName(fi)).ToArray();
                        }
                        return files;
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("获取文件列表失败:" + searchPattern, "GetUserFileNames:" + UserApplicationDataPath, ex);
                return null;
            }
            //var storage = GetIsolatedStorageFile();
            //return storage.GetFileNames(searchPattern);
        }
        /// <summary>
        /// 加載用戶文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string GetUserText(string res)
        {
            try
            {
                res = res.Replace("/", "\\");
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        if (storage.FileExists(res))
                        {
                            var isolatedFileStream = storage.OpenFile(res, FileMode.Open);
                            using (var streamReader = new StreamReader(isolatedFileStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("加载用户文本失败:" + res, "GetUserText:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        /// <summary>
        /// 加載用戶文本段
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string[] GetUserFileString(string res)
        {
            try
            {
                res = res.Replace("\\", "/");
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        if (storage.FileExists(res))
                        {
                            var isolatedFileStream = storage.OpenFile(res, FileMode.Open);
                            using (var streamReader = new StreamReader(isolatedFileStream))
                            {
                                List<string> list = new List<string>();
                                while (!streamReader.EndOfStream)
                                {
                                    string st = streamReader.ReadLine();
                                    list.Add(st);
                                }
                                return list.ToArray();
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("加载用户文本失败:" + res, "GetUserFileString:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        /// <summary>
        /// 加載用戶文件
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public byte[] GetUserFile(string res)
        {
            try
            {
                res = res.Replace("\\", "/");
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        if (storage.FileExists(res))
                        {
                            byte[] bytes;
                            using (var isolatedFileStream = storage.OpenFile(res, FileMode.Open))
                            {
                                using (var binaryReader = new BinaryReader(isolatedFileStream))
                                {
                                    bytes = binaryReader.ReadBytes(System.Convert.ToInt32(isolatedFileStream.Length));
                                }
                            }
                            return bytes;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("加载用户文件失败:" + res, "GetUserFile:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        ///// <summary>
        ///// 獲取用戶鍵值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public object GetUserValueByKey(string key)
        //{
        //    try
        //    {
        //        string[] strings = GetUserFileString("settings.config");
        //        if (strings != null)
        //        {
        //            string value = strings.FirstOrDefault(st => st.Contains("=") && st.Split('=')[0] == key);
        //            if (!System.String.IsNullOrEmpty(value))
        //            {
        //                return value.Split('=')[1];
        //            }
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        WebTools.TakeWarnMsg("获取用户键值失败:" + key, "GetUserValueByKey:", ex);
        //    }
        //    return null;
        //}
        public Stream LoadUserFileStream(string res)
        {
            return LoadUserFileStream(res, false);
        }
        /// <summary>
        /// 加載用戶文件流
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public Stream LoadUserFileStream(string res, bool write)
        {
            lock (Platform.IoLock)
            {
                using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                {
                    if (write || storage.FileExists(res))
                    {
                        return storage.OpenFile(res, write ? FileMode.OpenOrCreate : FileMode.Open);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        /// <summary>
        /// 加載用戶材質
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public Texture2D LoadUserTexture(string res)
        {
            try
            {
                res = res.Replace("\\", "/");
                using (var stream = Current.LoadUserFileStream(res))
                {
                    if (stream == null)
                    {
                        return null;
                    }
                    Texture2D tex = Texture2D.FromStream(GraphicsDevice, stream);
                    return tex;
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("加载用户材质失败:" + res, "LoadUserTexture:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        /// <summary>
        /// 判斷用戶文件是否存在
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public bool[] UserFileExist(string[] res)
        {
            if (res == null || res.Length == 0)
            {
                return null;
            }
            try
            {
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        List<bool> results = new List<bool>();
                        foreach (string re in res)
                        {
                            bool exis = false;
                            if (!System.String.IsNullOrEmpty(re.Trim()))
                            {
                                try
                                {
                                    exis = storage.FileExists(re.Trim());
                                }
                                catch
                                {

                                }
                            }
                            results.Add(exis);
                        }
                        return results.ToArray();
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("判断文件存在失败:" + System.String.Join(",", res), "UserFileExist:" + UserApplicationDataPath + System.String.Join(",", res), ex);
                return null;
            }
        }
        /// <summary>
        /// 保存用戶文本
        /// </summary>
        /// <param name="res"></param>
        /// <param name="content"></param>
        public void SaveUserFile(string res, string content, bool fullPathProvided = false)
        {
            try
            {
                DelUserFiles(new string[] { res }, null);
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        string path = res;
                        path = "/" + path;
                        using (var isolatedFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, storage))
                        {
                            using (var fileWriter = new StreamWriter(isolatedFileStream))
                            {
                                fileWriter.Write(content);
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文本失败:" + res, "SaveUserFile:" + UserApplicationDataPath + res, ex);
            }
        }
        ///// <summary>
        ///// 保存用戶鍵值
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="o"></param>
        //public void SaveUserValueByKey(string key, object o)
        //{
        //    string[] strings = GetUserFileString("settings.config");
        //    if (strings == null)
        //    {
        //        strings = new string[] { key + "=" + o.ToString() };
        //    }
        //    else
        //    {
        //        bool exist = false;
        //        for (int i = 0; i < strings.Length; i++)
        //        {
        //            var str = strings[i];
        //            if (str.Contains("="))
        //            {
        //                string[] data = str.Split('=');
        //                if (data[0] == key)
        //                {
        //                    exist = true;
        //                    data[1] = o.ToString();
        //                    strings[i] = key + "=" + o.ToString();
        //                }
        //            }
        //        }
        //        if (!exist)
        //        {
        //            strings = strings.Union(new string[] { key + "=" + o.ToString() }).ToArray();
        //        }
        //    }
        //    SaveUserFile("settings.config", System.String.Join("\r\n", strings));
        //}
        /// <summary>
        /// 保存用戶文件
        /// </summary>
        /// <param name="res"></param>
        /// <param name="bytes"></param>
        public void SaveUserFile(string res, byte[] bytes, PlatformTask action)
        {
            try
            {
                DelUserFiles(new string[] { res }, null);
                lock (Platform.IoLock)
                {
                    using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                    {
                        string path = res;  //string path = Path.GetFullPath(res);
                                            //if (!path.StartsWith("/"))
                                            //{
                                            //    path = "/" + path;
                                            //}
                        using (var isolatedFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, storage))
                        {
                            using (var binaryWriter = new BinaryWriter(isolatedFileStream))
                            {
                                binaryWriter.Write(bytes);
                            }
                        }
                        if (action != null)
                        {
                            action.Start();
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文件失败:" + res, "SaveUserFile:bytes" + bytes.Length + UserApplicationDataPath + res, ex);
            }
        }
        //public void SaveUserFileStatic(string picture, byte[] shot)
        //{
        //    SaveUserFile(picture, shot);
        //    //Task.Run(async () => await Season.Current.SaveUserFile(picture, shot));
        //}
        /// <summary>
        /// 刪除用戶文件
        /// </summary>
        /// <param name="files"></param>
        public void DelUserFiles(string[] files, PlatformTask action)
        {
            lock (Platform.IoLock)
            {
                using (IsolatedStorageFile storage = GetIsolatedStorageFile())
                {
                    foreach (string file in files)
                    {
                        if (UserFileExist(new string[] { file.NullToString().Trim() })[0])
                        {
                            try
                            {
                                storage.DeleteFile(file.Trim());
                            }
                            catch (System.Exception ex)
                            {
                                WebTools.TakeWarnMsg("删除用户文件失败:" + file, "storage.DeleteFile:" + UserApplicationDataPath + file, ex);
                            }
                        }
                        if (action != null)
                        {
                            action.Start();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 獲取獨立存取文件
        /// </summary>
        /// <returns></returns>
        protected IsolatedStorageFile GetIsolatedStorageFile()
        {
            return IsolatedStorageFile.GetUserStoreForApplication();
        }
        #endregion

        //public override void PreMultiplyAlphas(Texture2D ret)
        //{
        //    Byte4[] data = new Byte4[ret.Width * ret.Height];
        //    ret.GetData<Byte4>(data);
        //    for (int i = 0; i < data.Length; i++)
        //    {
        //        Vector4 vec = data[i].ToVector4();
        //        float alpha = vec.W / 255.0f;
        //        int a = (int)(vec.W);
        //        int r = (int)(alpha * vec.X);
        //        int g = (int)(alpha * vec.Y);
        //        int b = (int)(alpha * vec.Z);
        //        uint packed = (uint)(
        //            (a << 24) +
        //            (b << 16) +
        //            (g << 8) +
        //            r
        //            );

        //        data[i].PackedValue = packed;
        //    }
        //    ret.SetData<Byte4>(data);
        //}

        public static void Sleep(int time)
        {
            System.Threading.Thread.Sleep(time);
        }

        
        public override byte[] ScreenShot(GraphicsDevice graphics, RenderTarget2D screenshot)
        {
            graphics.SetRenderTarget(null);
            byte[] shot = null;
            using (MemoryStream ms = new MemoryStream())
            {
                screenshot.SaveAsJpeg(ms, screenshot.Width, screenshot.Height);
                screenshot.Dispose();
                shot = ms.ToArray(); //.GetBuffer();
                screenshot = new RenderTarget2D(GraphicsDevice, 800, 480, false, SurfaceFormat.Color, DepthFormat.None);
            }
            return shot;
        }
        /// <summary>
        /// 調用WebService (post方式)
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string GetWebServicePost(string strURL, string data, PlatformTask onUpload)
        {
            var client = new WebClient();
            string result = null;
            byte[] sendData = Encoding.GetEncoding("UTF-8").GetBytes(data);

            client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            client.Headers.Add("ContentLength", sendData.Length.ToString());

            if (onUpload != null)
            {
                client.UploadProgressChanged += (sender, e) =>
                {
                    //UploadProgressChangedEventArgs
                    onUpload.ParamArray = new string[] { e.ProgressPercentage.ToString() };
                };
            }

            byte[] recData = client.UploadData(strURL, "POST", sendData);
            MemoryStream stream = new MemoryStream(recData);
            XmlTextReader reader = new XmlTextReader(stream);
            reader.MoveToContent();
            result = reader.ReadInnerXml();
            reader.Close();
            stream.Close();
            //string result = Encoding.ASCII.GetString(response);
            //return result;
            result = result.Replace("&lt;", "<").Replace("&gt;", ">");
            return result;
        }
        /// <summary>
        /// 調用WebService
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public string GetWebService(string strURL)
        {
            var client = new WebClient();
            string result = null;
            byte[] response = client.DownloadData(new System.Uri(strURL));
            MemoryStream stream = new MemoryStream(response);
            XmlTextReader reader = new XmlTextReader(stream);
            reader.MoveToContent();
            result = reader.ReadInnerXml();
            reader.Close();
            stream.Close();
            //string result = Encoding.ASCII.GetString(response);
            //return result;
            result = result.Replace("&lt;", "<").Replace("&gt;", ">");
            return result;
            //创建一个HTTP请求
            //var client = new WebClient();
            //client.DownloadStringCompleted += (s, ev) => 
            //{ 
            //responseTextBlock.Text = ev.Result; 
            //};
            //client.DownloadStringAsync(new Uri(strURL));
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
            ////request.Method="get";
            //HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
            //Stream s = response.GetResponseStream();
            ////转化为XML，自己进行处理
            //XmlTextReader Reader = new XmlTextReader(s);
            //Reader.MoveToContent();
            //string strValue = Reader.ReadInnerXml();
            //strValue = strValue.Replace("&lt;", "<");
            //strValue = strValue.Replace("&gt;", ">");
            ////MessageBox.Show(strValue);
            //Reader.Close();
            //request.Abort();
            //response.Close();
            //return "";
        }

        public void DownloadWebData(string file, PlatformTask action)
        {
            var client = new WebClient();
            byte[] result = client.DownloadData(new System.Uri(file));
            if (action != null)
            {
                action.ParamArrayResultBytes = result;
                action.Start();
            }
        }

        public void OpenLink(string link)
        {
            try
            {
                var uri = Uri.Parse(link);
                //var uri = new Android.Net.Uri(link);
                var intent = new Intent(Intent.ActionView, uri);
                Activity1.StartActivity(intent);
            }
            catch (System.Exception ex)
            {
                WebTools.TakeWarnMsg("StartActivity", "打開Web出錯：", ex);
            }
        }

        public override void Exit()
        {
            //Activity1.Current.MoveTaskToBack(true);
            Activity1.Finish();
        }

        public static void ExceptionHandler(object sender, System.UnhandledExceptionEventArgs args)
        {

        }

        public override void ShowKeyBoard(PlayerIndex index, string name, string title, string desc, System.AsyncCallback CallbackFunction)
        {
            Guide.BeginShowKeyboardInput(index, name, title, desc, CallbackFunction, null);
        }

        public override string EndShowKeyBoard(System.IAsyncResult ar)
        {
            return Guide.EndShowKeyboardInput(ar);
            //return "";
        }

        private void ResizeBitmapAndSendToWebServer(string album_id)
        {
            //BitmapFactory.Options options = new BitmapFactory.Options();
            //options.InJustDecodeBounds = true; // <-- This makes sure bitmap is not loaded into memory.
            //// Then get the properties of the bitmap
            //BitmapFactory.DecodeFile(fileUri.Path, options);
            //Android.Util.Log.Debug("[BITMAP]", string.Format("Original width : {0}, and height : {1}", options.OutWidth, options.OutHeight));
            //// CalculateInSampleSize calculates the right aspect ratio for the picture and then calculate
            //// the factor where it will be downsampled with.
            //options.InSampleSize = CalculateInSampleSize(options, 1600, 1200);
            //Android.Util.Log.Debug("[BITMAP]", string.Format("Downsampling factor : {0}", CalculateInSampleSize(options, 1600, 1200)));
            //// Now that we know the downsampling factor, the right sized bitmap is loaded into memory.
            //// So we set the InJustDecodeBounds to false because we now know the exact dimensions.
            //options.InJustDecodeBounds = false;
            //// Now we are loading it with the correct options. And saving precious memory.
            //Bitmap bm = BitmapFactory.DecodeFile(fileUri.Path, options);
            //Android.Util.Log.Debug("[BITMAP]", string.Format("Downsampled width : {0}, and height : {1}", bm.Width, bm.Height));
            //// Convert it to Base64 by first converting the bitmap to
            //// a byte array. Then convert the byte array to a Base64 String.
            //MemoryStream stream = new MemoryStream();
            //bm.Compress(Bitmap.CompressFormat.Jpeg, 80, stream);
            //byte[] bitmapData = stream.ToArray();
            //bm.Dispose();

            //app.api.SendPhoto(Base64.EncodeToString(bitmapData, Base64Flags.Default), album_id);

        }

        public static readonly int PickImageId = 1000;
        public static readonly int PickFromCamera = 2000;
        public static Java.IO.File photoFile;
        public static Java.IO.File phtoDir;
        public static Bitmap bitmap;
        public static string picStatus = "";
        public static PlatformTask platformTask = null;

        public override void ChoosePicture(PlatformTask action)
        {
            picStatus = "ChoosePicture";
            platformTask = action;
            Activity1.Intent = new Intent();
            Activity1.Intent.SetType("image/*");
            Activity1.Intent.AddCategory(Intent.CategoryOpenable);
            Activity1.Intent.SetAction(Intent.ActionGetContent);
            try
            {
                Activity1.StartActivityForResult(Intent.CreateChooser(Activity1.Intent, "Select Picture"), PickImageId);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("Please install a File Manager", ex.Message.NullToString(), ex);
                //Toast.MakeText(this, "Please install a File Manager.", Toast.LENGTH_SHORT).show();
            }
        }

        public override void TakePhoto(PlatformTask action)
        {
            picStatus = "TakePhoto";
            platformTask = action;
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Intent intent = new Intent(MediaStore.ActionImageCapture);

                photoFile = new Java.IO.File(phtoDir, String.Format("myPhoto_{0}.jpg", System.Guid.NewGuid().ToString()));
                intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(photoFile));

                Activity1.StartActivityForResult(intent, PickFromCamera);
            }
        }

        private void TakeScreenshot(PlatformTask action)
        {
            //Date now = new Date();
            //android.text.format.DateFormat.format("yyyy-MM-dd_hh:mm:ss", now);
            // image naming and path  to include sd card  appending name you choose for file
            //String mPath = Environment.getExternalStorageDirectory().toString() + "/" + now + ".jpg";

            // create bitmap screen capture
            View v1 = Activity1.Window.DecorView.RootView;  // getWindow().getDecorView().getRootView();
            v1.DrawingCacheEnabled = true;  // .setDrawingCacheEnabled(true);
            Bitmap bitmap = Bitmap.CreateBitmap(v1.DrawingCache);  // .createBitmap(v1.getDrawingCache());
            v1.DrawingCacheEnabled = false;  //.setDrawingCacheEnabled(false);

            //File imageFile = new File(mPath);

            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 0, stream);
                byte[] bytes = stream.ToArray();
                if (action != null)
                {
                    action.ParamArrayResultBytes = bytes;
                    action.Start();
                }
            }
        }

        private void CreateDirectoryForPictures()
        {
            phtoDir = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "SanguoSeason");
            if (!phtoDir.Exists())
            {
                phtoDir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = Activity1.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        public override void MirrorPicture(byte[] image, PlatformTask action)
        {
            Bitmap bm = image.ToBitmap();
            int w = bm.Width;
            int h = bm.Height;
            Bitmap newb = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);// 创建一个新的和SRC长度宽度一样的位图
            Canvas cv = new Canvas(newb);
            Matrix m = new Matrix();
            //m.PostScale(1, -1);   //镜像垂直翻转
            m.PostScale(-1, 1);   //镜像水平翻转
            //m.PostRotate(-90);  //旋转-90度
            Bitmap new2 = Bitmap.CreateBitmap(bm, 0, 0, w, h, m, true);
            cv.DrawBitmap(new2, new Rect(0, 0, new2.Width, new2.Height), new Rect(0, 0, w, h), null);
            var bytes = newb.ToBytes();
            if (action != null)
            {
                action.ParamArrayResultBytes = bytes;
                action.Start();
            }
        }

        public override void RotatePicture(byte[] image, int rotate, PlatformTask action)
        {
            float orientationDegree = 0f;

            if (rotate == 0)
            {

            }
            else if (rotate == 1)
            {
                orientationDegree = 90;
            }
            else if (rotate == 2)
            {
                orientationDegree = 180;
            }
            else if (rotate == 3)
            {
                orientationDegree = 270;
            }

            Bitmap bm = image.ToBitmap();

            Matrix m = new Matrix();
            m.SetRotate(orientationDegree, (float)bm.Width / 2, (float)bm.Height / 2);

            Bitmap bm1 = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, m, true);

            var bytes = bm1.ToBytes();
            if (action != null)
            {
                action.ParamArrayResultBytes = bytes;
                action.Start();
            }

            //catch (OutOfMemoryError ex)            
        }

        public override void CropPicture(byte[] image, int x, int y, int width, int height, PlatformTask action)
        {
            Bitmap bm = image.ToBitmap();
            Bitmap resizedBitmap = Bitmap.CreateBitmap(bm, x, y, width, height);
            using (MemoryStream stream = new MemoryStream())
            {
                resizedBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bytes = stream.ToArray();
                if (action != null)
                {
                    action.ParamArrayResultBytes = bytes;
                    action.Start();
                }
            }
        }

        //public static byte[] ResizeImageFile(byte[] image, int targetSizeWidth, int targetSizeHeight)
        //{            
        //    Bitmap bm = image.ToBitmap();
        //    var draw = BitmapHelpers.ResizeImage(bm, targetSizeWidth, targetSizeHeight);
        //    return draw.ToBytes();
        //}

        /// <summary>
        /// 壓縮圖片
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public override void ResizeImageFile(byte[] imageFile, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            //byte[] pic = null;

            try
            {
                Bitmap bm = imageFile.ToBitmap();

                //Size newSize;
                int newWidth;
                int newHeight;

                if (sameRatio)
                {
                    float scale = GameTools.AutoSetScale(bm.Width, bm.Height, targetSizeWidth, targetSizeHeight);
                    newWidth = int.Parse((bm.Width * scale).ToString());
                    newHeight = int.Parse((bm.Height * scale).ToString());
                }
                else
                {
                    newWidth = int.Parse(targetSizeWidth.ToString());
                    newHeight = int.Parse(targetSizeHeight.ToString());
                }
                
                var draw = BitmapHelpers.ResizeImage(bm, targetSizeWidth, targetSizeHeight, false);
                //pic = draw.ToBytes();
                
                Bitmap resizedBitmap = Bitmap.CreateBitmap(draw, 0, 0, newWidth, newHeight);

                using (MemoryStream stream = new MemoryStream())
                {
                    resizedBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    byte[] bytes = stream.ToArray();
                    if (action != null)
                    {
                        action.ParamArrayResultBytes = bytes;
                        action.Start();
                    }
                }                
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("缩放图像失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
            }

            //if (action != null)
            //{
            //    action.ParamArrayResultBytes = pic;
            //    action.Start();
            //}
        }

        public override void CropResizePicture(byte[] image, int x, int y, int width, int height, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            byte[] pic = null;
            try
            {
                Bitmap bm = image.ToBitmap();

                Bitmap resizedBitmap = null;

                if (bm.Width > x + width && bm.Height > y + height)
                {
                    resizedBitmap = Bitmap.CreateBitmap(bm, x, y, width, height);
                }
                else
                {
                    resizedBitmap = bm;
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    resizedBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                    pic = stream.ToArray();
                }

                ResizeImageFile(pic, targetSizeWidth, targetSizeHeight, sameRatio, action);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("裁切缩放失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
            }
        }

        public void HandleActivityResult(int requestCode, Result resultCode, Intent data)
        {
            //WebTools.TakeWarnMsg("HandleActivityResult", requestCode + " " + resultCode.ToString(), null);
            if ((requestCode == Platform.PickImageId))
            {
                if ((resultCode == Result.Ok) && (data != null))
                {
                    picStatus = "ChoosePictureLoading";
                    var uri = data.Data;
                    //_imageView.SetImageURI(uri);
                    //Uri uri = data.getData();  
                    //Log.e("uri", uri.toString());  
                    ContentResolver cr = Activity1.ContentResolver; // this.getContentResolver();  
                    try
                    {
                        using (var stream = cr.OpenInputStream(uri))
                        {
                            Bitmap bm = BitmapFactory.DecodeStream(stream);

                            string extension = System.IO.Path.GetExtension(uri.ToString());
                            if (System.String.IsNullOrEmpty(extension))
                            {
                                extension = ".jpg";
                            }

                            int width = 540 - 6; // _imageView.Height;
                            int height = 540 - 6; // 270 - 6; // Resources.DisplayMetrics.HeightPixels;

                            var draw = BitmapHelpers.ResizeImage(bm, width, height, true);

                            var bitmapData = draw.ToBytes();

                            bm = null;
                            draw = null;
                            //WebTools.TakeWarnMsg("bitmapData", bitmapData.Length.ToString(), null);
                            if (platformTask != null)
                            {
                                platformTask.ParamArrayResult = new string[] { extension };
                                platformTask.ParamArrayResultBytes = bitmapData;
                                platformTask.Start();
                            }
                            //ImageView imageView = (ImageView) findViewById(R.id.iv01);  
                            /* 将Bitmap设定到ImageView */
                            //imageView.setImageBitmap(bitmap);  
                        }
                    }
                    catch (Exception ex)  // (Java.IO.FileNotFoundException e)
                    {
                        picStatus = "ChoosePictureFailure";
                        WebTools.TakeWarnMsg("选取照片失败:", ex.Message.NullToString(), ex);
                        //throw e;
                        //Log.e("Exception", e.getMessage(),e);
                    }

                    // Dispose of the Java side bitmap.
                    System.GC.Collect();
                }
                else
                {
                    picStatus = "ChoosePictureCanceled";
                }
                platformTask = null;
            }
            else if (requestCode == PickFromCamera)
            {
                if (resultCode == Result.Ok)  // && data != null)
                {
                    picStatus = "TakePhotoLoading";
                    // Make it available in the gallery

                    try
                    {
                        Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
                        Uri contentUri = Uri.FromFile(photoFile);
                        mediaScanIntent.SetData(contentUri);
                        Activity1.SendBroadcast(mediaScanIntent);

                        // Display in ImageView. We will resize the bitmap to fit the display
                        // Loading the full sized image will consume to much memory 
                        // and cause the application to crash.
                        string extension = System.IO.Path.GetExtension(contentUri.ToString());
                        int width = 540 - 6;  //480; // 540 - 6; // _imageView.Height;
                        int height = 540 - 6;  //800; // 270 - 6; // Resources.DisplayMetrics.HeightPixels;
                        bitmap = photoFile.Path.LoadAndResizeBitmap(width, height);
                        if (bitmap != null)
                        {
                            var bitmapData = bitmap.ToBytes();
                            if (platformTask != null)
                            {
                                platformTask.ParamArrayResult = new string[] { extension };
                                platformTask.ParamArrayResultBytes = bitmapData;
                                platformTask.Start();
                            }
                            //_imageView.SetImageBitmap(App.bitmap);
                            bitmap = null;
                        }
                    }
                    catch (Exception ex)  // (Java.IO.FileNotFoundException e)
                    {
                        picStatus = "TakePhotoFailure";
                        WebTools.TakeWarnMsg("读取照片失败:", ex.Message.NullToString(), ex);
                        //throw e;
                        //Log.e("Exception", e.getMessage(),e);
                    }

                    // Dispose of the Java side bitmap.
                    System.GC.Collect();
                }
                else
                {
                    picStatus = "TakePhotoCanceled";
                }
                platformTask = null;
            }
            else
            {

            }
        }

    }

    public static class AndroidContentManager
    {
        // Keep this static so we only call Game.Activity.Assets.List() once
        // No need to call it for each file if the list will never change.
        // We do need one file list per folder though.
        static ICSharpCode.SharpZipLib.Zip.ZipFile zif;
        static ApplicationInfo ainfo;

        static PackageInfo pinfo;

        static string obbPath;

        public static List<string> entries = null;

        public static void Init()
        {
            if (entries == null)
            {
                Activity activity = Game.Activity;

                try
                {
                    ainfo = activity.ApplicationInfo;
                    pinfo = activity.PackageManager.GetPackageInfo(ainfo.PackageName, PackageInfoFlags.MetaData);

                    obbPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "obb", ainfo.PackageName, $"main.{Platform.PackVersion}.{ainfo.PackageName}.obb");
                    //#if DEBUG
                    //                obbPath = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "obb", ainfo.PackageName + ".debug", String.Format("main.{0}.{1}.obb", expansionPackVersion, ainfo.PackageName));
                    //#else
                    //            obbPath = Path.Combine (Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "Android", "obb", ainfo.PackageName, String.Format ("main.{0}.{1}.obb", expansionPackVersion, ainfo.PackageName));
                    //#endif
                    entries = new List<string>();
                    if (File.Exists(obbPath))
                    {
                        zif = new ICSharpCode.SharpZipLib.Zip.ZipFile(obbPath);

                        entries = new List<string>();

                        for (int i = 0; i < zif.Count; i++)
                        {
                            var file = zif[i].Name;
                            if (System.IO.Path.GetExtension(file).Length > 0)
                            {
                                entries.Add(file);
                            }
                        }
                    }
                    else
                    {

                    }

                }
                catch (Exception e)
                {
                    //System.Diagnostics.Debug.WriteLine("++    Zip expansion file could not be opened    ++  {0}", e.Message);
                    zif = null;
                }
            }
        }

        public static Stream OpenStream(string assetName)
        {
            //Stream stream;

            string assetPath = assetName;  // System.IO.Path.Combine(RootDirectory, assetName) + ".xnb";

            Activity activity = Game.Activity;

            string filePath = assetPath;

            lock (Platform.IoLock)
            {
                var ze = zif.GetEntry(filePath);

                using (var stream = zif.GetInputStream(ze))
                {
                    var mstream = new MemoryStream((int)ze.Size);

                    stream.CopyTo(mstream);

                    mstream.Seek(0, SeekOrigin.Begin);

                    return mstream;
                }
            }
        }


        //static string[] textureExtensions = new string[] { ".jpg", ".bmp", ".jpeg", ".png", ".gif" };
        //static string[] songExtensions = new string[] { ".mp3", ".ogg", ".mid" };
        //static string[] soundEffectExtensions = new string[] { ".wav", ".mp3", ".ogg", ".mid" };
        //protected override string Normalize<T>(string assetName)
        //{
        //    string result = null;

        //    if (typeof(T) == typeof(Texture2D) || typeof(T) == typeof(Texture))
        //    {
        //        result = Normalize(assetName, textureExtensions);
        //    }
        //    else if ((typeof(T) == typeof(Song)))
        //    {
        //        result = Normalize(assetName, songExtensions);
        //    }
        //    else if ((typeof(T) == typeof(SoundEffect)))
        //    {
        //        result = Normalize(assetName, soundEffectExtensions);
        //    }
        //    if (result == null)
        //    { //item might not be in the package or be an unsupported file type
        //        result = base.Normalize<T>(assetName);
        //    }
        //    return result;
        //}

        //protected override object ReadRawAsset<T>(string assetName, string originalAssetName)
        //{
        //    if (zif == null)
        //        return base.ReadRawAsset<T>(assetName, originalAssetName);
        //    var ze = zif.GetEntry(assetName);
        //    if (ze == null)
        //    {
        //        return base.ReadRawAsset<T>(assetName, originalAssetName);
        //    }
        //    if (typeof(T) == typeof(Texture2D) || typeof(T) == typeof(Texture))
        //    {
        //        lock (ContentManagerLock)
        //        {
        //            using (MemoryStream mstream = new MemoryStream((int)ze.Size))
        //            {

        //                using (var stream = zif.GetInputStream(ze))
        //                {
        //                    stream.CopyTo(mstream);

        //                    mstream.Seek(0, SeekOrigin.Begin);

        //                }

        //                Texture2D texture = Texture2D.FromStream(
        //                                        graphicsDeviceService.GraphicsDevice, mstream);
        //                texture.Name = originalAssetName;
        //                return texture;
        //            }
        //        }
        //    }
        //    else if ((typeof(T) == typeof(Song)))
        //    {
        //        return new Song(obbPath, zif.LocateEntry(ze), ze.CompressedSize);
        //    }
        //    else if ((typeof(T) == typeof(SoundEffect)))
        //    {
        //        return new SoundEffect(obbPath, zif.LocateEntry(ze), ze.CompressedSize);
        //    }
        //    throw new NotImplementedException("This format of file is not supported as raw file");
        //}

        //internal string Normalize(string fileName, string[] extensions)
        //{
        //    if (zif == null)
        //        return null;
        //    int index = fileName.LastIndexOf(System.IO.Path.DirectorySeparatorChar);
        //    string path = string.Empty;
        //    string file = fileName;
        //    if (index >= 0)
        //    {
        //        path = fileName.Substring(0, index);
        //        file = fileName.Substring(index + 1, fileName.Length - index - 1);
        //    }

        //    Dictionary<string, int> files = null;
        //    if (!entries.TryGetValue(path, out files))
        //        return null;

        //    bool found = false;
        //    index = -1;
        //    foreach (string s in extensions)
        //    {
        //        if (files.TryGetValue(file + s, out index))
        //        {
        //            found = true;
        //            break;
        //        }
        //    }
        //    if (!found)
        //        return null;

        //    return zif[index].Name;
        //}
    }

    public static class BitmapHelpers
    {
        public static byte[] ToBytes(this Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                byte[] bitmapData = stream.ToArray();
                return bitmapData;
            }
        }

        public static Bitmap ToBitmap(this byte[] bytes)
        {
            using (var memo = new MemoryStream(bytes))
            {
                return BitmapFactory.DecodeStream(memo);
            }
        }

        public static Bitmap ResizeImage(Bitmap bitmap, int w, int h, bool sameRatio)
        {
            Bitmap BitmapOrg = bitmap;
            int width = BitmapOrg.Width;
            int height = BitmapOrg.Height;
            int newWidth = w;
            int newHeight = h;

            float scaleWidth = ((float)newWidth) / width;
            float scaleHeight = ((float)newHeight) / height;

            if (sameRatio)
            {
                if (scaleWidth > scaleHeight)
                {
                    scaleWidth = scaleHeight;
                }
                else
                {
                    scaleHeight = scaleWidth;
                }
            }

            Matrix matrix = new Matrix();
            matrix.PostScale(scaleWidth, scaleHeight);
            // if you want to rotate the Bitmap   
            // matrix.postRotate(45);   
            Bitmap resizedBitmap = Bitmap.CreateBitmap(BitmapOrg, 0, 0, width, height, matrix, true);
            return resizedBitmap;
            //return new BitmapDrawable(resizedBitmap);
        }

        public static Bitmap LoadAndResizeBitmap(this string fileName, int width, int height)
        {
            // First we get the the dimensions of the file on disk
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            // Next we calculate the ratio that we need to resize the image by
            // in order to fit the requested dimensions.
            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            // Now we will load the image and have BitmapFactory resize it for us.
            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            Bitmap resizedBitmap = BitmapFactory.DecodeFile(fileName, options);

            return resizedBitmap;
        }

    }
    
    public class PlatformTask
    {
        System.Threading.Thread thread;
        public bool IsStop = false;
        public string[] ParamArray = null;
        public string[] ParamArrayResult = null;
        public byte[] ParamArrayResultBytes = null;
        public System.AsyncCallback OnStartFinish = null;
        public PlatformTask(System.Action action)
        {
            thread = new System.Threading.Thread(() => 
                { 
                    action.Invoke();
                    if (OnStartFinish != null)
                    {
                        OnStartFinish.Invoke(null);
                        Platform.platformTask = null;
                    }
                });
        }
        public bool IsAlive
        {
            get
            {
                return thread != null && thread.ThreadState == System.Threading.ThreadState.Running;
            }
        }

        public void Abort()
        {
            IsStop = true;
        }
        public void Start()
        {
            thread.Start();
        }
    }

    public class PlatformTask2
    {
        System.Action act;
        public PlatformTask2(System.Action action)
        {
            act = action;
        }
        public void Start()
        {
            act.Invoke();
            //act.BeginInvoke(null, null);
        }
    }

}
