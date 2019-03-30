using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Text;
using Tools;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Threading;
using GameManager;
using System.IO.IsolatedStorage;
using Foundation;
using UIKit;
using System.Net;
using System.Xml;
using System.Diagnostics;
//using Microsoft.Xna.Framework.GamerServices;
using StoreKit;
//using System.Drawing;
using CoreGraphics;
using SharpCompress.Writer;
using SharpCompress.Common;
using SharpCompress.Reader;
using System.Drawing;
using AVFoundation;

//using Microsoft.Xna.Framework.GamerServices;
//using Microsoft.Xna.Framework.Storage;

namespace Platforms
{
    /// <summary>
    /// 各平台不同的實現
    /// </summary>
	public class Platform : PlatformBase
    {
		public static new PlatFormType PlatFormType = PlatFormType.iOS;

        public static new bool IsMobilePlatForm = true;

		public new bool DisplayMetroStart = false;

        public static AVAudioPlayer player = null;

//		static bool IsTrialOrigin = false;
//		public static bool? isTrial;
//		public static bool IsTrial
//		{
//			get
//			{
//				//LicenseInformation _licenseInformation = new LicenseInformation();
//				//bool _isTrial = _licenseInformation.IsTrial();
//				//Guide.IsTrialMode
//				if (isTrial == null)
//				{
//					//Guide.SimulateTrialMode = true;  Guide.IsTrialMode
//					isTrial = IsTrialOrigin && true && (Session.GameUser == null || String.IsNullOrEmpty(Session.GameUser.UserRole) || !Session.GameUser.UserRole.Contains("SanguoWind"));
//				}
//				return (bool)isTrial; 
//			}
//			set
//			{
//				isTrial = value;
//			}
//		}
//		public static bool IsMemberUser
//		{
//			get
//			{
//				return true;
//				//return Session.GameUser != null && !String.IsNullOrEmpty(Session.GameUser.UserRole) && Session.GameUser.UserRole.Contains("SanguoWind");
//			}
//		}

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
                return false;   //Guide.IsVisible;
			}
		}

		public new bool KeyBoardAvailable = false;


		public static bool IsPhone {
			get {
				return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone;
			}
		}

        public static new string PreferResolution = "925*520";

		public static string ResolutionInfo
		{
			get {
				nfloat scale = UIScreen.MainScreen.Scale;
				return "scale:" + scale + " Bounds.Width:" + UIScreen.MainScreen.Bounds.Width + " Bounds.Height:" + UIScreen.MainScreen.Bounds.Height;
			}
		}

		public override void SetTimerDisabled(bool timerDisabled)
		{
			UIApplication.SharedApplication.IdleTimerDisabled = timerDisabled;
		}
			
		public new string PreferFullMode = "Full";

        public new string CurrentLanguage
        {
            get
            {
                return NSLocale.PreferredLanguages[0];
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

        public override void PreparePhone()
        {
            nfloat scale = UIScreen.MainScreen.Scale;
            //return (UIScreen.MainScreen.Bounds.Height * scale).ToString() + "*" + (UIScreen.MainScreen.Bounds.Width * scale).ToString();
            nfloat width = UIScreen.MainScreen.Bounds.Width;
            nfloat height = UIScreen.MainScreen.Bounds.Height;
            if (width > height)
            {
                Session.MainGame.fullScreenDestination = new Microsoft.Xna.Framework.Rectangle(0, 0, Convert.ToInt32(width * scale), Convert.ToInt32(height * scale));
            }
            else
            {
                Session.MainGame.fullScreenDestination = new Microsoft.Xna.Framework.Rectangle(0, 0, Convert.ToInt32(height * scale), Convert.ToInt32(width * scale));
            }
        }

		public override void SetFullScreen(bool full)
		{
			//SystemTray.IsVisible = !full;
			//Season.SeasonGame.graphics.IsFullScreen = true;
		}

		public override void SetOrientations()
		{
            //MainGame.GraphicsDevice.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
		}

        public override string GetDeviceID()
        {
            return GetDeviceInfo().Replace(" ", ""); // base.GetDeviceID();
        }

        public override string GetDeviceInfo()
        {
            return UIDevice.CurrentDevice.Model + " " + UIDevice.CurrentDevice.SystemName + " " + UIDevice.CurrentDevice.SystemVersion + " " + UIDevice.CurrentDevice.Name;
        }

        public override string GetSystemInfo()
        {
            return System.Environment.OSVersion.Platform + " " + System.Environment.OSVersion.VersionString;
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
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string LoadText(string res)
        {
            res = res.Replace("\\", "/");
			string dir = NSBundle.MainBundle.ResourcePath;
            lock (Platform.IoLock)
            {
                return File.ReadAllText(dir + "/" + res);
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
            string dir = NSBundle.MainBundle.ResourcePath;
            lock (Platform.IoLock)
            {
                return File.ReadAllLines(dir + "/" + res);
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
            res = res.Replace("\\", "/");
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

                }

                lock (Platform.IoLock)
                {
                    using (var stream = isUser ? LoadUserFileStream(res) : TitleContainer.OpenStream(res))
                    {
                        //Texture tex = SharpDX.
                        Texture2D tex = Texture2D.FromStream(GraphicsDevice, stream);
                        //if (tex != null && Path.GetExtension(res).ToLower() == ".png")
                        //{
                        //    try
                        //    {
                        //        GameTools.PreMultiplyAlphas(tex);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        WebTools.TakeWarnMsg("处理透明层级失败:" + res, "PreMultiplyAlphas:" + UserApplicationDataPath + res, ex);
                        //    }
                        //}
                        return tex;
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("加载游戏材质失败:" + res, "LoadTexture:" + UserApplicationDataPath + res, ex);
                return new Texture2D(GraphicsDevice, 1, 1);
            }
        }
		public override string[] GetFiles(string dir)
		{
            if (dir.Contains("/"))
            {
                dir = dir.Substring(0, dir.LastIndexOf('/'));
            }
            //var resources = NSBundle.GetPathsForResources("", dir).NullToEmptyList().Select(re =>
            //    "Content" + re.Split(new string[] { "Content" }, StringSplitOptions.None)[1]
            //);
            var resources = NSBundle.GetPathsForResources("", dir).NullToEmptyList().Select(re =>
                re.Substring(re.LastIndexOf('/') + 1)
            );
            return resources.NullToEmptyArray();
		}

        public string GetFileFullPath(string file)
        {
            file = file.Replace("\\", "/");

            var dir = file.Substring(0, file.LastIndexOf('/'));

            return NSBundle.GetPathsForResources("", dir).NullToEmptyList().FirstOrDefault(fi => fi.Contains(file));
        }

        public override bool FileExists(string file)
        {
            var list = GetFiles(file);

            if (list != null && list.Length > 0)
            {
                if (file.Contains("/"))
                {
                    file = file.Substring(file.LastIndexOf('/') + 1);
                }
                return list.Contains(file);
            }
            return false;
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
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
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
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
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
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
                    if (storage.FileExists(res))
                    {
                        byte[] bytes;
                        using (var isolatedFileStream = storage.OpenFile(res, FileMode.Open))
                        {
                            using (var binaryReader = new BinaryReader(isolatedFileStream))
                            {
                                bytes = binaryReader.ReadBytes(Convert.ToInt32(isolatedFileStream.Length));
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
                using (var stream = LoadUserFileStream(res))
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
                lock (Platform.IoLock)
                {
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
                    List<bool> results = new List<bool>();
                    foreach (string re in res)
                    {
                        bool exis = false;
                        if (!String.IsNullOrEmpty(re.Trim()))
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
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
                    string path = res;
                    using (var isolatedFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, storage))
                    {
                        using (var fileWriter = new StreamWriter(isolatedFileStream))
                        {
                            fileWriter.Write(content);
                            //fileWriter.Flush();
                        }
                    }
                }
            }
            catch (Exception ex)
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
                lock (Platform.IoLock)
                {
                    IsolatedStorageFile storage = GetIsolatedStorageFile();
                    string path = res;
                    using (var isolatedFileStream = new IsolatedStorageFileStream(path, FileMode.OpenOrCreate, storage))
                    {
                        using (var binaryWriter = new BinaryWriter(isolatedFileStream))
                        {
                            binaryWriter.Write(bytes);
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
            lock (Platform.IoLock)
            {
                IsolatedStorageFile storage = GetIsolatedStorageFile();
                foreach (string file in files)
                {
                    if (UserFileExist(new string[] { file.NullToString().Trim() })[0])
                    {
                        try
                        {
                            storage.DeleteFile(file.Trim());
                        }
                        catch (Exception ex)
                        {
                            WebTools.TakeWarnMsg("删除用户文件失败:" + file, "storage.Delete:" + UserApplicationDataPath + file, ex);
                        }
                    }
                    if (action != null)
                    {
                        action.Start();
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

        ///// <summary>
        ///// 壓縮圖片
        ///// </summary>
        ///// <param name="imageFile"></param>
        ///// <param name="targetSize"></param>
        ///// <returns></returns>
        //public static byte[] ResizeImageFile(byte[] imageFile, int targetSizeWidth, int targetSizeHeight)
        //{
        //    using (System.Drawing.Image oldImage = System.Drawing.Image.FromStream(new MemoryStream(imageFile)))
        //    {
        //        Size newSize = CalculateDimensions(oldImage.Size, targetSizeWidth, targetSizeHeight);
        //        using (Bitmap newImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb))
        //        {
        //            using (Graphics canvas = Graphics.FromImage(newImage))
        //            {
        //                canvas.SmoothingMode = SmoothingMode.AntiAlias;
        //                canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //                canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;
        //                canvas.DrawImage(oldImage, new RectangleF(new PointF(0, 0), newSize));
        //                MemoryStream m = new MemoryStream();
        //                newImage.Save(m, ImageFormat.Jpeg);
        //                return m.GetBuffer();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 計算分辨率
        /// </summary>
        /// <param name="oldSize"></param>
        /// <param name="targetSize"></param>
        /// <returns></returns>
        //public static System.Drawing.Size CalculateDimensions(Size oldSize, int targetSizeWidth, int targetSizeHeight)
        //{
        //    Size newSize = new Size();
        //    if (targetSizeWidth == 0 && targetSizeHeight == 0)
        //    {
        //        newSize = oldSize;
        //    }
        //    else if (targetSizeWidth != 0 && targetSizeHeight != 0)
        //    {
        //        newSize.Width = targetSizeWidth;
        //        newSize.Height = targetSizeHeight;
        //    }
        //    else if (targetSizeWidth == 0)
        //    {
        //        newSize.Width = (int)(oldSize.Width * ((float)targetSizeHeight / (float)oldSize.Height));
        //        newSize.Height = targetSizeHeight;
        //    }
        //    else if (targetSizeHeight == 0)
        //    {
        //        newSize.Width = targetSizeWidth;
        //        newSize.Height = (int)(oldSize.Height * ((float)targetSizeWidth / (float)oldSize.Width));
        //    }
        //    return newSize;
        //}

		public static void Sleep(int time)
        {
			Thread.Sleep (time); //待查
        }

		public override void OpenMarket(string key)
        {
			NSUrl url = null;
			url = new NSUrl("itms-apps://itunes.apple.com/app/san-guo-chun-qiu-chuan/id869927528?mt=8");
            UIApplication.SharedApplication.OpenUrl(url);
        }

		public override void OpenReview(string key)
        {
			OpenMarket(key);
        }

		public override byte[] ScreenShot(GraphicsDevice graphics, RenderTarget2D screenshot)
		{
			/*
			 byte[] shot = null;

			UIViewController controller = Game1.Services.GetService(typeof(UIViewController)) as UIViewController;

			UIGraphics.BeginImageContext(controller.View.Frame.Size);
			using (var context = UIGraphics.GetCurrentContext())
			{
				controller.View.Layer.RenderInContext(context);
				using (var image = UIGraphics.GetImageFromCurrentImageContext())
				{
					//shot = image.AsPNG().ToArray();
					shot = image.AsJPEG().AsStream().StreamToBytes();
					//Save the image to file here   
				}
			}
			UIGraphics.EndImageContext();*/


			graphics.SetRenderTarget(null);
			byte[] shot = null;
			using (MemoryStream ms = new MemoryStream())
			{
				screenshot.SaveAsPng(ms, screenshot.Width, screenshot.Height);
				screenshot.Dispose();
				shot = ms.ToArray(); //.GetBuffer();
				//screenshot = new RenderTarget2D(GraphicsDevice, 800, 480, false, SurfaceFormat.Color, DepthFormat.None);
				screenshot = new RenderTarget2D(Platform.GraphicsDevice, Platform.GraphicsDevice.Viewport.Width, Platform.GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None);
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
			//            var client = new WebClient();
			//            string result = null;
			//
			//            client.DownloadStringAsync(new Uri(strURL));
			//            client.DownloadStringCompleted += (sender, e) =>
			//                {
			//                    result = e.Result;
			//                    if (e.Error != null)
			//                    {
			//                        result = "加载出错……";
			//                    }
			//                    else
			//                    {
			//                        result = result.Split(new string[] { "\">", "</" }, StringSplitOptions.None)[1];
			//                    }
			//                };
			//            DateTime time = DateTime.Now;
			//            while (result == null)
			//            {
			//                TimeSpan ts = DateTime.Now - time;
			//                if (ts.TotalSeconds > 30)  //30
			//                {
			//                    result = "加载超时……";
			//                    break;
			//                }
			//            }

			//			byte[] response = client.DownloadData(new Uri(strURL));
			//			MemoryStream stream = new MemoryStream(response);
			//			XmlTextReader reader = new XmlTextReader(stream);
			//			reader.MoveToContent();
			//			result = reader.ReadInnerXml();
			//			reader.Close();
			//			stream.Close();
			//            string result = Encoding.ASCII.GetString(response);
			//			//return result;
			//            result = result.Replace("&lt;", "<").Replace("&gt;", ">");
			//            return result;

			//创建一个HTTP请求
			//var client = new WebClient();
			//client.DownloadStringCompleted += (s, ev) => 
			//{ 
			//responseTextBlock.Text = ev.Result; 
			//};
			//client.DownloadStringAsync(new Uri(strURL));

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(strURL);
			//request.Method="get";
			HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse();
			Stream s = response.GetResponseStream();
			//转化为XML，自己进行处理
			XmlTextReader Reader = new XmlTextReader(s);
			Reader.MoveToContent();
			string strValue = Reader.ReadInnerXml();
			strValue = strValue.Replace("&lt;", "<");
			strValue = strValue.Replace("&gt;", ">");
			//MessageBox.Show(strValue);
			Reader.Close();
			request.Abort();
			response.Close();
			return strValue;
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
				OpenLink2(link);
			}
			catch (Exception ex)
			{
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
				//Type tIE; object oIE;
				//object[] oParameter = new object[1];
				//tIE = Type.GetTypeFromProgID("InternetExplorer.Application");
				//oIE = Activator.CreateInstance(tIE);
				//oParameter[0] = (bool)true;
				//tIE.InvokeMember("Visible", BindingFlags.SetProperty, null, oIE, oParameter);
				//oParameter[0] = link;  //(string)"http://www.linyuanle.com";
				//tIE.InvokeMember("Navigate2", BindingFlags.InvokeMethod, null, oIE, oParameter);
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
				//"IExplore.exe " + 
				ProcessStartInfo startInfo = new ProcessStartInfo(link);
				//startInfo.WindowStyle = ProcessWindowStyle.Minimized;
				startInfo.UseShellExecute = true;
				Process.Start(startInfo);
			}
		}

		public nfloat GetUIScale()
		{
			//#if iOS
			return UIScreen.MainScreen.Scale;
			//#endif
			//return 1f;
		}

		public static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Exception e = (Exception)args.ExceptionObject;
            WebTools.TakeWarnMsg("RuntimeTerminating: " + args.IsTerminating, "", e);
		}

		public override void SetBarStyle()
		{
			if (UIApplication.SharedApplication.RespondsToSelector (new ObjCRuntime.Selector ("setStatusBarHidden: withAnimation:")))
				UIApplication.SharedApplication.SetStatusBarHidden (true, UIStatusBarAnimation.Fade);
			else
 				UIApplication.SharedApplication.SetStatusBarHidden (true, true);
		}

		public override void ShowKeyBoard(PlayerIndex index, string name, string title, string desc, AsyncCallback CallbackFunction)
		{			
			//Guide.BeginShowKeyboardInput(index, name, title, desc, CallbackFunction, null);
		}

		public override string EndShowKeyBoard(IAsyncResult ar)
		{
            return "";   //Guide.EndShowKeyboardInput(ar);
			//return "";
		}

        public static string picStatus = "";
        public static PlatformTask platformTask = null;

		public override void ChoosePicture(PlatformTask action)
		{
			ChooseTakePictureBase (action, UIImagePickerControllerSourceType.PhotoLibrary);
		}

		public override void TakePhoto(PlatformTask action)
		{
			ChooseTakePictureBase (action, UIImagePickerControllerSourceType.Camera);
		}

		public void ChooseTakePictureBase(PlatformTask action, UIImagePickerControllerSourceType type)
        {
			//viewController = new ImageViewController();
			// create a new picker controller
			var imagePicker = new UIImagePickerController();

			//imagePicker.GetSupportedInterfaceOrientations ();

			// set our source to the photo library
			imagePicker.SourceType = type;

			// set what media types
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(type);

			imagePicker.FinishedPickingMedia += (sender, e) => {
				// determine what was selected, video or image
				bool isImage = false;
				switch(e.Info[UIImagePickerController.MediaType].ToString())
				{
				case "public.image":
					Console.WriteLine("Image selected");
					isImage = true;
					break;

				case "public.video":
					Console.WriteLine("Video selected");
					break;
				}

				Console.Write("Reference URL: [" + UIImagePickerController.ReferenceUrl + "]");

				// get common info (shared between images and video)
				NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;
				if(referenceURL != null)
					Console.WriteLine(referenceURL.ToString());

				// if it was an image, get the other image info
				if(isImage)
				{

					// get the original image
					UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
					if(originalImage != null)
					{
						// do something with the image
						Console.WriteLine("got the original image");
						//imageView.Image = originalImage;
					}

					// get the edited image
					UIImage editedImage = e.Info[UIImagePickerController.EditedImage] as UIImage;
					if(editedImage != null)
					{
						// do something with the image
						Console.WriteLine("got the edited image");
						//imageView.Image = editedImage;
					}

					//- get the image metadata
					NSDictionary imageMetadata = e.Info[UIImagePickerController.MediaMetadata] as NSDictionary;
					//if(imageMetadata != null)
					//{
						// do something with the metadata
						Console.WriteLine("got image metadata");
					//}

					if (action != null)
					{
						byte[] bytes = null;
						UIImage ima = null;
						if(editedImage != null)
						{
							ima = editedImage;
							//bytes = editedImage.AsJPEG().AsStream().StreamToBytes();
						}
						else if (originalImage != null)
						{
							ima = originalImage;
							//bytes = originalImage.AsJPEG().AsStream().StreamToBytes();
						}

						int width = 540 - 6;  
						int height = 540 - 6; 
						var resizedIma = MaxResizeImage(ima, width, height);
						bytes = resizedIma.AsJPEG().AsStream().StreamToBytes();

						string avatar = "";
						string extension = "";
						if (action.ParamArray != null && action.ParamArray.Length > 0)
						{
							avatar = action.ParamArray[0];
						}
						//if(imageMetadata != null)
						//{
							extension = ".jpg";
						//}
						action.ParamArrayResult = new string[] { extension };
						action.ParamArrayResultBytes = bytes;
						action.Start();
					}

				}
				// if it's a video
				else
				{
					// get video url
					NSUrl mediaURL = e.Info[UIImagePickerController.MediaURL] as NSUrl;
					if(mediaURL != null)
					{
						//
						Console.WriteLine(mediaURL.ToString());
					}
				}

				// dismiss the picker
				imagePicker.DismissModalViewController(true);
			};

			imagePicker.Canceled += (sender, e) =>  
			{
				imagePicker.DismissModalViewController(true);
			};

			imagePicker.ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation.LandscapeLeft | UIInterfaceOrientation.LandscapeRight);

			imagePicker.ShouldAutorotate();

			var gameController = MainGame.Services.GetService(typeof(UIViewController)) as UIViewController;
			gameController.PresentViewController (imagePicker, true, null);

        }

        //private void CreateDirectoryForPictures()
        //{
        //    phtoDir = new Java.IO.File(Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures), "CameraAppDemo");
        //    if (!phtoDir.Exists())
        //    {
        //        phtoDir.Mkdirs();
        //    }
        //}

		public override void MirrorPicture(byte[] image, PlatformTask action)
        {
			var oldImage = GetImagefromByteArray(image);

			CGImage imgRef = oldImage.CGImage; 

			float width = imgRef.Width; 
			float height = imgRef.Height;

			var rect = new CoreGraphics.CGRect (0, 0, width, height);
			UIGraphics.BeginImageContextWithOptions (rect.Size, false, 2);
			var context = UIGraphics.GetCurrentContext ();
			context.ClipToRect (rect);
			context.RotateCTM (Convert.ToSingle(Math.PI));
			context.TranslateCTM(-rect.Size.Width, -rect.Size.Height);
			context.DrawImage (rect, imgRef);

			var newImage = UIGraphics.GetImageFromCurrentImageContext ();
			var bytes = newImage.AsJPEG ().AsStream ().StreamToBytes ();

			if (action != null)
			{
				action.ParamArrayResultBytes = bytes;
				action.Start();
			}

//			let rect =  CGRectMake(0, 0, srcImage.size.width , srcImage.size.height);//创建矩形框
//			//根据size大小创建一个基于位图的图形上下文
//			UIGraphicsBeginImageContextWithOptions(rect.size, false, 2)
//			let currentContext =  UIGraphicsGetCurrentContext();//获取当前quartz 2d绘图环境
//			CGContextClipToRect(cu(www.111cn.net)rrentContext, rect);//设置当前绘图环境到矩形框
//			CGContextRotateCTM(currentContext, CGFloat(M_PI)); //旋转180度
//			//平移， 这里是平移坐标系，跟平移图形是一个道理
//			CGContextTranslateCTM(currentContext, -rect.size.width, -rect.size.height);
//			CGContextDrawImage(currentContext, rect, srcImage.CGImage);//绘图from:http://www.111cn.net/sj/iOS/100398.htm
//
//
//			//let ciimage: CIImage = CIImage(CGImage: imagenInicial.CGImage!)
//			//	let rotada3 = ciimage.imageByApplyingTransform(CGAffineTransformMakeScale(-1, 1))
//			//CGAffineTransform transform = CGAffineTransform.MakeIdentity(); 
//
//			CGAffineTransform transform = CGAffineTransform.MakeScale(-1f, 1f);
//			//transform.Rotate ((float)(3.0f * Math.PI / 2.0));
//
//			RectangleF bounds = new RectangleF (0, 0, width, height); 
//			//if (width > maxSize || height > maxSize) { 
//			//	float ratio = width / height; 
//			//	if (ratio > 1) { 
//			//		bounds.Width = maxSize; 
//			//		bounds.Height = bounds.Width / ratio; 
//			//	} else { 
//			//		bounds.Height = maxSize; 
//			//		bounds.Width = bounds.Height * ratio; 
//			//	} 
//			//} 
//
//			//float scaleRatio = bounds.Width / width; 
//			//SizeF imageSize = new SizeF (imgRef.Width, imgRef.Height); 
//			//float boundHeight; 
////			UIImageOrientation orient = image.Orientation; 
////			if (orient == UIImageOrientation.Up) { 
////				//EXIF = 1 
////				transform = CGAffineTransform.MakeIdentity(); 
////			} else if (orient == UIImageOrientation.UpMirrored) { 
////				//EXIF = 2 
////				transform = CGAffineTransform.MakeTranslation (imageSize.Width, 0); 
////				transform.Scale (-1.0f, 1.0f); 
////			} else if (orient == UIImageOrientation.Down) { 
////				//EXIF = 3 
////				transform = CGAffineTransform.MakeTranslation (imageSize.Width, imageSize.Height); 
////				transform.Rotate ((float) Math.PI); 
////			} else if (orient == UIImageOrientation.DownMirrored) { 
////				//EXIF = 4 
////				transform = CGAffineTransform.MakeTranslation (0, imageSize.Height); 
////				transform.Scale (1.0f, -1.0f); 
////			} else if (orient == UIImageOrientation.LeftMirrored) { 
//				//EXIF = 5 
//			//	boundHeight = bounds.Height; 
//			//	bounds.Height = bounds.Width; 
//			//	bounds.Width = boundHeight; 
//			//	transform = CGAffineTransform.MakeTranslation (imageSize.Height, imageSize.Width); 
//			//	transform.Scale (-1.0f, 1.0f); 
//			//	transform.Rotate ((float)(3.0f * Math.PI / 2.0)); 
////			} else if (orient == UIImageOrientation.Left) { 
////				//EXIF = 6 
////				boundHeight = bounds.Height; 
////				bounds.Height = bounds.Width; 
////				bounds.Width = boundHeight; 
////				transform = CGAffineTransform.MakeTranslation (0, imageSize.Width); 
////				transform.Rotate ((float)(3.0f * Math.PI / 2.0)); 
////			} else if (orient == UIImageOrientation.RightMirrored) { 
////				//EXIF = 7 
////				boundHeight = bounds.Height; 
////				bounds.Height = bounds.Width; 
////				bounds.Width = boundHeight; 
////				transform = CGAffineTransform.MakeScale (-1.0f, 1.0f); 
////				transform.Rotate ((float)(Math.PI / 2.0)); 
////			} else if (orient == UIImageOrientation.Right) { 
////				//EXIF = 8 
////				boundHeight = bounds.Height; 
////				bounds.Height = bounds.Width; 
////				bounds.Width = boundHeight; 
////				transform = CGAffineTransform.MakeTranslation (imageSize.Height, 0); 
////				transform.Rotate ((float)(Math.PI / 2.0)); 
////			} else { 
////				throw new InvalidOperationException ("Invalid image orientation"); 
////			} 
//
//			UIGraphics.BeginImageContext(bounds.Size); 
//
//			CGContext context = UIGraphics.GetCurrentContext (); 
//
//			//if (orient == UIImageOrientation.Right || orient == UIImageOrientation.Left) { 
//			//	context.ScaleCTM (-scaleRatio, scaleRatio); 
//			//	context.TranslateCTM (-height, 0f); 
//			//} else { 
//			//	context.ScaleCTM (scaleRatio, -scaleRatio); 
//			//	context.TranslateCTM (0f, -height); 
//			//} 
//
//			context.ConcatCTM(transform); 
//
//			context.DrawImage (new RectangleF(0, 0, width, height), imgRef); 
//			UIImage imageCopy = UIGraphics.GetImageFromCurrentImageContext (); 
//			UIGraphics.EndImageContext (); 
//
//			return imageCopy.AsJPEG().AsStream().StreamToBytes();
//
//			//return newImage.AsJPEG ().AsStream ().StreamToBytes ();
//            //Bitmap bm = image.ToBitmap();
//            //int w = bm.Width;
//            //int h = bm.Height;
//            //Bitmap newb = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);// 创建一个新的和SRC长度宽度一样的位图
//            //Canvas cv = new Canvas(newb);
//            //Android.Graphics.Matrix m = new Android.Graphics.Matrix();
//            ////m.PostScale(1, -1);   //镜像垂直翻转
//            //m.PostScale(-1, 1);   //镜像水平翻转
//            ////m.PostRotate(-90);  //旋转-90度
//            //Bitmap new2 = Bitmap.CreateBitmap(bm, 0, 0, w, h, m, true);
//            //cv.DrawBitmap(new2, new Rect(0, 0, new2.Width, new2.Height), new Rect(0, 0, w, h), null);
//            //return newb.ToBytes();
//            //return null;
        }

        /// <summary>
        /// 播放歌曲
        /// </summary>
        /// <param name="url"></param>
        public override void PlaySong(string res)
        {
            try
            {
                if (String.IsNullOrEmpty(Path.GetExtension(res)))
                {
                    res = res + ".mp3";
                }

                res = GetFileFullPath(res);

                var url = new NSUrl(res);

                //var song = AVAsset.FromUrl(url);

                //var item = AVPlayerItem.FromAsset(song);

                player = AVAudioPlayer.FromUrl(url);  // AVPlayer.FromPlayerItem(item);

                SetMusicVolume((int)Setting.Current.MusicVolume);

                player.NumberOfLoops = -1;

                player.Play();
                /*
                Session.Current.MusicContent.Unload();
                Song song = Song.FromUri(res, new Uri(res, UriKind.Relative));  // Session.Current.MusicContent.Load<Song>(res);
                SetMusicVolume((int)Setting.Current.MusicVolume);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Play(song);
                */
                //    var songs = Directory.EnumerateFiles(directory, "*.mp3")
                //.Select(file => Song.FromUri(file, new Uri(file)))
                //.ToList();

                //You can look in the Music folder by setting directory to the following:
                //string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //监控此
            }
        }
        public override void StopSong()
        {
            try
            {
                Session.Current.MusicContent.Unload();
                //MediaPlayer.Stop();
                if (player != null)
                {
                    player.Stop();
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
        public override void PauseSong()
        {
            try
            {
                if (player != null)
                {
                    player.Pause();
                }
                //MediaPlayer.Pause();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
        public override void ResumeSong()
        {
            try
            {
                if (player != null)
                {
                    player.Play();
                }
                //MediaPlayer.Resume();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }

        /// <summary>
        /// 設置音量
        /// </summary>
        /// <param name="volume"></param>
        public override void SetMusicVolume(int volume)
        {
            //if (Sound != null) {
            //  Sound.Volume = Convert.ToSingle(volume)/100f;
            //}
            try
            {
                if (player != null)
                {
                    player.Volume = Convert.ToSingle(volume) / 100;
                }
                //MediaPlayer.Volume = Convert.ToSingle(volume) / 100;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                //Why?
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

			orientationDegree = Radians(orientationDegree);

			var oldImage = GetImagefromByteArray(image);

			var bytes = RotateImage (oldImage, orientationDegree).AsJPEG().AsStream().StreamToBytes();
			if (action != null)
			{
				action.ParamArrayResultBytes = bytes;
				action.Start();
			}
            //Bitmap bm = image.ToBitmap();

            //Android.Graphics.Matrix m = new Android.Graphics.Matrix();
            //m.SetRotate(orientationDegree, (float)bm.Width / 2, (float)bm.Height / 2);

            //Bitmap bm1 = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, m, true);

            //return bm1.ToBytes();

            //catch (OutOfMemoryError ex) 
            //return null;
        }

		static float Radians (float degrees) 
		{
			float M_PI = 3.1415926f;
			return degrees * M_PI/180;
		}

		public static UIImage RotateImage (UIImage src, float angle)
		{
			UIImage Ret, Tmp;
			float newSide = Math.Max (src.CGImage.Width, src.CGImage.Height);// * src.CurrentScale;
			SizeF size = new SizeF (newSide, newSide);

			UIGraphics.BeginImageContext (size);
			CGContext context = UIGraphics.GetCurrentContext ();

			context.TranslateCTM (newSide / 2, newSide / 2);

			//context.RotateCTM (angle);
			//src.Draw (new PointF (-src.Size.Width / 2, -src.Size.Height / 2));
			//Ret = UIGraphics.GetImageFromCurrentImageContext ();        

			context.RotateCTM (angle);
			src.Draw( new RectangleF (-src.CGImage.Width / 2, -src.CGImage.Height / 2, newSide, newSide));
			Ret = UIGraphics.GetImageFromCurrentImageContext ();    

			UIGraphics.EndImageContext ();  // Restore context

			if (src.CurrentScale != 1.0f)
			{
				Tmp = new UIImage (Ret.CGImage, src.CurrentScale, UIImageOrientation.Up);
				Ret.Dispose ();
				return (Tmp);
			}

			return Ret;
		}

		public override void CropPicture(byte[] image, int x, int y, int width, int height, PlatformTask action)
        {
			var oldImage = GetImagefromByteArray(image);
			var newImage = CropImage (oldImage, x, y, width, height);
                //Bitmap bm = image.ToBitmap();
                //Bitmap resizedBitmap = Bitmap.CreateBitmap(bm, x, y, width, height);
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    resizedBitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                //    byte[] bitmapData = stream.ToArray();
                //    return bitmapData;
                //}
			var bytes = newImage.AsJPEG().AsStream().StreamToBytes();
			if (action != null)
			{
				action.ParamArrayResultBytes = bytes;
				action.Start();
			}
        }

		public override void ResizeImageFile(byte[] image, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
			byte[] pic = null;

			try
			{

				var oldImage = GetImagefromByteArray(image);

				CGImage imgRef = oldImage.CGImage;

				float width = imgRef.Width;
				float height = imgRef.Height;

				Size newSize;

				if (sameRatio)
				{
					float scale = GameTools.AutoSetScale(Convert.ToInt32(width), Convert.ToInt32(height), targetSizeWidth, targetSizeHeight);
					newSize = new Size(Convert.ToInt32(width * scale), Convert.ToInt32(height * scale));
				}
				else
				{
					newSize = new Size(Convert.ToInt32(targetSizeWidth), Convert.ToInt32(targetSizeHeight));
				}

				var newImage = ResizeImage(oldImage, targetSizeWidth, targetSizeHeight);

				pic = newImage.AsJPEG().AsStream().StreamToBytes();
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
				var oldImage = GetImagefromByteArray(image);
				var newImage = CropImage(oldImage, x, y, width, height);
				pic = newImage.AsJPEG().AsStream().StreamToBytes();

				ResizeImageFile(pic, targetSizeWidth, targetSizeHeight, sameRatio, action);
			}
			catch (Exception ex)
			{
				WebTools.TakeWarnMsg("裁切缩放失败:" + ex.Message.NullToString(), "ResizeImageFile", ex);
			}
		}

		public static UIImage GetImagefromByteArray (byte[] imageBuffer)
		{
			NSData imageData = NSData.FromArray(imageBuffer);
			return UIImage.LoadFromData(imageData);
		}

		// resize the image to be contained within a maximum width and height, keeping aspect ratio
		public static UIImage MaxResizeImage(UIImage sourceImage, float maxWidth, float maxHeight)
		{
			var sourceSize = sourceImage.Size;
			var maxResizeFactor = Math.Max(maxWidth / sourceSize.Width, maxHeight / sourceSize.Height);
			if (maxResizeFactor > 1) return sourceImage;
			var width = maxResizeFactor * sourceSize.Width;
			var height = maxResizeFactor * sourceSize.Height;
			UIGraphics.BeginImageContext(new SizeF(Convert.ToSingle(width), Convert.ToSingle(height)));
			sourceImage.Draw(new RectangleF(0, 0, Convert.ToSingle(width), Convert.ToSingle(height)));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}

		// resize the image (without trying to maintain aspect ratio)
		public static UIImage ResizeImage(UIImage sourceImage, float width, float height)
		{
			UIGraphics.BeginImageContext(new SizeF(width, height));
			sourceImage.Draw(new RectangleF(0, 0, width, height));
			var resultImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return resultImage;
		}

		// crop the image, without resizing
		private static UIImage CropImage(UIImage sourceImage, int crop_x, int crop_y, int width, int height)
		{
			var imgSize = sourceImage.Size;
			UIGraphics.BeginImageContext(new SizeF(width, height));
			var context = UIGraphics.GetCurrentContext();
			var clippedRect = new RectangleF(0, 0, width, height);
			context.ClipToRect(clippedRect);
			var drawRect = new RectangleF(-crop_x, -crop_y, Convert.ToSingle(imgSize.Width), Convert.ToSingle(imgSize.Height));
			sourceImage.Draw(drawRect);
			var modifiedImage = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();
			return modifiedImage;
		}

		public static void ReStart()
        {

        }
        
    }

	public class PlatformTask
    {
		Thread thread;
        public bool IsStop = false;
        public string[] ParamArray = null;
        public string[] ParamArrayResult = null;
        public byte[] ParamArrayResultBytes = null;
        public System.AsyncCallback OnStartFinish = null;
		public PlatformTask(Action action)
		{
			thread = new Thread(() => 
            {
                if (action != null)
                {
                    action.Invoke();
                }
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
		Action act;
		public PlatformTask2(Action action)
		{
		    act = action;
		}
		public void Start()
		{
			//act.Invoke();
			act.BeginInvoke(null, null);
		}
	}

}

//public static Task Task(Action action)
//{
//    return new Task(() =>
//    {
//        action.Invoke();
//    });
//}

//public static Thread Task(Action action)
//{
//    return new Thread(() =>
//    {
//        action.Invoke();
//    });
//} 

//String text = "0123456789";
//using (Stream stmText = new MemoryStream(text.Length))
//{
//    StreamWriter swText = new StreamWriter(stmText);
//    swText.Write(text);
//    swText.Flush();
//    stmText.Position = 0;
//   // Use the stream
//}

//Linux
//OpenTKGamePlatform.cs
// OnIsMouseVisibleChanged();
//GamePlatform.cs
//http://stackoverflow.com/questions/16426060/using-isolatedstorage-in-xamarin
