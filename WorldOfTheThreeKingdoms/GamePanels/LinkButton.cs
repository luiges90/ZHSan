using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using GameManager;
using Platforms;

namespace GamePanels
{
    /// <summary>
    /// 鏈接按鈕
    /// </summary>
    public class LinkButton
    {
        //Device gra = SeasonGame.Current.Device;
        int PerCharacterWidth = 28;
        int PerCharacterHeight = 30;
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                //Texture = FontCharacter.Load(text, Session.DisplayFont, 12, color);
            }
        }
        public float TextScale = 1f;
        public bool FireEventWhenUnEnable = false;
        public bool MouseOver { get; set; }
        public bool Visible { get; set; }
        public bool Enable { get; set; }
        public Vector2 Position { get; set; }
        public Color Color = Color.White;
        public bool PreMouseOver { get; set; }
        public event ButtonPressEventHandler OnButtonPress;
        DateTime? lastClickTime;
        public LinkButton(string name)
        {
            Text = name;
            Visible = Enable = true; MouseOver = false;
        }
        public void ClearOnButtonPress()
        {
            OnButtonPress = null;
        }
        public void OnClick()
        {
            if (OnButtonPress != null)
            {
                OnButtonPress.Invoke(null, null);
            }
        }
        public void Update(Vector3? basePos, bool sound)
        {
            MouseOver = IsInTexture(InputManager.PoX, InputManager.PoY, basePos);
            if (MouseOver)
            {
                if (!PreMouseOver && sound) Platform.Current.PlayEffect(@"Content\Sound\Move");
                if (InputManager.IsPressed)
                {
                    if (lastClickTime != null)
                    {
                        TimeSpan ts = DateTime.Now - (DateTime)lastClickTime;
                        if (ts.TotalSeconds < 1 || !Platform.IsActive)
                        {
                            return;
                        }
                    }
                    lastClickTime = DateTime.Now;
                    OnClick();
                }
            }
        }
        public bool IsInTexture(int poX, int poY, Vector3? basePos)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                PreMouseOver = MouseOver;
                if (basePos == null)
                {
                    MouseOver = this.Position.X <= poX && poX <= this.Position.X + (PerCharacterWidth * Text.Length)
                        && this.Position.Y <= poY && poY <= this.Position.Y + PerCharacterHeight;
                }
                else
                {
                    MouseOver = this.Position.X + ((Vector3)basePos).X <= poX && poX <= this.Position.X + ((Vector3)basePos).X + (PerCharacterWidth * Text.Length)
                        && this.Position.Y + ((Vector3)basePos).Y <= poY && poY <= this.Position.Y + ((Vector3)basePos).Y + PerCharacterHeight;
                }
            }
            else
            {
                MouseOver = false;
            }
            return MouseOver;
        }
        public void Draw()
        {
            Draw(1f);
        }
        public void Draw(float alpha)
        {
            if (Visible)
            {
                CacheManager.DrawString(Session.Current.Font, Text, Position, (Enable ? Color : Color.Silver) * alpha, 0f, Vector2.Zero, TextScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);
                //Rectangle rec = new Rectangle(Convert.ToInt32(Position.X), Convert.ToInt32(Position.Y + Texture.Height - 3), Convert.ToInt32(Texture.Width), 1);
                Rectangle rec = new Rectangle(0, 0, PerCharacterWidth * Text.Length, 1);
                if (MouseOver)
                {
                    CacheManager.Draw(@"Content\Textures\Resources\Start\Line-Red", Position + new Vector2(0, PerCharacterHeight - 5), rec, Color.White * alpha, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, TextScale);
                }
            }
        }
    }
}
