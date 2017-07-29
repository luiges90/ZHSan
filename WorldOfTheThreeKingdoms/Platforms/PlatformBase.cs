using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Globalization;
using GameManager;
using Tools;

namespace Platforms
{

    using MediaPlayer = Microsoft.Xna.Framework.Media.MediaPlayer;

    public enum PlatFormType
    {
        Win,
        Android,
        iOS,
        UWP,
        Desktop  //Mac, Linux
    }
    
    public abstract class PlatformBase
    {
        public static string Product = "WorldOfTheThreeKingdoms";

        public static PlatFormType PlatFormType;

        public static Platform Current = new Platform();
        //System.IO.File.Exists(GameApplicationUrl))
        //System.Reflection.AssemblyName.GetAssemblyName(GameApplicationUrl).Version.ToString();
        public static string GameVersion = "1.0.2.0";

        public static string PreferResolution = "925*520";

        public virtual bool? IsTrialCheck()
        {
            return true;
        }

        public static bool IsMobile = true;

        public bool DebugMode = true;
        public bool ProcessGameData = false;

        public bool AssetsPng = false;

        public bool QuickTest = false;

        public string Channel = "";

        public bool DisplayMetroStart = false;
        public bool OpenWeb = true;

        public bool IsOnline = true;

        public string GetSlash = "\\";  // "//";

        public string PlatformPre = @"";

        public bool AdAvaliable = false;

        public string PreferFullMode = "Full";

        public string Location = "";

        //IGraphicsDeviceService service = base.Game.Services.GetService(typeof(IGraphicsDeviceService)) as IGraphicsDeviceService;
        //this.batch = new SpriteBatch(service.GraphicsDevice);

        /// <summary>
        /// 解決方案文件夾
        /// </summary>
        public string SolutionDir = "";

        /// <summary>
        /// 內存使用占用
        /// </summary>
        public string MemoryUsage
        {
            get
            {
                return (System.GC.GetTotalMemory(false) / 1024).ToString();
                //Android.Activity1.os.Debug.getNativeHeapAllocatedSize()
                //return "";
            }
        }

        /// <summary>
        /// 程序路徑
        /// </summary>
        public string ApplicationUrl = "";
        /// <summary>
        /// 遊戲路徑
        /// </summary>
        public string GameApplicationUrl = "";

        public bool WindowInputCapturerEnable = false;

        public bool IsGuideVisible = false;

        public bool KeyBoardAvailable = true;

        public bool IsMobilePlatForm = false;

        public string CurrentLanguage
        {
            get
            {
                return CultureInfo.CurrentCulture.ToString();
            }
        }

        public static object SerializerLock = new object();

        public static object IoLock = new object();

        public static bool SessionActive { get; set; }

        public static Game MainGame = null;

        public PlatformBase()
        {

        }

        public virtual void SetMouseVisible(bool visible)
        {
            
        }

        public virtual void SetWindowAllowUserResizing(bool allow)
        {
            
        }

        public virtual void SetTimerDisabled(bool timerDisabled)
        {

        }

        public virtual void PreparePhone()
        {

        }

        public virtual void SetFullScreen(bool full)
        {
            //SystemTray.IsVisible = !full;
            //Session.Current.graphics.IsFullScreen = true;
        }

        public virtual void SetOrientations()
        {
            //Session.Current.graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        public virtual void EnableFrameCount()
        {

        }

        public virtual string GetDeviceID()
        {
            return "";
        }

        public virtual string GetDeviceInfo()
        {
            return "";
        }

        public virtual string GetSystemInfo()
        {
            return "";
        }

        //public virtual string GetPhoneModel()
        //{
        //    return "";
        //}

        //public virtual string GetPhoneID()
        //{
        //    return "";
        //}

        public virtual float GetUIScale()
        {
            return 1f;
        }

        public virtual void OpenFactory()
        {

        }

        public virtual void PrepareAd()
        {

        }

        public virtual void AdOffersStart()
        {

        }

        public virtual void ShowAd()
        {

        }

        public virtual void ShowOffersWall()
        {

        }

        public virtual void ShowOffersWallDialog()
        {

        }

        public virtual void ShowShareWallDialog()
        {

        }

        public virtual void EndShowAd()
        {

        }

        public virtual void DisplayAdView()
        {

        }

        public virtual void HideAdView()
        {

        }

        public virtual bool InputTextNow()
        {
            return false;
        }

        public virtual void ShowText(string text, AsyncCallback callBack)
        {

        }

        public virtual void HideText(string result)
        {

        }

        public virtual void InitInputCapturer()
        {

        }

        public virtual List<Character> GetChars()
        {
            return null;
        }

        public virtual void ClearChars()
        {

        }

        public virtual void ApplicationViewChanged()
        {

        }

        public virtual void ProcessViewChanged()
        {

        }

        public virtual void SetBarStyle()
        {

        }

        public virtual void ShowKeyBoard()
        {
            //Guide.BeginShowKeyboardInput(PlayerIndex.One, Title, Desc, this.Text, CallbackFunction, null);
        }

        public virtual void ShowKeyBoard(PlayerIndex index, string name, string title, string desc, AsyncCallback callBack)
        {
            //Guide.BeginShowKeyboardInput (PlayerIndex.One, "????????", "????????????????", Session.NickName, CallbackFunction, null);
        }

        public virtual string EndShowKeyBoard(IAsyncResult ar)
        {
            //Guide.EndShowKeyboardInput(ar);
            return "";
        }

        //public byte[] ProcessPictureBytes = null;
        //public SeasonTask ProcessPictureTask = null;

        public virtual void TakePhoto(PlatformTask action)
        {

        }

        public virtual void ChoosePicture(PlatformTask action)
        {

        }

        public virtual void MirrorPicture(byte[] image, PlatformTask action)
        {
            
        }

        public virtual void RotatePicture(byte[] image, int rotate, PlatformTask action)
        {
            
        }

        public virtual void CropPicture(byte[] image, int x, int y, int width, int height, PlatformTask action)
        {
            
        }

        public virtual void ResizeImageFile(byte[] image, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {
            
        }

        public virtual void CropResizePicture(byte[] image, int x, int y, int width, int height, int targetSizeWidth, int targetSizeHeight, bool sameRatio, PlatformTask action)
        {

        }

        public virtual void OpenMarket(string key)
        {

        }

        public virtual void OpenReview(string key)
        {

        }

        public virtual byte[] ScreenShot(GraphicsDevice graphics, RenderTarget2D screenshot)
        {
            return null;
        }

		/// <summary>
		/// 設置音量
		/// </summary>
		/// <param name="volume"></param>
		public virtual void SetMusicVolume(int volume)
		{
			//if (Sound != null) {
			//	Sound.Volume = Convert.ToSingle(volume)/100f;
			//}
			try
			{
				MediaPlayer.Volume = Convert.ToSingle(volume) / 100;
			}
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
			catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
			{
				//Why?
			}
		}
		/// <summary>
		/// 播放歌曲
		/// </summary>
		/// <param name="url"></param>
		public virtual void PlaySong(string res)
		{
			try
			{
				Session.Current.MusicContent.Unload();
				Song song = Session.Current.MusicContent.Load<Song>(res);
                SetMusicVolume((int)Setting.Current.MusicVolume);
				MediaPlayer.IsRepeating = true;
				MediaPlayer.Play(song);

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
        public virtual void StopSong()
        {
            try
            {
                Session.Current.MusicContent.Unload();
                MediaPlayer.Stop();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
        public virtual void PauseSong()
        {
            try
            {
                MediaPlayer.Pause();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                
            }
        }
        public virtual void ResumeSong()
        {
            try
            {
                MediaPlayer.Resume();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="url"></param>
        public virtual bool PlayEffect(string res)
		{
            if (String.IsNullOrEmpty(res))
            {
                return true;
            }
			try
			{
				SoundEffect effect = Session.Current.SoundContent.Load<SoundEffect>(res);
				effect.Play(Convert.ToSingle(Setting.Current.SoundVolume) / 100, 0.0f, 0.0f);
                return true;
			}
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
			catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
			{
                //监控此
                return false;
			}
		}

        public virtual void PlayEffects(string[] list, string ext)
        {
            if (list == null || list.Length == 0) return;

            foreach(var li in list)
            {
                //if (FileContentExists(li, ext))
                //{
                    if (PlayEffect(li))
                    {
                        return;
                    }
                //}
            }
        }

        public bool FileContentExists(string res, string ext)
        {
            return FileExists("Content/" + res + ext.NullToString(".xnb"));
        }

        public virtual void PauseGame()
        {
            //Session.PauseGame();
        }

        public virtual void ResumeGame()
        {
            //Session.ResumeGame();
        }

        public virtual void Exit()
        {

        }

        //public virtual void ReStart()
        //{

        //}

        #region 用戶文件夾處理

        protected string UserApplicationDataPath
        {
            get
            {
                return String.Empty;
            }
        }

        public virtual string[] GetFiles(string dir)
        {
            return null;
        }

        public virtual string ReadAllText(string file)
        {
            return "";
        }

        public virtual string[] ReadAllLines(string file)
        {
            return null;
        }

        public virtual byte[] ReadAllBytes(string file)
        {
            return null;
        }

        public virtual void WriteAllText(string file, string xml1)
        {
            
        }

        public virtual void WriteAllBytes(string file, byte[] bytes1)
        {
            
        }

        public virtual Stream FileOpenWrite(string file, bool write)
        {
            return null;
        }

        public virtual bool FileExists(string file)
        {
            return false;
        }

        public virtual void FileDelete(string file)
        {
            
        }

        public virtual bool DirectoryExists(string dir)
        {
            return false;
        }

        public virtual void DirectoryCreateDirectory(string dir)
        {
            
        }

        public virtual string DirectoryName(string dir)
        {
            return "";
        }

        public virtual string GetFileNameFromPath(string file)
        {
            return "";
        }

        #endregion

    }

    /// <summary>
    /// character message
    /// </summary>
    public class Character
    {
        /// <summary>
        /// 是否已经使用过
        /// </summary>
        public bool IsUsed { get; set; }
        /// <summary>
        /// char类型
        /// </summary>
        public characterType CharaterType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public char Chars { get; set; }
    }
    public enum characterType
    {
        Char = 0,
        BackSpace = 8,
        Tab = 9,
        Enter = 13,
        Esc = 27
    }


}
