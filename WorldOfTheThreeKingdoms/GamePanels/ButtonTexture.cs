using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Platforms;
using GameManager;

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
    public class ButtonTexture
    {
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
        public float Alpha = 1f;
        public float Scale = 1f;

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

        public int Width
        {
            get
            {
                return Convert.ToInt32(TextureRecs.Recs[0].Width * Scale);
            }
        }
        public int Height
        {
            get
            {
                return Convert.ToInt32(TextureRecs.Recs[0].Height * Scale);
            }
        }
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
                    MouseOver = this.Position.X - ExtDis <= poX && poX <= this.Position.X + OriginWidth + ExtDis
                        && this.Position.Y - ExtDis <= poY && poY <= this.Position.Y + OriginHeight + ExtDis;
                }
                else
                {
                    MouseOver = this.Position.X + ((Vector2)basePos).X - ExtDis <= poX && poX <= this.Position.X + ((Vector2)basePos).X + OriginWidth + ExtDis
                        && this.Position.Y + ((Vector2)basePos).Y - ExtDis <= poY && poY <= this.Position.Y + ((Vector2)basePos).Y + OriginHeight + ExtDis;
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