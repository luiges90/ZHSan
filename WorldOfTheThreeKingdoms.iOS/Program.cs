using System;
using Foundation;
using UIKit;

namespace WorldOfTheThreeKingdoms.iOS
{
    [Register("AppDelegate")]
    class Program : UIApplicationDelegate
    {
        private static MainGame game;

        public static void RunGame()
        {
            game = new MainGame();
            game.Run();
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            UIApplication.Main(args, null, "AppDelegate");
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}
