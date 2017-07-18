using GameGlobal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;


namespace GameObjects
{

    public class FrameContent
    {
        private bool cancelButtonEnabled;
        public Point CancelButtonPosition;
        public GameDelegates.VoidFunction CancelFunction;
        protected Rectangle client;
        protected Point defaultCancelButtonPosition;
        protected int defaultFrameHeight;
        protected int defaultFrameWidth;
        protected Point defaultMapViewSelectorButtonPosition;
        protected Point defaultOKButtonPosition;
        protected Rectangle framePosition;
        public FrameFunction Function;
        protected bool isShowing;
        public Point MapViewSelectorButtonPosition;
        public GameDelegates.VoidFunction MapViewSelectorFunction;
        private bool okButtonEnabled;
        public Point OKButtonPosition;
        public GameDelegates.VoidFunction OKFunction;
        private Rectangle realClient;
       /* private bool selectallButtonEnabled;
        public Point SelectAllButtonPosition;
        public GameDelegates.VoidFunction SelectAllFunction;
        protected Point defaultSelectAllButtonPosition;
        */
        public event ItemClick OnItemClick;

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public virtual string GetCurrentTitle()
        {
            return "";
        }

        public virtual string GetTitleString()
        {
            return "";
        }

        public virtual void InitializeMapViewSelectorButton()
        {
        }

        public virtual void ReCalculate()
        {
        }

        public void TriggerItemClick()
        {
            if (this.OnItemClick != null)
            {
                this.OnItemClick();
            }
        }

        public bool CancelButtonEnabled
        {
            get
            {
                return this.cancelButtonEnabled;
            }
            set
            {
                this.cancelButtonEnabled = value;
            }
        }
       
        //public bool SelectAllButtonEnabled
        //{
        //    get
        //    {
        //        return this.selectallButtonEnabled;
        //    }
        //    set
        //    {
        //        this.selectallButtonEnabled = value;
        //    }
        //}
        
        public virtual bool CanClose
        {
            get
            {
                return true;
            }
        }

        public Rectangle Client
        {
            get
            {
                return this.client;
            }
            set
            {
                this.client = value;
            }
        }

        public Point DefaultCancelButtonPosition
        {
            get
            {
                return this.defaultCancelButtonPosition;
            }
            set
            {
                this.defaultCancelButtonPosition = value;
            }
        }
        /*
        public Point DefaultSelectAllButtonPosition
        {
            get
            {
                return this.defaultSelectAllButtonPosition;
            }
            set
            {
                this.defaultSelectAllButtonPosition = value;
            }
        }
        */

        public int DefaultFrameHeight
        {
            get
            {
                return this.defaultFrameHeight;
            }
            set
            {
                this.defaultFrameHeight = value;
            }
        }

        public int DefaultFrameWidth
        {
            get
            {
                return this.defaultFrameWidth;
            }
            set
            {
                this.defaultFrameWidth = value;
            }
        }

        public Point DefaultMapViewSelectorButtonPosition
        {
            get
            {
                return this.defaultMapViewSelectorButtonPosition;
            }
            set
            {
                this.defaultMapViewSelectorButtonPosition = value;
            }
        }

        public Point DefaultOKButtonPosition
        {
            get
            {
                return this.defaultOKButtonPosition;
            }
            set
            {
                this.defaultOKButtonPosition = value;
            }
        }

        public Rectangle FramePosition
        {
            get
            {
                return this.framePosition;
            }
            set
            {
                Rectangle client = this.client;
                this.framePosition = new Rectangle(value.X, value.Y, this.defaultFrameWidth, this.defaultFrameHeight);
                this.OKButtonPosition = this.defaultOKButtonPosition;
               // this.SelectAllButtonPosition = this.defaultSelectAllButtonPosition;
                this.CancelButtonPosition = this.defaultCancelButtonPosition;
                this.MapViewSelectorButtonPosition = this.defaultMapViewSelectorButtonPosition;
                int num = value.Width - this.defaultFrameWidth;
                int num2 = value.Height - this.defaultFrameHeight;
                if (num != 0)
                {
                    this.framePosition.Width += num;
                    client.Width += num;
                    this.OKButtonPosition.X += num;
                   // this.SelectAllButtonPosition.X += num;
                    this.CancelButtonPosition.X += num;
                    this.MapViewSelectorButtonPosition.X += num;
                }
                if (num2 != 0)
                {
                    this.framePosition.Height += num2;
                    client.Height += num2;
                    this.OKButtonPosition.Y += num2;
                   // this.SelectAllButtonPosition.Y += num2;
                    this.CancelButtonPosition.Y += num2;
                    this.MapViewSelectorButtonPosition.Y += num2;
                }
                this.realClient = new Rectangle(this.framePosition.X + client.X, this.framePosition.Y + client.Y, client.Width, client.Height);
            }
        }

        public virtual bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                this.isShowing = value;
            }
        }

        public virtual bool MapViewSelectorButtonEnabled
        {
            get
            {
                return false;
            }
        }

        public bool OKButtonEnabled
        {
            get
            {
                return this.okButtonEnabled;
            }
            set
            {
                this.okButtonEnabled = value;
            }
        }

        public Rectangle RealClient
        {
            get
            {
                return this.realClient;
            }
        }

        public delegate void ItemClick();
    }
}

