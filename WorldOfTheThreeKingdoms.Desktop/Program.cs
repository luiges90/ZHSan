using System;
using System.Threading;
using Platforms;
using Tools;

namespace WorldOfTheThreeKingdoms.Desktop
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        static bool isDebug = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
                if (isDebug)
                {
                    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
                }

                using (MainGame game = new MainGame())
                {
                    game.Run();
                }
        }

        static void UIExceptionHandler(object sender, ThreadExceptionEventArgs args)
        {
            Exception e = (Exception)args.Exception;
            //Season.Current.OpenLink(WebTools.WebSite2 + "/service.aspx?mes=" + e.ToString() + "&platform=" + Season.PlatForm.ToString() + "&ver=" + Season.GameVersion.ToString());
            WebTools.TakeWarnMsg("RuntimeTerminating: " + args, "", e);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            //Season.Current.OpenLink(WebTools.WebSite2 + "/service.aspx?mes=" + e.ToString() + "&platform=" + Season.PlatForm.ToString() + "&ver=" + Season.GameVersion.ToString());
            WebTools.TakeWarnMsg("RuntimeTerminating: " + args.IsTerminating, "", e);
        }
    }
}
