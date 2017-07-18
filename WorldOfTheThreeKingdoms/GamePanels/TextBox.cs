using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Tools;
using Platforms;
using GameManager;

namespace GamePanels
{
    public enum TextBoxMode
    {
        Normal, Password, Chinese
    }
    public enum TextBoxStyle
    {
        Big, Medium, Small, Tiny, Desc, Plus, Wide
    }
    /// <summary>
    /// 文本框
    /// </summary>
    public class TextBox
    {
        ButtonTexture btTexture;

        float totalElapse;
        bool displaycursor;

        float lastInputTime = 0f;
        float afterLastInputTime = 0f;
		float afterLastTextTime = 0f;

        int PerCharacterWidth = 29;
        int PerCharacterHeight = 29;

        public float tranAlpha = 1f;

        public int? MaxLength = null;

        string text = "";
        public string Text
        {
            get
            {
                return text.NullToStringTrim();
            }
            set
            {
                if (text != value)
                {
                    ViewText = "";
                }
                text = value;                
            }
        }
        public string preText = "";
        public string ViewText = "";
        public string Title = "";
        public string Desc = "";
        public string Cursor;
        public bool MouseOver { get; set; }
        public bool Visible { get; set; }
        public bool Enable 
        {
            get { return btTexture.Enable; }
            set { btTexture.Enable = value; }
        }
        public bool FireEventWhenUnEnable = false;
        public bool Selected = false;
        public bool CanAdd = true;
        Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set
            {
                position = value;
                btTexture.Position = Position + new Vector2(3, 3);
            }
        }

        public float TextScale = 1f;

        Vector2 TextPosition
        {
            get
            {
                Vector2 pos = new Vector2(5, 5);
                int charWidth1 = 28, charWidth2 = 14;
                if (TextBoxStyle == GamePanels.TextBoxStyle.Big)
                {
                    
                }
                else if (TextBoxStyle == GamePanels.TextBoxStyle.Medium)
                {
                    //charWidth1 = 20, charWidth2 = 10;
                }
                else if (TextBoxStyle == GamePanels.TextBoxStyle.Small)
                {
                    charWidth1 = 20; charWidth2 = 10;
                }
                else if (TextBoxStyle == GamePanels.TextBoxStyle.Tiny)
                {
                    charWidth1 = 20; charWidth2 = 10;
                }

                if (!String.IsNullOrEmpty(ViewText))
                {
                    char[] chars = ViewText.ToCharArray();
                    foreach (char ch in chars)
                    {
                        if (ch > 128)
                        {
                            pos.X += charWidth1 * TextScale;
                        }
                        else
                        {
                            pos.X += charWidth2 * TextScale;
                        }
                    }
                }
                return pos;
            }
        }

        public TextBoxMode TextBoxMode = TextBoxMode.Normal;
        public TextBoxStyle TextBoxStyle = TextBoxStyle.Big;
        public bool DisplayStarWhenEmpty { get; set; }
        public event ButtonPressEventHandler OnButtonPress, OnTextBoxSelected;
        public event EventHandler OnKeyTabPress;
        public event EventHandler OnKeyEnterPress;
        public bool EventFired = false;

        public event EventHandler CheckTextValid;

        public TextBox(TextBoxStyle style, string text)
        {
            TextBoxStyle = style;
            btTexture = new ButtonTexture(@"Content\Textures\Resources\Start\TextBox-" + TextBoxStyle.ToString(), "TextBox-" + TextBoxStyle.ToString(), null) 
            { 
                FireEventWhenUnEnable = true 
            };
            Cursor = TextBoxStyle.ToString();
            MouseOver = false; Visible = true; Enable = true;
            Text = text;
            ViewText = Text.WordsSubString(8);
            //if (Setting.Current.Language == "传统" && !String.IsNullOrEmpty(ViewText))
            //{
            //    ViewText = ViewText.TranslationWords(false, true);
            //}
        }

        public void PressButton()
        {
            PressButton(this, null);
        }

        public void PressButton(object sender, ButtonPressEventArgs e)
        {
            if (OnButtonPress != null) OnButtonPress.Invoke(sender, e);
        }

        public void HandleInputChinese()
        {
            //处理从IME得到的字符
            List<Character> getChars = Platform.Current.GetChars();   // WindowInputCapturer.myCharacters;
            foreach (var getChar in getChars)
            {
                if (getChar.IsUsed == false)
                {
                    if (getChar.CharaterType == characterType.Char)
                    {
                        //sfx.AddText(getChar.Chars.ToString());
                        if (CanAdd)
                        {
                            Text += getChar.Chars.ToString();
                        }
                        Platform.Current.PlayEffect(@"Content\Sound\Move");
                    }
                    //回车等功能键触发事件也可以由KeyboardState来截取处理
                    else if (getChar.CharaterType == characterType.Enter)
                    {
                        //Text += "\r";
                        if (OnKeyEnterPress != null) OnKeyEnterPress.Invoke(null, null);
                        Platform.Current.ClearChars(); //WindowInputCapturer.myCharacters.Clear();
                        break;
                    }
                    else if (getChar.CharaterType == characterType.Tab)
                    {
                        //Text += "    ";
                        if (OnKeyTabPress != null) { OnKeyTabPress.Invoke(null, null); }
                        Platform.Current.ClearChars(); //WindowInputCapturer.myCharacters.Clear();
                        break;
                    }
                    else if (getChar.CharaterType == characterType.BackSpace)
                    {
                        Text = WordTools.BackSpaceString(Text);
                    }
                    getChar.IsUsed = true;
                }
            }
            if (getChars != null && getChars.Count > 0)
            {

            }
        }

        public void HandleInput(float gameTime)
        {
            if (!Enable)
            {
                return;
            }
            EventFired = false;
            btTexture.Update(InputManager.PoX, InputManager.PoY, null);
            if (btTexture.MouseOver && InputManager.IsPressed)
            {
                if (OnButtonPress != null) PressButton();
                if (Platform.PlatFormType == PlatFormType.Win)
                 {
                    if (!Selected)
                    {
                        if (TextBoxMode == GamePanels.TextBoxMode.Chinese)
                        {
                            Platform.Current.ClearChars();  //WindowInputCapturer.myCharacters.Clear();
                            Platform.Current.WindowInputCapturerEnable = true; //WindowInputCapturer.Enable = true;
                        }
                        else
                        {
                            Platform.Current.WindowInputCapturerEnable = false; // WindowInputCapturer.Enable = false;
                        }
                    }
                }

				if (Platform.PlatFormType == PlatFormType.iOS || Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.UWP)
                {
                    if (!Platform.Current.IsGuideVisible)
                    {
                        Platform.Current.ShowKeyBoard(PlayerIndex.One, Title, Desc, this.Text, CallbackFunction);
                        //try
                        //{
                        //Guide.BeginShowKeyboardInput(PlayerIndex.One, Title, Desc, this.Text, CallbackFunction, null);
                        //}
                        //catch(Exception ex)
                        //{
                        //}
                    }
                }
                //else if (Season.PlatForm == PlatForm.WinRT)
                //{
                //    //if (!Season.KeyBoardAvailable)
                //    //{
                //        Season.Current.ShowText(Text, CallbackFunction);
                //    //}
                //}
                bool preSelected = Selected;
                Selected = true;
                if (!preSelected && OnTextBoxSelected != null)
                {
                    OnTextBoxSelected.Invoke(null, null);
                }
            }

            btTexture.Selected = Selected;
            if (Selected)
            {                
                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.UWP || Platform.PlatFormType == PlatFormType.Desktop)   // || Season.PlatForm == PlatForm.MacOS || Season.PlatForm == PlatForm.Linux
                {
                    if (InputManager.HasKeys && (TextBoxMode != GamePanels.TextBoxMode.Chinese || Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.UWP && Platform.Current.KeyBoardAvailable)) //Season.PlatForm == PlatForm.WinRT || 
                    {
                        if (InputManager.KeyBoardState == InputManager.KeyBoardStatePre)
                        {
                            afterLastInputTime += gameTime;
                            if (afterLastInputTime < 0.1f)
                            {
                                return;
                            }
							afterLastInputTime = 0f;
                        }
                        else
                        {
                        }                        
						bool shift = false;
						if (InputManager.KeyBoardState.IsKeyDown (Microsoft.Xna.Framework.Input.Keys.LeftShift)
							|| InputManager.KeyBoardState.IsKeyDown (Microsoft.Xna.Framework.Input.Keys.RightShift)) {
							shift = true;
						}
                        if (InputManager.KeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Tab))
                        {
                            if (OnKeyTabPress != null) 
                            { 
                                OnKeyTabPress.Invoke(null, null);
                                EventFired = true;
                            }
                        }
                        else if (InputManager.KeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
                        {
                            if (OnKeyEnterPress != null)
                            {
                                OnKeyEnterPress.Invoke(null, null);
                                EventFired = true;
                            }
                        }
                        else if (InputManager.KeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Back) && !String.IsNullOrEmpty(Text))
                        {
                            afterLastTextTime += gameTime;
                            if (afterLastTextTime < 0.04f)
                            {
                                return;
                            }
                            afterLastTextTime = 0f;
                            Text = Text.Remove(Text.Length - 1, 1);
                        }
                        else if (InputManager.KeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space))
                        {
                            afterLastTextTime += gameTime;
                            if (afterLastTextTime < 0.04f)
                            {
                                return;
                            }
                            afterLastTextTime = 0f;
                            Text = Text.Insert(Text.Length, " ");
                        }
                        else
                        {
                            string tex = InputManager.ConvertKeyToChar(shift);
                            if (!String.IsNullOrEmpty(tex))
                            {
                                afterLastTextTime += gameTime;
                                if (afterLastTextTime < 0.1f && !String.IsNullOrEmpty(Text) && Text.ToCharArray()[Text.Length - 1].ToString() == tex)
                                {
                                    return;
                                }
                                //if (!String.IsNullOrEmpty (Text) && Text.ToCharArray ()[Text.Length-1].ToString() == tex) {
                                //	return;
                                //}
                                afterLastTextTime = 0f;
                                Text += tex;
                                Platform.Current.PlayEffect(@"Content\Sound\Move");
                            }
                        }
                        ViewText = Text;
                    }
                }
            }
        }

        private void CallbackFunction(IAsyncResult ar)
        {
            if (ar == null || ar.IsCompleted == true)
            {
                string text = Platform.Current.EndShowKeyBoard(ar); // Guide.EndShowKeyboardInput(ar);
                if (!String.IsNullOrEmpty(text))
                {
                    Text = text;
                    ViewText = Text.WordsSubString(8);
                    if (CheckTextValid != null)
                    {
                        CheckTextValid.Invoke(null, null);
                    }
                }
            }
        }

        public void Update(float gameTime)
        {
			if (Platform.PlatFormType == PlatFormType.iOS && Platform.Current.IsGuideVisible) {
				//Enable = false;
			}
            if (Selected)
            {
                if (Platform.PlatFormType == PlatFormType.Win)
                {
                    if (TextBoxMode == GamePanels.TextBoxMode.Chinese)
                    {
                        HandleInputChinese();
                    }
                }

                if (MaxLength != null && Text.Length > (int)MaxLength)
                {
                    Text = Text.WordsSubString((int)MaxLength, 0);
                }

                float elapsed = gameTime;
                totalElapse += elapsed;
                if (totalElapse >= 0.5f)
                {
                    displaycursor = !displaycursor;
                    totalElapse -= 0.5f;
                }
                ProcessViewText();
            }
            if (!String.IsNullOrEmpty(Text) && String.IsNullOrEmpty(ViewText))
            {
                ProcessViewText();
            }
        }

        void ProcessViewText()
        {
            if (!String.IsNullOrEmpty(Text) && preText != Text)  //Session.Language == "传统" && 
            {
                Text = Text.TranslationWords(false, true);
            }
            else
            {

            }
            preText = Text;
            if (ViewText != Text && (!String.IsNullOrEmpty(ViewText) || !String.IsNullOrEmpty(Text)))
            {
                ViewText = Text;
            }
        }
        
        public bool IsInTexture(int poX, int poY)
        {
            if (Visible && (Enable || FireEventWhenUnEnable))
            {
                MouseOver = this.Position.X <= poX && poX <= this.Position.X + (Text.Length * PerCharacterWidth) // texture.Width
                    && this.Position.Y <= poY && poY <= this.Position.Y + PerCharacterHeight;
            }
            else
            {
                MouseOver = false;
            }
            return MouseOver;
        }

        public void Draw()
        {
            if (Visible)
            {
                if (TextBoxStyle == TextBoxStyle.Wide)
                {
                    CacheManager.Draw(@"Content\Textures\Resources\Start\TextBox-Wide", Position, Color.White * tranAlpha);
                    int resultRow = 0;
                    int resultWidth = 0;
                    string result = ViewText.SplitLineString(Convert.ToInt32(15f / TextScale)-1, TextScale < 1f ? 4 : 3, ref resultRow, ref resultWidth, 2);
                    if (resultRow == 0)
                    {
                        resultRow = 1;
                    }
                    result = CacheManager.CheckTextCache(Session.Current.Font, result, true, true);
                    CacheManager.DrawString(Session.Current.Font, result, Position + new Vector2(2, 7), Color.Black * tranAlpha, 0f, Vector2.Zero, TextScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0f);

                    if (Selected)
                    {

                        if (resultRow >= 5 && resultWidth >= 28 * 15 * TextScale)
                        {
                            CanAdd = false;
                        }
                        else
                        {
                            CanAdd = true;
                        }

                        if (Selected && tranAlpha > 0.5f)
                        {
                            CacheManager.Draw(@"Content\Textures\Resources\Start\Input-Cursor-Big", Position + new Vector2(resultWidth * TextScale, (resultRow - 1) * 32) + new Vector2(5, 7), Color.White * tranAlpha);
                        }
                    }
                }
                else
                {
                    btTexture.Alpha = tranAlpha;
                    btTexture.Draw();
                    if (TextBoxMode != GamePanels.TextBoxMode.Password)
                    {
                        Vector2 pos = new Vector2(5, 6);
                        float scale = 1f;
                        if (TextBoxStyle == GamePanels.TextBoxStyle.Big)
                        {
                            if (TextScale < 1f)
                            {
                                pos = new Vector2(5, 9);
                            }
                        }
                        else if (TextBoxStyle == GamePanels.TextBoxStyle.Medium)
                        {
                            scale = 0.9f;
                        }
                        else if (TextBoxStyle == GamePanels.TextBoxStyle.Small)
                        {
                            scale = 0.7f;
                        }
                        else if (TextBoxStyle == GamePanels.TextBoxStyle.Tiny)
                        {
                            scale = 0.7f;
                        }
                        else if (TextBoxStyle == TextBoxStyle.Plus)
                        {
                            //scale = 0.7f;
                            pos = new Vector2(5, 20);
                        }
                        CacheManager.DrawString(Session.Current.Font, ViewText, Position + pos, (Selected ? Color.OrangeRed : Color.Black) * tranAlpha, 0f, Vector2.Zero, scale * TextScale, Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 0, true, true);
                    }
                    else
                    {
                        string pass = "";
                        if (!String.IsNullOrEmpty(Text))
                        {
                            for (int i = 0; i < Text.Length; i++)
                            {
                                pass += "*";
                            }
                        }
                        else if (DisplayStarWhenEmpty && !Selected)
                        {
                            pass = "******";
                        }
                        CacheManager.DrawString(Session.Current.Font, pass, Position + new Vector2(5, 6), Color.Black * tranAlpha);
                    }
                    if (Selected && displaycursor && Enable)
                    {
                        string curs = Cursor.Replace("Tiny", "Small").Replace("Medium", "Small").Replace("Wide", "Big").Replace("Plus", "Big");
                        Vector2 pos = new Vector2(5, 3);
                        if (TextBoxStyle == TextBoxStyle.Plus)
                        {
                            pos = new Vector2(5, 15);
                        }
                        CacheManager.Draw(@"Content\Textures\Resources\Start\Input-Cursor-" + curs, Position + TextPosition + pos, Color.White * tranAlpha);
                    }
                }
            }
        }
    }
}
