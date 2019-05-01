using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Platforms;

namespace Tools
{
    /// <summary>
    /// 序列化對象器
    /// </summary>
    public class SimpleSerializer
    {
        #region XMLSerializer
        public static string SerializeXML<T>(T t)
        {
            using (StringWriter sw = new StringWriter())
            {
                lock (Platform.SerializerLock)
                {
                    Platform.SessionActive = false;
                    XmlSerializer xz = new XmlSerializer(t.GetType());
                    xz.Serialize(sw, t);
                    Platform.SessionActive = true;
                }
                return sw.ToString();
            }
        }

        public static T DeserializeXML<T>(string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                XmlSerializer xz = new XmlSerializer(typeof(T));
                try
                {
                    lock (Platform.SerializerLock)
                    {
                        Platform.SessionActive = false;
                        var o = (T)xz.Deserialize(sr);
                        Platform.SessionActive = true;
                        return o;
                    }
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    return default(T);
                }
            }
        }

        public static void SerializeXML<T>(T t, string file)
        {
            string xml = SerializeXML(t);
            Platform.Current.SaveUserFile(file, xml);
        }

        public static T DeserializeXMLFile<T>(string file, bool isUserFile)
        {
            string content = isUserFile ? Platform.Current.GetUserText(file) : Platform.Current.LoadText(file);
            return DeserializeXML<T>(content);
        }

        #endregion

        #region JsonSerializer
        public static string SerializeJson<T>(T t, bool zip = false, bool Indented = false, bool Net = false)
        {
            if (Net)
            {
                string result = null;
                lock (Platform.SerializerLock)
                {
                    result = JsonConvert.SerializeObject(t, Indented ? Newtonsoft.Json.Formatting.Indented : Newtonsoft.Json.Formatting.None);
                }
                if (zip)
                {
                    result = result.GZipCompressString();
                }
                return result;
            }
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream stream = new MemoryStream())
                {
                    lock (Platform.SerializerLock)
                    {
                        serializer.WriteObject(stream, t);
                    }
                    var array = stream.ToArray();
                    string result = Encoding.UTF8.GetString(array, 0, array.Length);
                    if(Platform.PlatFormType== PlatFormType.Win)
                    {
                        result = result.Replace("{\"", "{\r\n\"");
                        result = result.Replace("[{", "[\r\n{");
                        result = result.Replace(",\"", ",\r\n\"");
                        result = result.Replace("}", "\r\n}");
                        result = result.Replace("},{", "},\r\n{");
                        result = result.Replace("}]", "}\r\n]");
                    }
                    else if (!Platform.IsMobilePlatForm)
                    {
                        result = result.Replace("{\"", "{\n\"");
                        result = result.Replace("[{", "[\n{");
                        result = result.Replace(",\"", ",\n\"");
                        result = result.Replace("}", "\n}");
                        result = result.Replace("},{", "},\n{");
                        result = result.Replace("}]", "}\n]");
                    }

                    if (zip)
                    {
                        result = result.GZipCompressString();
                    }
                    return result;
                }
            }
        }

        public static T DeserializeJson<T>(string s, bool zip = false, bool Net = false)
        {
            if (zip)
            {
                s = s.GZipDecompressString();
            }

            T t;
            if (Net)
            {
                try
                {
                    lock (Platform.SerializerLock)
                    {
                        t = JsonConvert.DeserializeObject<T>(s);
                    }
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    t = DeserializeJson<T>(s, false, false);
                }
            }
            else
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                byte[] buffer = Encoding.UTF8.GetBytes(s);
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    //try
                    //{
                    //lock (Platform.SerializerLock)
                    //{
                        t = (T)serializer.ReadObject(stream);
                    //}
                    //}
                    //catch (Exception ex)
                    //{
                    //    t = default(T);
                    //}
                }
            }
            return t;
        }

        public static bool SerializeJsonFile<T>(T t, string file, bool zip = false, bool Net = false, bool fullPathProvided = false)
        {
            try
            {
                string json = SerializeJson(t, zip, Net);
                Platform.Current.SaveUserFile(file, json, fullPathProvided);
                return true;
            }
            catch (Exception ex)
            {
#if DEBUG
                throw ex;
#else
                WebTools.TakeWarnMsg("序列用户对象失败:" + file, "SerializeJson:" + t.GetType(), ex);
                return false;
#endif
            }
        }

        public static T DeserializeJsonFile<T>(string file, bool isUserFile, bool zip = false, bool Net = false)
        {
            try
            {
                string content = isUserFile ? Platform.Current.GetUserText(file) : Platform.Current.LoadText(file);
                content = content.NullToString().Trim().Replace("\n", "").Replace("\r", "");

                //string str = WordTools.ConvertJsonString(content);

                return DeserializeJson<T>(content, zip, Net);
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("读取用户对象失败:" + file, "DeserializeJsonFile:" + file + " " + isUserFile, ex);
                return default(T);
            }
        }

#endregion

    }
}