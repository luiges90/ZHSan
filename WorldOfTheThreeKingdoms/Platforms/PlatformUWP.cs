using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Media;
using SanguoSeason;
using GameManager;
using GamePanels;
using SeasonContracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Data.Xml.Dom;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System;
using Windows.System.Display;
using Windows.System.Profile;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;
using Windows.Services.Store;
using WorldOfTheThreeKingdoms.UWP;
using Windows.ApplicationModel.Store;
using Platforms;
using Tools;
using Windows.Storage.Search;
using Windows.Graphics.Display;
using System.Reflection;

namespace Platforms
{
    /// <summary>
    /// 各平台不同的實現
    /// </summary>
    public class Platform : PlatformBase
    {
        public static GamePage GamePage1;

        public static new PlatFormType PlatFormType = PlatFormType.UWP;

        public static new bool IsMobilePlatForm = true;

        public new string Channel = "";

        public new bool WindowInputCapturerEnable = true;

        //static ApplicationViewState ViewState = ApplicationViewState.FullScreenLandscape;

        public static bool IsActive
        {
            get
            {
                return MainGame.IsActive;
            }
        }

        public new bool IsGuideVisible
        {
            get
            {
                return false; // Guide.IsVisible;
            }
        }

        bool? keyboardAvailable = null;
        public new bool KeyBoardAvailable
        {
            get
            {
                if (keyboardAvailable == null)
                {
                    KeyboardCapabilities keyboardCapabilities = new KeyboardCapabilities();
                    keyboardAvailable = keyboardCapabilities.KeyboardPresent != 0;
                }
                return (bool)keyboardAvailable;
            }
        }

        static GraphicsDeviceManager GraphicsDeviceManager = null;

        public static GraphicsDevice GraphicsDevice
        {
            get
            {
                return GraphicsDeviceManager != null ? GraphicsDeviceManager.GraphicsDevice : null;
            }
        }

        static bool? isMobile;

        public static new bool IsMobile
        {
            get
            {
                if (isMobile == null)
                {
                    var qualifiers = Windows.ApplicationModel.Resources.Core.ResourceContext.GetForCurrentView().QualifierValues;
                    isMobile = (qualifiers.ContainsKey("DeviceFamily") && qualifiers["DeviceFamily"] == "Mobile");
                }
                return (bool)isMobile;
            }
        }

        public static void InitGraphicsDeviceManager()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(MainGame); //SharedGraphicsDeviceManager.Current;
            //GamePage.CurrentGamePage.Game1.SupportedOrientations = Microsoft.Phone.Controls.SupportedPageOrientation.Landscape;
            //GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        public static void SetGraphicsWidthHeight(int width, int height)
        {
            //width = Session.MainGame.Window.ClientBounds.Width;
            //height = Session.MainGame.Window.ClientBounds.Height;
            //int bufferWidth = GraphicsDeviceManager.PreferredBackBufferWidth;
            //int bufferHeight = GraphicsDeviceManager.PreferredBackBufferHeight;
            GraphicsDeviceManager.PreferredBackBufferWidth = width;
            GraphicsDeviceManager.PreferredBackBufferHeight = height;
            GraphicsDevice.Viewport = new Viewport(0, 0, width, height);
        }

        //public override void ProcessViewChanged()
        //{
        //    //InputManager.Scale = new Vector2(Convert.ToSingle(Session.MainGame.Window.ClientBounds.Width) / Convert.ToSingle(Session.ResolutionX),
        //    //    Convert.ToSingle(Session.MainGame.Window.ClientBounds.Height) / Convert.ToSingle(Session.ResolutionY));

        //    Session.MainGame.SpriteScale1 = Matrix.CreateScale(InputManager.Scale.X, InputManager.Scale.Y, 1);
        //    //InputManager.ScaleOne = new Vector2(Convert.ToSingle(Game1.Window.ClientBounds.Width) / Convert.ToSingle(Session.ResolutionX),
        //    //    Convert.ToSingle(GamePage1.Height) / Convert.ToSingle(Session.ResolutionY));
        //    Session.MainGame.disScale = true;

        //    //CoreGame.Current.SpriteScale = Matrix.CreateScale(screenScale, screenScale, 1);
        //    //InputManager.Scale = new Vector2(screenScale, screenScale);
        //    Platform.SetGraphicsWidthHeight(Session.MainGame.Window.ClientBounds.Width, Session.MainGame.Window.ClientBounds.Height);

        //    //InputManager.ScaleOne = new Vector2(1, 1);
        //    if (ViewState == Windows.UI.ViewManagement.ApplicationViewState.Snapped)
        //    {

        //    }
        //    else if (ViewState == Windows.UI.ViewManagement.ApplicationViewState.Filled)
        //    {
        //        //InputManager.ScaleOne = new Vector2(1.2f, 1f);
        //    }
        //    else
        //    {
        //        //SeasonGame.graphics.PreferredBackBufferWidth = 1024;
        //        //SeasonGame.graphics.PreferredBackBufferHeight = 768;
        //        //SeasonGame.graphics.ApplyChanges();
        //    }
        //    GraphicsDevice.Viewport = new Viewport(0, 0, GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight);
        //}

        public static void GraphicsApplyChanges()
        {
            GraphicsDeviceManager.ApplyChanges();
        }

        public override void PreparePhone()
        {
            var size = GetCurrentDisplaySize();

            int width = Convert.ToInt32(size.Width);
            int height = Convert.ToInt32(size.Height);
            //int width = GraphicsDeviceManager.GraphicsDevice.Viewport.Width;
            //int height = GraphicsDeviceManager.GraphicsDevice.Viewport.Height;
            if (width >= height)
            {
                Session.MainGame.fullScreenDestination = new Rectangle(0, 0, width, height);
            }
            else
            {
                Session.MainGame.fullScreenDestination = new Rectangle(0, 0, height, width);
            }
        }

        public static Size GetCurrentDisplaySize()
        {
            var displayInformation = DisplayInformation.GetForCurrentView();
            TypeInfo t = typeof(DisplayInformation).GetTypeInfo();
            var props = t.DeclaredProperties.Where(x => x.Name.StartsWith("Screen") && x.Name.EndsWith("InRawPixels")).ToArray();
            var w = props.Where(x => x.Name.Contains("Width")).First().GetValue(displayInformation);
            var h = props.Where(x => x.Name.Contains("Height")).First().GetValue(displayInformation);
            var size = new Size(System.Convert.ToDouble(w), System.Convert.ToDouble(h));
            switch (displayInformation.CurrentOrientation)
            {
                case DisplayOrientations.Landscape:
                case DisplayOrientations.LandscapeFlipped:
                    size = new Size(Math.Max(size.Width, size.Height), Math.Min(size.Width, size.Height));
                    break;
                case DisplayOrientations.Portrait:
                case DisplayOrientations.PortraitFlipped:
                    size = new Size(Math.Min(size.Width, size.Height), Math.Max(size.Width, size.Height));
                    break;
            }
            return size;
        }

        public override void SetMouseVisible(bool visible)
        {
            MainGame.IsMouseVisible = visible;
        }

        public override void SetWindowAllowUserResizing(bool allow)
        {
            MainGame.Window.AllowUserResizing = allow;
        }

        public override void SetTimerDisabled(bool timerDisabled)
        {
            try
            {
                var displayRequest = new DisplayRequest();
                if (timerDisabled)
                {
                    displayRequest.RequestActive();
                }
                else
                {
                    displayRequest.RequestRelease();
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("设置应用常亮失败:", "SetTimerDisabled:" + timerDisabled, ex);
            }
        }

        public override void SetFullScreen(bool full)
        {
            GraphicsDeviceManager.IsFullScreen = full;
            //SeasonGame.graphics.IsFullScreen = full;
            //CoreGame.Current.graphics.IsFullScreen = true;
        }

        public override void SetOrientations()
        {
            GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        public override string GetDeviceID()
        {
            return AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
        }

        public override string GetDeviceInfo()
        {
            string info = "";
            try
            {
                string sv = AnalyticsInfo.VersionInfo.DeviceFamilyVersion;
                ulong v = ulong.Parse(sv);
                ulong v1 = (v & 0xFFFF000000000000L) >> 48;
                ulong v2 = (v & 0x0000FFFF00000000L) >> 32;
                ulong v3 = (v & 0x00000000FFFF0000L) >> 16;
                ulong v4 = (v & 0x000000000000FFFFL);
                string version = $"{v1}.{v2}.{v3}.{v4}";
                info = AnalyticsInfo.VersionInfo.DeviceFamily + " " + version;
            }
            catch
            {

            }
            return info;
        }

        public override string GetSystemInfo()
        {
            string info = "";
            try
            {
                info = AnalyticsInfo.DeviceForm;
            }
            catch
            {

            }
            return info;
        }

        #region 加載資源文件
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string LoadText(string res)
        {
            res = res.Replace("/", "\\");
            return Task.Run(async () => await GetAppText(res)).Result;
        }
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string[] LoadTexts(string res)
        {
            string text = LoadText(res);
            return text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
        }

        public override string[] GetFiles(string dir)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            var files = Task.Run(async () => await folder.GetFilesAsync(CommonFileQuery.DefaultQuery)).Result;

            List<string> result = new List<string>();

            foreach (var file in files)
            {
                result.Add(file.Name);
            }

            return result.ToArray();
        }

        private async Task<string> GetAppText(string res)
        {
            var folder = Windows.ApplicationModel.Package.Current.InstalledLocation;

            var file = await folder.GetFileAsync(res);

            return await Windows.Storage.FileIO.ReadTextAsync(file);
        }
        /// <summary>
        /// 加載資源文件
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public byte[] LoadFile(string res)
        {
            res = res.Replace("\\", "/");
            using (var dest = new MemoryStream())
            {
                lock (Platform.IoLock)
                {
                    using (Stream stream = TitleContainer.OpenStream(res))
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

                }
                lock (Platform.IoLock)
                {
                    using (var stream = isUser ? LoadUserFileStream(res) : TitleContainer.OpenStream(res))
                    {
                        Texture2D tex = Texture2D.FromStream(GraphicsDevice, stream);
                        return tex;
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("加载游戏材质失败:" + res, "LoadTexture:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        #endregion

        #region 處理用戶文件

        public async Task<Stream> GetStorageFileStream(string res, bool write)
        {
            try
            {
                var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                if (write)
                {
                    //var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                    //localFolder.Path + "\\" + res
                    var storageFile = await localFolder.CreateFileAsync(res, CreationCollisionOption.ReplaceExisting);
                    return await storageFile.OpenStreamForWriteAsync();
                }
                else
                {
                    var file = await localFolder.GetFileAsync(res); // StorageFile.GetFileFromPathAsync(res);
                    return await file.OpenStreamForReadAsync();
                }
            }
            catch (Exception ex)
            {
                return null;
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
        /// 加載用戶文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string GetUserText(string res)
        {
            try
            {
                res = res.Replace("/", "\\");
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var stream = Task.Run(async () => await GetStorageFileStream(res, false)).Result;  //folder.Path + "\\" +
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        return streamReader.ReadToEnd();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
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
                res = res.Replace("/", "\\");
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var stream = Task.Run(async () => await GetStorageFileStream(res, false)).Result;
                if (stream != null)
                {
                    using (var streamReader = new StreamReader(stream))
                    {
                        List<string> list = new List<string>();
                        while (!streamReader.EndOfStream)
                        {
                            list.Add(streamReader.ReadLine());
                        }
                        return list.ToArray();
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
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
                res = res.Replace("/", "\\");
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var stream = Task.Run(async () => await GetStorageFileStream(res, false)).Result;
                if (stream != null)
                {
                    byte[] bytes;
                    using (var binaryReader = new BinaryReader(stream))
                    {
                        bytes = binaryReader.ReadBytes(Convert.ToInt32(stream.Length));
                    }
                    return bytes;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
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
        //            if (!String.IsNullOrEmpty(value))
        //            {
        //                return value.Split('=')[1];
        //            }
        //        }
        //    }
        //    catch (Exception ex)
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
            Stream stream = Task.Run(async () => await GetStorageFileStream(res, write)).Result;
            return stream;
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
            catch (Exception ex)
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
                var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
                //IsolatedStorageFile storage = GetIsolatedStorageFile();
                List<bool> results = new List<bool>();
                foreach (string re in res)
                {
                    bool exis = false;
                    if (!String.IsNullOrEmpty(re.Trim()))
                    {
                        try
                        {
                            exis = Task.Run(async () => await GetStorageFileStream(re.Trim(), false)).Result != null;  //folder.Path + "\\" + 
                        }
                        catch
                        {

                        }
                    }
                    results.Add(exis);
                }
                return results.ToArray();
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("判断文件存在失败:" + String.Join(",", res), "UserFileExist:" + UserApplicationDataPath + String.Join(",", res), ex);
                return null;
            }
        }
        public void SaveUserFile(string res, string content)
        {
            //Windows.Storage.StorageFile storageFile = await localFolder.CreateFileAsync("settings.config", CreationCollisionOption.OpenIfExists);
            //if (UserFileExist(new string[] { res })[0])
            //{
            //    Task.Run(async () => await Season.Current.DelUserFilesAsync(new string[] { res }));
            //Season.Current.DelUserFiles(new string[] { res });
            //}
            try
            {
                //DelUserFiles(new string[] { res });
                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var storageFile = Task.Run(async () => await localFolder.CreateFileAsync(res, CreationCollisionOption.ReplaceExisting)).Result;
                var writeStream = Task.Run(async () => await storageFile.OpenStreamForWriteAsync()).Result;
                using (var writer = new StreamWriter(writeStream))
                {
                    writer.Write(content);
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文本失败:" + res, "SaveUserFile:" + UserApplicationDataPath + res, ex);
            }
        }

        public void SaveUserFile(string res, byte[] bytes)
        {
            try
            {
                //DelUserFiles(new string[] { res });
                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var storageFile = Task.Run(async () => await localFolder.CreateFileAsync(res, CreationCollisionOption.ReplaceExisting)).Result;
                var writeStream = Task.Run(async () => await storageFile.OpenStreamForWriteAsync()).Result;
                using (var writer = new BinaryWriter(writeStream))
                {
                    writer.Write(bytes);
                }
                //if (action != null)
                //{
                //    action.Start();
                //}
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文本失败:" + res, "SaveUserFile:" + UserApplicationDataPath + res, ex);
            }
        }

        //async Task SaveUserFileAsync(string res, byte[] bytes, SeasonTask action)
        //{
        //    try
        //    {
        //        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        //        Windows.Storage.StorageFile storageFile = await localFolder.CreateFileAsync(res, CreationCollisionOption.OpenIfExists);
        //        Stream writeStream = await storageFile.OpenStreamForWriteAsync();
        //        using (var writer = new BinaryWriter(writeStream))
        //        {
        //            writer.Write(bytes);
        //        }
        //        if (action != null)
        //        {
        //            action.Start();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        WebTools.TakeWarnMsg("保存用户文件失败:" + res, "SaveUserFile:bytes" + bytes.Length + UserApplicationDataPath + res, ex);
        //    }
        //}

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
        //    SaveUserFile("settings.config", String.Join("\r\n", strings));
        //}

        public void SaveUserFile(string res, byte[] bytes, PlatformTask action)
        {
            try
            {
                SaveUserFile(res, bytes);
                if (action != null)
                {
                    action.Start();
                }

                //var task = new SeasonTask(() => {
                //    SaveUserFile(res, bytes);
                //    if (action != null)
                //    {
                //        action.Start();
                //    }
                //});
                //DelUserFiles(new string[] { res }, task);

                //var task = new SeasonTask(() => {
                //    SaveUserFileAsync(res, bytes, action);
                //});
                //DelUserFiles(new string[] { res }, task);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文件失败:" + res, "SaveUserFile:bytes" + bytes.Length + UserApplicationDataPath + res, ex);
            }
        }

        //public void SaveUserFileStatic(string picture, byte[] shot)
        //{
        //    //Task.Run(async () => await Season.Current.SaveUserFile(picture, shot, null));
        //}

        //public void DelUserFiles(string[] files)
        //{
        //    Task.Run(async () => await Season.Current.DelUserFilesAsync(files));
        //}

        public async Task DelUserFiles(string[] files, PlatformTask action)
        {
            foreach (string file in files)
            {
                try
                {
                    if (UserFileExist(new string[] { file.NullToString().Trim() })[0])
                    {
                        var folder = Windows.Storage.ApplicationData.Current.LocalFolder;

                        var f = await StorageFile.GetFileFromPathAsync(folder.Path + "\\" + file.Trim());
                        if (f != null)
                        {
                            try
                            {
                                await f.DeleteAsync();
                                if (action != null)
                                {
                                    action.Start();
                                }
                            }
                            catch (Exception ex)
                            {
                                WebTools.TakeWarnMsg("删除用户文件失败:" + file, "StorageFile.Delete:" + UserApplicationDataPath + file, ex);
                            }
                        }
                    }
                    else
                    {
                        if (action != null)
                        {
                            action.Start();
                        }
                    }
                }
                catch (Exception ex)
                {
                    WebTools.TakeWarnMsg("删除用户文件失败:" + file, "StorageFile.Delete:" + UserApplicationDataPath + file, ex);
                }
            }
        }

        #endregion

        public static void Sleep(int time)
        {
            System.Threading.Tasks.Task.Delay(time);
        }

        public async override void OpenMarket(string key)
        {
            try
            {
                //Guide.ShowMarketplace(PlayerIndex.One);            
                //WindowsRuntimeSystemExtensions.AsTask(Guide._dispatcher.RunAsync(0, delegate
                //{
                Uri uri = new Uri("ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
                await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("打开应用市场失败:" + key, "OpenMarket：" + UserApplicationDataPath, ex);
            }
            //WindowsRuntimeSystemExtensions.AsTask<bool>(Launcher.LaunchUriAsync(uri)).Wait();
            //}));
        }

        public async override void OpenReview(string key)
        {
            try
            {
                var uri = new Uri(@"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
                await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                string msg = ex.ToString();
            }
            //await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:REVIEW?PFN=7fd9f4fa-bb75-40c1-9ae8-8304078b895b_eejrnnmnm6bfw");
            //Task.Run(async () => await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-windows-store:Search?query=三國")));
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
                screenshot = new RenderTarget2D(graphics, screenshot.Width, screenshot.Height, false, SurfaceFormat.Color, DepthFormat.None);
            }
            return shot;
        }

        public Stream GZipStream(MemoryStream ms, bool compress)
        {
            GZipStream compressedzipStream = new GZipStream(ms, compress ? CompressionMode.Compress : CompressionMode.Decompress, true);
            return compressedzipStream as Stream;
        }

        /// <summary>
        /// 調用WebService
        /// </summary>
        /// <param name="strURL"></param>
        /// <returns></returns>
        public string GetWebService(string strURL)
        {
            Uri uri = new Uri(strURL);  // "http://example.com/sample.xml");
            //XmlDocument xmlDocument = await XmlDocument.LoadFromUriAsync(uri);

            var xmlDocument = Task.Run(async () => await Windows.Data.Xml.Dom.XmlDocument.LoadFromUriAsync(uri)).Result;
            string strValue = xmlDocument.InnerText.Replace("&lt;", "<").Replace("&gt;", ">");
            return strValue;

            //strValue = strValue.Replace("&lt;", "<");
            //strValue = strValue.Replace("&gt;", ">");
            //strValue = strValue.Replace("&lt;", "<");
            //strValue = strValue.Replace("&gt;", ">");
            ////MessageBox.Show(strValue);
            //Reader.Close();
            //request.Abort();
            //response.Close();
            //do something with the xmlDocument.
            //var client = new WebClient();
            //string result = null;
            //byte[] response = client.DownloadData(new Uri(strURL));
            //MemoryStream stream = new MemoryStream(response);
            //XmlTextReader reader = new XmlTextReader(stream);
            //reader.MoveToContent();
            //result = reader.ReadInnerXml();
            //reader.Close();
            //stream.Close();
            ////string result = Encoding.ASCII.GetString(response);
            ////return result;
            //result = result.Replace("&lt;", "<").Replace("&gt;", ">");
            //return result;
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

        public string GetWebServicePost(string strURL, string data, PlatformTask onUpload)
        {
            return Task.Run(async () => await GetWebServicePostAsync(strURL, data)).Result;
        }

        /// <summary>
        /// 調用WebService (post方式)
        /// </summary>
        /// <param name="strURL"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<string> GetWebServicePostAsync(string strURL, string data)
        {
            var requestBody = data;
            var handler = new HttpClientHandler { UseDefaultCredentials = true, AllowAutoRedirect = false };
            var client = new System.Net.Http.HttpClient(handler);

            HttpContent httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            System.Net.Http.HttpResponseMessage response = await client.PostAsync(strURL, httpContent);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var xml = XDocument.Load(stream);

            //var x = await Windows.Data.Xml.Dom.XmlDocument..LoadFromUriAsync(new Uri(strURL));

            var text = xml.Elements().FirstOrDefault().Value;

            text = text.Replace("&lt;", "<").Replace("&gt;", ">");

            return text;

            //XmlTextReader reader = new XmlTextReader(stream);
            //.MoveToContent();
            //result = reader.ReadInnerXml();
            //reader.Close();
            //stream.Close();
            //string result = Encoding.ASCII.GetString(response);
            //return result;
            //result = result.Replace("&lt;", "<").Replace("&gt;", ">");

            //return response.Content.ReadAsStringAsync().Result;
        }

        //public byte[] DownloadWebData(string file)
        //{
        //    return Task.Run(async () => await DownloadWebDataAsync(file)).Result;
        //}

        public async void DownloadWebData(string file, PlatformTask action)
        {
            try
            {
                Uri uri = new Uri(file);
                var client = new Windows.Web.Http.HttpClient();

                Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsInputStreamAsync();

                using (var ms = new InMemoryRandomAccessStream())
                {
                    await response.Content.WriteToStreamAsync(ms);
                    ms.Seek(0ul);
                    var bytes = ms.AsStreamForRead().StreamToBytes();

                    if (action != null)
                    {
                        action.ParamArrayResultBytes = bytes;
                        action.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("用户文件下载失败:" + file, "DownloadFile:" + file, ex);
            }
            //byte[] response = client.DownloadData(new Uri(file));
            //return response;
        }

        public void OpenLink(string link)
        {
            //var uri = new Uri(@"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
            //var uri = new Uri("http://www.baidu.com");
            //bool result = Task.Run(async () => await Windows.System.Launcher.LaunchUriAsync(uri)).Result;
            //string resu = result.ToString();
            //return;
            //Launcher.LaunchUriAsync(new Uri("http://verysoftware.co.uk"));
            try
            {
                Platform.Current.OpenLink2(link);
            }
            catch (Exception ex)
            {
                //try
                //{
                //    Season.OpenLink1(link);
                //}
                //catch (Exception ex2)
                //{
                //    SeasonTools.SendErrMsg("InvokeMember打開IE出錯：", ex2);
                //}
                WebTools.TakeWarnMsg("ProcessStartInfo打開IE出錯：", "", ex);
            }
        }
        /// <summary>
        /// 打開IE窗口1
        /// </summary>
        public void OpenLink1(string link)
        {
            if (!OpenWeb)
            {
                return;
            }
            else
            {

            }
        }
        /// <summary>
        /// 打開IE窗口2
        /// </summary>
        public void OpenLink2(string link)
        {
            if (!OpenWeb)
            {
                return;
            }
            else
            {
                var uri = new Uri(link); // new Uri(WebTools.WebSite2); //  @"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
                Windows.System.Launcher.LaunchUriAsync(uri).GetResults();
            }
        }

        public override void OpenFactory()
        {
            //var factory = new GameFrameworkViewSource<Game1>();
            //WinRT?8
            //var factory = new MonoGame.Framework.GameFrameworkViewSource<Game1>();
            //Windows.ApplicationModel.Core.CoreApplication.Run(factory);
        }

        public override void ShowKeyBoard(PlayerIndex index, string name, string title, string desc, AsyncCallback CallbackFunction)
        {
            GamePage1.ViewTxtSeason(desc, CallbackFunction);
            //Guide.BeginShowKeyboardInput(index, name, title, desc, CallbackFunction, null);
        }

        public override string EndShowKeyBoard(IAsyncResult ar)
        {
            return GamePage1.GetTxtSeason();
            //return Guide.EndShowKeyboardInput(ar);
            //return "";
        }

        public static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.Exception;
            Session.MainGame.err = e.ToString();
            //if (Season.PlatForm == PlatForm.Win)
            //{
            //    Season.Current.OpenLink(WebTools.WebSite2 + "/service.aspx?mes=" + e.ToString() + "&platform=" + Season.PlatForm.ToString());
            //}
            WebTools.TakeWarnMsg("RuntimeTerminating: " + args, "", e);
        }

        public override void ApplicationViewChanged()
        {
            //Game1.ApplicationViewChanged += (sender, e) =>
            //{
            //    ViewState = e.ViewState;
            //    Session.ChangeDisplay(false);
            //};         
        }

        //public static void ShowKeyBoard()
        //{
        //    Guide.BeginShowKeyboardInput(PlayerIndex.One, Title, Desc, this.Text, CallbackFunction, null);
        //}        

        public override void Exit()
        {
            //App.Current.Terminate();
            //try
            //{
            //    App.Current.Exit();
            //}
            //catch (Exception ex)
            //{
            //    //string msg = ex.ToString();
            //}
            //Environment.Exit();
            //Application.Current.Exit();
            //new Microsoft.Xna.Framework.Game().Exit();
            //CoreGame.Current.Exit();
        }

        public static string picStatus = "";

        public async override void ChoosePicture(PlatformTask action)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                var stream = file.OpenStreamForReadAsync();
                await stream.ContinueWith((st) =>
                {
                    if (action != null && stream != null)
                    {
                        var photoBytes = stream.Result.StreamToBytes();

                        var task = new PlatformTask(() => { });
                        task.OnStartFinish += new AsyncCallback((result) =>
                        {
                            action.ParamArrayResultBytes = task.ParamArrayResultBytes;  // photoBytes;
                            action.Start();
                        });

                        ResizeImageFile(photoBytes, 540 - 6, 540 - 6, true, task);
                    }
                });
            }
        }

        public async override void TakePhoto(PlatformTask action)
        {
            CameraCaptureUI cc = new CameraCaptureUI();
            cc.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            cc.PhotoSettings.CroppedAspectRatio = new Size(4, 3);
            cc.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;
            cc.PhotoSettings.AllowCropping = true;
            StorageFile sf = await cc.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (sf != null)
            {
                var stream = sf.OpenStreamForReadAsync();
                await stream.ContinueWith((st) =>
                {
                    if (action != null && stream != null)
                    {
                        var photoBytes = stream.Result.StreamToBytes();

                        var task = new PlatformTask(() => { });
                        task.OnStartFinish += new AsyncCallback((result) =>
                        {
                            action.ParamArrayResultBytes = task.ParamArrayResultBytes;  // photoBytes;
                            action.Start();
                        });

                        ResizeImageFile(photoBytes, 540 - 6, 540 - 6, true, task);
                    }
                });
            }
        }

        public async override void MirrorPicture(byte[] image, PlatformTask action)
        {
            var memStream = new MemoryStream(image);

            IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
            var decoder = await BitmapDecoder.CreateAsync(imageStream);

            using (imageStream)
            {
                var resizedStream = new InMemoryRandomAccessStream();

                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);

                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                encoder.BitmapTransform.Flip = BitmapFlip.Horizontal;

                await encoder.FlushAsync();
                resizedStream.Seek(0);
                var outBuffer = new byte[resizedStream.Size];
                await resizedStream.ReadAsync(outBuffer.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                //uint x = await resizedStream.WriteAsync(outBuffer.AsBuffer());
                //outBuffer;

                if (action != null)
                {
                    action.ParamArrayResultBytes = outBuffer;
                    action.Start();
                }
            }
        }

        //public override void RotatePicture(byte[] image, int rotate, SeasonTask action)
        //{
        //    RotatePictureAsync(image, rotate, action);
        //}

        public async override void RotatePicture(byte[] image, int rotate, PlatformTask action)
        {
            BitmapRotation orientationDegree = BitmapRotation.None;

            if (rotate == 0)
            {

            }
            else if (rotate == 1)
            {
                orientationDegree = BitmapRotation.Clockwise90Degrees;
            }
            else if (rotate == 2)
            {
                orientationDegree = BitmapRotation.Clockwise180Degrees;
            }
            else if (rotate == 3)
            {
                orientationDegree = BitmapRotation.Clockwise270Degrees;
            }

            var memStream = new MemoryStream(image);

            IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
            var decoder = await BitmapDecoder.CreateAsync(imageStream);

            using (imageStream)
            {
                var resizedStream = new InMemoryRandomAccessStream();

                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);

                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                encoder.BitmapTransform.Rotation = orientationDegree;

                await encoder.FlushAsync();
                resizedStream.Seek(0);
                var outBuffer = new byte[resizedStream.Size];
                await resizedStream.ReadAsync(outBuffer.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                //uint x = await resizedStream.WriteAsync(outBuffer.AsBuffer());
                //outBuffer;
                if (action != null)
                {
                    action.ParamArrayResultBytes = outBuffer;
                    action.Start();
                }
            }
        }

        public async override void CropPicture(byte[] image, int x, int y, int width, int height, PlatformTask action)
        {
            var memStream = new MemoryStream(image);

            IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
            var decoder = await BitmapDecoder.CreateAsync(imageStream);

            using (imageStream)
            {
                var resizedStream = new InMemoryRandomAccessStream();

                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);

                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                encoder.BitmapTransform.Bounds = new BitmapBounds()
                {
                    X = (uint)Math.Round(Convert.ToDecimal(x), 0),
                    Y = (uint)Math.Round(Convert.ToDecimal(y), 0),
                    Width = (uint)Math.Round(Convert.ToDecimal(width), 0),
                    Height = (uint)Math.Round(Convert.ToDecimal(height), 0)
                };

                await encoder.FlushAsync();
                resizedStream.Seek(0);
                var outBuffer = new byte[resizedStream.Size];
                await resizedStream.ReadAsync(outBuffer.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                //uint x = await resizedStream.WriteAsync(outBuffer.AsBuffer());
                //outBuffer;

                if (action != null)
                {
                    action.ParamArrayResultBytes = outBuffer;
                    action.Start();
                }
            }

            //MemoryStream stream = new MemoryStream(image);
            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.SetSource(stream.AsRandomAccessStream());

            //WriteableBitmap writeableBitmap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);
            //writeableBitmap.SetSource(stream.AsRandomAccessStream());

            //writeableBitmap = writeableBitmap.Crop(x, y, width, height);

            //MemoryStream ms2 = new MemoryStream();
            //writeableBitmap.ToStreamAsJpeg(ms2.AsRandomAccessStream());

            //if (action != null)
            //{
            //    action.ParamArrayResultBytes = ms2.ToArray();
            //    action.Start();
            //}
        }

        //public override void ResizeImageFile(byte[] image, int targetSizeWidth, int targetSizeHeight, SeasonTask action)
        //{
        //    ResizeImageFileAsync(image, targetSizeWidth, targetSizeHeight, 100, action);
        //}

        public async override void ResizeImageFile(byte[] image, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            var memStream = new MemoryStream(image);

            IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
            var decoder = await BitmapDecoder.CreateAsync(imageStream);
            //if (decoder.PixelHeight > targetSizeWidth || decoder.PixelWidth > targetSizeHeight)
            //{
            using (imageStream)
            {
                var resizedStream = new InMemoryRandomAccessStream();

                BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);

                Size newSize;

                if (sameRatio)
                {
                    float scale = GameTools.AutoSetScale(Convert.ToInt32(decoder.PixelWidth), Convert.ToInt32(decoder.PixelHeight), targetSizeWidth, targetSizeHeight);
                    newSize = new Size(Convert.ToInt32(decoder.PixelWidth * scale), Convert.ToInt32(decoder.PixelHeight * scale));
                }
                else
                {
                    newSize = new Size(Convert.ToInt32(targetSizeWidth), Convert.ToInt32(targetSizeHeight));
                }

                //double widthRatio = (double)targetSizeWidth / decoder.PixelWidth;
                //double heightRatio = (double)targetSizeHeight / decoder.PixelHeight;

                //double scaleRatio = Math.Min(widthRatio, heightRatio);

                //if (targetSizeWidth == 0)
                //    scaleRatio = heightRatio;

                //if (targetSizeHeight == 0)
                //    scaleRatio = widthRatio;

                uint aspectHeight = (uint)Math.Floor(newSize.Height); // decoder.PixelHeight * scaleRatio);
                uint aspectWidth = (uint)Math.Floor(newSize.Width); // decoder.PixelWidth * scaleRatio);

                encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                encoder.BitmapTransform.ScaledHeight = aspectHeight;
                encoder.BitmapTransform.ScaledWidth = aspectWidth;

                await encoder.FlushAsync();
                resizedStream.Seek(0);
                var outBuffer = new byte[resizedStream.Size];
                await resizedStream.ReadAsync(outBuffer.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);
                //uint x = await resizedStream.WriteAsync(outBuffer.AsBuffer());
                //return outBuffer;
                if (action != null)
                {
                    action.ParamArrayResultBytes = outBuffer;
                    action.Start();
                }
            }
            //}
        }

        public async override void CropResizePicture(byte[] image, int x, int y, int width, int height, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            byte[] pic = null;
            try
            {
                var memStream = new MemoryStream(image);

                IRandomAccessStream imageStream = memStream.AsRandomAccessStream();
                var decoder = await BitmapDecoder.CreateAsync(imageStream);

                using (imageStream)
                {
                    var resizedStream = new InMemoryRandomAccessStream();

                    BitmapEncoder encoder = await BitmapEncoder.CreateForTranscodingAsync(resizedStream, decoder);

                    encoder.BitmapTransform.InterpolationMode = BitmapInterpolationMode.Linear;

                    encoder.BitmapTransform.Bounds = new BitmapBounds()
                    {
                        X = (uint)Math.Round(Convert.ToDecimal(x), 0),
                        Y = (uint)Math.Round(Convert.ToDecimal(y), 0),
                        Width = (uint)Math.Round(Convert.ToDecimal(width), 0),
                        Height = (uint)Math.Round(Convert.ToDecimal(height), 0)
                    };

                    await encoder.FlushAsync();
                    resizedStream.Seek(0);
                    pic = new byte[resizedStream.Size];
                    await resizedStream.ReadAsync(pic.AsBuffer(), (uint)resizedStream.Size, InputStreamOptions.None);

                    ResizeImageFile(pic, targetSizeWidth, targetSizeHeight, sameRatio, action);
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("裁切缩放失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
            }
        }


        public static T RunSync<T>(Task<T> tast)
        {
            //http://www.cnblogs.com/bnbqian/p/4513192.html
            return Task.Run<Task<T>>(() => tast).Unwrap().GetAwaiter().GetResult();
        }

        //SoundEffect song = null;

        ///// <summary>
        ///// 播放歌曲
        ///// </summary>
        ///// <param name="url"></param>
        //public override void PlaySong(string res)
        //{
        //    try
        //    {
        //        if (song != null)
        //        {
        //            song.Dispose();
        //        }
        //        song = Session.Current.SoundContent.Load<SoundEffect>(res);
        //        song.Play(Convert.ToSingle(Setting.Current.MusicVolume) / 100, 0.0f, 0.0f);
        //    }
        //    catch (Exception ex)
        //    {
        //        //监控此
        //    }
        //}

    }

    public enum FileAction
    {
        Create,
        Read,
        Write
    }  

    public class PlatformTask
    {
        Task task;
        public bool IsStop = false;
        //Action act;
        public string[] ParamArray = null;
        public string[] ParamArrayResult = null;
        public byte[] ParamArrayResultBytes = null;
        public AsyncCallback OnStartFinish = null;
        public PlatformTask(Action action)
        {
            task = new Task(() =>
            {
                if (action != null)
                {
                    action.Invoke();
                }
                if (OnStartFinish != null)
                {
                    OnStartFinish.Invoke(null);
                }
            });
        }

        public bool IsAlive
        {
            get
            {
                return task != null && task.Status == TaskStatus.Running;
            }
        }

        public void Abort()
        {
            IsStop = true;
        }

        public void Start()
        {
            task.Start(); //.RunSynchronously();
            //Task.Run(act);
            //act.BeginInvoke(null, null);
        }
    }

    public class PlatformTask2
    {
        Task task;
        public PlatformTask2(Action action)
        {
            task = new Task(() =>
            {
                action.Invoke();
            });
        }
        public void Start()
        {
            task.RunSynchronously(); //.BeginInvoke(null, null);
        }
    }

    public static class AsyncInline
    {
        public static T RunSync<T>(Task<T> tast)
        {
            //http://www.cnblogs.com/bnbqian/p/4513192.html
            return Task.Run<Task<T>>(() => tast).Unwrap().GetAwaiter().GetResult();
        }

        public static void Run(Func<Task> item)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            synch.Post(async _ =>
            {
                try
                {
                    await item();
                }
                catch (Exception e)
                {
                    synch.InnerException = e;
                    throw;
                }
                finally
                {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
        }
        public static T Run<T>(Func<Task<T>> item)
        {
            var oldContext = SynchronizationContext.Current;
            var synch = new ExclusiveSynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synch);
            T ret = default(T);
            synch.Post(async _ =>
            {
                try
                {
                    ret = await
                    item();
                }
                catch (Exception e)
                {
                    synch.InnerException = e;
                    throw;
                }
                finally
                {
                    synch.EndMessageLoop();
                }
            }, null);
            synch.BeginMessageLoop();
            SynchronizationContext.SetSynchronizationContext(oldContext);
            return ret;
        }

        private class ExclusiveSynchronizationContext : SynchronizationContext
        {
            private bool done;
            public Exception InnerException { get; set; }
            readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
            readonly Queue<Tuple<SendOrPostCallback, object>> items =
             new Queue<Tuple<SendOrPostCallback, object>>();

            public override void Send(SendOrPostCallback d, object state)
            {
                throw new NotSupportedException("We cannot send to our same thread");
            }
            public override void Post(SendOrPostCallback d, object state)
            {
                lock (items)
                {
                    items.Enqueue(Tuple.Create(d, state));
                }
                workItemsWaiting.Set();
            }
            public void EndMessageLoop()
            {
                Post(_ => done = true, null);
            }
            public void BeginMessageLoop()
            {
                while (!done)
                {
                    Tuple<SendOrPostCallback, object> task = null;
                    lock (items)
                    {
                        if (items.Count > 0)
                        {
                            task = items.Dequeue();
                        }
                    }
                    if (task != null)
                    {
                        task.Item1(task.Item2);
                        if (InnerException != null) // the method threw an exeption
                        {
                            throw new AggregateException("AsyncInline.Run method threw an exception.",
                             InnerException);
                        }
                    }
                    else
                    {
                        workItemsWaiting.WaitOne();
                    }
                }
            }
            public override SynchronizationContext CreateCopy()
            {
                return this;
            }
        }
    }

    //    All I can say is we use this to successfully open the review page in the marketplace...
    //WebLink.OpenUrl(@"ms-windows-store:REVIEW?PFN=" + Package.Current.Id.FamilyName);

    //
    //... that makes me believe that the showing the marketplace code...
    //            var uri = new Uri(@"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
    //            Task.Run(async () => await Windows.System.Launcher.LaunchUriAsync(uri)).Wait(); 


    //... should be correct. Especially considering that the docs say it should work like this...

    //http://msdn.microsoft.com/en-us/library/windows/apps/hh974767.aspx

    //          var uri = new Uri(@"ms-windows-store:PDP?PFN=" + Package.Current.Id.FamilyName);
    //            Task.Run(async () => await Windows.System.Launcher.LaunchUriAsync(uri)).Wait(); 

}
