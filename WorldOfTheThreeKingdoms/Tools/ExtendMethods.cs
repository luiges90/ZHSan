using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using GameManager;
namespace Tools
{
    public static class ExtendMethods
    {
        /// <summary>
        /// float转int小数自动进一位
        /// </summary>
        /// <param name="source">原float数</param>
        /// <returns>返回转换后的int</returns>
        public static int ConvertToIntPlus(this float source)
        {
            return Convert.ToInt32(source + 0.5);
        }
        
        public static string NullToString(this object o)
        {
            return NullToString(o, "");
        }

        public static string NullToString(this object o, string words)
        {
            return o == null || String.IsNullOrEmpty(o.ToString()) ? words : o.ToString();
        }

        public static string NullToStringTrim(this object o)
        {
            return NullToString(o, "").Trim();
        }

        public static string[] RemoveEmptyEntry(this string[] array)
        {
            return array.Where(ar => !String.IsNullOrEmpty(ar)).ToArray();
        }

        public static string[] RemoveEmptyEntryAndTrim(this string[] array)
        {
            if (array != null && array.Length > 0)
            {
                array = array.Select(ar => ar.NullToString().Trim()).Where(ar => !String.IsNullOrEmpty(ar)).NullToEmptyArray();
            }
            else
            {
                array = new string[] { };
            }
            return array;
        }

        /// 将 Stream 转成 byte[]
        public static byte[] StreamToBytes(this Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// 将 byte[] 转成 Stream
        public static Stream BytesToStream(this byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        public static DateTime? ToDateTime(this string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime))
            {
                return null;
            }
            else
            {
                return DateTime.Parse(dateTime);
            }
        }

        public static bool IsToday(this DateTime? dateTime)
        {
            return dateTime.IsDate(DateTime.Now.ToSeasonDate());
        }

        public static bool IsDate(this DateTime? dateTime, string date)
        {
            return dateTime != null && dateTime.ToSeasonDate() == date;
        }

        public static string ToSeasonDate(this string dateTime)
        {
            if (String.IsNullOrEmpty(dateTime))
            {

            }
            else
            {
                DateTime dt;
                if (DateTime.TryParse(dateTime, out dt))
                {
                    return dt.ToSeasonDate();
                }
            }
            return "";
        }

        public static string ToSeasonDate(this DateTime dateTime)
        {
            return ((DateTime)dateTime).ToString("yyyy-MM-dd");
        }

        public static string ToSeasonDate2(this DateTime dateTime)
        {
            return ((DateTime)dateTime).ToString("yyy-MM-dd");
        }

        public static string ToSeasonDate(this DateTime? dateTime)
        {
            return dateTime == null ? "" : ToSeasonDate((DateTime)dateTime);
        }

        public static string ToSeasonDateTimeTicks(this DateTime dateTime)
        {
            return ((DateTime)dateTime).ToString("yyyyMMddHHmmss");
        }

        public static string ToSeasonDateTime(this DateTime dateTime)
        {
            return ((DateTime)dateTime).ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToSeasonDateTime(this DateTime? dateTime)
        {
            return dateTime == null ? "" : ToSeasonDateTime((DateTime)dateTime);
        }

        public static string ToSeasonShortTime(this object dateTime)
        {
            if (dateTime is DateTime)
            {
                DateTime dt = (DateTime)dateTime;
                if (dt.ToSeasonDate() == DateTime.Now.ToSeasonDate())
                {
                    return dt.ToString("HH:mm:ss");
                }
                else
                {
                    return dt.ToSeasonDate();
                }
            }
            else
            {
                return "";
            }
        }

        //public static List<T> NullToEmptyList<T>(this List<T> list)
        //{
        //    return list == null ? new List<T>() : list;
        //}

        public static List<T> NullToEmptyList<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>() : list.ToList();
        }

        public static T[] NullToEmptyArray<T>(this IEnumerable<T> list)
        {
            return list == null ? new T[] { } : list.ToArray();
        }

        public static List<T> CloneList<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>() : list.Select(li => li).NullToEmptyList();
        }

        public static string GetExceptionDetail(this Exception ex, bool stackTrace = true)
        {
            return (ex != null ? (ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "") + (stackTrace ? " " + ex.StackTrace : "")) : "");
        }

    }
}
