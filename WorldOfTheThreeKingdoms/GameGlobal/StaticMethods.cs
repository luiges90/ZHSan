using GameManager;
using MersenneTwister;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
////using System.Drawing;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;


namespace GameGlobal
{
    public class Font
    {
        public string Name { get; set; }
        public float Size = 14;
        public string Style { get; set; }

        public float Scale
        {
            get
            {
                return Size / 20f;
            }
        }

        public Font()
        {

        }

        public Font(string name, float size, string style)
        {
            Name = name;
            Size = size;
            Style = style;
        }

        public Vector2 GetWidthHeight(string text)
        {
            //return TextManager.GetWidthHeight(text, CacheManager.FontPair, 1f);
            float width = 0f;

            float scale = (Size == 0f ? 14 : Size) / 20;

            var chars = text.ToCharArray();

            foreach (char ch in chars)
            {
                width += (ch > 128 ? 28 : 14);
            }
            return new Vector2(Convert.ToInt32(width * scale), 25 * scale);
        }

        public void SetFreeTextBuilder(Font font)
        {
            Name = font.Name;
            Size = font.Size;
            Style = font.Style;
        }
    }

    public class StaticMethods
    {
        public static Random RandomDigit = Randoms.Create();

        public static void AdjustRectangleInViewport(ref Microsoft.Xna.Framework.Rectangle rect)
        {
            if (rect.Left < 0)
            {
                rect.X += rect.Width;
            }
            if (rect.Top < 0)
            {
                rect.Y += rect.Height;
            }
        }

        public static void AdjustRectangleInViewport(ref Microsoft.Xna.Framework.Rectangle rect, Microsoft.Xna.Framework.Point viewportSize)
        {
            if (rect.Right > viewportSize.X)
            {
                rect.X -= rect.Width;
            }
            if (rect.Bottom > viewportSize.Y)
            {
                rect.Y -= rect.Height;
            }
        }

        public static Microsoft.Xna.Framework.Rectangle CenterRectangle(Microsoft.Xna.Framework.Rectangle desRectangle, Microsoft.Xna.Framework.Rectangle rectangleToBeCentered)
        {
            return new Microsoft.Xna.Framework.Rectangle(desRectangle.Left + ((desRectangle.Width - rectangleToBeCentered.Width) / 2), desRectangle.Top + (((desRectangle.Height - rectangleToBeCentered.Height) * 2) / 3), rectangleToBeCentered.Width, rectangleToBeCentered.Height);
        }

        public static bool GetBoolMethodValue(object ClassInstance, string methodName, params object[] param)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(methodName);
            return ((method != null) && ((bool) method.Invoke(ClassInstance, param)));
        }

        public static Microsoft.Xna.Framework.Rectangle GetBottomLeftRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left, rectDes.Bottom - rect.Height, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetBottomRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left + ((rectDes.Width - rect.Width) / 2), rectDes.Bottom - rect.Height, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetBottomRightRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Right - rect.Width, rectDes.Bottom - rect.Height, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetCenterRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left + ((rectDes.Width - rect.Width) / 2), rectDes.Top + ((rectDes.Height - rect.Height) / 2), rect.Width, rect.Height);
        }

        //public static object GetConstValue(Type type, string PropertyName)
        //{
        //    FieldInfo field = type.GetField(PropertyName, BindingFlags.Public | BindingFlags.Static);
        //    if (field != null)
        //    {
        //        return field.GetRawConstantValue();
        //    }
        //    return null;
        //}

        public static ContextMenuResult GetContextMenuResultByName(string Name)
        {
            try
            {
                return (ContextMenuResult) Enum.Parse(typeof(ContextMenuResult), Name, false);
            }
            catch
            {
                return ContextMenuResult.None;
            }
        }

        public static Microsoft.Xna.Framework.Rectangle GetLeftRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left, rectDes.Top + ((rectDes.Height - rect.Height) / 2), rect.Width, rect.Height);
        }

        public static object GetListMethodValue(object ClassInstance, string methodName)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(methodName);
            if (method != null)
            {
                return method.Invoke(ClassInstance, null);
            }
            return null;
        }

        public static string GetNumberStringByGranularity(int number, int granularity)
        {
            int num = number / granularity;
            int num2 = granularity * num;
            return (num2.ToString() + "↑");
        }

        public static string GetPercentString(float rate, int digits)
        {
            return (Math.Round((double) (rate * 100f), digits) + "%");
        }

        public static object GetMethodValue(object ClassInstance, string MethodName, object[] param)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(MethodName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (method != null)
            {
                try
                {
                    object obj = method.Invoke(ClassInstance, param);
                    if (obj != null)
                    {
                        return obj;
                    }
                    return "----";
                }
                catch
                {
                    return "----";
                }
            }
            return "----";
        }

        public static object GetPropertyValue(object ClassInstance, string PropertyName)
        {
            PropertyInfo property = ClassInstance.GetType().GetProperty(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (property != null)
            {
                try
                {
                    object obj2 = property.GetValue(ClassInstance, null);
                    if (obj2 != null)
                    {
                        return obj2;
                    }
                    return "----";
                }
                catch
                {
                    return "----";
                }
            }
            else
            {
                FieldInfo field = ClassInstance.GetType().GetField(PropertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (field != null){
                    try
                    {
                        object obj2 = field.GetValue(ClassInstance);
                        if (obj2 != null)
                        {
                            return obj2;
                        }
                        return "----";
                    }
                    catch
                    {
                        return "----";
                    }
                }
            }
            return "----";
        }

        public static int GetRandomValue(int a, int b)
        {
            int num;
            int num2;
            if (b == 0)
            {
                return 0;
            }
            if (b > 0)
            {
                num = a / b;
                num2 = a % b;
                if ((num2 > 0) && (Random(b) < num2))
                {
                    num++;
                }
                return num;
            }
            b = Math.Abs(b);
            num = a / b;
            num2 = a % b;
            if ((num2 > 0) && (Random(b) < num2))
            {
                num++;
            }
            return -num;
        }

        public static int GetBigRandomValue(int a, int  b)
        {
            int num;
            int num2;
            if (b == 0)
            {
                return 0;
            }
            if (b > 0)
            {
                num =(int) (a / b);
                num2 =(int) (a % b);
                if ((num2 > 0) && (Random((int) b) < num2))
                {
                    num++;
                }
                return num;
            }
            //b = Math.Abs(b);
            b = -b;
            num = (int)(a / b);
            num2 = (int)(a % b);
            if ((num2 > 0) && (Random((int) b) < num2))
            {
                num++;
            }
            return -num;
        }

        public static Microsoft.Xna.Framework.Rectangle GetRectangleFitViewport(int width, int height, Microsoft.Xna.Framework.Point viewportSize)
        {
            int x = width;
            int y = height;
            if (viewportSize.X < width)
            {
                x = viewportSize.X;
            }
            if (viewportSize.Y < height)
            {
                y = viewportSize.Y;
            }
            return new Microsoft.Xna.Framework.Rectangle((viewportSize.X - x) / 2, (viewportSize.Y - y) / 2, x, y);
        }

        public static Microsoft.Xna.Framework.Rectangle GetRightRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Right - rect.Width, rectDes.Top + ((rectDes.Height - rect.Height) / 2), rect.Width, rect.Height);
        }

        public static string GetStringMethodValue(object ClassInstance, string methodName, params object[] param)
        {
            MethodInfo method = ClassInstance.GetType().GetMethod(methodName);
            if (method != null)
            {
                return method.Invoke(ClassInstance, param).ToString();
            }
            return "----";
        }

        public static Microsoft.Xna.Framework.Rectangle GetTopLeftRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left, rectDes.Top, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetTopRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Left + ((rectDes.Width - rect.Width) / 2), rectDes.Top, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetTopRightRectangle(Microsoft.Xna.Framework.Rectangle rectDes, Microsoft.Xna.Framework.Rectangle rect)
        {
            return new Microsoft.Xna.Framework.Rectangle(rectDes.Right - rect.Width, rectDes.Top, rect.Width, rect.Height);
        }

        public static Microsoft.Xna.Framework.Rectangle GetViewportCenterRectangle(int width, int height, Microsoft.Xna.Framework.Point viewportSize)
        {
            return new Microsoft.Xna.Framework.Rectangle((viewportSize.X - width) / 2, (viewportSize.Y - height) / 2, width, height);
        }

        public static Microsoft.Xna.Framework.Rectangle LeftRectangle(Microsoft.Xna.Framework.Rectangle desRectangle, Microsoft.Xna.Framework.Rectangle rectangleToBeLefted)
        {
            return new Microsoft.Xna.Framework.Rectangle(desRectangle.Left, desRectangle.Top + (((desRectangle.Height - rectangleToBeLefted.Height) * 2) / 3), rectangleToBeLefted.Width, rectangleToBeLefted.Height);
        }

        public static void LoadFontAndColorFromXMLNode(XmlNode node, out Font font, out Microsoft.Xna.Framework.Color color)
        {
            font = new Font(node.Attributes.GetNamedItem("FontName").Value, float.Parse(node.Attributes.GetNamedItem("FontSize").Value), node.Attributes.GetNamedItem("FontStyle").Value);
            color = new Microsoft.Xna.Framework.Color();
            uint x = uint.Parse(node.Attributes.GetNamedItem("FontColor").Value);
            color.PackedValue = (x & 0xFF000000) | ((x & 0x00FF0000) >> 16) | (x & 0x0000FF00) | ((x & 0x000000FF) << 16);
        }

        public static Microsoft.Xna.Framework.Color LoadColor(String code)
        {
            Microsoft.Xna.Framework.Color color = new Microsoft.Xna.Framework.Color();
            uint x = uint.Parse(code);
            color.PackedValue = (x & 0xFF000000) | ((x & 0x00FF0000) >> 16) | (x & 0x0000FF00) | ((x & 0x000000FF) << 16);
            return color;
        }

        public static Microsoft.Xna.Framework.Point? LoadFromString(string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 2)
            {
                return new Microsoft.Xna.Framework.Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
            }
            return null;
        }

        public static void LoadFromString(out int[] intArray, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                intArray[i] = int.Parse(strArray[i]);
            }
        }

        public static void LoadFromString(List<Microsoft.Xna.Framework.Point> pointList, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            pointList.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                pointList.Add(new Microsoft.Xna.Framework.Point(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
            }
        }

        public static void LoadFromString(List<int> intList, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            intList.Clear();
            for (int i = 0; i < strArray.Length; i++)
            {
                intList.Add(int.Parse(strArray[i]));
            }
        }

        public static void LoadFromString(List<string> list, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            list.Clear();
            for (int i = 0; i < strArray.Length; i++)
            {
                list.Add(strArray[i]);
            }
        }

        public static void LoadFromString(Dictionary<int, int> list, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            list.Clear();
            if(strArray.Length>=2)
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    list.Add(int.Parse(strArray[i]), int.Parse(strArray[i + 1]));
                }
            }
        }

        public static Microsoft.Xna.Framework.Rectangle LoadRectangleFromXMLNode(XmlNode node)
        {
            return new Microsoft.Xna.Framework.Rectangle(int.Parse(node.Attributes.GetNamedItem("X").Value), int.Parse(node.Attributes.GetNamedItem("Y").Value), int.Parse(node.Attributes.GetNamedItem("Width").Value), int.Parse(node.Attributes.GetNamedItem("Height").Value));
        }

        public static bool PointInRectangle(Microsoft.Xna.Framework.Point point, Microsoft.Xna.Framework.Rectangle rect)
        {
            if (CacheManager.Scale != Vector2.One)
            {
                //return ((((point.X * CacheManager.Scale.X > rect.Left * CacheManager.Scale.X) && (point.Y * CacheManager.Scale.Y > rect.Top * CacheManager.Scale.Y)) && (point.X * CacheManager.Scale.X < rect.Right * CacheManager.Scale.X)) && (point.Y * CacheManager.Scale.Y < rect.Bottom * CacheManager.Scale.Y));
                return ((((point.X > rect.Left * CacheManager.Scale.X) && (point.Y > rect.Top * CacheManager.Scale.Y)) && (point.X < rect.Right * CacheManager.Scale.X)) && (point.Y < rect.Bottom * CacheManager.Scale.Y));
            }
            else
            {
                return ((((point.X > rect.Left) && (point.Y > rect.Top)) && (point.X < rect.Right)) && (point.Y < rect.Bottom));
            }
        }

        public static bool PointInViewport(Microsoft.Xna.Framework.Point position, Microsoft.Xna.Framework.Point viewportSize)
        {
            return PointInRectangle(position, new Microsoft.Xna.Framework.Rectangle(-1, -1, viewportSize.X + 1, viewportSize.Y + 1));
        }

        public static int Random(int maxValue)
        {
            if (maxValue <= 0)
            {
                return 0;
            }
            return RandomDigit.Next(maxValue);
        }

        public static double Random()
        {
            return RandomDigit.NextDouble();
        }

        public static bool RectangleInViewport(Microsoft.Xna.Framework.Rectangle rect, Microsoft.Xna.Framework.Point viewportSize)
        {
            if ((((rect.Left >= viewportSize.X) || (rect.Right <= 0)) || (rect.Top >= viewportSize.Y)) || (rect.Bottom <= 0))
            {
                return false;
            }
            return true;
        }

        public static Microsoft.Xna.Framework.Rectangle RightRectangle(Microsoft.Xna.Framework.Rectangle desRectangle, Microsoft.Xna.Framework.Rectangle rectangleToBeRighted)
        {
            return new Microsoft.Xna.Framework.Rectangle(desRectangle.Right - rectangleToBeRighted.Width, desRectangle.Top + (((desRectangle.Height - rectangleToBeRighted.Height) * 2) / 3), rectangleToBeRighted.Width, rectangleToBeRighted.Height);
        }

        public static string SaveToString(int[] intArray)
        {
            if (intArray == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < intArray.Length; i++)
            {
                builder.Append(intArray[i].ToString() + " ");
            }
            return builder.ToString();
        }

        public static string SaveToString(List<Microsoft.Xna.Framework.Point> pointList)
        {
            if (pointList == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (Microsoft.Xna.Framework.Point point in pointList)
            {
                builder.Append(point.X.ToString() + " " + point.Y.ToString() + " ");
            }
            return builder.ToString();
        }

        public static string SaveToString(List<int> intList)
        {
            if (intList == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (int num in intList)
            {
                builder.Append(num.ToString() + " ");
            }
            return builder.ToString();
        }

        public static string SaveToString(List<string> list)
        {
            if (list == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (string str in list)
            {
                builder.Append(str + " ");
            }
            return builder.ToString();
        }

        public static string SaveToString(Microsoft.Xna.Framework.Point? point)
        {
            if (point.HasValue)
            {
                return (point.Value.X.ToString() + " " + point.Value.Y.ToString());
            }
            return string.Empty;
        }

        public static string SaveToString(Dictionary<int, int> intList) 
        {
            if (intList == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<int, int> num in intList)
            {
                builder.Append(num.Key.ToString() + " " + num.Value.ToString() + " ");
            }
            return builder.ToString();
        }

        public static string SaveToString(List<KeyValuePair<int, int>> List)
        {
            if (List == null)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<int, int> num in List)
            {
                builder.Append(num.Key.ToString() + " " + num.Value.ToString() + " ");
            }
            return builder.ToString();
        }
        public static void LoadFromString(List<KeyValuePair<int, int>> list, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            list.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                list.Add(new KeyValuePair<int, int>(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
            }
        }
        public static GameObjects.zainanlei LoadzainanfromString(string zainanstring)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = zainanstring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            GameObjects.zainanlei zainan = new GameObjects.zainanlei();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                zainan.zainanleixing = int.Parse(strArray[i]);
                zainan.shengyutianshu = int.Parse(strArray[i + 1]);
            }
            return zainan;
        }
    }
}

