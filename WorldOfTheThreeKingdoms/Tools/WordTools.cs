using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Net.Mail;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using Newtonsoft.Json;
//using Microsoft.VisualBasic;

namespace Tools
{
    public static class WordTools
    {
        private static string[,] translationWordsString;
        public static string[,] TranslationWordsString
        {
            get
            {
                if (translationWordsString == null)
                {
                    translationWordsString = GetTranslationWords();
                }
                return translationWordsString;
            }
        }

        public static string TranslationWords(this string content, bool ToTradition, bool ignoreNoChinese)
        {
            string[,] translationWords = TranslationWordsString; // GetTranslationWords();
            string strTxt = content;
            string str = "";
            string chars = "";
            Regex rx = new Regex("^[\u4e00-\u9fa5]$");
            for (int i = 0; i < strTxt.Length; i++)
            {
                chars = strTxt.Substring(i, 1);

                bool convert = true;

                if (ignoreNoChinese)
                {
                    if (!rx.IsMatch(chars))
                    {
                        convert = false;
                    }
                }

                if (convert)
                {
                    for (int k = 0; k < translationWords.Length / 2; k++)
                    {
                        if (translationWords[k, ToTradition ? 0 : 1] == chars)
                        {
                            chars = translationWords[k, ToTradition ? 1 : 0];
                            //exitfor;
                            break;
                        }
                    }
                }

                str = str + chars;
            }
            return str;
        }

        private static string[,] GetTranslationWords()
        {
            string txt = Platform.Current.LoadText("Content/Data/ChineseCodes.txt").Trim();
            string[,] aa = new string[txt.Length / 2, 2];
            int j = 0;
            for (int i = 0; i < txt.Length / 2; i++)
            {
                aa[i, 0] = txt.Substring(j, 1);
                j++;
                aa[i, 1] = txt.Substring(j, 1);
                j++;
            }
            return aa;
        }
        /// <summary>
        /// 将Html标签转化为空格
        /// </summary>
        /// <param name="strHtml">待转化的字符串</param>
        /// <returns>经过转化的字符串</returns>
        public static string StripHtml(string strHtml, int? length)
        {
            Regex objRegExp = new Regex("<(.|\n)+?>");
            string strOutput = objRegExp.Replace(strHtml, "");
            strOutput = strOutput.Replace("<", "&lt;");
            strOutput = strOutput.Replace(">", "&gt;");
            return strOutput;
            //string wordsOnly = strOutput.StringRemoveMoreSpace();
            //if (length != null && wordsOnly.Length > length) wordsOnly = wordsOnly.Substring(0, (int)length) + "....";
            //return wordsOnly;
        }
        /// <summary>
        /// 把所有空格變為一個空格并Trim
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringRemoveMoreSpace(this string str)
        {
            Regex r = new Regex(@"\s+");
            return r.Replace(str, " ").Trim();
        }

        public static string WordsSubString(this string str, int length, int dots = 2)
        {
			if (String.IsNullOrEmpty(str)) {
				return "";
			}
            string dotString = "";
            for (int i = 0; i < dots; i++)
            {
                dotString += ".";
            }
            return str.Length > length ? str.Substring(0, length) + dotString : str;
        }

        public static string SplitLineString(this string str, int length)
        {
            int resultRow = 0;
            int resultWidth = 0;
            return SplitLineString(str, length, 0, ref resultRow, ref resultWidth);
        }

        public static string SplitLineString(this string str, int length, int maxRow, ref int resultRow, ref int resultWidth, int dots = 2, int charWidth1 = 28, int charWidth2 = 14)
        {
            if (String.IsNullOrEmpty(str))
            {
                return String.Empty;
            }
            //if (str.Length < length)
            //{
            //    return str;
            //}
            char[] chars = str.ToCharArray();
            int row = 1;
            StringBuilder sb = new StringBuilder();
            resultWidth = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                char ch = chars[i];
                sb.Append(ch);

                int charWidth = ch > 128 ? charWidth1 : charWidth2;

                //if ((i + 1) % length == 0 && i != 0)
                if (resultWidth + charWidth > charWidth1 * (length-1) && i != 0)
                {
                    if (maxRow != 0 && row >= maxRow)
                    {
                        if (dots > 0 && sb.Length - dots >= 1)
                        {
                            sb.Remove(sb.Length - dots, 1);
                        }
                        for (int j = 0; j < dots; j++)
                        {                            
                            sb.Append(".");
                        }
                        break;
                    }
                    sb.Append("\r\n");
                    resultWidth = 0;
                    row++;
                }
                else
                {
                    resultWidth += charWidth;
                }
            }
            resultRow = row;
            return sb.ToString();
        }

        public static string[] SplitMultiLineString(this string str, int length)
        {
            if (str.Length < length)
            {
                return new string[] { str };
            }
            char[] chars = str.ToCharArray();
            List<string> list = new List<string>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < chars.Length; i++)
            {
                char ch = chars[i];
                sb.Append(ch);
                if ((i + 1) % length == 0 && i != 0)
                {
                    list.Add(sb.ToString());
                    sb.Clear();
                }
            }
            if (sb.Length > 0)
            {
                list.Add(sb.ToString());
            }
            return list.ToArray();
        }
        /// <summary>
        /// 用于载取中文字符串
        /// </summary>
        /// <param name="stringToBackSpace"></param>
        /// <returns></returns>
        public static string BackSpaceString(string stringToBackSpace)
        {
            if (String.IsNullOrEmpty(stringToBackSpace))
            {
                return String.Empty;
            }
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.None); // RegexOptions.Compiled);
            char[] stringChar = stringToBackSpace.ToCharArray();
            StringBuilder sb = new StringBuilder();
            int nLength = 0;
            for (int i = 0; i < stringChar.Length - 1; i++)
            {
                if (regex.IsMatch((stringChar[i]).ToString()))
                {
                    sb.Append(stringChar[i]);
                    nLength += 2;
                }
                else
                {
                    sb.Append(stringChar[i]);
                    nLength = nLength + 1;
                }
            }
            return sb.ToString();
        }

        public static int GetWordsLength(this string words, int charWidth1 = 28, int charWidth2 = 14)
        {
            int length = 0;
            if (String.IsNullOrEmpty(words))
            {

            }
            else
            {
                char[] chars = words.ToCharArray();
                foreach(var char0 in chars)
                {
                    int charWidth = char0 > 128 ? charWidth1 : charWidth2;
                    length += charWidth;
                }
                
            }
            return length;
        }


        /// <summary>
        /// JSON字符串格式化
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string JsonTree(string json)
        {
            int level = 0;
            var jsonArr = json.ToArray();  // Using System.Linq;
            string jsonTree = string.Empty;
            for (int i = 0; i < json.Length; i++)
            {
                char c = jsonArr[i];
                if (level > 0 && '\n' == jsonTree.ToArray()[jsonTree.Length - 1])
                {
                    jsonTree += TreeLevel(level);
                }
                switch (c)
                {
                    case '[':
                        jsonTree += c + "\n";
                        level++;
                        break;
                    case ',':
                        jsonTree += c + "\n";
                        break;
                    case ']':
                        jsonTree += "\n";
                        level--;
                        jsonTree += TreeLevel(level);
                        jsonTree += c;
                        break;
                    default:
                        jsonTree += c;
                        break;
                }
            }
            return jsonTree;
        }
        /// <summary>
        /// 树等级
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private static string TreeLevel(int level)
        {
            string leaf = string.Empty;
            for (int t = 0; t < level; t++)
            {
                leaf += "\t";
            }
            return leaf;
        }

        public static string ConvertJsonString(string str)
        {
            //格式化json字符串 
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,
                    IndentChar = ' '
                }
                ;
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

    }
}
