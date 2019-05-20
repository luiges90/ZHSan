using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace OptionDialogPlugin
{

    internal class OptionDialog
    {
        private bool dragging;
        private bool isShowing;
        internal int ItemHeight;
        internal int ItemWidth;
        internal int Margin;
        private List<GameDelegates.VoidFunction> OptionFunctions = new List<GameDelegates.VoidFunction>();
        private List<object> OptionObjects = new List<object>();
        private List<bool> OptionObjectsSelected = new List<bool>();
        internal PlatformTexture OptionSelectedTexture;
        internal FreeTextList OptionTextList;
        internal FreeTextList OptionTextListPage=new FreeTextList();
        internal PlatformTexture OptionTexture;
        private List<PlatformTexture> OptionTextures = new List<PlatformTexture>();
        internal GameDelegates.ObjectFunction ReturnObjectFunction;
        
        internal Dictionary<string, OptionStyle> Styles = new Dictionary<string, OptionStyle>();
        private Rectangle TitleDisplayPosition;
        internal int TitleHeight;
        internal int TitleMargin;
        internal FreeText TitleText;
        internal PlatformTexture TitleTexture;
        internal int TitleWidth;
        GamePanels.ButtonTexture btPre, btNext;
        internal int pageIndex = 1;
        int pageCount = 0;
        int pageitems = 10;
        int page = 0;
        internal void AddOptionItem(string Text, object obj, GameDelegates.VoidFunction optionFunction)
        {
            this.OptionTextList.AddText(Text);
            this.OptionObjects.Add(obj);
            this.OptionTextures.Add(this.OptionTexture);
            this.OptionFunctions.Add(optionFunction);
        }

        internal void Clear()
        {
            this.ReturnObjectFunction = null;
            this.OptionTextList.Clear();
            this.OptionObjects.Clear();
            this.OptionTextures.Clear();
            this.OptionFunctions.Clear();
        }

        public void Draw()
        {
            if (this.OptionTextList.Count != 0)
            {
                if(this.OptionTextList.Count>pageitems)
                {
                    btNext.Draw();
                    btPre.Draw();
                }
                Rectangle? sourceRectangle = null;
                CacheManager.Draw(this.TitleTexture, this.TitleDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                this.TitleText.Draw(0.1999f);
                //if (personList.IndexOf(p) < (onepageperson) * pageIndex && personList.IndexOf(p) >= (onepageperson) * (pageIndex - 1))
                for (int i = 0; i < this.OptionTextures.Count; i++)
                {
                    if(i<10 * pageIndex && i>= pageitems * (pageIndex - 1))
                    {
                        sourceRectangle = null;
                        CacheManager.Draw(this.OptionTextures[i], this.OptionTextList.DisplayPosition(i), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                        this.OptionTextList.Draw(i, 0.1999f);
                    }
                }
                //float optionText = 0.1999f;
                //this.OptionTextList.Draw(optionText);
            }
        }

        internal void EndAddOptions()
        {
            //this.OptionTextList.ResetAllTextTextures();
            this.ItemWidth = this.OptionTextList.MaxWidth + this.Margin;
            int n = 0;
            for (int i = 0; i < this.OptionTextures.Count; i++)
            {
                n++;
                this.OptionTextList[i].Position = new Rectangle(0, n * this.ItemHeight, this.ItemWidth, this.ItemHeight);
                if (n % pageitems == 0)
                {
                    n = 0;
                }
            }
        }

        internal void Initialize(Screen screen)
        {
            
        }

        private void InvokeFunction(int i)
        {
            if (this.OptionFunctions[i] != null)
            {
                if (this.ReturnObjectFunction != null)
                {
                    this.ReturnObjectFunction(this.OptionObjects[i]);
                }
                this.OptionFunctions[i]();
            }
        }

        private void screen_OnMouseLeftDown(Point position)
        {

        }

        private void screen_OnMouseLeftUp(Point position)
        {
            for (int i = 0; i < this.OptionTextures.Count; i++)
            {
                if (StaticMethods.PointInRectangle(position, this.OptionTextList.DisplayPosition(i)))
                {
                    this.IsShowing = false;
                    this.InvokeFunction(i + pageitems * (pageIndex - 1));
                    break;
                }
            }

            this.dragging = false;
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (StaticMethods.PointInRectangle(position, this.TitleDisplayPosition))
            {
                this.dragging = leftDown;
            }
            else
            {
                for (int i = 0; i < this.OptionTextures.Count; i++)
                {
                    if (StaticMethods.PointInRectangle(position, this.OptionTextList.DisplayPosition(i)))
                    {
                        this.OptionTextures[i] = this.OptionSelectedTexture;
                        if (this.OptionObjects[i] is GameObject)
                        {
                            (this.OptionObjects[i] as GameObject).Selected = true;
                        }
                    }
                    else
                    {
                        this.OptionTextures[i] = this.OptionTexture;
                        if (this.OptionObjects[i] is GameObject)
                        {
                            (this.OptionObjects[i] as GameObject).Selected = false;
                        }
                    }
                }
            }
            if (this.dragging)
            {
                this.OptionTextList.DisplayOffset = new Point(this.OptionTextList.DisplayOffset.X + Session.MainGame.mainGameScreen.MouseOffset.X, this.OptionTextList.DisplayOffset.Y + Session.MainGame.mainGameScreen.MouseOffset.Y);
                this.TitleDisplayPosition = StaticMethods.GetCenterRectangle(new Rectangle(this.OptionTextList.DisplayOffset.X, (this.OptionTextList.DisplayOffset.Y - this.TitleHeight) - this.TitleMargin, this.ItemWidth, this.TitleHeight), new Rectangle(0, 0, this.TitleWidth, this.TitleHeight));
                this.TitleText.DisplayOffset = new Point(this.TitleDisplayPosition.X, this.TitleDisplayPosition.Y);
            }
        }

        private void screen_OnMouseRightDown(Point position)
        {
            this.IsShowing = false;
        }

        internal void SetDisplayOffset(ShowPosition showPosition)
        {
            Rectangle rectDes = new Rectangle(0, 0, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            switch (showPosition)
            {
                case ShowPosition.Center:
                    rect = StaticMethods.GetCenterRectangle(rectDes, rect);
                    break;

                case ShowPosition.Top:
                    rect = StaticMethods.GetTopRectangle(rectDes, rect);
                    break;

                case ShowPosition.Left:
                    rect = StaticMethods.GetLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.Right:
                    rect = StaticMethods.GetRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.Bottom:
                    rect = StaticMethods.GetBottomRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopLeft:
                    rect = StaticMethods.GetTopLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopRight:
                    rect = StaticMethods.GetTopRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomLeft:
                    rect = StaticMethods.GetBottomLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomRight:
                    rect = StaticMethods.GetBottomRightRectangle(rectDes, new Rectangle(rect.X, rect.Y, rect.Width, rect.Height + this.ItemHeight));
                    break;

                case ShowPosition.Mouse:
                    rect.X = Session.MainGame.mainGameScreen.MousePosition.X;
                    rect.Y = Session.MainGame.mainGameScreen.MousePosition.Y;
                    break;
            }
            this.OptionTextList.DisplayOffset = new Point(rect.X, rect.Y);
            this.TitleDisplayPosition = StaticMethods.GetCenterRectangle(new Rectangle(rect.X, (rect.Y - this.TitleHeight) - this.TitleMargin, this.ItemWidth, this.TitleHeight), new Rectangle(0, 0, this.TitleWidth, this.TitleHeight));
            this.TitleText.DisplayOffset = new Point(this.TitleDisplayPosition.X, this.TitleDisplayPosition.Y);
            btPre = new GamePanels.ButtonTexture(@"Content\Textures\Resources\Start\next-alpha", "Left", new Vector2(this.OptionTextList.DisplayOffset.X + 100, this.OptionTextList.DisplayOffset.Y + Height + 50))
            {
                Enable = true,
                Scale = 0.8f
            };
            btPre.OnButtonPress += (sender, e) =>
            {
                if (pageIndex > 1)
                {
                    pageIndex--;
                }

            };

            btNext = new GamePanels.ButtonTexture(@"Content\Textures\Resources\Start\next-alpha", "Right", new Vector2(this.OptionTextList.DisplayOffset.X + Width - 250, this.OptionTextList.DisplayOffset.Y + Height + 50))
            {
                Enable = false,
                Scale = 0.8f
            };
            btNext.OnButtonPress += (sender, e) =>
            {
                if (pageIndex < pageCount)
                {
                    pageIndex++;
                }

            };
        }

        internal void SetStyle(string style)
        {
            OptionStyle style2 = null;
            this.Styles.TryGetValue(style, out style2);
            if (style2 != null)
            {
                this.TitleTexture = style2.TitleTexture;
                this.TitleWidth = style2.TitleWidth;
                this.TitleHeight = style2.TitleHeight;
                this.TitleMargin = style2.TitleMargin;
                this.TitleText = style2.TitleText;
                this.OptionTextList = style2.OptionTextList;
                this.ItemHeight = style2.ItemHeight;
                this.Margin = style2.Margin;
                this.OptionTexture = style2.OptionTexture;
                this.OptionSelectedTexture = style2.OptionSelectedTexture;
            }
        }

        internal void SetTitle(string title)
        {
            this.TitleText.Text = title;
        }

        public void Update()
        {
            if (this.OptionTextList.Count > pageitems)
            {
                OptionTextListPage.TextList = Tools.GenericTools.GetPageList<TextItem>(OptionTextList.TextList, pageIndex.ToString(), pageitems, ref pageCount, ref page);
                if (pageIndex > 1)
                {
                    btPre.Enable = true;
                }
                else
                {
                    btPre.Enable = false;
                }
                if (pageIndex < pageCount)
                {
                    btNext.Enable = true;
                }
                else
                {
                    btNext.Enable = false;
                }
                btPre.Update();
                btNext.Update();
            }

            //for (int i = 0; i < OptionTextListPage.Count; i++)
            //{
            //    var btOne = OptionTextListPage[i];
            //    btOne.Position = new Vector2(150, 145 + 55 * i);
            //    btOne.Update();
            //}
        }

        internal Point DisplayOffset
        {
            get
            {
                return this.OptionTextList.DisplayOffset;
            }
            set
            {
                this.OptionTextList.DisplayOffset = value;
            }
        }

        internal int Height
        {
            get
            {
                if (this.OptionTextList.Count > pageitems)
                {
                    return (this.ItemHeight * pageitems);
                }
                else return this.ItemHeight * this.OptionTextList.Count;
            }
        }

        internal bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                if (this.isShowing != value)
                {
                    this.isShowing = value;
                    if (value)
                    {
                        Update();
                        this.OptionObjectsSelected.Clear();
                        foreach (GameObject obj2 in this.OptionObjects)
                        {
                            if (obj2 != null)
                            {
                                this.OptionObjectsSelected.Add(obj2.Selected);
                            }
                        }
                        Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Dialog, DialogKind.Options));
                        Session.MainGame.mainGameScreen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                        Session.MainGame.mainGameScreen.OnMouseRightDown += new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                    }
                    else
                    {
                        Session.MainGame.mainGameScreen.PopUndoneWork();
                        Session.MainGame.mainGameScreen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                        Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                        Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                        Session.MainGame.mainGameScreen.OnMouseRightDown -= new Screen.MouseRightDown(this.screen_OnMouseRightDown);
                        foreach (GameObject obj2 in this.OptionObjects)
                        {
                            if (obj2 != null)
                            {
                                obj2.Selected = this.OptionObjectsSelected[0];
                                this.OptionObjectsSelected.RemoveAt(0);
                            }
                        }
                    }
                }
            }
        }

        internal int Width
        {
            get
            {
                return this.ItemWidth;
            }
        }
    }
}

