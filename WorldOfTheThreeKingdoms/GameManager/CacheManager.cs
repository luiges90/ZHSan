using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tools;
using FontStashSharp;
namespace GameManager
{
    public class PlatformTexture
    {
        public string Name { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }

    public struct PlatformColor
    {
        public static Color DarkRed = new Color(139, 0, 0); //; new Color(179, 53, 26);
        public static Color DarkRed2 = new Color(204, 0, 0); //; new Color(179, 53, 26);
        public static Color DarkBlue = new Color(18, 58, 83); //; new Color(179, 53, 26);
        public static Color DarkGreen = new Color(64, 71, 44);
        public static Color DarkGreen2 = new Color(24, 52, 36);
        public static Color DarkOrange = new Color(149, 80, 30);
        public static Color WhiteSilver = new Color(238, 231, 205);
        public static Color ShadowBlack = new Color(29, 29, 13);
        public static Color ShadowBlack2 = new Color(112, 114, 113);
        public static Color Orange = new Color(170, 92, 26);
        public static Color DarkCyan = new Color(62, 126, 144);
        public static Color CyanWhite = new Color(204, 204, 0);
        public static Color PinkRed = new Color(217, 51, 51);
        public static Color ShadowYellow = new Color(239, 224, 175);
    }

    public enum TextureShape
    {
        None,
        Circle,
        Diamond,
        Ellipse
    }

    public enum CacheType
    {
        Live,
        Scene,
        Page,
        Temp,
        None
    }

    public static class CacheManager
    {
        public static object CacheLock = new Object();

        static Dictionary<string, Texture2D> TextureDics = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Texture2D> TextureTempDics = new Dictionary<string, Texture2D>();

        public static Dictionary<string, string> DicTexts = new Dictionary<string, string>();

        public static Vector2 Scale = Vector2.One;

        public static FontPair FontPair = new FontPair()
        {
            Name = @"Content\Font\FZLB_GBK.TTF",
            Size = 30,
            Style = "",
            Width = 30,
            Height = 32
        };

        public static void Clear(CacheType type)
        {
            lock (CacheLock)
            {
                if (type != CacheType.Temp && DicTexts != null)
                {
                    lock (DicTexts)
                    {
                        DicTexts.Clear();
                    }
                }

                //CoreGame.Current.AudioContent.Unload();
                foreach (var tex in TextureTempDics)
                {
                    if (tex.Value != null && !tex.Value.IsDisposed)
                    {
                        tex.Value.Dispose();
                    }
                }

                //TextureTempDics.ToList().ForEach(te => te.Value.Dispose());
                TextureTempDics.Clear();

                var dics = TextureDics.ToList();
                for (int i = dics.Count - 1; i >= 0; i--)
                {
                    var tex = dics[i];
                    var rec = Session.TextureRecs.FirstOrDefault(te => te.Key.Split('#')[0] == tex.Key).Value;
                    if (type == CacheType.Live && (rec.CacheType == "Live" || rec.CacheType == "Scene" || rec.CacheType == "Page" || rec.CacheType == "Temp") ||
                        type == CacheType.Scene && (rec.CacheType == "Scene" || rec.CacheType == "Page" || rec.CacheType == "Temp") ||
                        type == CacheType.Page && (rec.CacheType == "Page" || rec.CacheType == "Temp") ||
                        type == CacheType.Temp && (rec.CacheType == "Temp"))
                    {
                        if (tex.Value != null && !tex.Value.IsDisposed)
                        {
                            tex.Value.Dispose();
                        }
                        TextureDics.Remove(tex.Key);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(rec.CacheType))
                        {
                            //這應該是用戶材質
                            TextureDics.Remove(tex.Key);
                        }
                        //去除空或已經失效的材質
                        if (tex.Value == null || tex.Value.IsDisposed)
                        {
                            TextureDics.Remove(tex.Key);
                        }
                    }
                }
            }
            if (type == CacheType.Page || type == CacheType.Scene)
            {
                try
                {
                    Session.Current.SoundContent.Unload();
                    //Session.Current.MusicContent.Unload();
                    Session.Current.Content.Unload();
                }
                catch (Exception ex)
                {
                    WebTools.TakeWarnMsg("清空声音缓存失败:" + type, "SoundContent.Unload:", ex);
                }
            }
        }

        public static void RemoveTempDics(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                lock (CacheLock)
                {
                    if (TextureTempDics.ContainsKey(key))
                    {
                        Texture2D tex = TextureTempDics[key];
                        TextureTempDics.Remove(key);
                        if (tex != null && tex.IsDisposed)
                        {
                            tex.Dispose();
                        }
                        tex = null;
                    }
                }
            }
        }

        public static void Remove(string key)
        {
            if (!String.IsNullOrEmpty(key))
            {
                lock (CacheLock)
                {
                    if (TextureDics.ContainsKey(key))
                    {
                        Texture2D tex = TextureDics[key];
                        TextureDics.Remove(key);
                        if (tex != null && tex.IsDisposed)
                        {
                            tex.Dispose();
                        }
                        tex = null;
                    }
                }
            }
        }

        //static void Remove(Texture2D tex)
        //{
        //    if (tex != null)
        //    {
        //        if (TextureDics.ContainsValue(tex))
        //        {
        //            var pa = (from pair in TextureDics where pair.Value == tex select pair).FirstOrDefault();
        //            TextureDics.Remove(pa.Key);
        //        }
        //        if (!tex.IsDisposed)
        //        {
        //            tex.Dispose();
        //        }
        //        tex = null;
        //    }
        //}

        public static PlatformTexture GetTempTexture(string name)
        {
            return new PlatformTexture()
            {
                Name = name
            };
        }

        //public static Texture2D LoadTempTexture(string name)
        //{
        //    return Platform.Current.LoadTexture(name, false);
        //}

        public static Texture2D LoadAvatar(string name, bool isUser, bool isTemp, TextureShape shape, float[] shapeParms)
        {
            return LoadTexture(name, isUser, isTemp, shape, shapeParms);
        }

        public static Texture2D LoadTexture(string name)
        {
            return LoadTexture(name, false, false, TextureShape.None, null);
        }

        static Texture2D LoadTexture(string name, bool isUser, bool isTemp, TextureShape shape, float[] shapeParms)
        {
            var dics = isTemp ? TextureTempDics : TextureDics;
            Texture2D tex = null;
            bool reload = false;
            lock (dics)
            {
                if (dics != null && (!dics.TryGetValue(name, out tex) || tex != null && tex.IsDisposed))
                {
                    reload = true;
                    if (tex != null) dics.Remove(name);
                }
            }
            if (reload)
            {
                string res = "";

                if (isUser)
                {
                    res = name;
                }
                else
                {
                    //res = "Textures" + Platform.Current.GetSlash + name;
                    res = name;
                    if (!name.Contains("."))
                    {
                        TextureRecs rec = Session.TextureRecs.FirstOrDefault(te => te.Key.Split('#')[0] == name).Value;
                        res = res + "." + rec.Ext;
                    }
                    else
                    {

                    }
                }

                tex = Platform.Current.LoadTexture(res, isUser);

                GameTools.ProcessTextureShape(tex, shape, shapeParms);

                lock (dics)
                {
                    if (tex == null)
                    {
                        dics.Add(name, null);
                    }
                    else
                    {
                        dics.Add(name, tex);
                    }
                }
            }
            return tex;
        }

        public static Texture2D LoadShapeTexture(string name, bool isTemp, TextureShape shape, float[] shapeParms)
        {
            var dics = isTemp ? TextureTempDics : TextureDics;
            Texture2D tex = null;
            bool reload = false;
            lock (dics)
            {
                if (dics != null && (!dics.TryGetValue(name, out tex) || tex != null && tex.IsDisposed))
                {
                    reload = true;
                    if (tex != null) dics.Remove(name);
                }
            }
            if (reload)
            {
                tex = GameTools.CreateShapeTexture(shape, Color.White, shapeParms);

                lock (dics)
                {
                    if (tex == null)
                    {
                        dics.Add(name, null);
                    }
                    else
                    {
                        dics.Add(name, tex);
                    }
                }
            }
            return tex;
        }

        public static void Draw(string name, Vector2 pos, Color color)
        {
            Texture2D tex = LoadTexture(name);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, color);
            }
        }

        public static void Draw(string name, Vector2 pos, Rectangle? source, Color color)
        {
            Draw(name, pos, source, color, SpriteEffects.None, 1f);
        }

        public static Bounds Draw(string name, Vector2 pos, Rectangle? source, Color color, SpriteEffects effect, float scale, float depth = 0f)
        {
            Texture2D tex = LoadTexture(name);

            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, source, color, 0f, Vector2.Zero, scale, effect, depth);
            }

            if (source == null)
                return new Bounds();
            return new Bounds() { X = pos.X, Y = pos.Y, X2 = pos.X + source.Value.Width * scale, Y2 = pos.Y + source.Value.Height * scale };
        }

        public static void Draw(string name, Vector2 pos, Rectangle? source, Color color, SpriteEffects effect, Vector2 scale, float depth = 0f)
        {
            Texture2D tex = LoadTexture(name);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, source, color, 0f, Vector2.Zero, scale, effect, depth);
            }
            else
            {

            }

        }

        public static void Draw(string name, Vector2 pos, Rectangle? source, Color color, float rotation, SpriteEffects effect, Vector2 scale)
        {
            Texture2D tex = LoadTexture(name);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, source, color, rotation, Vector2.Zero, scale, effect, 0f);
            }
        }

        public static void Draw(PlatformTexture platformTexture, Vector2 pos, Rectangle? source, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effect, float depth)
        {
            if (platformTexture != null && !String.IsNullOrEmpty(platformTexture.Name))
            {
                Texture2D tex = LoadTexture(platformTexture.Name);
                if (tex != null && !tex.IsDisposed)
                {
                    Session.Current.SpriteBatch.Draw(tex, pos, source, color, rotation, origin, scale, effect, depth);
                }
            }
        }

        public static void Draw(string text, Rectangle rec, Rectangle? source, Color color, float rotation, Vector2 origin, SpriteEffects effect, float depth)
        {
            var texture = new PlatformTexture()
            {
                Name = text
            };
            Draw(texture, rec, source, color, rotation, origin, effect, depth);
        }


        public static void Draw(PlatformTexture platformTexture, Rectangle rec, Rectangle? source, Color color, float rotation, Vector2 origin, SpriteEffects effect, float depth)
        {
            if (platformTexture != null && !String.IsNullOrEmpty(platformTexture.Name))
            {
                Texture2D tex = LoadTexture(platformTexture.Name);
                if (tex != null && !tex.IsDisposed)
                {
                    if (Scale != Vector2.One)
                    {
                        rec = new Rectangle(Convert.ToInt16(rec.X * Scale.X), Convert.ToInt16(rec.Y * Scale.Y), Convert.ToInt16(rec.Width * Scale.X), Convert.ToInt16(rec.Height * Scale.Y));
                    }
                    Session.Current.SpriteBatch.Draw(tex, rec, source, color, rotation, origin, effect, depth);
                }
            }
        }

        public static void Draw(string name, Rectangle dest, Color color)
        {
            Texture2D tex = LoadTexture(name);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, dest, color);
            }
        }

        public static void Draw(string name, string sec, Vector2 pos, Color color)
        {
            Draw(name, sec, pos, color, new Vector2(1, 1));
        }

        public static void Draw(string name, string sec, Vector2 pos, Color color, Vector2 scale)
        {
            Texture2D tex = LoadTexture(name);
            if (tex != null && !tex.IsDisposed)
            {
                if (Session.TextureRecs.ContainsKey(name + "#" + sec))
                {
                    Session.Current.SpriteBatch.Draw(tex, pos, Session.TextureRecs[name + "#" + sec].Recs[0], color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }

        public static void DrawGeneralAvatar(string id, Vector2 pos, float alpha, float? scale = null, bool isTemp = true, TextureShape shape = TextureShape.None, float[] shapeParams = null)
        {
            if (String.IsNullOrEmpty(id))
            {
                CacheManager.DrawAvatar(@"Avatars\General\Custom.jpg", pos + new Vector2(3, 3), Color.White * alpha, scale == null ? 1f : (float)scale, false, isTemp, shape, shapeParams);
            }
            else if (id.StartsWith("Avatar"))
            {
                if (!id.Contains("."))
                {
                    id = id + ".jpg";
                }
                CacheManager.DrawAvatar(id, pos + new Vector2(3, 3), Color.White * alpha, scale == null ? 0.5f : 0.5f * (float)scale, true, isTemp, shape, shapeParams);
            }
            else
            {
                if (id.Contains('-'))
                {
                    id = id.Split('-')[1];
                }
                CacheManager.DrawAvatar(@"Avatars\General\" + id + ".jpg", pos + new Vector2(3, 3), Color.White * alpha, scale == null ? 1f : (float)scale, false, isTemp, shape, shapeParams);
            }
        }

        public static void DrawAvatar(string name, Vector2 pos, Color color, float scale, bool isUser = false, bool isTemp = true, TextureShape shape = TextureShape.None, float[] shapeParams = null)
        {
            if (!String.IsNullOrEmpty(name))
            {
                Texture2D tex = LoadAvatar(name, isUser, isTemp, shape, shapeParams);
                if (tex != null && !tex.IsDisposed)
                {
                    Session.Current.SpriteBatch.Draw(tex, pos, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                }
            }
        }

        public static void DrawAvatar(string name, Rectangle pos, Color color, bool isUser = false, bool isTemp = true, TextureShape shape = TextureShape.None, float[] shapeParams = null, float depth = 0f)
        {
            if (!String.IsNullOrEmpty(name))
            {
                Texture2D tex = LoadAvatar(name, isUser, isTemp, shape, shapeParams);
                if (tex != null && !tex.IsDisposed)
                {
                    if (Scale != Vector2.One)
                    {
                        pos = new Rectangle(Convert.ToInt16(pos.X * Scale.X), Convert.ToInt16(pos.Y * Scale.Y), Convert.ToInt16(pos.Width * Scale.X), Convert.ToInt16(pos.Height * Scale.Y));
                    }
                    Session.Current.SpriteBatch.Draw(tex, pos, null, color, 0f, Vector2.Zero, SpriteEffects.None, depth);
                }
            }
        }

        public static void DrawAvatar(string name, Vector2 pos, Color color, Vector2 scale, Rectangle? source = null, bool isUser = false, bool isTemp = true)
        {
            Texture2D tex = LoadAvatar(name, isUser, isTemp, TextureShape.None, null);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, source, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public static void DrawZhsanAvatar(Person person, string type, Rectangle pos, Color color, float depth)
        {
            if (person == null)
            {
                return;
            }
            float pictureID = Convert.ToSingle(person.PictureIndex);

            //if (person.Age >= 50)
            //{
            //    pictureID += 0.5f;
            //}
            //if (person.Age <= 20)
            //{
            //    pictureID += 0.2f;
            //}
            DrawZhsanAvatar(pictureID, person.FallbackPictureIndex, type, pos, color, depth);
        }

        public static void DrawZhsanAvatar(float pictureIndex, int fallbackIndex, string type, Rectangle pos, Color color, float depth)
        {
            if (type == "f" || type == "t")
            {
                type = "";
            }

            string id = String.Format("Content/Textures/GameComponents/PersonPortrait/Images/Player/{0}{1}.jpg", Convert.ToInt32(pictureIndex), type);
            if (!(Setting.Current == null || String.IsNullOrEmpty(Setting.Current.MODRuntime)))
            {
                id = id.Replace("Content", "MODs/" + Setting.Current.MODRuntime);
            }

            if (Platform.Current.FileExists(id))
            {

            }
            else
            {
                id = String.Format(@"Content/Textures/GameComponents/PersonPortrait/Images/Default/{0}{1}.jpg", Convert.ToInt32(pictureIndex), type);
                if (!(Setting.Current == null || String.IsNullOrEmpty(Setting.Current.MODRuntime)))
                {
                    id = id.Replace("Content", "MODs/" + Setting.Current.MODRuntime);
                }

                if (Platform.Current.FileExists(id))
                {

                }
                else
                {
                    id = String.Format(@"Content/Textures/GameComponents/PersonPortrait/Images/Default/{0}{1}.jpg", Convert.ToInt32(pictureIndex), "");
                    if (Platform.Current.FileExists(id))
                    {

                    }
                    else
                    {
                        id = String.Format(@"Content/Textures/GameComponents/PersonPortrait/Images/Default/{0}{1}.jpg", fallbackIndex, "");
                    }
                }
            }

            DrawAvatar(id, pos, Color.White, false, true, TextureShape.None, null, depth);
        }

        public static void DrawShape(string name, TextureShape shape, float[] shapeParms, Vector2 pos, Color color, Vector2 scale, Rectangle? source = null, bool isTemp = true)
        {
            Texture2D tex = LoadShapeTexture(name, isTemp, shape, shapeParms);
            if (tex != null && !tex.IsDisposed)
            {
                Session.Current.SpriteBatch.Draw(tex, pos, source, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public static string CheckTextCache(SpriteFont font, string text, bool checkTradition, bool upload)
        {
            text = text.NullToString();

            lock (DicTexts)
            {
                if (DicTexts == null)
                {
                    DicTexts = new Dictionary<string, string>();
                }

                if (!DicTexts.ContainsKey(text))
                {
                    string origin = text;

                    if (checkTradition)
                    {
                        //繁轉成簡
                        //text = text.TranslationWords(false, true);
                    }

                    DicTexts.Add(origin, text);
                }
                if (DicTexts.ContainsKey(text))
                {
                    string tex = DicTexts[text];
                    return !String.IsNullOrEmpty(tex) ? tex : text;
                }
                else
                {
                    return text;
                }
            }
        }

        public static void DrawString(SpriteFont font, string text, Vector2 pos, Color color, bool checkTradition = false, bool upload = false)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos, color);

                TextManager.DrawTexts(text, FontPair, pos, color);
            }
        }

        public static void DrawString(SpriteFont font, string text, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, bool checkTradition = false, bool upload = false)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos * Scale, color, rotation, origin, scale * Scale, effects, layerDepth);

                TextManager.DrawTexts(text, FontPair, pos, color, 0, scale, layerDepth);
            }
        }
        
        /// <summary>
        /// 画文字并返回文字范围的矩形列表（支持多行文字）
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        /// <param name="rotation"></param>
        /// <param name="origin"></param>
        /// <param name="scale"></param>
        /// <param name="effects"></param>
        /// <param name="layerDepth"></param>
        /// <param name="checkTradition"></param>
        /// <param name="upload"></param>
        /// <returns>返回文字范围的矩形列表</returns>
        public static List<Bounds> DrawStringReturnBounds(SpriteFont font, string text, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, bool checkTradition = false, bool upload = false)
        {
            List<Bounds> bounds = new List<Bounds>();
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos * Scale, color, rotation, origin, scale * Scale, effects, layerDepth);

                bounds = TextManager.DrawTextsReturnBounds(text, FontPair, pos, color, 0, scale, layerDepth);
            }
            return bounds;
        }

        public static List<Bounds> DrawStringReturnBounds(SpriteBatch batch,SpriteFont font, string text, Vector2 pos, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth, bool checkTradition = false, bool upload = false)
        {
            List<Bounds> bounds = new List<Bounds>();
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos * Scale, color, rotation, origin, scale * Scale, effects, layerDepth);

                bounds = TextManager.DrawTextsReturnBounds(batch, text, FontPair, pos, color, 0, scale, layerDepth);
            }
            return bounds;
        }

        /// <summary>
        /// 计算文字的边界范围
        /// </summary>
        /// <param name="font"></param>
        /// <param name="text"></param>
        /// <param name="pos"></param>
        /// <param name="scale"></param>
        /// <param name="checkTradition"></param>
        /// <param name="upload"></param>
        /// <returns></returns>
        public static List<Bounds> CalculateTextBounds(SpriteFont font, string text, Vector2 pos, float scale,  bool checkTradition = false, bool upload = false)
        {
            List<Bounds> bounds = new List<Bounds>();
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos * Scale, color, rotation, origin, scale * Scale, effects, layerDepth);

                bounds = TextManager.CalcTextsBounds(text, FontPair, pos, 0, scale);
            }
            return bounds;
        }

        /// <summary>
        /// 将文字处理成自动换行
        /// </summary>
        /// <param name="font">字体</param>
        /// <param name="text">要处理的文字</param>
        /// <param name="lineWidth">行宽度</param>
        /// <param name="scale">缩放倍数</param>
        /// <param name="checkTradition"></param>
        /// <param name="upload"></param>
        /// <returns>返回经过自动换行处理过的文字</returns>
        public static string AutoWrap(SpriteFont font,string text,float lineWidth,float scale, bool checkTradition = false, bool upload = false)
        {
            if (!String.IsNullOrEmpty(text))
            {
                text = CheckTextCache(font, text, checkTradition, upload);
                //Session.Current.SpriteBatch.DrawString(font, text, pos * Scale, color, rotation, origin, scale * Scale, effects, layerDepth);

                return TextManager.HandleAutoWrap(text, FontPair, lineWidth, scale);
            }

            return null;
        }
    }
}
