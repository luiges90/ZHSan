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
        public static string GameVersion = "1.2.8.8";

        public static int PackVersion = 1288;

        public static string GameVersionType = "dev";

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

        //public virtual void SetWindowBorder(bool visible)
        //{

        //}

        public virtual Vector2 GetWorkingArea()
        {
            return Vector2.Zero;
        }

        public virtual void SetFullScreen(bool full)
        {
            //SystemTray.IsVisible = !full;
            //Session.Current.graphics.IsFullScreen = true;
        }

        public virtual void SetFullScreen2(bool full)
        {

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
                if (String.IsNullOrEmpty(Path.GetExtension(res)))
                {
                    res = res + ".mp3";
                }

                if (Platform.PlatFormType == PlatFormType.Android)
                {
                    if (res.Contains("\\"))
                    {
                        res = res.Substring(res.LastIndexOf('\\') + 1);
                    }
                }

                Session.Current.MusicContent.Unload();
                Song song = Song.FromUri(res, new Uri(res, UriKind.Relative));  // Session.Current.MusicContent.Load<Song>(res);
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
        List<string> songs2 = new List<string>();
        List<Song> songslist = new List<Song>();
        public virtual void PlaySong(string[] songs)
        {
            try
            {
                string res = "";
                songs2 = new List<string>();
                songslist = new List<Song>();
                foreach (var item in songs)
                {
                    res = item;
                    if ((!item.EndsWith(".mp3") && !item.EndsWith(".wav")))
                    {
                        continue;
                    }
                    if (Platform.PlatFormType == PlatFormType.Android)
                    {
                        if (res.Contains("\\"))
                        {
                            res = res.Substring(res.LastIndexOf('\\') + 1);
                        }
                    }
                    songs2.Add(res);
                    Song song3 = Song.FromUri(res, new Uri(res, UriKind.Relative));
                    songslist.Add(song3);
                }
                if (songslist.Count >= 1)
                {
                    Session.Current.MusicContent.Unload();
                    SetMusicVolume((int)Setting.Current.MusicVolume);
                    MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
                    void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
                    {
                        if (songs2.Count > 0 && Session.GlobalVariables.PlayMusic && MediaPlayer.State== MediaState.Stopped)
                        {
                            MediaPlayer.Play(getrandomsong());
                        }
                    }
                    MediaPlayer.Play(getrandomsong());
                }
            }
            catch (Exception ex)
            {
                //监控此
            }
        }
        Song getrandomsong()
        {
             Random rd = new Random();
             int index = rd.Next(0, songslist.Count);
            return songslist[index];
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
            if (String.IsNullOrEmpty(Path.GetExtension(res)))
            {
                res = res + ".wav";
            }
            try
            {
                var bytes = Current.LoadFile(res);
                var mem = new MemoryStream(bytes);
                SoundEffect effect = SoundEffect.FromStream(mem); // Session.Current.SoundContent.Load<SoundEffect>(res);
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

            foreach (var li in list)
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

        public string GetMODFile(string res)
        {
            //res = res.Replace("\\", "/");

            //根據MOD來選擇素材
            if (Setting.Current == null || String.IsNullOrEmpty(Setting.Current.MODRuntime))
            {

            }
            else
            {
                var mod = res.Replace("Content", "MODs/" + Setting.Current.MODRuntime);

                if (Platform.Current.FileExists(mod))
                {
                    res = mod;
                }
            }

            return res;
        }

        public string[] GetMODFiles(string dir, bool full)
        {
            string[] files = null;

            //根據MOD來選擇素材
            if (String.IsNullOrEmpty(Setting.Current.MODRuntime))
            {
                files = GetFilesBasic(dir, full).NullToEmptyArray();
            }
            else
            {
                var mod = dir.Replace("Content", "MODs\\" + Setting.Current.MODRuntime);

                files = GetFiles(mod, full).NullToEmptyArray();

                if (files.Length == 0)
                {
                    files = GetFilesBasic(dir, full).NullToEmptyArray();
                }
                else
                {
                    
                }
            }

            return files;
        }

        public string[] GetMODDirectories(string dir, bool full)
        {
            string[] dirs = null;

            //根據MOD來選擇文件夾
            if (String.IsNullOrEmpty(Setting.Current.MODRuntime))
            {
                dirs = GetDirectories(dir, false, full).NullToEmptyArray();
            }
            else
            {
                var mod = dir.Replace("Content", "MODs\\" + Setting.Current.MODRuntime);

                dirs = GetDirectories(mod, false, full).NullToEmptyArray();

                if (dirs.Length == 0)
                {
                    dirs = GetDirectoriesBasic(dir, false, full).NullToEmptyArray();
                }
                else
                {

                }
            }

            return dirs;
        }

        protected string UserApplicationDataPath
        {
            get
            {
                return String.Empty;
            }
        }

        public virtual string[] GetDirectories(string dir, bool all, bool full)
        {
            return null;
        }

        public virtual string[] GetDirectoriesBasic(string dir, bool all, bool full)
        {
            return null;
        }

        public virtual string[] GetDirectoriesExpan(string dir, bool all, bool full)
        {
            return null;
        }

        public virtual string[] GetFiles(string dir, bool all)
        {
            return null;
        }

        public virtual string[] GetFilesBasic(string dir, bool all = false)
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
