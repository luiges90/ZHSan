using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
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
using System.Net;
using System.Xml;
using System.Diagnostics;
using SeasonContracts;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Platforms;
using System.Reflection;

namespace Platforms
{
	/// <summary>
    /// 各平台不同的實現
    /// </summary>
	public class Platform : PlatformBase
    {
		public static new PlatFormType PlatFormType = PlatFormType.Desktop;

        public static new bool IsMobilePlatForm = false;

		public static string PreferResolution = "1280*720";

		public new string PreferFullMode = "Window";

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

		public new bool IsGuideVisible
		{
			get
			{
                return Guide.IsVisible;
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
		}

		public override void SetFullScreen(bool full)
		{
			GraphicsDeviceManager.IsFullScreen = full;
		}

        public override string GetDeviceInfo()
        {
            return "";
        }

        public override string GetSystemInfo()
        {
            return System.Environment.OSVersion.Platform + " " + System.Environment.OSVersion.VersionString;
        }

        #region 加載資源文件
        /// <summary>
        /// 加載資源文本
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public string LoadText(string res)
        {
            res = res.Replace("\\", "/");
            //lock (Platform.IoLock)
            //{
                return File.ReadAllText(res);
            //}
			//MacOS
			//string dir = AppDomain.CurrentDomain.BaseDirectory;
			//return File.ReadAllText(dir + "Content/Resources/" + res);
        }
		/// <summary>
		/// 加載資源文本
		/// </summary>
		/// <param name="res"></param>
		/// <returns></returns>
		public string[] LoadTexts(string res)
		{
            //lock (Platform.IoLock)
            //{
                return File.ReadAllLines(res);
            //}
		}
        /// <summary>
        /// 加載資源文件
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public byte[] LoadFile(string res)
        {
            res = res.Replace("\\", "/");
            lock (Platform.IoLock)
            {
                using (var dest = new MemoryStream())
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

                //lock (Platform.IoLock)
                //{
                    using (var stream = isUser ? LoadUserFileStream(res) : TitleContainer.OpenStream(res))
                    {
                        Texture2D tex = Texture2D.FromStream(Platform.GraphicsDevice, stream);
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
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("加载游戏材质失败:" + res, "LoadTexture:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        #endregion

        #region 處理文件夾事宜

        public override string[] GetFiles(string dir)
        {
            return Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories);
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

        #region 處理用戶文件
        public new string UserApplicationDataPath
        {
            get
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/WorldOfTheThreeKingdoms/";
                lock (Platform.IoLock)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
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
        public string[] GetUserFileNames(string searchPattern)
        {
            try
            {
                lock (Platform.IoLock)
                {
                    var files = Directory.GetFiles(UserApplicationDataPath, searchPattern);
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
                res = res.Replace("\\", "/");
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
                res = res.Replace("\\", "/");
                lock (Platform.IoLock)
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
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("加载用户文件失败:" + res, "GetUserFile:" + UserApplicationDataPath + res, ex);
                return null;
            }
        }
        /// <summary>
        /// 獲取用戶鍵值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetUserValueByKey(string key)
        {
            try
            {
                string[] strings = GetUserFileString("settings.config");
                if (strings != null)
                {
                    string value = strings.FirstOrDefault(st => st.Contains("=") && st.Split('=')[0] == key);
                    if (!String.IsNullOrEmpty(value))
                    {
                        return value.Split('=')[1];
                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("获取用户键值失败:" + key, "GetUserValueByKey:", ex);
            }
            return null;
        }
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
        public void SaveUserFile(string res, string content)
        {
            try
            {
                DelUserFiles(new string[] { res }, null);
                lock (Platform.IoLock)
                {
                    File.WriteAllText(UserApplicationDataPath + res, content);
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
        protected IsolatedStorageFile GetIsolatedStorageFile()
        {
			return null;
        }

        #endregion

        public static void Sleep(int time)
        {
            Thread.Sleep(time);
        }

		public override void OpenMarket(string key)
        {
			 OpenLink(WebTools.WebSite);
        }

		public override void OpenReview(string key)
        {
			OpenLink(WebTools.WebSite);
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
//            return null;
//            graphics.GraphicsDevice.SetRenderTarget(null);
//            byte[] shot = null;
//            using (MemoryStream ms = new MemoryStream())
//            {
//                screenshot.SaveAsJpeg(ms, screenshot.Width, screenshot.Height);
//                screenshot.Dispose();
//                shot = ms.ToArray(); //.GetBuffer();
//                screenshot = new RenderTarget2D(graphics.GraphicsDevice, 800, 480, false, SurfaceFormat.Color, DepthFormat.None);
//            }
//            return shot;
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
			catch (Exception ex)
			{
                WebTools.TakeWarnMsg("ProcessStartInfo打開IE出錯：", "", ex);
			}
		}

		public override void Exit()
		{
			MainGame.Exit ();
		}

		public override void ShowKeyBoard(PlayerIndex index, string name, string title, string desc, AsyncCallback CallbackFunction)
		{			
			Guide.BeginShowKeyboardInput(index, name, title, desc, CallbackFunction, null);
		}

		public override string EndShowKeyBoard(IAsyncResult ar)
		{
			return Guide.EndShowKeyboardInput(ar);
			//return "";
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
                    MessageBox.Show("仅能上传jpg,png,gif,bmp格式的图片！");
                }
                else
                {
                    //获取用户选择的文件，并判断文件大小不能超过5000K，fileInfo.Length是以字节为单位的
                    FileInfo fileInfo = new FileInfo(fileDialog.FileName);
                    if (fileInfo.Length > 5000 * 1024)
                    {
                        MessageBox.Show("上传的图片不能大于5000K");
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
        Thread thread;
        public bool IsStop = false;
        public string[] ParamArray = null;
        public string[] ParamArrayResult = null;
        public byte[] ParamArrayResultBytes = null;
        public AsyncCallback OnStartFinish = null;
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
		//Action act;
		//public SeasonTask(Action action)
		//{
		//    act = action;
		//}
		//public void Start()
		//{
		//	act.Invoke();
		//	//act.BeginInvoke(null, null);
		//}

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


}

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