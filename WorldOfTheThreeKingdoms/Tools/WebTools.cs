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

        public static UserIdentity GetUserIdentity()
        {
            var user = new UserIdentity()
            {
                Guid = Setting.Current != null ? Setting.Current.UserGuid : "",
                UserID = 0,
                UserName = "",
                Contact = "",
                DeviceId = Platform.Current.GetDeviceID(),
                DeviceInfo = Platform.Current.GetDeviceInfo(),
                Product = Platform.Product,
                Platform = Platform.PlatFormType.ToString(),
                Channel = Platform.Current.Channel,
                Version = Platform.GameVersion
            };
            return user;
        }

        public static void SendErrMsg(string msg, Exception ex)
        {
            var errorLog = new ErrorLog()
            {
                UserIdentity = GetUserIdentity(),
                ResultStatus = msg,
                ErrorMsg = ex.GetExceptionDetail()
            };

            errorLog.ResultStatus = errorLog.UserIdentity.DeviceInfo + " " + errorLog.ResultStatus;

            string status = "";
            string errorMsg = "";
            var mes = SeasonMessage(errorLog, null, ref status, ref errorMsg, null);
            if (mes == null)
            {
                //出錯。。。
            }
            //if (Season.PlatForm != PlatForm.iOS) {
            //WriteSeasonLog("Error", "Sanguo" + Season.PlatForm.ToString(), log);
            //}
        }

        public static RecordBase SeasonMessage(RecordBase info, string binary, ref string status, ref string errorMsg, PlatformTask onUpload)
        {
            status = ""; errorMsg = "";
            string json = "";
            try
            {
                string url = String.Format("{0}/Webservice.asmx/SeasonMessageFile2?", WebTools.WebSite);
                string jso = SimpleSerializer.SerializeJson<RecordBase>(info, false, false, false);
                string jsonEncryptZip = EncryptionHelper.Encrypt(jso, des3Key).GZipCompressString().UrlEncodePath2();
                string binaryEncryptZip = "";
                if (!String.IsNullOrEmpty(binary))
                {
                    binaryEncryptZip = binary.UrlEncodePath2();
                }
                string data = String.Format("info={0}&binary={1}&product={2}&guid={3}&platform={4}", jsonEncryptZip, binaryEncryptZip, "WorldOfTheThreeKingdoms", Setting.Current.UserGuid, Platform.PlatFormType.ToString());
                json = new Platform().GetWebServicePost(url.UrlEncodePath2(), data, onUpload);
                json = json.GZipDecompressString();
                json = EncryptionHelper.Decrypt(json, des3Key);
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
            }
            if (!String.IsNullOrEmpty(json))
            {
                if (json.Length <= 20)
                {
                    status = json;
                }
                else
                {
                    try
                    {
                        RecordBase message = null;

                        if (info is Record)
                        {
                            message = SimpleSerializer.DeserializeJson<Record>(json, false, false);
                        }
                        else if (info is ClientStatus)
                        {
                            message = SimpleSerializer.DeserializeJson<ClientStatus>(json, false, false);
                        }
                        else if (info is ErrorLog)
                        {
                            message = SimpleSerializer.DeserializeJson<ErrorLog>(json, false, false);
                        }
                        else if (info is UnknownTexts)
                        {
                            message = SimpleSerializer.DeserializeJson<UnknownTexts>(json, false, false);
                        }
                        else if (info is WebRecord)
                        {
                            message = SimpleSerializer.DeserializeJson<WebRecord>(json, false, false);
                        }

                        return message;
                    }
                    catch (Exception ex)
                    {
                        //解析出錯
                        status = json;
                        if (String.IsNullOrEmpty(errorMsg))
                        {
                            errorMsg = ex.ToString();
                        }
                    }
                }
            }
            return null;
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
