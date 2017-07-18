using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Imaging;
//using System.Drawing.Text;
using System.IO;
using WorldOfTheThreeKingdoms;


namespace GameFreeText
{

    //public class FreeTextBuilder
    //{
    //    private Brush brush;
    //    public Font font;
    //    private GraphicsDevice graphicsDevice;
    //    private Graphics myDrawing;
    //    private Dictionary<string, Texture2D> textTexturePool;
    //    private int textTexturePoolSize;

    //    public FreeTextBuilder()
    //    {
    //        this.textTexturePoolSize = 0x3e8;
    //        this.brush = Brushes.White;
    //        this.textTexturePool = new Dictionary<string, Texture2D>();
    //        this.myDrawing = Graphics.FromHwnd(IntPtr.Zero);
    //    }

    //    public FreeTextBuilder(int size)
    //    {
    //        this.textTexturePoolSize = 0x3e8;
    //        this.brush = Brushes.White;
    //        this.textTexturePool = new Dictionary<string, Texture2D>();
    //        this.myDrawing = Graphics.FromHwnd(IntPtr.Zero);
    //        this.textTexturePoolSize = size;
    //    }

    //    private void AddTextureToPool(string text, Texture2D texture)
    //    {
    //        if (this.textTexturePool.Count >= this.textTexturePoolSize)
    //        {
    //            this.textTexturePool.Clear();
    //        }
    //        this.textTexturePool.Add(text, texture);
    //    }

    //    public Texture2D CreateTextTexture(string text)
    //    {
    //        if (((text == null) || (this.font == null)) || (text.Length == 0))
    //        {
    //            return null;
    //        }
    //        Texture2D textureFromPool = this.GetTextureFromPool(text);
    //        if (textureFromPool == null)
    //        {
    //            try
    //            {
    //                Size size = this.myDrawing.MeasureString(text, this.font).ToSize();
    //                Bitmap image = new Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
    //                this.myDrawing = Graphics.FromImage(image);
    //                this.myDrawing.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
    //                this.myDrawing.DrawString(text, this.font, this.brush, new PointF(0f, 0f));
    //                MemoryStream stream = new MemoryStream();
    //                image.Save(stream, ImageFormat.Bmp);
    //                image.Dispose();
    //                stream.Position = 0;
    //                textureFromPool = Platform.Current.LoadTextureFromStream(stream);
    //                stream.Dispose();
    //                this.AddTextureToPool(text, textureFromPool);
    //            }
    //            catch
    //            {
    //            }
    //        }
    //        return textureFromPool;
    //    }

    //    public int GetTextHeight(string text)
    //    {
    //        return this.myDrawing.MeasureString(text, this.font).ToSize().Height;
    //    }

    //    private Texture2D GetTextureFromPool(string text)
    //    {
    //        Texture2D textured;
    //        this.textTexturePool.TryGetValue(text, out textured);
    //        return textured;
    //    }

    //    public int GetTextWidth(string text)
    //    {
    //        return this.myDrawing.MeasureString(text, this.font).ToSize().Width;
    //    }

    //    public void SetCapacity(int capacity)
    //    {
    //        this.textTexturePoolSize = capacity;
    //    }

    //    public void SetFreeTextBuilder(GraphicsDevice graphicsDevice, Font font)
    //    {
    //        this.SetFreeTextBuilderGraphicsDevice(graphicsDevice);
    //        this.SetFreeTextBuilderFont(font);
    //    }

    //    private void SetFreeTextBuilderFont(Font font)
    //    {
    //        this.font = font;
    //    }

    //    private void SetFreeTextBuilderGraphicsDevice(GraphicsDevice graphicsDevice)
    //    {
    //        this.graphicsDevice = graphicsDevice;
    //    }
    //}
}

