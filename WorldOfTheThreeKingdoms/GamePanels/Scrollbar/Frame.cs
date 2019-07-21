using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontStashSharp;
using Tools;
using Platforms;
using GameManager;

namespace GamePanels.Scrollbar
{
    public interface IFrameContent
    {
        /// <summary>
        /// 控件的ID
        /// </summary>
        string ID { get; set; }
        /// <summary>
        /// 控件的名字
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 在框架内的偏移量
        /// </summary>
        Vector2 OffsetPos { get; set; }
        /// <summary>
        /// 缩放的倍数
        /// </summary>
        float Scale { get; set; }
        /// <summary>
        /// 深度
        /// </summary>
        float Depth { get; set; }
        /// <summary>
        /// 控件的边界范围
        /// </summary>
        List<Bounds> bounds { get; set; }
        /// <summary>
        /// 控件的宽
        /// </summary>
        float Width { get; set; }
        /// <summary>
        /// 控件的高
        /// </summary>
        float Height { get; set; }
        /// <summary>
        /// 包含控件的框架
        /// </summary>
        Frame baseFrame { get; set; }
        /// <summary>
        /// 控件的透明度
        /// </summary>
        float Alpha { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        Color color { get; set; }
        /// <summary>
        /// 在画布上绘制控件
        /// </summary>
        /// <param name="batch">SpriteBatch</param>
        void DrawToCanvas(SpriteBatch batch);
        /// <summary>
        /// 计算控件的尺寸
        /// </summary>
        void CalculateControlSize();
        /// <summary>
        /// 画布内控件的更新
        /// </summary>
        void UpdateCanvas();
    }

    /// <summary>
    /// 框架内滚动条设置
    /// </summary>
    public enum FrameScrollbarType
    {
        Horizontal,
        Vertical,
        Both,
        Auto,
        None
    }
    public class Frame
    {
        /// <summary>
        /// 框架的位置坐标
        /// </summary>
        public Vector2 Position;
        /// <summary>
        /// 画布的宽高
        /// </summary>
        public float CanvasWidth, CanvasHeight;
        /// <summary>
        /// 画布的背景图片
        /// </summary>
        public Texture2D BackgroundPic = null;
        /// <summary>
        /// 画布内的控件列表
        /// </summary>
        public List<IFrameContent> ContentContorls;
        /// <summary>
        /// 画布
        /// </summary>
        protected Texture2D Canvas;
        /// <summary>
        /// 控件的可视框架矩形
        /// </summary>
        public Rectangle VisualFrame;
        /// <summary>
        /// 画布默认的背景色
        /// </summary>
        public Color BackgroundColor = new Color(0, 0, 0, 0);
        /// <summary>
        /// 背景的透明度
        /// </summary>
        public float BackgroundAlpha = 1f;
        /// <summary>
        /// 绘制画布的颜色
        /// </summary>
        public Color color;
        /// <summary>
        /// 画布的深度
        /// </summary>
        public float Depth;
        /// <summary>
        /// 用于绘制画布的SpriteBatch
        /// </summary>
        private SpriteBatch batch;
        /// <summary>
        /// 绘制画布的RenderTarget2D
        /// </summary>
        private RenderTarget2D renderTarget2D;
        /// <summary>
        /// 画布的透明度
        /// </summary>
        public float Aplha;
        /// <summary>
        /// 框架内是否包含水平滚动条
        /// </summary>
        public bool HasHorizontalScrollbar = false;
        /// <summary>
        /// 框架内是否包含垂直滚动条
        /// </summary>
        public bool HasVerticalScrollbar = false;
        /// <summary>
        /// 包含框架内所有滚动条的列表
        /// </summary>
        public List<Scrollbar> Scrollbars;
        /// <summary>
        /// 锁定对象
        /// </summary>
        protected static object batchlock = new object();
        /// <summary>
        /// 控件内滚动条类型的设置
        /// </summary>
        public FrameScrollbarType frameScrollbarType;
        /// <summary>
        /// 画布的右边界填充距离
        /// </summary>
        public int CanvasRightPadding;
        /// <summary>
        /// 画布的下边界填充距离
        /// </summary>
        public int CanvasBottomPadding;
        /// <summary>
        /// 是否固定背景（如果为假则背景画在画布上，随着滚动条移动而有变化）
        /// </summary>
        public bool FixedBackground;
        /// <summary>
        /// 同时有水平滚动条和垂直滚动条时，再两种滚动条交界处空白的颜色
        /// </summary>
        public Color BlankBlockColor =new Color(57,57,57,255);
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pos">框架的位置</param>
        /// <param name="visualFrame">设置可视窗的矩形</param>
        /// <param name="bgPicPath">背景图片的路径</param>
        /// <param name="alpha">画布的透明度</param>
        /// <param name="framescrollbartype">包含滚动条的类型</param>
        public Frame(Vector2 pos, Rectangle visualFrame, string bgPicPath = null, float alpha = 1f, FrameScrollbarType framescrollbartype = FrameScrollbarType.Auto, int canvasRightPadding = 0, int canvasBottomPadding = 0, bool fixBackground = true)
        {
            ContentContorls = new List<IFrameContent>();
            Canvas = null;
            Position = pos;
            VisualFrame = visualFrame;
            if (bgPicPath != null)
                BackgroundPic = Platform.Current.LoadTexture(bgPicPath);

            color = Color.White;
            Aplha = alpha;
            Depth = 0f;
            frameScrollbarType = framescrollbartype;
            CanvasWidth = CanvasHeight = 0;
            CanvasRightPadding = canvasBottomPadding;
            CanvasBottomPadding = canvasBottomPadding;
            FixedBackground = fixBackground;

            batch = new SpriteBatch(Platform.GraphicsDevice);//创建SpriteBatch

            Scrollbars = new List<Scrollbar>();


            switch (frameScrollbarType)//根据不同的滚动条设置，设定包含何种类型的滚动条
            {
                case FrameScrollbarType.Horizontal:
                    HasHorizontalScrollbar = true;
                    break;
                case FrameScrollbarType.Vertical:
                    HasVerticalScrollbar = true;
                    break;
                case FrameScrollbarType.Both:
                    HasVerticalScrollbar = true;
                    HasHorizontalScrollbar = true;
                    break;
                case FrameScrollbarType.Auto://自动模式的滚动条在Draw方法内设置（实时更新）
                case FrameScrollbarType.None:
                    HasHorizontalScrollbar = false;
                    HasVerticalScrollbar = false;
                    break;
            }


            //根据设置生成相应的滚动条
            if (HasHorizontalScrollbar)
                Scrollbars.Add(new Scrollbar(this, ScrollbarType.Horizontal));
            if (HasVerticalScrollbar)
                Scrollbars.Add(new Scrollbar(this));

        }

        public void Draw()
        {
            if (ContentContorls.Count < 1)//框架包含控件
                return;

            lock (batchlock)//锁定并绘制控件
            {
                Platform.GraphicsDevice.SetRenderTarget(renderTarget2D);//设置Draw到画布上

                batch.Begin();

                if (BackgroundPic == null)
                    Platform.GraphicsDevice.Clear(BackgroundColor);//背景填充颜色
                else
                {
                    Platform.GraphicsDevice.Clear(new Color(0, 0, 0, 0));//如果有背景图片先要将画布底色变成透明
                    if (!FixedBackground)//如果不是固定背景则将背景绘制到画布上
                        batch.Draw(BackgroundPic, new Vector2(0, 0), null, Color.White * BackgroundAlpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);
                }
                ContentContorls.ForEach(cc => cc.DrawToCanvas(batch));//绘制每个控件
                batch.End();

                Platform.GraphicsDevice.SetRenderTarget(null);//恢复绘制到屏幕上

                Canvas = renderTarget2D;//将所绘制内容赋值给画布

                if (BackgroundPic != null && FixedBackground) //绘制背景图片到固定位置
                    Session.Current.SpriteBatch.Draw(BackgroundPic, Position, null, Color.White * BackgroundAlpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0);

                //在屏幕上将画布绘制到相应的可视框架内
                Session.Current.SpriteBatch.Draw(Canvas, Position, VisualFrame, color * Aplha, 0f, Vector2.Zero, 1f, SpriteEffects.None, Depth);
            }

            //处理Auto类型的滚动条
            if (frameScrollbarType == FrameScrollbarType.Auto)
            {

                if (!HasHorizontalScrollbar && CanvasWidth > VisualFrame.Width)//如果画布宽度大于可视框架宽度且还没有水平滚动条则生成水平滚动条
                {
                    HasHorizontalScrollbar = true;
                    Scrollbars.Add(new Scrollbar(this, ScrollbarType.Horizontal));
                }
                else if (HasHorizontalScrollbar && CanvasWidth <= VisualFrame.Width)//如果画布宽度小于可视框架宽度且还有水平滚动条则删除水平滚动条
                {
                    HasHorizontalScrollbar = false;
                    Scrollbars.Remove(Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Horizontal).FirstOrDefault());
                }

                if (!HasVerticalScrollbar && CanvasHeight > VisualFrame.Height)//如果画布高度大于可视框架高度且还没有垂直滚动条则生成垂直滚动条
                {
                    HasVerticalScrollbar = true;
                    Scrollbars.Add(new Scrollbar(this));
                }
                else if (HasVerticalScrollbar && CanvasHeight <= VisualFrame.Height)//如果画布高度小于可视框架高度且有垂直滚动条则删除垂直滚动条
                {
                    HasVerticalScrollbar = false;
                    Scrollbars.Remove(Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Vertical).FirstOrDefault());
                }
            }

            Scrollbars.ForEach(sb => sb.Draw());

            if (HasHorizontalScrollbar && HasVerticalScrollbar) //如果两种滚动条都要，则生成两种滚动条交界处的空白块
            {
                Scrollbar horizontalScrollbar = Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Horizontal).FirstOrDefault();
                Scrollbar verticalScrollbar = Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Vertical).FirstOrDefault();
                int width = verticalScrollbar.ButtonTexture.Width;
                int height = horizontalScrollbar.ButtonTexture.Height;
                Texture2D BlankBlock = new Texture2D(Platform.GraphicsDevice, width, height);
                Color[] blankBlockColor = Enumerable.Repeat(BlankBlockColor, width * height).ToArray();
                BlankBlock.SetData(blankBlockColor);
                Session.Current.SpriteBatch.Draw(BlankBlock, new Vector2(verticalScrollbar.BarPos.X, horizontalScrollbar.BarPos.Y), Color.White);
            }
        }

        /// <summary>
        /// 添加控件后计算新的画布大小
        /// </summary>
        /// <param name="contentContorl">添加的控件</param>
        private void CalculateCanvasSize(IFrameContent contentContorl)
        {
            contentContorl.CalculateControlSize();//计算添加控件的大小

            //如果新添加的控件大于现在画布的范围则生成适合新控件大小的画布
            if (contentContorl.OffsetPos.X + contentContorl.Width > CanvasWidth - CanvasRightPadding || contentContorl.OffsetPos.Y + contentContorl.Height > CanvasHeight - CanvasBottomPadding)
            {
                if (contentContorl.OffsetPos.X + contentContorl.Width > CanvasWidth - CanvasRightPadding)
                    CanvasWidth = contentContorl.OffsetPos.X + contentContorl.Width + CanvasRightPadding;//添加画布边界填充距离

                if (contentContorl.OffsetPos.Y + contentContorl.Height > CanvasHeight - CanvasBottomPadding)
                    CanvasHeight = contentContorl.OffsetPos.Y + contentContorl.Height + CanvasBottomPadding;//添加画布边界填充距离

                if (renderTarget2D != null)
                    renderTarget2D.Dispose();
                renderTarget2D = new RenderTarget2D(Platform.GraphicsDevice, CanvasWidth.ConvertToIntPlus(), CanvasHeight.ConvertToIntPlus());
            }

        }

        /// <summary>
        /// 用于删减控件后重新计算并生成新画布
        /// </summary>
        public void ReCalcuateCanvasSize()
        {
            CanvasWidth = CanvasHeight = 0;
            ContentContorls.ForEach(cc =>
            {
                cc.CalculateControlSize();
                CanvasWidth = cc.OffsetPos.X + cc.Width > CanvasWidth ? cc.OffsetPos.X + cc.Width : CanvasWidth;
                CanvasHeight = cc.OffsetPos.Y + cc.Height > CanvasHeight ? cc.OffsetPos.Y + cc.Height : CanvasHeight;
            });

            //添加画布边界填充
            CanvasWidth += CanvasRightPadding;
            CanvasHeight += CanvasBottomPadding;

            if (renderTarget2D != null)
                renderTarget2D.Dispose();
            renderTarget2D = new RenderTarget2D(Platform.GraphicsDevice, CanvasWidth.ConvertToIntPlus(), CanvasHeight.ConvertToIntPlus());
        }

        /// <summary>
        /// 向框架中添加控件
        /// </summary>
        /// <param name="contentContorl"></param>
        public void AddContentContorl(IFrameContent contentContorl)
        {
            ContentContorls.Add(contentContorl);//向画布内控件列表中添加新控件
            CalculateCanvasSize(contentContorl);
        }

        /// <summary>
        /// 清除画布所有内容
        /// </summary>
        public void Clear()
        {
            ContentContorls.Clear();
            CanvasWidth = CanvasHeight = 0;
            if (renderTarget2D != null)
                renderTarget2D.Dispose();
            if (Canvas != null)
                Canvas.Dispose();
        }

        public void Update()
        {

            Scrollbars.ForEach(sb => sb.Update());//处理滚动条Update方法

            //根据滚动条的位置重新计算可视框架在画布上的坐标，以用于显示相应范围的画布内容
            if (Canvas != null)
            {
                if (HasHorizontalScrollbar)
                    VisualFrame.X = (int)(Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Horizontal).FirstOrDefault().Value * (Canvas.Width - VisualFrame.Width));
                if (HasVerticalScrollbar)
                    VisualFrame.Y = (int)(Scrollbars.Where(sb => sb.scrollbarType == ScrollbarType.Vertical).FirstOrDefault().Value * (Canvas.Height - VisualFrame.Height));
            }

            ContentContorls.ForEach(cc => cc.UpdateCanvas());
        }

    }


}
