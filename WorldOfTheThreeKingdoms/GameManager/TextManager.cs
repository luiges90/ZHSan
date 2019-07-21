using Microsoft.Xna.Framework;
using Platforms;
using SpriteFontPlus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Tools;
namespace GameManager
{
    public struct FontPair
    {
        public string Name { get; set; }

        public int Size { get; set; }

        public string Style { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }

    public static class TextManager
    {

        public static DynamicSpriteFont font = null;

        public static Object lockObj = new Object();

        public static Dictionary<string, Microsoft.Xna.Framework.Graphics.Texture2D> texs = new Dictionary<string, Microsoft.Xna.Framework.Graphics.Texture2D>();

        public static void Init(string name, int size)
        {
            var bytes = Platform.Current.LoadFile(name);
            font = DynamicSpriteFont.FromTtf(bytes, size, 2048, 2048);
        }

        public static void DrawTexts(string text, FontPair pair, Microsoft.Xna.Framework.Vector2 pos, Microsoft.Xna.Framework.Color color, int space = 0, float scale = 1f, float? depth = null)
        {
            if (font == null)
            {
                Init(pair.Name, pair.Size);
            }

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            var texs = text.Split('\n');

            for (int i = 0; i < texs.Length; i++)
            {
                var te = texs[i];
                font.DrawString(Session.Current.SpriteBatch, te, pos + new Vector2(0, i * pair.Size * scale), color, new Vector2(scale, scale), depth == null ? 0 : (float)depth);
            }

            //Session.Current.SpriteBatch.Draw(font.Texture, pos, null, color, 0f, Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, depth == null ? 0 : (float)depth);
        }

        public static List<Bounds> DrawTextsReturnBounds(string text, FontPair pair, Microsoft.Xna.Framework.Vector2 pos, Microsoft.Xna.Framework.Color color, int space = 0, float scale = 1f, float? depth = null)
        {
            List<Bounds> bounds = new List<Bounds>();
            Bounds bound;
            if (font == null)
            {
                Init(pair.Name, pair.Size);
            }

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            var texs = text.Split('\n');

            for (int i = 0; i < texs.Length; i++)
            {
                var te = texs[i];

                bound = font.DrawStringReturnBounds(Session.Current.SpriteBatch, te, pos + new Vector2(0, i * pair.Size * scale), color, new Vector2(scale, scale), depth == null ? 0 : (float)depth);
                if (scale != 1f)   //当字体的缩放倍数不为一时，相应的字体范围也要乘以缩放倍数，字体范围才准确
                {
                    bound.X2 = bound.X + bound.Width * scale;
                    bound.Y2 = bound.Y + bound.Height * scale;
                }
                bounds.Add(bound);

            }

            return bounds;
            //Session.Current.SpriteBatch.Draw(font.Texture, pos, null, color, 0f, Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, depth == null ? 0 : (float)depth);
        }
        public static List<Bounds> DrawTextsReturnBounds(SpriteBatch batch, string text, FontPair pair, Microsoft.Xna.Framework.Vector2 pos, Microsoft.Xna.Framework.Color color, int space = 0, float scale = 1f, float? depth = null)
        {
            List<Bounds> bounds = new List<Bounds>();
            Bounds bound;
            if (font == null)
            {
                Init(pair.Name, pair.Size);
            }

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            var texs = text.Split('\n');

            for (int i = 0; i < texs.Length; i++)
            {
                var te = texs[i];

                bound = font.DrawStringReturnBounds(batch, te, pos + new Vector2(0, i * pair.Size * scale), color, new Vector2(scale, scale), depth == null ? 0 : (float)depth);
                if (scale != 1f)   //当字体的缩放倍数不为一时，相应的字体范围也要乘以缩放倍数，字体范围才准确
                {
                    bound.X2 = bound.X + bound.Width * scale;
                    bound.Y2 = bound.Y + bound.Height * scale;
                }
                bounds.Add(bound);

            }

            return bounds;
            //Session.Current.SpriteBatch.Draw(font.Texture, pos, null, color, 0f, Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, depth == null ? 0 : (float)depth);
        }

        public static List<Bounds> CalcTextsBounds(string text, FontPair pair, Microsoft.Xna.Framework.Vector2 pos, int space = 0, float scale = 1f, float? depth = null)
        {
            List<Bounds> bounds = new List<Bounds>();
            Bounds bound;
            if (font == null)
            {
                Init(pair.Name, pair.Size);
            }

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            var texs = text.Split('\n');

            for (int i = 0; i < texs.Length; i++)
            {
                var te = texs[i];

                bound = font.CalcStringBounds(te, pos + new Vector2(0, i * pair.Size * scale), new Vector2(scale, scale));
                if (scale != 1f)   //当字体的缩放倍数不为一时，相应的字体范围也要乘以缩放倍数，字体范围才准确
                {
                    bound.X2 = bound.X + bound.Width * scale;
                    bound.Y2 = bound.Y + bound.Height * scale;
                }
                bounds.Add(bound);

            }

            return bounds;
            //Session.Current.SpriteBatch.Draw(font.Texture, pos, null, color, 0f, Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, depth == null ? 0 : (float)depth);
        }

        /// <summary>
        /// 将文字处理成自动换行的形式
        /// </summary>
        /// <param name="text">要处理的文字</param>
        /// <param name="pair">FontPair</param>
        /// <param name="lineWidth">行宽度</param>
        /// <param name="scale">缩放倍数</param>
        /// <returns>返回经过自动换行处理过的文字</returns>
        public static string HandleAutoWrap(string text, FontPair pair, float lineWidth, float scale)
        {
            Bounds bound;
            string autoWrapText = null;//换行后的文字
            if (font == null)
            {
                Init(pair.Name, pair.Size);
            }

            text = text.Replace("\r\n", "\n").Replace("\r", "\n");

            var texs = text.Split('\n');

            int currentIndex;//指向在一行文字内当前指向的处理到第几个字的索引
            string currentLine;//当前行需判断的文字
            for (int i = 0; i < texs.Length; i++)
            {
                currentLine = null;
                var te = texs[i];
                currentIndex = 0;
                for (int j = 0; j < te.Length; j++)
                {

                    currentLine = te.Substring(currentIndex, j - currentIndex + 1);//取出当前索引位置前的所有文字用于判断这些文字是否超过行宽度
                    bound = font.CalcStringBounds(currentLine.ToString(), new Vector2(0, i * pair.Size * scale), new Vector2(scale, scale));

                    if (bound.Width * scale > lineWidth)//如果当前这些文字超过行宽的
                    {
                        autoWrapText += (currentLine.Substring(0, currentLine.Length - 1) + '\n');//换行，并将当前行所有文字存入修改后的自动换行变量中
                        currentIndex = j;
                        j--;//当前的字超过行宽度，需要倒回去一个字开始继续处理
                    }
                }

                autoWrapText += (currentLine + '\n');//将没有超界的文字加入总文字内
            }
            return autoWrapText;
        }
        /*

    //public static void Init(string name)
    //{
    //    FontCollection fonts = new FontCollection();

    //    //string basePath = @"C:\Docs\Projects\Fonts-master\Fonts-master\tests\SixLabors.Fonts.Tests\Fonts\";

    //    var bytes = Platform.Current.LoadFile(name);

    //    using (var ms = new MemoryStream(bytes))
    //    {
    //        font = fonts.Install(ms);
    //    }

    //    //FontFamily font = fonts.Install(basePath + "FZLB_GBK.TTF");
    //}


    public static void DrawTexts(string text, FontPair pair, Microsoft.Xna.Framework.Vector2 pos, Microsoft.Xna.Framework.Color color, int space = 0, float scale = 1f, float? depth = null)
{

    if (font == null)
    {
        Init(pair.Name);
    }

    Microsoft.Xna.Framework.Graphics.Texture2D tex = null;

    lock (lockObj)
    {
        if (texs.ContainsKey(text))
        {
            tex = texs[text];
        }
        else
        {
            tex = RenderText(font, text, pair.Size);
            texs.Add(text, tex);
        }
    }

    if (tex == null)
    {

    }
    else
    {
        Session.Current.SpriteBatch.Draw(tex, pos, null, color, 0f, Microsoft.Xna.Framework.Vector2.Zero, scale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, depth == null ? 0 : (float)depth);
    }

}


public static Microsoft.Xna.Framework.Graphics.Texture2D RenderText(Font font, string text, int width, int height)
{
    string path = System.IO.Path.GetInvalidFileNameChars().Aggregate(text, (x, c) => x.Replace($"{c}", "-"));
    string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine("Output", System.IO.Path.Combine(path)));

    using (Image<Rgba32> img = new Image<Rgba32>(width, height))
    {
        img.Mutate(x => x.Fill(Rgba32.White));

        IPathCollection shapes = SixLabors.Shapes.Temp.TextBuilder.GenerateGlyphs(text, new SixLabors.Primitives.PointF(50f, 4f), new RendererOptions(font, 72));
        img.Mutate(x => x.Fill(Rgba32.Black, shapes));

        using (var ms = new MemoryStream())
        {
            img.SaveAsPng(ms);
            return Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Platform.GraphicsDevice, ms);
        }

        //Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fullPath));

        //using (FileStream fs = File.Create(fullPath + ".png"))
        //{
        //    img.SaveAsPng(fs);
        //}
    }
}

public static Microsoft.Xna.Framework.Graphics.Texture2D RenderText(RendererOptions font, string text)
{
    GlyphBuilder builder = new GlyphBuilder();
    TextRenderer renderer = new TextRenderer(builder);
    SixLabors.Primitives.SizeF size = TextMeasurer.Measure(text, font);
    renderer.RenderText(text, font);

    return builder.Paths.SaveImage((int)size.Width + 20, (int)size.Height + 20, font.Font.Name, text + ".png");
}
public static Microsoft.Xna.Framework.Graphics.Texture2D RenderText(FontFamily font, string text, float pointSize = 12)
{
    return RenderText(new RendererOptions(new Font(font, pointSize), 96) { ApplyKerning = true, WrappingWidth = 340 }, text);
}

public static Microsoft.Xna.Framework.Graphics.Texture2D SaveImage(this IEnumerable<IPath> shapes, int width, int height, params string[] path)
{
    path = path.Select(p => System.IO.Path.GetInvalidFileNameChars().Aggregate(p, (x, c) => x.Replace($"{c}", "-"))).ToArray();
    string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine("Output", System.IO.Path.Combine(path)));

    using (Image<Rgba32> img = new Image<Rgba32>(width, height))
    {
        img.Mutate(x => x.Fill(Rgba32.Transparent));

        foreach (IPath s in shapes)
        {
            // In ImageSharp.Drawing.Paths there is an extension method that takes in an IShape directly.
            img.Mutate(x => x.Fill(Rgba32.HotPink, s.Translate(new Vector2(0, 0))));
        }
        // img.Draw(Color.LawnGreen, 1, shape);

        using (var ms = new MemoryStream())
        {
            img.SaveAsPng(ms);
            return Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Platform.GraphicsDevice, ms);
        }

        // Ensure directory exists
        //Directory.CreateDirectory(System.IO.Path.GetDirectoryName(fullPath));

        //using (FileStream fs = File.Create(fullPath))
        //{
        //    img.SaveAsPng(fs);
        //}
    }
}

public static Microsoft.Xna.Framework.Graphics.Texture2D SaveImage(this IEnumerable<IPath> shapes, params string[] path)
{
    IPath shape = new ComplexPolygon(shapes.ToArray());
    shape = shape.Translate(shape.Bounds.Location * -1) // touch top left
            .Translate(new Vector2(10)); // move in from top left

    StringBuilder sb = new StringBuilder();
    IEnumerable<ISimplePath> converted = shape.Flatten();
    converted.Aggregate(sb, (s, p) =>
    {
        foreach (Vector2 point in p.Points)
        {
            sb.Append(point.X);
            sb.Append('x');
            sb.Append(point.Y);
            sb.Append(' ');
        }
        s.Append('\n');
        return s;
    });
    string str = sb.ToString();
    shape = new ComplexPolygon(converted.Select(x => new Polygon(new LinearLineSegment(x.Points.ToArray()))).ToArray());

    path = path.Select(p => System.IO.Path.GetInvalidFileNameChars().Aggregate(p, (x, c) => x.Replace($"{c}", "-"))).ToArray();
    string fullPath = System.IO.Path.GetFullPath(System.IO.Path.Combine("Output", System.IO.Path.Combine(path)));
    // pad even amount around shape
    int width = (int)(shape.Bounds.Left + shape.Bounds.Right);
    int height = (int)(shape.Bounds.Top + shape.Bounds.Bottom);
    if (width < 1)
    {
        width = 1;
    }
    if (height < 1)
    {
        height = 1;
    }
    using (Image<Rgba32> img = new Image<Rgba32>(width, height))
    {
        img.Mutate(x => x.Fill(Rgba32.DarkBlue));

        // In ImageSharp.Drawing.Paths there is an extension method that takes in an IShape directly.
        img.Mutate(x => x.Fill(Rgba32.HotPink, shape));
        // img.Draw(Color.LawnGreen, 1, shape);

        // Ensure directory exists
        using (var ms = new MemoryStream())
        {
            img.SaveAsPng(ms);
            return Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(Platform.GraphicsDevice, ms);
        }
    }
}


private const float SMALLWIDTHSCALE = 0.54f;

private const float LITTLEWIDTHSCALE = 0.48f;

private static char[] SMALLCHARS = new char[] { '，', '。', '、', '“', '”', '"', ' ', '（', '）' };

private static char[] LITTLECHARS = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.',
    'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
    'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M',
    ',', '(', ')', '*', '-', ':', ';', '[', ']', '/'};


private static Object LockObject = new object();

private static Dictionary<string, FontFace> FontFaceDics = new Dictionary<string, FontFace>();

private static Dictionary<FontPair, Dictionary<Char, Texture2D>> FontTextureDics = new Dictionary<FontPair, Dictionary<Char, Texture2D>>();

private static FontFace GetFontFace(FontPair pair)
{
    lock (LockObject)
    {
        //檢查字體是否存在
        if (FontFaceDics.ContainsKey(pair.Name))
        {

        }
        else
        {
            var bytes = Platform.Current.LoadFile(pair.Name);

            using (var ms = new MemoryStream(bytes))
            {
                var fontFace = new FontFace(ms);

                FontFaceDics.Add(pair.Name, fontFace);
            }
        }
        return FontFaceDics[pair.Name];
    }
}

private static Dictionary<Char, Texture2D> GetFontDics(FontPair pair)
{
    lock (LockObject)
    {
        //檢查指定字庫是否存在文字
        if (FontTextureDics.ContainsKey(pair))
        {

        }
        else
        {
            var textureDics = new Dictionary<char, Texture2D>();

            FontTextureDics.Add(pair, textureDics);
        }

        return FontTextureDics[pair];
    }
}

public static Vector2 GetWidthHeight(string text, FontPair pair, float scale = 1f)
{
    Vector2 already = Vector2.Zero;

    int pairWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale);

    int pairHeight = Convert.ToInt32(Convert.ToSingle(pair.Height) * scale);

    int SmallWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale * SMALLWIDTHSCALE);

    int LittleWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale * LITTLEWIDTHSCALE);

    Color? tmpColor = null;

    already.Y += pairHeight;

    for (int i = 0; i < text.Length; i++)
    {
        char ch = text[i];

        if (ch == '\n')
        {
            already.X = 0;
            already.Y += pairHeight;
            continue;
        }

        if (ch == '\r')
        {
            continue;
        }

        if (ch == '<')
        {
            int recent = text.IndexOf('>', i);

            if (recent > i)
            {
                var next = text[i + 1];

                if (next == '/')
                {
                    i = recent;
                    tmpColor = null;
                    continue;
                }
                else
                {
                    string co = text.Substring(i + 1, recent - i - 1);
                    var col = GetColor(co);
                    if (col == null)
                    {

                    }
                    else
                    {
                        i = recent;
                        tmpColor = col;
                        continue;
                    }
                }
            }

        }

        Vector2 ext = Vector2.Zero;

        int targetWidth = pairWidth;
        int targetHeight = pairHeight;

        if (SMALLCHARS.Contains(ch))
        {
            targetWidth = SmallWidth;
        }
        else if (LITTLECHARS.Contains(ch))
        {
            targetWidth = LittleWidth;
        }

        already.X += targetWidth;
    }
    return already;
}

private static Texture2D GetCharTexture(FontFace fontFace, Dictionary<Char, Texture2D> texs, FontPair pair, char ch)
{
    lock (LockObject)
    {
        if (texs.ContainsKey(ch))
        {

        }
        else
        {
            var tex = RenderTexture2D(ch, fontFace, pair);
            texs.Add(ch, tex);
        }
        return texs[ch];
    }
}

public static void DrawTexts(string text, FontPair pair, Vector2 pos, Color color, int space = 0, float scale = 1f, float? depth = null)
{
    var fontFace = GetFontFace(pair);

    var texs = GetFontDics(pair);

    if (fontFace == null || texs == null)
    {

    }
    else
    {
        Vector2 already = Vector2.Zero;

        int pairWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale);

        int pairHeight = Convert.ToInt32(Convert.ToSingle(pair.Height) * scale);

        int SmallWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale * SMALLWIDTHSCALE);

        int LittleWidth = Convert.ToInt32(Convert.ToSingle(pair.Width) * scale * LITTLEWIDTHSCALE);

        Color? tmpColor = null;

        for (int i = 0; i < text.Length; i++)
        {
            char ch = text[i];

            if (ch == '\n')
            {
                already.X = 0;
                already.Y += pairHeight;
                continue;
            }

            if (ch == '\r')
            {
                continue;
            }

            if (ch == '<')
            {
                int recent = text.IndexOf('>', i);

                if (recent > i)
                {
                    var next = text[i + 1];

                    if (next == '/')
                    {
                        i = recent;
                        tmpColor = null;
                        continue;
                    }
                    else
                    {
                        string co = text.Substring(i + 1, recent - i - 1);
                        var col = GetColor(co);
                        if (col == null)
                        {

                        }
                        else
                        {
                            i = recent;
                            tmpColor = col;
                            continue;
                        }
                    }
                }

            }

            Vector2 ext = Vector2.Zero;

            Texture2D tex = GetCharTexture(fontFace, texs, pair, ch);

            int targetWidth = pairWidth;
            int targetHeight = pairHeight;

            if (SMALLCHARS.Contains(ch))
            {
                targetWidth = SmallWidth;
            }
            else if (LITTLECHARS.Contains(ch))
            {
                targetWidth = LittleWidth;  // tex == null ? LittleWidth : Convert.ToInt16(Convert.ToSingle(tex.Width) * scale);
            }

            if (tex == null)
            {

            }
            else
            {
                int width = targetWidth - tex.Width;

                int height = targetHeight - tex.Height;

                ext.X = width / 2;

                ext.Y = height / 2;

                var drawPos = pos + already + ext;

                if (depth == null)
                {
                    Session.Current.SpriteBatch.Draw(tex, drawPos, tmpColor == null ? color : (Color)tmpColor);
                }
                else
                {
                    Session.Current.SpriteBatch.Draw(tex, drawPos, null, tmpColor == null ? color : (Color)tmpColor, 0f, Vector2.Zero, scale, SpriteEffects.None, (float)depth);
                }
            }

            already.X += targetWidth;
        }
    }
}

private static Texture2D RenderTexture2D(char c, FontFace fontFace, FontPair pair)
{
    try
    {
        var surface = RenderSurface(c, fontFace, pair);

        var image = SaveAsImage(surface);

        using (var ms = new MemoryStream())
        {
            image.SaveAsPng(ms);
            return Texture2D.FromStream(Platform.GraphicsDevice, ms);
        }
    }
    catch (Exception ex)
    {
        return null;
    }
}

private static unsafe Surface RenderSurface(char c, FontFace font, FontPair pair)
{
    var glyph = font.GetGlyph(c, pair.Size);
    var surface = new Surface
    {
        Bits = Marshal.AllocHGlobal(glyph.RenderWidth * glyph.RenderHeight),
        Width = glyph.RenderWidth,
        Height = glyph.RenderHeight,
        Pitch = glyph.RenderWidth
    };

    var stuff = (byte*)surface.Bits;
    for (int i = 0; i < surface.Width * surface.Height; i++)
        *stuff++ = 0;

    glyph.RenderTo(surface);

    return surface;
}

private static Image<Rgba32> SaveAsImage(Surface surface)
{
    int width = surface.Width;
    int height = surface.Height;
    int len = width * height;
    byte[] data = new byte[len];
    Marshal.Copy(surface.Bits, data, 0, len);
    byte[] pixels = new byte[len * 4];

    int index = 0;
    for (int i = 0; i < len; i++)
    {
        byte c = data[i];
        pixels[index++] = c;
        pixels[index++] = c;
        pixels[index++] = c;
        pixels[index++] = c;
    }

    var image = Image.LoadPixelData<Rgba32>(pixels, width, height);
    Marshal.FreeHGlobal(surface.Bits); //Give the memory back!
    return image;
}

private static Color? GetColor(string name)
{
    switch(name)
    {
        case "MediumBlue": return Color.MediumBlue;
        case "MediumOrchid": return Color.MediumOrchid;
        case "MediumPurple": return Color.MediumPurple;
        case "MediumSeaGreen": return Color.MediumSeaGreen;
        case "MediumSlateBlue": return Color.MediumSlateBlue;
        case "MediumSpringGreen": return Color.MediumSpringGreen;
        case "MediumTurquoise": return Color.MediumTurquoise;
        case "MediumVioletRed": return Color.MediumVioletRed;
        case "MonoGameOrange": return Color.MonoGameOrange;
        case "MintCream": return Color.MintCream;
        case "MistyRose": return Color.MistyRose;
        case "Moccasin": return Color.Moccasin;
        case "MediumAquamarine": return Color.MediumAquamarine;
        case "NavajoWhite": return Color.NavajoWhite;
        case "Navy": return Color.Navy;
        case "OldLace": return Color.OldLace;
        case "MidnightBlue": return Color.MidnightBlue;
        case "Maroon": return Color.Maroon;
        case "LightYellow": return Color.LightYellow;
        case "Linen": return Color.Linen;
        case "LawnGreen": return Color.LawnGreen;
        case "LemonChiffon": return Color.LemonChiffon;
        case "LightBlue": return Color.LightBlue;
        case "LightCoral": return Color.LightCoral;
        case "LightCyan": return Color.LightCyan;
        case "LightGoldenrodYellow": return Color.LightGoldenrodYellow;
        case "LightGray": return Color.LightGray;
        case "LightGreen": return Color.LightGreen;
        case "LightPink": return Color.LightPink;
        case "LightSalmon": return Color.LightSalmon;
        case "LightSeaGreen": return Color.LightSeaGreen;
        case "LightSkyBlue": return Color.LightSkyBlue;
        case "LightSlateGray": return Color.LightSlateGray;
        case "LightSteelBlue": return Color.LightSteelBlue;
        case "Olive": return Color.Olive;
        case "Lime": return Color.Lime;
        case "LimeGreen": return Color.LimeGreen;
        case "Magenta": return Color.Magenta;
        case "OliveDrab": return Color.OliveDrab;
        case "PaleGreen": return Color.PaleGreen;
        case "OrangeRed": return Color.OrangeRed;
        case "Silver": return Color.Silver;
        case "SkyBlue": return Color.SkyBlue;
        case "SlateBlue": return Color.SlateBlue;
        case "SlateGray": return Color.SlateGray;
        case "Snow": return Color.Snow;
        case "SpringGreen": return Color.SpringGreen;
        case "SteelBlue": return Color.SteelBlue;
        case "Tan": return Color.Tan;
        case "Teal": return Color.Teal;
        case "Thistle": return Color.Thistle;
        case "Tomato": return Color.Tomato;
        case "Turquoise": return Color.Turquoise;
        case "Violet": return Color.Violet;
        case "Wheat": return Color.Wheat;
        case "White": return Color.White;
        case "WhiteSmoke": return Color.WhiteSmoke;
        case "Yellow": return Color.Yellow;
        case "Sienna": return Color.Sienna;
        case "Orange": return Color.Orange;
        case "SeaShell": return Color.SeaShell;
        case "SandyBrown": return Color.SandyBrown;
        case "Orchid": return Color.Orchid;
        case "PaleGoldenrod": return Color.PaleGoldenrod;
        case "LavenderBlush": return Color.LavenderBlush;
        case "PaleTurquoise": return Color.PaleTurquoise;
        case "PaleVioletRed": return Color.PaleVioletRed;
        case "PapayaWhip": return Color.PapayaWhip;
        case "PeachPuff": return Color.PeachPuff;
        case "Peru": return Color.Peru;
        case "Pink": return Color.Pink;
        case "Plum": return Color.Plum;
        case "PowderBlue": return Color.PowderBlue;
        case "Purple": return Color.Purple;
        case "Red": return Color.Red;
        case "RosyBrown": return Color.RosyBrown;
        case "RoyalBlue": return Color.RoyalBlue;
        case "SaddleBrown": return Color.SaddleBrown;
        case "Salmon": return Color.Salmon;
        case "SeaGreen": return Color.SeaGreen;
        case "Lavender": return Color.Lavender;
        case "HotPink": return Color.HotPink;
        case "Ivory": return Color.Ivory;
        case "DarkGray": return Color.DarkGray;
        case "DarkGoldenrod": return Color.DarkGoldenrod;
        case "DarkCyan": return Color.DarkCyan;
        case "DarkBlue": return Color.DarkBlue;
        case "Cyan": return Color.Cyan;
        case "Crimson": return Color.Crimson;
        case "Cornsilk": return Color.Cornsilk;
        case "Khaki": return Color.Khaki;
        case "Coral": return Color.Coral;
        case "Chocolate": return Color.Chocolate;
        case "Chartreuse": return Color.Chartreuse;
        case "CadetBlue": return Color.CadetBlue;
        case "BurlyWood": return Color.BurlyWood;
        case "Brown": return Color.Brown;
        case "BlueViolet": return Color.BlueViolet;
        case "Blue": return Color.Blue;
        case "BlanchedAlmond": return Color.BlanchedAlmond;
        case "Black": return Color.Black;
        case "Bisque": return Color.Bisque;
        case "Beige": return Color.Beige;
        case "Azure": return Color.Azure;
        case "Aquamarine": return Color.Aquamarine;
        case "Aqua": return Color.Aqua;
        case "AntiqueWhite": return Color.AntiqueWhite;
        case "AliceBlue": return Color.AliceBlue;
        case "Transparent": return Color.Transparent;
        case "TransparentBlack": return Color.TransparentBlack;
        case "DarkGreen": return Color.DarkGreen;
        case "DarkKhaki": return Color.DarkKhaki;
        case "CornflowerBlue": return Color.CornflowerBlue;
        case "DarkOliveGreen": return Color.DarkOliveGreen;
        case "Indigo": return Color.Indigo;
        case "IndianRed": return Color.IndianRed;
        case "YellowGreen": return Color.YellowGreen;
        case "DarkMagenta": return Color.DarkMagenta;
        case "GreenYellow": return Color.GreenYellow;
        case "Green": return Color.Green;
        case "Gray": return Color.Gray;
        case "Goldenrod": return Color.Goldenrod;
        case "Gold": return Color.Gold;
        case "GhostWhite": return Color.GhostWhite;
        case "Gainsboro": return Color.Gainsboro;
        case "Fuchsia": return Color.Fuchsia;
        case "ForestGreen": return Color.ForestGreen;
        case "FloralWhite": return Color.FloralWhite;
        case "Honeydew": return Color.Honeydew;
        case "DodgerBlue": return Color.DodgerBlue;
        case "DimGray": return Color.DimGray;
        case "DeepSkyBlue": return Color.DeepSkyBlue;
        case "DeepPink": return Color.DeepPink;
        case "DarkViolet": return Color.DarkViolet;
        case "DarkTurquoise": return Color.DarkTurquoise;
        case "DarkSlateGray": return Color.DarkSlateGray;
        case "DarkSlateBlue": return Color.DarkSlateBlue;
        case "DarkSeaGreen": return Color.DarkSeaGreen;
        case "DarkSalmon": return Color.DarkSalmon;
        case "DarkRed": return Color.DarkRed;
        case "DarkOrchid": return Color.DarkOrchid;
        case "DarkOrange": return Color.DarkOrange;
        case "Firebrick": return Color.Firebrick;
        default: return null;
    }
}

*/
    }
}
