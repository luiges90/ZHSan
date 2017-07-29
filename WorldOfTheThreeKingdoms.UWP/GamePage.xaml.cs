using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GameManager;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.Graphics.Display;
using Tools;
using Windows.Storage;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Platforms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WorldOfTheThreeKingdoms.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
		readonly MainGame Game1;

        AsyncCallback asyncCallBack;

        public GamePage()
        {
            this.InitializeComponent();

            Platform.GamePage1 = this;

            // Create the game.
            var launchArguments = string.Empty;
            Game1 = MonoGame.Framework.XamlGame<MainGame>.Create(launchArguments, Window.Current.CoreWindow, swapChainPanel);

            //ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.FullScreen;
            //ApplicationView.GetForCurrentView().TryEnterFullScreenMode();

            //this.SizeChanged += (sender, e) =>
            //{
            //    if (CoreGame.Current.beginInitGameData)
            //    {
            //        Session.ChangeDisplay(true);
            //    }
            //};
        }

        public void ViewAd()
        {

        }

        public void HideAd()
        {

        }

        public void ViewTxtSeason(string text, AsyncCallback callBack)
        {
            asyncCallBack = callBack;
            grid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            txtSeason.Text = text;
            txtSeason.Focus(FocusState.Keyboard);
        }

        public string GetTxtSeason()
        {
            return txtSeason.Text;
        }

        public void HideTxtSeason()
        {
            grid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            if (asyncCallBack != null)
            {
                asyncCallBack.Invoke(null);
                asyncCallBack = null;
                //IAsyncResult ar = demo.BeginRun(null, null);
                // You can do other things here  
                // Use AsyncWaitHandle.WaitOne method to block thread for 1 second at most  
                //ar.AsyncWaitHandle.WaitOne(1000, false);
            }
        }
        
        void btnSeason_Click(object sender, RoutedEventArgs e)
        {
            HideTxtSeason();
        }

    }
}
