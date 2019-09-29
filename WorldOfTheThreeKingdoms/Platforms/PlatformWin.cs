using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO.Compression;
using Message = System.Windows.Forms.Message;
using NativeWindow = System.Windows.Forms.NativeWindow;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Management;
using System.Net.NetworkInformation;
using SharpCompress.Common;
using Microsoft.Xna.Framework.Input;
using Tools;
using WorldOfTheThreeKingdoms;
using GameManager;
using WorldOfTheThreeKingdoms.GameScreens;

namespace Platforms
{
    /// <summary>
    /// 各平台不同的實現
    /// </summary>
    public class Platform : PlatformBase
    {
        public static new PlatFormType PlatFormType = PlatFormType.Win;

        public static new bool IsMobilePlatForm = false;

        public new string PreferFullMode = "Window";

        public static new string PreferResolution = "1368*768";

        public new bool DebugMode = true;
        public new bool ProcessGameData = true;

        public new bool AssetsPng = false;

        public new bool QuickTest = true;

        public new bool DisplayMetroStart = false;

        public static WindowInputCapturer wic;

        /// <summary>
        /// 內存使用占用
        /// </summary>
        public new string MemoryUsage
        {
            get
            {
                return (System.GC.GetTotalMemory(false) / 1024).ToString();
                //Android.Activity1.os.Debug.getNativeHeapAllocatedSize()
                //return "";
            }
        }

        //static bool IsTrialOrigin = true;
        //public static bool? isTrial;
        //public static bool IsTrial
        //{
        //    get
        //    {
        //        //Guide.SimulateTrialMode = true;  //Guide.IsTrialMode
        //        if (isTrial == null)
        //        {
        //            isTrial = IsTrialOrigin && !IsMemberUser;
        //        }
        //        return (bool)isTrial;
        //    }
        //    set
        //    {
        //        isTrial = value;
        //    }
        //}

        //public static bool IsMemberUser
        //{
        //    get
        //    {
        //        return Session.GameUser != null && !String.IsNullOrEmpty(Session.GameUser.UserRole) && Session.GameUser.UserRole.Contains("SanguoWind");
        //    }
        //}

        public new string Location
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        public static bool IsActive
        {
            get
            {
                return MainGame != null ? MainGame.IsActive : false;
            }
        }

        public new bool WindowInputCapturerEnable
        {
            get
            {
                return WindowInputCapturer.Enable;
            }
            set
            {
                WindowInputCapturer.Enable = value;
            }
        }

        public new bool KeyBoardAvailable = true;

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
            GraphicsDeviceManager.PreferredBackBufferWidth = width;  //1024;
            GraphicsDeviceManager.PreferredBackBufferHeight = height; //680;
        }

        public static void GraphicsApplyChanges()
        {
            GraphicsDeviceManager.ApplyChanges();
        }

        public override void SetMouseVisible(bool visible)
        {
            MainGame.IsMouseVisible = visible;
        }

        public override void SetWindowAllowUserResizing(bool allow)
        {
            MainGame.Window.AllowUserResizing = true;

            Form xnaWindow = (Form)Control.FromHandle((MainGame.Window.Handle));

            xnaWindow.MouseWheel += (sender, e) =>
            {
                InputManager.PinchMove = Convert.ToSingle(e.Delta) / 480;
            };

            xnaWindow.WindowState = FormWindowState.Maximized;

            //Mouse.WindowHandle = xnaWindow.Handle; // Game1.Window.Handle;  // Season.m_RenderControl.FindForm().Handle
        }

        //public override void SetWindowBorder(bool visible)
        //{
        //    Form xnaWindow = (Form)Control.FromHandle((MainGame.Window.Handle));
        //    xnaWindow.FormBorderStyle = visible ? FormBorderStyle.Fixed3D : FormBorderStyle.None;
        //}

        public override Vector2 GetWorkingArea()
        {
            Form xnaWindow = (Form)Control.FromHandle((MainGame.Window.Handle));
            //xnaWindow.ClientRectangle
            var ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(xnaWindow);
            int mywidth = ScreenArea.Width; //屏幕宽度 
            int myheight = ScreenArea.Height; //屏幕高度
            return new Vector2(mywidth, myheight);
        }

        public override void SetFullScreen(bool full)
        {
            GraphicsDeviceManager.IsFullScreen = full;
        }

        public override void SetFullScreen2(bool full)
        {
            Form xnaWindow = (Form)Control.FromHandle((MainGame.Window.Handle));

            //xnaWindow.MaximizeBox = false;
            //xnaWindow.MinimizeBox = false;

            //xnaWindow.WindowState = FormWindowState.Maximized;

            //修改后的全屏代码
            //if (full)
            //{
            WinHelper.RestoreFullScreen(xnaWindow.Handle);//传入窗体句柄            

            //this.IsFullScreen = false;
            //}
            //else
            //{
            //    WinHelper.FullScreen(xnaWindow.Handle);
            //    //this.IsFullScreen = true;
            //}
        }

        /// <summary>
        /// 解決方案文件夾
        /// </summary>
        public new string SolutionDir
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { "WorldOfTheThreeKingdoms" }, StringSplitOptions.None)[0];  //.Replace(@"WorldOfTheThreeKingdoms\bin\Win\", "");                
            }
        }

        public override string GetDeviceID()
        {
            try
            {
                string addr = "";
                var sts = GetMacByNetworkInterface();
                if (sts != null && sts.Count > 0)
                {
                    addr = sts[0];
                }
                return addr;
            }
            catch
            {
                return "";
            }
        }

        //返回描述本地计算机上的网络接口的对象(网络接口也称为网络适配器)。
        public static NetworkInterface[] NetCardInfo()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        ///<summary>
        /// 通过NetworkInterface读取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static List<string> GetMacByNetworkInterface()
        {
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macs;
        }

        public override string GetDeviceInfo()
        {
            string hostName = "";
            try
            {
                hostName = GraphicsDevice.Adapter.Description.ToString() + " " + System.Net.Dns.GetHostName();
            }
            catch
            {

            }

            return hostName;  // + " " + Session.Resolution;
        }

        public override string GetSystemInfo()
        {
            return System.Environment.OSVersion.Platform + " " + System.Environment.OSVersion.VersionString;
        }

        /// <summary>
        /// 應用程序目錄
        /// </summary>
        public new string ApplicationUrl
        {
            get
            {
                return Application.StartupPath + "\\";
            }
        }
        /// <summary>
        /// 遊戲完整路徑
        /// </summary>
        public new string GameApplicationUrl
        {
            get
            {
                return ApplicationUrl + "WorldOfTheThreeKingdoms.exe";
            }
        }
        public bool editing = false;
        #region 加載資源文件
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string LoadText(string res)
        {
            res = res.Replace("\\", "/");
            if (!editing)
            {
                res = base.GetMODFile(res);
            }


            lock (Platform.IoLock)
            {
                return File.ReadAllText(res);
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
                return File.ReadAllLines(res);
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
        }

        #region 處理文件夾事宜


        public override string[] GetDirectories(string dir, bool all, bool full)
        {
            if (Directory.Exists(dir))
            {
                return Directory.GetDirectories(dir, "*.*", all ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            else
            {
                return null;
            }
        }

        public override string[] GetDirectoriesBasic(string dir, bool all, bool full)
        {
            return GetDirectories(dir, all, true);
        }

        public override string[] GetFiles(string dir, bool all = false)
        {
            if (DirectoryExists(dir))
            {
                return Directory.GetFiles(dir, "*.*", all ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);
            }
            else
            {
                return null;
            }
        }

        public override string[] GetFilesBasic(string dir, bool all = false)
        {
            return GetFiles(dir, false);
        }

        public override string ReadAllText(string file)
        {
            return File.ReadAllText(file);
        }

        public override string[] ReadAllLines(string file)
        {
            return File.ReadAllLines(file);
        }

        public override byte[] ReadAllBytes(string file)
        {
            return File.ReadAllBytes(file);
        }

        public override void WriteAllText(string file, string xml1)
        {
            File.WriteAllText(file, xml1, Encoding.UTF8);
        }

        public override void WriteAllBytes(string file, byte[] bytes1)
        {
            File.WriteAllBytes(file, bytes1);
        }

        public override Stream FileOpenWrite(string file, bool write)  //FileStream
        {
            if (write)
            {
                return File.OpenWrite(file) as Stream;
            }
            else
            {
                return File.OpenRead(file) as Stream;
            }
        }

        public override bool FileExists(string file)
        {
            return File.Exists(file);
        }

        public override void FileDelete(string file)
        {
            if (FileExists(file))
            {
                File.Delete(file);
            }
        }

        public override bool DirectoryExists(string dir)
        {
            return Directory.Exists(dir);
        }

        public override void DirectoryCreateDirectory(string dir)
        {
            Directory.CreateDirectory(dir);
        }

        public override string DirectoryName(string dir)
        {
            if (String.IsNullOrEmpty(dir))
            {
                return "";
            }
            else
            {
                return Path.GetDirectoryName(dir);
            }
        }

        public override string GetFileNameFromPath(string file)
        {
            return Path.GetFileName(file);
        }

        #endregion

        public Texture2D LoadTextureFromStream(Stream stream)
        {
            return Texture2D.FromStream(GraphicsDevice, stream);
        }

        public Texture2D LoadTexture(string res)
        {
            return LoadTexture(res, false);
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

                lock (Platform.IoLock)
                {

                    using (var stream = isUser ? LoadUserFileStream(res) : TitleContainer.OpenStream(res))
                    {
                        Texture2D tex = Texture2D.FromStream(Platform.GraphicsDevice, stream);
                        //貌似在Win7下，這個算法出錯，只能預先處理好材質了再載入了
                        //if (MainMenuScreen.Current.btnTextureAlpha.Selected &&
                        //    tex != null && Path.GetExtension(res).ToLower() == ".png" && !res.Contains("Cloud"))
                        //{
                        //    try
                        //    {
                        //        GameTools.PreMultiplyAlphas(tex);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        //WebTools.TakeWarnMsg("处理透明层级失败:" + res, "PreMultiplyAlphas:" + UserApplicationDataPath + res, ex);
                        //    }
                        //}
                        return tex;
                    }
                }
            }
            catch (Exception ex)
            {
                if (res.Contains("yueluo_1.0"))
                {

                }
                else
                {
                    WebTools.TakeWarnMsg("加载游戏材质失败:" + res, "LoadTexture:" + UserApplicationDataPath + res, ex);
                }
                return null;
            }
        }
        #endregion

        #region 處理用戶文件

        private new string UserApplicationDataPath
        {
            get
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WorldOfTheThreeKingdoms\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return path;
            }
        }

        public bool UserDirectoryExist(string path)
        {
            try
            {
                return DirectoryExists(UserApplicationDataPath + path);
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
                DirectoryCreateDirectory(UserApplicationDataPath + path);
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
        public string[] GetUserFileNames(string dir, string searchPattern)
        {
            try
            {
                lock (Platform.IoLock)
                {
                    var files = Directory.GetFiles(UserApplicationDataPath + dir, searchPattern);
                    if (files != null)
                    {
                        files = files.Select(fi => Path.GetFileName(fi)).ToArray();
                    }
                    return files;
                }
            }
            catch (Exception ex)
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
                lock (Platform.IoLock)
                {
                    if (File.Exists(UserApplicationDataPath + res))
                    {
                        using (var dest = new MemoryStream())
                        {
                            using (Stream stream = File.Open(UserApplicationDataPath + res, FileMode.Open))
                            {
                                using (var streamReader = new StreamReader(stream))
                                {
                                    return streamReader.ReadToEnd();
                                }
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
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
                lock (Platform.IoLock)
                {
                    if (File.Exists(UserApplicationDataPath + res))
                    {
                        using (var dest = new MemoryStream())
                        {
                            using (Stream stream = File.Open(UserApplicationDataPath + res, FileMode.Open))
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
                        }
                    }
                    else
                    {
                        return null;
                    }
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
                lock (Platform.IoLock)
                {
                    if (File.Exists(UserApplicationDataPath + res))
                    {
                        using (var dest = new MemoryStream())
                        {
                            using (Stream stream = File.Open(UserApplicationDataPath + res, FileMode.Open))
                            {
                                stream.CopyTo(dest);
                                return dest.ToArray();
                            }
                        }
                    }
                    else
                    {
                        return null;
                    }
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
            lock (Platform.IoLock)
            {
                if (write || File.Exists(UserApplicationDataPath + res))
                {
                    return File.Open(UserApplicationDataPath + res, write ? FileMode.OpenOrCreate : FileMode.Open);
                }
                else
                {
                    return null;
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
                using (var stream = Current.LoadUserFileStream(res))
                {
                    if (stream == null)
                    {
                        return null;
                    }
                    Texture2D tex = Texture2D.FromStream(Platform.GraphicsDevice, stream);
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
                List<bool> results = new List<bool>();
                foreach (string re in res)
                {
                    bool exis = false;
                    if (!String.IsNullOrEmpty(re.Trim()))
                    {
                        try
                        {
                            lock (Platform.IoLock)
                            {
                                exis = File.Exists(UserApplicationDataPath + re.Trim());
                            }
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
                    String path = res;
                    if (!fullPathProvided)
                    {
                        path = UserApplicationDataPath + res;
                    }
                    File.WriteAllText(path, content);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else
                WebTools.TakeWarnMsg("保存用户文本失败:" + res, "SaveUserFile:" + UserApplicationDataPath + res, ex);
#endif
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
        //    SaveUserFile("settings.config", String.Join("\r\n", strings));
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
                //File.WriteAllBytes(UserApplicationDataPath + res, bytes);
                lock (Platform.IoLock)
                {
                    using (var isolatedFileStream = File.OpenWrite(UserApplicationDataPath + res))
                    {
                        using (var fileWriter = new BinaryWriter(isolatedFileStream))
                        {
                            fileWriter.Write(bytes);
                        }
                    }
                }
                if (action != null)
                {
                    action.Start();
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("保存用户文件失败:" + res, "SaveUserFile:bytes" + bytes.Length + UserApplicationDataPath + res, ex);
            }
        }
        
        /// <summary>
        /// 刪除用戶文件
        /// </summary>
        /// <param name="files"></param>
        public void DelUserFiles(string[] files, PlatformTask action)
        {
            foreach (string file in files)
            {
                if (UserFileExist(new string[] { file.NullToString().Trim() })[0])
                {
                    try
                    {
                        lock (Platform.IoLock)
                        {
                            File.Delete(UserApplicationDataPath + file.Trim());
                        }
                    }
                    catch (Exception ex)
                    {
                        WebTools.TakeWarnMsg("删除用户文件失败:" + file, "File.Delete:" + UserApplicationDataPath + file, ex);
                    }
                }
                if (action != null)
                {
                    action.Start();
                }
            }
        }
        /// <summary>
        /// 獲取獨立存取文件
        /// </summary>
        /// <returns></returns>
        private IsolatedStorageFile GetIsolatedStorageFile()
        {
            return IsolatedStorageFile.GetUserStoreForApplication();
        }
#endregion

        public static void Sleep(int time)
        {
            Thread.Sleep(time);
        }

        public override void OpenMarket(string key)
        {
            
        }

        public override void OpenReview(string key)
        {
            
        }

        public override byte[] ScreenShot(GraphicsDevice graphicsDevice, RenderTarget2D screenshot)
        {
            graphicsDevice.SetRenderTarget(null);
            byte[] shot = null;
            using (MemoryStream ms = new MemoryStream())
            {
                screenshot.SaveAsJpeg(ms, screenshot.Width, screenshot.Height);
                screenshot.Dispose();
                shot = ms.ToArray(); //.GetBuffer();                
            }
            //screenshot = new RenderTarget2D(graphicsDevice, 800, 480, false, SurfaceFormat.Color, DepthFormat.None);
            //screenshot = new RenderTarget2D(Season.GraphicsDevice, Season.GraphicsDevice.Viewport.Width, Season.GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None);
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

            //byte[] sendData = Season.Current.ReadAllBytes(@"C:\Projects\a.mp4");

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
            if (!OpenWeb)
            {
                return "";
            }
            else
            {
                var client = new WebClient();
                string result = null;
                byte[] response = client.DownloadData(new Uri(strURL));
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
            }
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
            byte[] result = client.DownloadData(new Uri(file));
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
                //"IExplore.exe " + 
                ProcessStartInfo startInfo = new ProcessStartInfo(link);
                //startInfo.WindowStyle = ProcessWindowStyle.Minimized;
                startInfo.UseShellExecute = true;
                Process.Start(startInfo);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //SeasonTools.SendErrMsg("ProcessStartInfo打開Web出錯：", ex);
                //try
                //{
                //    Type tIE; object oIE;
                //    object[] oParameter = new object[1];
                //    tIE = Type.GetTypeFromProgID("InternetExplorer.Application");
                //    oIE = Activator.CreateInstance(tIE);
                //    oParameter[0] = (bool)true;
                //    tIE.InvokeMember("Visible", BindingFlags.SetProperty, null, oIE, oParameter);
                //    oParameter[0] = link;
                //    tIE.InvokeMember("Navigate2", BindingFlags.InvokeMethod, null, oIE, oParameter);
                //}
                //catch (Exception ex2)
                //{
                //    SeasonTools.SendErrMsg("InvokeMember打開Web出錯：", ex2);
                //}
            }
        }

        public override void InitInputCapturer()
        {
            wic = new WindowInputCapturer(MainGame.Window.Handle);
        }

        public override List<Character> GetChars()
        {
            return WindowInputCapturer.myCharacters;
        }

        public override void ClearChars()
        {
            WindowInputCapturer.myCharacters.Clear();
        }

        public static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            //Session.Current.err = e.ToString();
            //if (Season.PlatForm == PlatForm.Win)
            //{
            //    Season.Current.OpenLink(WebTools.WebSite2 + "/service.aspx?mes=" + e.ToString() + "&platform=" + Season.PlatForm.ToString());
            //}
            //SeasonTools.SendErrMsg("RuntimeTerminating: " + args.IsTerminating, e);
        }

        public override void Exit()
        {
            try
            {
                MainGame.Exit();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //退出失敗，當不要緊
            }
        }

        public static string picStatus = "";

        static float[] rotates = new float[] { 0f, (float)Math.PI / 2.0f, (float)Math.PI, (float)Math.PI * 3.0f / 2.0f };

        public override void ChoosePicture(PlatformTask action)
        {
            picStatus = "ChoosePicture";

            //初始化一个OpenFileDialog类
            OpenFileDialog fileDialog = new OpenFileDialog();

            //如果我们要为弹出的选择框中过滤文件类型，可以设置OpenFileDialog的Filter属性。比如我们只允许用户选择.xls文件，可以作如下设置：
            //fileDialog.Filter = "*.jpeg|*.jpg|*.png|*.gif|*.bmp";
            fileDialog.Filter = "JPG格式（*.jpg）|*.jpg|PNG格式（*.png）|*.png|GIF格式（*.gif）|*.gif|BMP格式（*.bmp）|*.bmp";

            //判断用户是否正确的选择了文件
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                //获取用户选择文件的后缀名
                string extension = Path.GetExtension(fileDialog.FileName);
                //声明允许的后缀名
                string[] str = new string[] { ".jpeg", ".jpg", ".png", ".gif", ".bmp" };
                if (!str.Contains(extension.ToLower()))
                {
                    System.Windows.Forms.MessageBox.Show("仅能上传jpg,png,gif,bmp格式的图片！");
                }
                else
                {
                    //获取用户选择的文件，并判断文件大小不能超过5000K，fileInfo.Length是以字节为单位的
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    if (fileInfo.Length > 5000 * 1024)
                    {
                        System.Windows.Forms.MessageBox.Show("上传的图片不能大于5000K");
                    }
                    else
                    {
                        byte[] bytes = null;

                        lock (Platform.IoLock)
                        {
                            bytes = File.ReadAllBytes(fileDialog.FileName);
                        }

                        var task = new PlatformTask(() => { });
                        task.OnStartFinish += new AsyncCallback((result) =>
                        {
                            if (action != null)
                            {
                                string avatar = "";
                                if (action.ParamArray != null && action.ParamArray.Length > 0)
                                {
                                    avatar = action.ParamArray[0];
                                }
                                action.ParamArrayResult = new string[] { avatar + extension };
                                action.ParamArrayResultBytes = task.ParamArrayResultBytes;
                                action.Start();
                            }
                        });

                        ResizeImageFile(bytes, 540 - 6, 540 - 6, true, task); //270 - 6);  //, 76 * 2, 91 * 2);

                        //if (!String.IsNullOrEmpty(avatar))
                        //{
                        //    Season.Current.SaveUserFileStatic(avatar, photoBytes);
                        //    if (action != null)
                        //    {
                        //        action.Start();
                        //    }
                        //}
                        //if (Session.GameUser != null)
                        //{
                        //    Session.GameUser.Avatar = avatar;
                        //    if (action != null)
                        //    {
                        //        action.Start();
                        //    }
                        //}
                        //else
                        //{
                        //    throw new Exception("Session.GameUser is Null!");
                        //}

                        //MemoryStream stream = new MemoryStream(photoBytes);
                        //ChooseTexture = Texture2D.FromStream(GraphicsDevice, stream);
                        //在这里就可以写获取到正确文件后的代码了
                    }
                }
            }
        }

        /// <summary>
        /// 計算分辨率
        /// </summary>
        /// <param name="oldSize"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public static Size CalculateDimensions(Size oldSize, int targetSizeWidth, int targetSizeHeight)
        {
            Size newSize = new Size();
            if (targetSizeWidth == 0 && targetSizeHeight == 0)
            {
                newSize = oldSize;
            }
            else if (targetSizeWidth != 0 && targetSizeHeight != 0)
            {
                newSize.Width = targetSizeWidth;
                newSize.Height = targetSizeHeight;
            }
            else if (targetSizeWidth == 0)
            {
                newSize.Width = (int)(oldSize.Width * ((float)targetSizeHeight / (float)oldSize.Height));
                newSize.Height = targetSizeHeight;
            }
            else if (targetSizeHeight == 0)
            {
                newSize.Width = targetSizeWidth;
                newSize.Height = (int)(oldSize.Height * ((float)targetSizeWidth / (float)oldSize.Width));
            }
            return newSize;
        }

        static byte[] SavePngFromBitmap(Bitmap bitmap)
        {
            var imageStream = new MemoryStream();
            using (imageStream)
            {
                // Save bitmap in some format.
                bitmap.Save(imageStream, ImageFormat.Png);
                imageStream.Position = 0;

                // Do something with the memory stream. For example:
                byte[] imageBytes = imageStream.ToArray();
                // Save bytes to the database.
                return imageBytes;
            }
        }

        public override void MirrorPicture(byte[] image, PlatformTask action)
        {
            RotateFlipType flip = RotateFlipType.RotateNoneFlipX;
            using (MemoryStream ms = new MemoryStream(image))
            {
                Image img = Image.FromStream(ms);

                var bmp = new Bitmap(img);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(System.Drawing.Color.White);
                    gfx.DrawImage(img, 0, 0, img.Width, img.Height);
                }

                bmp.RotateFlip(flip);

                var bytes = SavePngFromBitmap(bmp);

                if (action != null)
                {
                    action.ParamArrayResultBytes = bytes;
                    action.Start();
                }
            }
        }

        public override void RotatePicture(byte[] image, int rotate, PlatformTask action)
        {
            RotateFlipType flip = RotateFlipType.RotateNoneFlipNone;
            if (rotate == 0)
            {

            }
            else if (rotate == 1)
            {
                flip = RotateFlipType.Rotate90FlipNone;
            }
            else if (rotate == 2)
            {
                flip = RotateFlipType.Rotate180FlipNone;
            }
            else if (rotate == 3)
            {
                flip = RotateFlipType.Rotate270FlipNone;
            }
            using (MemoryStream ms = new MemoryStream(image))
            {
                Image img = Image.FromStream(ms);

                var bmp = new Bitmap(img);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.Clear(System.Drawing.Color.White);
                    gfx.DrawImage(img, 0, 0, img.Width, img.Height);
                }

                bmp.RotateFlip(flip);

                ////create an empty Bitmap image
                //Bitmap bmp = new Bitmap(img.Width, img.Height);

                ////turn the Bitmap into a Graphics object
                //Graphics gfx = Graphics.FromImage(bmp);

                ////now we set the rotation point to the center of our image
                //gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

                ////now rotate the image
                //gfx.RotateTransform(rotationAngle);

                //gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

                ////set the InterpolationMode to HighQualityBicubic so to ensure a high
                ////quality image once it is transformed to the specified size
                //gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

                ////now draw our new image onto the graphics object
                //gfx.DrawImage(img, new PointF(0, 0));

                //dispose of our Graphics object
                //gfx.Dispose();

                var bytes = SavePngFromBitmap(bmp);
                if (action != null)
                {
                    action.ParamArrayResultBytes = bytes;
                    action.Start();
                }
            }
        }

        public override void CropPicture(byte[] image, int x, int y, int width, int height, PlatformTask action)
        {
            // Get your texture
            //Texture2D texture = Content.Load<Texture2D>("myTexture");

            // Calculate the cropped boundary
            Microsoft.Xna.Framework.Rectangle newBounds = new Microsoft.Xna.Framework.Rectangle(x, y, width, height); // avatar.Bounds;
            //newBounds.X -= Convert.ToInt32(posExts.X);
            //newBounds.Y -= Convert.ToInt32(posExts.Y);
            //const int resizeBy = 20;
            //newBounds.X += resizeBy;
            //newBounds.Y += resizeBy;
            //newBounds.Width -= resizeBy * 2;
            //newBounds.Height -= resizeBy * 2;

            // Create a new texture of the desired size
            var croppedPicture = new Texture2D(Platform.GraphicsDevice, newBounds.Width, newBounds.Height);

            // Copy the data from the cropped region into a buffer, then into the new texture
            var data = new Microsoft.Xna.Framework.Color[newBounds.Width * newBounds.Height];

            using (var memo = new MemoryStream(image))
            {
                var tex = Texture2D.FromStream(GraphicsDevice, memo);
                tex.GetData(0, newBounds, data, 0, newBounds.Width * newBounds.Height);
                croppedPicture.SetData(data);
                var memo2 = new MemoryStream();
                croppedPicture.SaveAsPng(memo2, width, height);
                var bytes = memo2.ToArray();
                if (action != null)
                {
                    action.ParamArrayResultBytes = bytes;
                    action.Start();
                }
                //Color[] colors = new Color[] { Color.White };
                //texture = new Texture2D(GraphicsDevice, 1, 1);
                //texture.SetData<Color>(colors);
            }

        }

        /// <summary>
        /// 壓縮圖片
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        public override void ResizeImageFile(byte[] imageFile, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            byte[] pic = null;

            try
            {
                using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
                {
                    Size newSize;

                    if (sameRatio)
                    {
                        float scale = GameTools.AutoSetScale(oldImage.Width, oldImage.Height, targetSizeWidth, targetSizeHeight);
                        newSize = new Size(Convert.ToInt32(oldImage.Width * scale), Convert.ToInt32(oldImage.Height * scale));
                    }
                    else
                    {
                        newSize = new Size(Convert.ToInt32(targetSizeWidth), Convert.ToInt32(targetSizeHeight));
                    }

                    using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
                    {
                        using (Graphics canvas = Graphics.FromImage(newImage))
                        {
                            canvas.SmoothingMode = SmoothingMode.AntiAlias;
                            canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            canvas.DrawImage(oldImage, new RectangleF(new PointF(0, 0), newSize));
                            MemoryStream m = new MemoryStream();
                            newImage.Save(m, ImageFormat.Jpeg);
                            pic = m.GetBuffer();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("缩放图像失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
            }

            if (action != null)
            {
                action.ParamArrayResultBytes = pic;
                action.Start();
            }

        }

        public override void CropResizePicture(byte[] image, int x, int y, int width, int height, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            byte[] pic = null;
            try
            {
                Microsoft.Xna.Framework.Rectangle newBounds = new Microsoft.Xna.Framework.Rectangle(x, y, width, height);

                var croppedPicture = new Texture2D(Platform.GraphicsDevice, newBounds.Width, newBounds.Height);

                var data = new Microsoft.Xna.Framework.Color[newBounds.Width * newBounds.Height];

                using (var memo = new MemoryStream(image))
                {
                    var tex = Texture2D.FromStream(GraphicsDevice, memo);
                    tex.GetData(0, newBounds, data, 0, newBounds.Width * newBounds.Height);
                    croppedPicture.SetData(data);
                    var memo2 = new MemoryStream();
                    croppedPicture.SaveAsPng(memo2, width, height);
                    pic = memo2.ToArray();
                }

                ResizeImageFile(pic, targetSizeWidth, targetSizeHeight, sameRatio, action);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("裁切缩放失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
            }
        }

    }

    public class PlatformTask
    {
        Action act;
        public bool IsStop = false;
        public string[] ParamArray = null;
        public string[] ParamArrayResult = null;
        public byte[] ParamArrayResultBytes = null;
        public AsyncCallback OnStartFinish = null;
        public PlatformTask(Action action)
        {
            act = action;
        }
        public bool IsAlive
        {
            get
            {
                return act != null;
            }
        }

        public void Abort()
        {
            IsStop = true;
        }
        public void Start()
        {
            act.BeginInvoke(OnStartFinish, null);
        }
    }

    public class PlatformTask2
    {
        Thread thread;
        public PlatformTask2(Action action)
        {
            thread = new Thread(() => { action.Invoke(); });
        }
        public void Start()
        {
            thread.Start();
        }
    }

    public sealed class IMM
    {
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("imm32.dll", CharSet = CharSet.Auto)]
        public extern static IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

    }

    public static class WindowMessage
    {
        public const int ImeSetContext = 0x0281;
        public const int InputLanguageChange = 0x0051;
    }

    public sealed class WindowInputCapturer : NativeWindow, IDisposable
    {
        //自定义字符串输出类
        //由于大部分输入法都支持输出词组，
        //所以这里使用List装载，不然每次只能获取一个字符
        //--by fhmsha
        public static List<Character> myCharacters = new List<Character>();

        public static bool Enable = false;

        private const int DLGC_WANTCHARS = 0x0080;

        private const int DLGC_WANTALLKEYS = 0x0004;

        private enum WindowMessages : int
        {
            WM_GETDLGCODE = 0x0087,
            WM_CHAR = 0x0102,
        }

        public WindowInputCapturer(IntPtr windowHandle)
        {
            AssignHandle(windowHandle);
        }

        public void Dispose()
        {
            if (!this.disposed)
            {
                ReleaseHandle();
                this.disposed = true;
            }
        }
        IntPtr context = IntPtr.Zero;

        protected override void WndProc(ref Message message)
        {
            //clayman
            if (message.Msg == WindowMessage.InputLanguageChange)
            {
                return; //Don't pass this message to base class!!!!
            }
            if (message.Msg == WindowMessage.ImeSetContext)
            {
                if (message.WParam.ToInt32() == 1)
                {
                    IntPtr imeContext = IMM.ImmGetContext(this.Handle);
                    if (context == IntPtr.Zero)
                        context = imeContext;
                    IMM.ImmAssociateContext(this.Handle, context);
                }
            }
            base.WndProc(ref message);

            if (Enable)
            {
                switch (message.Msg)
                {
                    case (int)WindowMessages.WM_GETDLGCODE:
                        {
                            if (Is32Bit)
                            {
                                int returnCode = message.Result.ToInt32();
                                returnCode |= (DLGC_WANTALLKEYS | DLGC_WANTCHARS);
                                message.Result = new IntPtr(returnCode);
                            }
                            else
                            {
                                long returnCode = message.Result.ToInt64();
                                returnCode |= (DLGC_WANTALLKEYS | DLGC_WANTCHARS);
                                message.Result = new IntPtr(returnCode);
                            }

                            break;
                        }
                    case (int)WindowMessages.WM_CHAR:
                        {
                            int charInt = message.WParam.ToInt32();
                            Character myCharacter = new Character();
                            myCharacter.IsUsed = false;
                            myCharacter.Chars = (char)charInt;
                            //汉字的unicode编码范围是4e00-9fa5（19968至40869）
                            //全/半角标点可以查看charInt输出
                            switch (charInt)
                            {
                                case 8:
                                    myCharacter.CharaterType = characterType.BackSpace;
                                    break;
                                case 9:
                                    myCharacter.CharaterType = characterType.Tab;
                                    break;
                                case 13:
                                    myCharacter.CharaterType = characterType.Enter;
                                    break;
                                case 27:
                                    myCharacter.CharaterType = characterType.Esc;
                                    break;
                                default:
                                    myCharacter.CharaterType = characterType.Char;
                                    break;
                            }
                            myCharacters.Add(myCharacter);
                            break;
                        }
                }
            }
        }

        private static bool Is32Bit
        {
            get { return (IntPtr.Size == 4); }
        }
        private bool disposed;

    }
}
