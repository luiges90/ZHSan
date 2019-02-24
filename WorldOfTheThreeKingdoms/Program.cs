using GameManager;
using Platforms;
using Steamworks;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WorldOfTheThreeKingdoms
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*bool flag;
            Mutex mutex = new Mutex(true, "WorldOfTheThreeKingdoms", out flag);
            if (!flag)
            {
                MessageBox.Show("游戏已经在运行中。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                mutex.ReleaseMutex();
                new MainProcessManager().Processing();
            }*/

            //SteamAPI.Init();

            string exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(exeDir);

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
            {
                //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
                
                using (MainGame game = new MainGame())
                {
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        game.Run();
                    }
                    else
                    {
                        try
                        {
                            game.Run();
                        }
                        catch (Exception ex)
                        {
                            PrintError(ex);
                        }
                    }
                }
            }
            else if (Platform.PlatFormType == PlatFormType.UWP)
            {
                Platform.Current.OpenFactory();
            }
        }

        static void UIExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            Exception e = (Exception)args.Exception;
            PrintError(e);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            PrintError(e);
        }

        public static void PrintError(Exception e)
        {
            DateTime dt = System.DateTime.Now;
            String dateSuffix = "_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "h" + dt.Minute;
            String logPath = "CrashLog" + dateSuffix + ".log";
            StreamWriter sw = new StreamWriter(new FileStream(logPath, FileMode.Create));

            sw.WriteLine("==================== Message ====================");
            sw.WriteLine(e.Message);
            sw.WriteLine("=================== StackTrace ==================");
            sw.WriteLine(e.StackTrace);

            sw.Close();

            //String savePath = "CrashSave" + dateSuffix + (Session.GlobalVariables.EncryptSave ? ".zhs" : ".mdb");
            //try
            //{
            //    Session.MainGame.SaveGameWhenCrash(savePath);
            //}
            //catch (Exception eSave)
            //{
            //    // 保存失败，这里要做什么好？
            //}

            MessageBox.Show("中华三国志遇到严重错误，请提交游戏目录下的'" + logPath + "'。", "游戏错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

    }
#endif
}
