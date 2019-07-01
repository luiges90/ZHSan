using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FontStashSharp;
using Microsoft.Xna.Framework.Graphics;
using Tools;
using GameManager;
using Platforms;
namespace GamePanels.Scrollbar
{
    class TextureContent : IFrameContent
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public Vector2 OffsetPos { get; set; }
        public float Scale { get; set; }
        public float Depth { get; set; }
        public List<Bounds> bounds { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public Frame baseFrame { get; set; }
        /// <summary>
        /// 要绘制的材质
        /// </summary>
        public Texture2D Texture;
        /// <summary>
        /// 材质的路径
        /// </summary>
        public string TexturePath;
        public float Alpha { get; set; }
        /// <summary>
        /// 源矩形
        /// </summary>
        public Rectangle? source;
        /// <summary>
        /// 材质的底色
        /// </summary>
        public Color color { get; set; }
        /// <summary>
        /// 旋转
        /// </summary>
        public float Rotation;
        public Vector2 Origin;
        /// <summary>
        /// 效果
        /// </summary>
        public SpriteEffects spriteEffects;
        public void DrawToCanvas(SpriteBatch batch)
        {
            batch.Draw(Texture, OffsetPos, source, color, Rotation, Origin, Scale, spriteEffects, Depth);
        }

        public void CalculateControlSize()
        {
            Width = Texture.Width * Scale;
            Height = Texture.Height * Scale;
            bounds = new List<Bounds>();
            bounds.Add(new Bounds() { X = OffsetPos.X, Y = OffsetPos.Y, X2 = OffsetPos.X + Width, Y2 = OffsetPos.Y + Height });
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="offsetPos">控件在画布内的偏移量坐标</param>
        /// <param name="texturePath">材质的路径</param>
        /// <param name="baseframe">包含控件的框架</param>
        /// <param name="scale">缩放倍数</param>
        /// <param name="depth">深度</param>
        public TextureContent(Vector2 offsetPos, string texturePath, Frame baseframe, float scale = 1f, float depth = 0, string id = null, string name = null)
        {
            ID = id;
            Name = name;
            OffsetPos = offsetPos;
            TexturePath = texturePath;
            baseFrame = baseframe;
            Scale = scale;
            Depth = depth;
            source = null;
            color = Color.White;
            Rotation = 0f;
            Origin = Vector2.Zero;
            spriteEffects = SpriteEffects.None;
            if (TexturePath != null)
                Texture = Platform.Current.LoadTexture(TexturePath);
        }

        public void UpdateCanvas()
        {

        }
    }
}
