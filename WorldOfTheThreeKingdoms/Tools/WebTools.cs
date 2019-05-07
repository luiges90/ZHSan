using GameManager;
using Platforms;
using SanguoSeason.Encryption;
using SeasonContracts;
using System;
using System.Linq;

namespace Tools
{
    public static class WebTools
    {
        private const string des3Key = "WorldOfTheThreeKingdoms.Zhsan";

        public static string WebSite
        {
            get
            {
                string webSite = "http://www.zhsan.com";
                return webSite;
            }
        }

        public static void TakeWarnMsg(string warn, string detail, Exception ex)
        {
            string ext = "";
            if (ex != null && !String.IsNullOrEmpty(ex.Message))
            {
                if (ex.Message.Contains("Out of memory"))
                {
                    ext = "系统资源不足";
                }
                else if (ex.Message.Contains("Access to the path") && ex.Message.Contains("is denied"))
                {
                    ext = "文件访问禁止";
                }
                else if (ex.Message.Contains("Disk full") || ex.Message.Contains("Not enough free space") || ex.Message.Contains("Not enough storage"))
                {
                    ext = "系统存储爆满";
                }
                else if (ex.Message.Contains("This resource could not be created"))
                {
                    ext = "系统内存不足";
                }
            }

            if (Session.MainGame != null)
            {
                Session.MainGame.warn = warn + ext;
                Session.MainGame.lastWarnTime = DateTime.Now;
            }

            new PlatformTask(() =>
            {
                WebTools.SendErrMsg(warn + ext + detail, ex);
            }).Start();
        }

        public static void SendErrMsg(string msg, Exception ex)
        {
            //暫不上報錯誤
            return; 
        }

        public static void DownloadFile(string webFile, string localFile, PlatformTask action)
        {            
            try
            {
                var task2 = new PlatformTask(() => { });
                task2.OnStartFinish += new AsyncCallback((result0) =>
                {
                    byte[] bytes = task2.ParamArrayResultBytes;
                    if (bytes != null && bytes.Length > 0)
                    {
                        var task = new PlatformTask(() =>
                        {
                            if (action != null)
                            {
                                action.Start();
                            }
                        });
                        Platform.Current.SaveUserFile(localFile, bytes, task);
                    }
                    else
                    {
                        WebTools.TakeWarnMsg("用户文件下载失败:" + localFile, "DownloadFile:" + webFile, new Exception("文件空或长度为0"));
                    }
                });
                Platform.Current.DownloadWebData(WebSite + (webFile.StartsWith("/") ? "" : "/") + webFile, task2);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("用户文件下载失败:" + localFile, "DownloadFile:" + webFile, ex);
            }
        }

        public static string UrlEncodePath(this string url)
        {
            url = Uri.EscapeDataString(url);
            return url.Replace("+", "%2B").Replace("#", "%23");
        }

        public static string UrlEncodePath2(this string url)
        {
            return url.Replace("+", "%2B").Replace("#", "%23");
        }

    }

}
