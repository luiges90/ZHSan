using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Platforms;
using GameManager;
using GamePanels.Scrollbar;
using FontStashSharp;

namespace GamePanels
{
    public class ButtonPressEventArgs : EventArgs
    {
        public ButtonPressEventArgs()
        {
        }
    }
    public delegate void ButtonPressEventHandler(object sender, ButtonPressEventArgs e);
    /// <summary>
    /// 控制按鈕
    /// </summary>
    public class ButtonTexture : IFrameContent
    {
        public Vector2 OffsetPos
        {
            get
            {
                return Position;
            }
            set
            {
                Position = value;
            }
        }
        public float Alpha { get; set; }
        public float Depth { get; set; }
        public float Scale { get; set; }
        /// <summary>
        /// 包含控件所有范围矩阵的列表
        /// </summary>
        public List<Bounds> bounds { get; set; }
        //public float Width { get; set; }
        //public float Height { get; set; }
        public Frame baseFrame { get; set; }
        public Color color { get; set; }
        public string ID { get; set; }
        public string Text { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        TextureRecs TextureRecs { get; set; }
        public object Sender { get; set; }
        public string ViewText { get; set; }
        public SpriteFont ViewFont { get; set; }
        public float ViewTextScale = 1f;
        public Vector2 ViewTextPos { get; set; }
        public bool MouseOver = false;
        public bool PreMouseOver { get; set; }
        public bool Selected = false;
        public bool Visible = true;
        public bool Enable = true;
        public bool Locked = false;
        public bool FireEventWhenUnEnable = false;
        public bool Sound = true;
        //public float Alpha = 1f;
        //public float Scale = 1f;

        public float DrawScale = 1f;

        public int ExtDis = 0;
        public Rectangle? Rectangle
        {
            get
            {
                if (TextureRecs.Recs != null)
                {
                    if (Locked)
                    {
                        return TextureRecs.Recs.Length > 3 ? TextureRecs.Recs[3] : TextureRecs.Recs[0];
                    }
                    else if (Enable)
                    {
                        return (!Selected && !MouseOver ? TextureRecs.Recs[0] : (TextureRecs.Recs.Length > 1 ? TextureRecs.Recs[1] : TextureRecs.Recs[0]));
                    }
                    else
                    {
                        if (TextureRecs.Recs.Length > 3)
                        {
                            return Selected ? TextureRecs.Recs[3] : TextureRecs.Recs[2];
                        }
                        else if (TextureRecs.Recs.Length > 2)
                        {
                            return TextureRecs.Recs[2];
                        }
                        else
                        {
                            return Selected ? TextureRecs.Recs[1] : TextureRecs.Recs[0];
                        }
                    }
                }
                else return null;
            }
        }
        public Vector2 Position { get; set; }

        public int OriginWidth
        {
            get
            {
                return Convert.ToInt32(TextureRecs.Recs[0].Width);
            }
        }
        public int OriginHeight
        {
            get
            {
                return Convert.ToInt32(TextureRecs.Recs[0].Height);
            }
        }

        public float Width
        {
            get
            {
                return Convert.ToSingle(TextureRecs.Recs[0].Width * Scale);
            }
            set
            {
                width = value;
            }
        }
        public float width;
        public float Height
        {
            get
            {
                return Convert.ToSingle(TextureRecs.Recs[0].Height * Scale);
            }
            set
            {
                height = value;
            }
        }
        public float height;
        public event ButtonPressEventHandler OnMouseOver, OnButtonPress;
        public Color ViewTextColor1 = Color.White;
        public Color ViewTextColor2 = Color.White;
        public DateTime? prePressTime;
        public ButtonTexture(string text, string name, Vector2? pos)
            : this(text, name, pos, null)
        {
        }
        public ButtonTexture(string text, string name, Vector2? pos, string extension)
        {
            Text = text;
            Name = name;
            Key = Text + "#" + Name;
            TextureRecs = Session.TextureRecs[Key];
            if (pos != null) Position = (Vector2)pos;
            Alpha = 1f;
            Scale = 1f;
            Depth = 0f;
        }

        /// <summary>
        /// 在框架内调用的构造函数
        /// </summary>
        /// <param name="text">材质的路径</param>
        /// <param name="name">按钮的名称</param>
        /// <param name="pos">控件的位置</param>
        /// <param name="frame">包含控件的上级框架</param>
        /// <param name="scale">缩放倍数</param>
        /// <param name="alpha">透明度</param>
        /// <param name="depth">深度</param>
        /// <param name="_color">材质的背景色</param>
        public ButtonTexture(string text, string name, Vector2? pos, Frame frame, float scale = 1f, float alpha = 1f, float depth = 0f, Color? _color = null) : this(text, name, pos, null)
        {
            Alpha = alpha;
            Scale = scale;
            Depth = depth;
            color = _color ?? Color.White;
            baseFrame = frame;
        }
        public void PressButton()
        {
            InputManager.ClickTime++;
            DateTime now = DateTime.Now;
            if (prePressTime != null)
            {
                TimeSpan ts = now - (DateTime)prePressTime;
                if (ts.TotalSeconds < 0.3f || !Platform.IsActive)
                {
                    return;
                }
            }
            prePressTime = now;
            if (OnButtonPress != null) OnButtonPress.Invoke(this, null);
        }
        public bool IsInTexture(float poX, float poY, Vector2? basePos)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                PreMouseOver = MouseOver;
                if (basePos == null)
                {
                    MouseOver = this.Position.X - ExtDis <= poX && poX <= this.Position.X + OriginWidth * Scale + ExtDis
                        && this.Position.Y - ExtDis <= poY && poY <= this.Position.Y + OriginHeight * Scale + ExtDis;
                }
                else
                {
                    MouseOver = this.Position.X + ((Vector2)basePos).X - ExtDis <= poX && poX <= this.Position.X + ((Vector2)basePos).X + OriginWidth * Scale + ExtDis
                        && this.Position.Y + ((Vector2)basePos).Y - ExtDis <= poY && poY <= this.Position.Y + ((Vector2)basePos).Y + OriginHeight * Scale + ExtDis;
                }
            }
            else
            {
                MouseOver = false;
            }
            return MouseOver;
        }
        public bool IsInTexture(int poX, int poY, Vector2? basePos)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                PreMouseOver = MouseOver;
                if (basePos == null)
                {
                    MouseOver = this.Position.X - ExtDis <= poX && poX <= this.Position.X + Width + ExtDis
                        && this.Position.Y - ExtDis <= poY && poY <= this.Position.Y + Height + ExtDis;
                }
                else
                {
                    MouseOver = this.Position.X + ((Vector2)basePos).X - ExtDis <= poX && poX <= this.Position.X + ((Vector2)basePos).X + Width + ExtDis
                        && this.Position.Y + ((Vector2)basePos).Y - ExtDis <= poY && poY <= this.Position.Y + ((Vector2)basePos).Y + Height + ExtDis;
                }
            }
            else
            {
                MouseOver = false;
            }
            return MouseOver;
        }
        public void Update()
        {
            Update(null);
        }
        public void Update(Vector2? basePos)
        {
            Update(InputManager.PoX, InputManager.PoY, basePos);
        }
        public void Update(int poX, int poY, Vector2? basePos)
        {
            Update(poX, poY, basePos, InputManager.IsPressed, Sound);
        }
        //SoundPlayer player = new SoundPlayer("Content/Textures/Resources/Start/Select");
        public void Update(int poX, int poY, Vector2? basePos, bool press, bool sound)
        {
            MouseOver = Enable && IsInTexture(Convert.ToSingle(poX) / DrawScale, Convert.ToSingle(poY) / DrawScale, basePos) && (poX != 0 || poY != 0);
            if (MouseOver)
            {
                if (!PreMouseOver && sound)
                {
                    //player.Play();
                    Platform.Current.PlayEffect(@"Content\Sound\Select");
                }
                if (OnMouseOver != null) OnMouseOver.Invoke(null, null);
                if (press)
                {
                    PressButton();
                    press = false;
                }
            }
        }
        public void Draw()
        {
            Draw(null);
        }
        public void Draw(Vector2? basePos)
        {
            Draw(basePos, Color.White * Alpha, Alpha, null);
        }
        public void Draw(Vector2? basePos, Color color)
        {
            Draw(basePos, color, Alpha, null);
        }
        public void Draw(Vector2? basePos, Color color, float alpha, int? texIndex)
        {
            Alpha = alpha;
            if (Visible)
            {
                CacheManager.Draw(Text, (basePos == null ? Position : (Vector2)(Position + basePos)) * DrawScale, texIndex == null ? Rectangle : TextureRecs.Recs[(int)texIndex], color * Alpha, SpriteEffects.None, Scale);
                if (!String.IsNullOrEmpty(ViewText))
                {
                    CacheManager.DrawString(ViewFont == null ? Session.Current.Font : ViewFont, ViewText, ((basePos == null ? Position : (Vector2)(Position + basePos)) + ViewTextPos) * DrawScale, (MouseOver || Selected) ? ViewTextColor2 * Alpha : ViewTextColor1 * Alpha, 0f, Vector2.Zero, Scale * ViewTextScale, SpriteEffects.None, 0f);
                }
            }
        }


        public void DrawToCanvas(SpriteBatch batch)
        {
            if (Visible)
            {
                bounds = new List<Bounds>();
                batch.Draw(CacheManager.LoadTexture(Text), Position * DrawScale, Rectangle, color * Alpha, 0f, Vector2.Zero, Scale, SpriteEffects.None, Depth);
                bounds.Add(new Bounds() { X = Position.X, Y = Position.Y, X2 = Position.X + ((Rectangle)Rectangle).Width, Y2 = Position.Y + ((Rectangle)Rectangle).Height });
                if (!String.IsNullOrEmpty(ViewText))
                {

                    List<Bounds> textBounds = CacheManager.DrawStringReturnBounds(batch, Session.Current.Font, Text, Position * DrawScale, (MouseOver || Selected) ? ViewTextColor2 * Alpha : ViewTextColor1 * Alpha, 0f, Vector2.Zero, Scale * ViewTextScale, SpriteEffects.None, Depth);
                    //处理文字边界范围
                    textBounds.Add(bounds[0]);
                    bounds = textBounds;
                }

            }
        }

        public void CalculateControlSize()
        {
            if (!String.IsNullOrEmpty(ViewText))
                bounds = CacheManager.CalculateTextBounds(Session.Current.Font, Text, OffsetPos, Scale);
            else
                bounds = new List<Bounds>();

            bounds.Add(new Bounds() { X = OffsetPos.X, Y = OffsetPos.Y, X2 = OffsetPos.X + ((Rectangle)Rectangle).Width, Y2 = OffsetPos.Y + ((Rectangle)Rectangle).Height });//加上复选框的范围
            Width = 0;
            Height = 0;
            bounds.ForEach(b =>
            {
                Width = Width > b.Width ? Width : b.Width;
                Height = Height > b.Height ? Height : b.Height;
            });


        }

        public void UpdateCanvas()
        {
            int poX = InputManager.PoX;
            int poY = InputManager.PoY;
            bool press = InputManager.IsPressed;
            MouseOver = Enable && IsInCanvasTexture(Convert.ToSingle(poX) / DrawScale, Convert.ToSingle(poY) / DrawScale) && (poX != 0 || poY != 0);
            if (MouseOver)
            {
                if (!PreMouseOver && Sound)
                {
                    //player.Play();
                    Platform.Current.PlayEffect(@"Content\Sound\Select");
                }
                if (OnMouseOver != null) OnMouseOver.Invoke(null, null);
                if (press)
                {
                    PressButton();
                }
            }
        }

        /// <summary>
        /// 判断鼠标是否经过控件
        /// </summary>
        /// <param name="poX">鼠标横坐标</param>
        /// <param name="poY">鼠标纵坐标</param>
        /// <returns>返回是否经过控件的布尔值</returns>
        public bool IsInCanvasTexture(float poX, float poY)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {

                PreMouseOver = MouseOver;

                foreach (Bounds relativeBound in bounds)
                    //通过三个条件相与判定鼠标是否经过控件
                    if (IsInFrame(relativeBound))//控件某一范围在可视框架之内
                    {
                        Bounds bound = new Bounds()//将画布内控件范围的相对坐标变成屏幕上当前的绝对坐标
                        {
                            X = baseFrame.Position.X + relativeBound.X - baseFrame.VisualFrame.X,
                            Y = baseFrame.Position.Y + relativeBound.Y - baseFrame.VisualFrame.Y,
                            X2 = baseFrame.Position.X + relativeBound.X2 - baseFrame.VisualFrame.X,
                            Y2 = baseFrame.Position.Y + relativeBound.Y2 - baseFrame.VisualFrame.Y
                        };


                        MouseOver = baseFrame.Position.X <= poX && poX <= baseFrame.Position.X + baseFrame.VisualFrame.Width  //鼠标在可视框架之内
                            && baseFrame.Position.Y <= poY && poY <= baseFrame.Position.Y + baseFrame.VisualFrame.Height
                            && bound.X - ExtDis <= poX && poX <= bound.X2 + ExtDis  //鼠标在控件范围之内
                            && bound.Y - ExtDis <= poY && poY <= bound.Y2 + ExtDis;

                        if (MouseOver)
                            return MouseOver;//一旦判断鼠标在一个范围内则停止其他范围矩形的判断检索
                    }
            }
            else
            {
                MouseOver = false;
            }
            return MouseOver;
        }

        /// <summary>
        /// 判定控件的范围是否在上级框架之内
        /// </summary>
        /// <param name="bound">控件的范围</param>
        /// <returns>返回是否在框架内的布尔值</returns>
        protected bool IsInFrame(Bounds bound)
        {
            return (bound.X > baseFrame.VisualFrame.X && bound.X < baseFrame.VisualFrame.X + baseFrame.VisualFrame.Width) ||
                (bound.X2 > baseFrame.VisualFrame.X && bound.X2 < baseFrame.VisualFrame.X + baseFrame.VisualFrame.Width) ||
                (bound.Y > baseFrame.VisualFrame.Y && bound.Y < baseFrame.VisualFrame.Y + baseFrame.VisualFrame.Height) ||
                (bound.Y2 > baseFrame.VisualFrame.Y && bound.Y2 < baseFrame.VisualFrame.Y + baseFrame.VisualFrame.Height);
        }
    }

}

/*
        public ButtonTexture(string name, Texture2D tex1, Texture2D tex2, Texture2D tex3, Vector2? pos)
        {
            Name = name;
            Textures = new Texture2D[] { tex1, tex2, tex3 };
            if (pos != null) Position = (Vector2)pos;
        }
        public ButtonTexture(string text, string name, Rectangle[] recs, Vector2? pos, bool live)
        {
            Text = text;
            Name = name;
            Recs = recs;
            if (pos != null) Position = (Vector2)pos;
            Textures = new Texture2D[] { FontCharacter.Load(Text, "", live ? CacheLiveType.Live : CacheLiveType.Scene, false) };
        }
*/
