using Microsoft.Xna.Framework.Content;
using Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Platforms;
using GameGlobal;

namespace GameManager
{
    [DataContract]
    public class Setting
    {
        [DataMember]
        public string UserGuid { get; set; }
        [DataMember]
        public string DeviceID { get; set; }
        [DataMember]
        public string Language { get; set; }
        [DataMember]
        public int? MusicVolume { get; set; }
        [DataMember]
        public int? SoundVolume { get; set; }
        [DataMember]
        public string DisplayMode { get; set; }
        [DataMember]
        public string Resolution { get; set; }
        [DataMember]
        public string GamerName { get; set; }
        [DataMember]
        public string Difficulty { get; set; }
        [DataMember]
        public string BattleSpeed { get; set; }

        [DataMember]
        public string MOD { get; set; }

        public string MODRuntime
        {
            get
            {
                if (Session.Current == null || Session.Current.Scenario == null || Session.Current.Scenario.MOD == null)
                {
                    return Setting.Current.MOD;
                }
                else
                {
                    return Session.Current.Scenario.MOD;
                }
            }
        }

        [DataMember]
        public GlobalVariables GlobalVariables { get; set; }

        public static Setting Current = null;

        public Setting()
        {

        }

        public static void Init(bool prepare)
        {
            try
            {
                string file1 = "Setting.config";
                //string file2 = "settings.config";
                if (Platform.Current.UserFileExist(new string[] { file1 })[0])
                {
                    try
                    {
                        Current = SimpleSerializer.DeserializeJsonFile<Setting>(file1, true, false);

                        if (prepare)
                        {
                            Prepare();
                        }

                        Save();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("初始用户设置失败:Setting.config", "Init:", ex);
            }

            if (Current == null)
            {
                Current = new Setting();

                if (prepare)
                {
                    Prepare();
                }

                Save();
            }

            //if (Platform.PlatFormType == PlatFormType.iOS)  //|| Platform.PlatForm == PlatForm.WinRT || Platform.PlatForm == PlatForm.WP)
            //{
            //	Session.RealResolution = Session.Resolution = Platform.PreferResolution;
            //}

        }

        public static void Save()
        {
            string file1 = "Setting.config";
            SimpleSerializer.SerializeJsonFile<Setting>(Setting.Current, file1, false);
        }

        static void Prepare()
        {
            if (Current != null)
            {
                if (String.IsNullOrEmpty(Current.UserGuid))
                {
                    Current.UserGuid = Guid.NewGuid().ToString();
                }

                if (String.IsNullOrEmpty(Current.DeviceID))
                {
                    Current.DeviceID = Platform.Current.GetDeviceID();
                }

                if (String.IsNullOrEmpty(Current.DisplayMode))
                {
                    Current.DisplayMode = Platform.Current.PreferFullMode;
                }
                if (Current.MusicVolume == null)
                {
                    Current.MusicVolume = 70;
                }
                if (Current.SoundVolume == null)
                {
                    Current.SoundVolume = 50;
                }

                //if (Current.NewsBoard == null)
                //{
                //    Current.NewsBoard = new NewsBoard() { Detail = "游戏公告加载中，请稍候……" };
                //}

                if (String.IsNullOrEmpty(Current.Language))
                {
                    string name = "";
                    try
                    {
                        name = Platform.Current.CurrentLanguage; // System.Globalization.CultureInfo.InstalledUICulture.Name;
                    }
                    catch (Exception ex)
                    {
                        //獲取系統語言失敗
                    }
                    if (name.ToLower().Contains("cn"))  // == "zh-cn")
                    {
                        Current.Language = "cn";
                    }
                    else
                    {
                        Current.Language = "tw";
                    }
                }

                if (Current.GlobalVariables == null)
                {
                    Current.GlobalVariables = Session.globalVariablesBasic.Clone();
                }

                if (String.IsNullOrEmpty(Session.Resolution))  // Season.PlatForm == PlatForm.iOS || Season.PlatForm == PlatForm.WinRT || Season.PlatForm == PlatForm.WP)
                {
                    Session.Resolution = Platform.PreferResolution;
                }
                Session.RealResolution = Session.Resolution = Setting.Current.Resolution;

            }
        }

    }
}
