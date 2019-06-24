using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;
using Platforms;
using FontStashSharp;

namespace GamePanels
{
    public struct CheckBoxSetting
    {
        public SpriteFont ViewFont;
        public Color? ViewTextColor;
        /// <summary>
        /// 鼠标移动上去的颜色
        /// </summary>
        public Color? ViewTextColorMouseOver;
        public float? DrawScale;
        /// <summary>
        /// 整个控件的缩放倍数
        /// </summary>
        public float? Scale;
        /// <summary>
        /// 字体与复选框之间的距离偏移量
        /// </summary>
        public Vector2? Offset;
        public float? ViewTextScale;
    }
    public class CheckBoxPressEventArgs : EventArgs
    {
        public CheckBoxPressEventArgs()
        {
        }
    }
    public delegate void CheckBoxPressEventHandler(object sender, ButtonPressEventArgs e);
    /// <summary>
    /// 复选框，可以改变字体和字号，还有文字与复选框之间的距离
    /// </summary>
    public class CheckBox
    {
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 复选框中要显示的文字
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 材质文件所在路径
        /// </summary>
        public string Path { get; set; }
        public Vector2 Position { get; set; }
        public string Key { get; set; }
        /// <summary>
        /// 包含所有材质的资源
        /// </summary>
        public TextureRecs cbTextureRecs { get; set; }
        public float Alpha = 1f;
        public bool Visible = true;
        public float DrawScale = 1f;
        public bool Sound = true;
        /// <summary>
        /// 整个控件的缩放倍数
        /// </summary>
        public float Scale = 1f;
        //public SpriteFont ViewFont;
        /// <summary>
        /// 鼠标是否经过
        /// </summary>
        public bool MouseOver = false;
        /// <summary>
        /// 上一次是否鼠标已经在范围内的标志
        /// </summary>
        public bool PreMouseOver { get; set; }
        public bool Selected = false;
        /// <summary>
        /// 字体的颜色
        /// </summary>
        public Color ViewTextColor = Color.White;
        /// <summary>
        /// 鼠标移动上去的颜色
        /// </summary>
        public Color ViewTextColorMouseOver = Color.Yellow;
        public DateTime? prePressTime;
        /// <summary>
        /// 文字的缩放倍数
        /// </summary>
        public float ViewTextScale = 1f;
        public bool Enable = true;
        public bool Locked = false;
        public int ExtDis = 0;
        public bool FireEventWhenUnEnable = false;
        public event CheckBoxPressEventHandler OnMouseOver, OnButtonPress;
        /// <summary>
        /// 包含控件所有范围矩阵的列表
        /// </summary>
        protected List<Bounds> bounds;
        /// <summary>
        /// 根据是否选择过经过控件决定要显示的材质矩形
        /// </summary>
        public Rectangle? cbRectangle
        {
            get
            {
                if (cbTextureRecs.Recs != null)
                {
                    if (Locked)
                    {
                        return cbTextureRecs.Recs.Length > 3 ? cbTextureRecs.Recs[3] : cbTextureRecs.Recs[0];
                    }
                    else if (Enable)
                    {
                        return (!Selected && !MouseOver ? cbTextureRecs.Recs[0] : (cbTextureRecs.Recs.Length > 1 ? cbTextureRecs.Recs[1] : cbTextureRecs.Recs[0]));
                    }
                    else
                    {
                        if (cbTextureRecs.Recs.Length > 3)
                        {
                            return Selected ? cbTextureRecs.Recs[3] : cbTextureRecs.Recs[2];
                        }
                        else if (cbTextureRecs.Recs.Length > 2)
                        {
                            return cbTextureRecs.Recs[2];
                        }
                        else
                        {
                            return Selected ? cbTextureRecs.Recs[1] : cbTextureRecs.Recs[0];
                        }
                    }
                }
                else return null;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">包含材质的路径</param>
        /// <param name="name">材质的名称</param>
        /// <param name="text">复选框中要显示的文字</param>
        /// <param name="pos">控件的位置</param>
        public CheckBox(string path, string name, string text, Vector2? pos)
        {
            Text = text;
            Name = name;
            Path = path;
            Key = path + "#" + name;
            cbTextureRecs = Session.TextureRecs[Key];
            if (pos != null) Position = (Vector2)pos;
        }

        /// <summary>
        /// 按下复选框
        /// </summary>
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

        /// <summary>
        /// 判断鼠标是否在控件范围内
        /// </summary>
        /// <param name="poX">鼠标的横坐标</param>
        /// <param name="poY">鼠标的纵坐标</param>
        /// <param name="basePos">偏移量</param>
        /// <returns>返回鼠标是否经过控件</returns>
        public bool IsInTexture(float poX, float poY, Vector2? basePos)
        {

            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                PreMouseOver = MouseOver;
                foreach (Bounds bound in bounds)//搜索控件的所有范围矩形列表
                {

                    if (basePos == null)
                    {
                        MouseOver = bound.X - ExtDis <= poX && poX <= bound.X2 + ExtDis
                            && bound.Y - ExtDis <= poY && poY <= bound.Y2 + ExtDis;
                    }
                    else
                    {
                        MouseOver = bound.X + ((Vector2)basePos).X - ExtDis <= poX && poX <= bound.X2 + ((Vector2)basePos).X + ExtDis
                            && bound.Y + ((Vector2)basePos).Y - ExtDis <= poY && poY <= bound.Y2 + ((Vector2)basePos).Y + ExtDis;
                    }

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
        /// 判断鼠标是否在控件范围内
        /// </summary>
        /// <param name="poX">鼠标的横坐标</param>
        /// <param name="poY">鼠标的纵坐标</param>
        /// <param name="basePos">偏移量</param>
        /// <returns>返回鼠标是否经过控件</returns>
        public bool IsInTexture(int poX, int poY, Vector2? basePos)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                PreMouseOver = MouseOver;
                foreach (Bounds bound in bounds)
                {
                    if (basePos == null)
                    {
                        MouseOver = bound.X - ExtDis <= poX && poX <= bound.X2 + ExtDis
                            && bound.Y - ExtDis <= poY && poY <= bound.Y2 + ExtDis;
                    }
                    else
                    {
                        MouseOver = bound.X + ((Vector2)basePos).X - ExtDis <= poX && poX <= bound.X2 + ((Vector2)basePos).X + ExtDis
                            && bound.Y + ((Vector2)basePos).Y - ExtDis <= poY && poY <= bound.Y2 + ((Vector2)basePos).Y + ExtDis;
                    }
                    if (MouseOver)
                        return MouseOver;
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

        /// <summary>
        /// 空参数的画图方法
        /// </summary>
        public void Draw()
        {
            Draw(null, Color.White * Alpha, 0.8f, null, null, null);
        }

        /// <summary>
        /// 使用结构体作为函数参数的好处是可以统一定制一批控件的样式设置
        /// </summary>
        /// <param name="cbSetting">控件的样式设置结构体</param>
        public void Draw(CheckBoxSetting cbSetting)
        {
            DrawScale = cbSetting.DrawScale ?? DrawScale;
            ViewTextScale = cbSetting.ViewTextScale ?? ViewTextScale;
            ViewTextColor = cbSetting.ViewTextColor ?? ViewTextColor;
            ViewTextColorMouseOver = cbSetting.ViewTextColorMouseOver ?? ViewTextColorMouseOver;
            Scale = cbSetting.Scale ?? Scale;
            Draw(null, Color.White * Alpha, 1f, null, cbSetting.Offset, cbSetting.ViewFont);
        }

        /// <summary>
        /// 画控件的方法
        /// </summary>
        /// <param name="basePos">控件位置的偏移量</param>
        /// <param name="color">颜色</param>
        /// <param name="alpha"></param>
        /// <param name="texIndex">用哪个材质</param>
        /// <param name="offset">控件文字的偏移量（文字与复选框之间的距离）</param>
        /// <param name="viewFont">字体</param>
        public void Draw(Vector2? basePos, Color color, float alpha, int? texIndex, Vector2? offset, SpriteFont viewFont)
        {
            Alpha = alpha;
            if (Visible)
            {

                Bounds bound = CacheManager.Draw(Path, (basePos == null ? Position : (Vector2)(Position + basePos)) * DrawScale, texIndex == null ? cbRectangle : cbTextureRecs.Recs[(int)texIndex], color * Alpha, SpriteEffects.None, Scale);

                //偏移量要加上画复选框后的宽度
                if (offset != null)
                    offset += new Vector2(bound.Width, 0);
                else
                    offset = new Vector2(bound.Width, 2);//默认的偏移量

                bounds = CacheManager.DrawStringReturnBounds(viewFont ?? Session.Current.Font, Text, (basePos == null ? (Vector2)(Position + offset) : (Vector2)(Position + basePos + offset)) * DrawScale, (MouseOver || Selected) ? ViewTextColorMouseOver * Alpha : ViewTextColor * Alpha, 0f, Vector2.Zero, Scale * ViewTextScale, SpriteEffects.None, 0f);

                bounds.Add(bound);

            }

        }
    }
}