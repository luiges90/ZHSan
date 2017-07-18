using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePanels
{

    public class NumericSetTexture
    {
        public ButtonTexture leftTexture;
        public ButtonTexture rightTexture;

        public int MinNumber { get; set; }
        public int MaxNumber { get; set; }
        public int AllowNumber { get; set; }
        public int? NowNumber { get; set; }
        public Vector2 Position { get; set; }
        public int clickTimes = 0;
        public int Unit { get; set; }

        public bool DisNumber = true;

        public bool DisNumberText = true;

        public string ViewText = "";

        public Color NumColor = Color.White;

        public bool IsModeS = false;

        public bool Enable = true;
        
        int groundTextureWidth, groundTextureHeight, fillTextureWidth, fillTextureHeight;

        public NumericSetTexture(int minNumber, int maxNumber, int allowNumber, int? nowNumber, Vector2 position, bool isModeS)
        {
            MinNumber = minNumber; MaxNumber = maxNumber; AllowNumber = allowNumber; NowNumber = nowNumber; 
            Position = position;
            Unit = 10;
            IsModeS = isModeS;
            if (IsModeS)
            {
                groundTextureWidth = 147; groundTextureHeight = 32;
                fillTextureWidth = 145; fillTextureHeight = 28;
                leftTexture = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSetS", "Minus", null) { Enable = Enable };
                rightTexture = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSetS", "Plus", null) { Enable = Enable };
            }
            else
            {
                groundTextureWidth = 226; groundTextureHeight = 48;
                fillTextureWidth = 216; fillTextureHeight = 38;
                leftTexture = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSet", "Minus", null) { Enable = Enable };
                rightTexture = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSet", "Plus", null) { Enable = Enable };
            }
            SetPositions(position);
        }

        public void SetPositions(Vector2 pos)
        {
            Position = pos;
            if (IsModeS)
            {
                leftTexture.Position = Position;
                rightTexture.Position = Position + new Vector2(leftTexture.Width + 4 + groundTextureWidth, 0);
            }
            else
            {
                leftTexture.Position = Position;
                rightTexture.Position = Position + new Vector2(leftTexture.Width + 20 + groundTextureWidth, 0);
            }
        }

        public void Update(Vector2? basePos, ref int nowNumber)
        {
            bool pressed = false;
            Update(InputManager.PoX, InputManager.PoY, ref nowNumber, ref pressed, basePos);
        }

        public void Update(int poX, int poY, ref int nowNumber, ref bool pressed, Vector2? basePosition)
        {
            if (!Enable) return;
            leftTexture.MouseOver = leftTexture.IsInTexture(poX, poY, basePosition);
            rightTexture.MouseOver = rightTexture.IsInTexture(poX, poY, basePosition);
            if (InputManager.IsPressed)
            {
                if (leftTexture.MouseOver)
                {
                    clickTimes++;
                    if (NowNumber > MinNumber && clickTimes >= 1) { NowNumber -= Unit; clickTimes = 0; }
                }
                else if (rightTexture.MouseOver)
                {
                    clickTimes++;
                    if (NowNumber < AllowNumber && NowNumber < MaxNumber && clickTimes >= 1) { NowNumber += Unit; clickTimes = 0; }
                }
                if (IsInTexture(poX, poY, leftTexture.Position + new Vector2(leftTexture.Width + (IsModeS ? 2 : 10), 0f) + (basePosition == null ? Vector2.Zero : (Vector2)basePosition), groundTextureWidth, groundTextureHeight))
                {
                    int width = poX - Convert.ToInt32(leftTexture.Position.X) - leftTexture.Width - 1 - (basePosition == null ? 0 : (int)((Vector2)basePosition).X);
                    int value = ((width * MaxNumber / groundTextureWidth) / Unit) * Unit;
                    if (value < AllowNumber && value < MaxNumber) NowNumber = value;
                    else NowNumber = AllowNumber <= MaxNumber ? AllowNumber : MaxNumber;
                }
                else
                {
                    pressed = false;
                }
            }
            if (NowNumber < MinNumber) NowNumber = MinNumber;
            nowNumber = NowNumber == null ? 0 : (int)NowNumber;
        }

        public static bool IsInTexture(int poX, int poY, Vector2 position, int width, int height)
        {
            if (position.X <= poX && poX <= position.X + width && position.Y <= poY && poY <= position.Y + height)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Draw()
        {
            Draw(1f);
        }

        public void Draw(float alpha)
        {
            Draw(Vector2.Zero, alpha);
        }

        public void Draw(Vector2 basePosition, float alpha)
        {
            Vector2 pos = Position + new Vector2(leftTexture.Width + (IsModeS ? 2 : 10), 0);
            CacheManager.Draw(IsModeS ? @"Content\Textures\Resources\Start\NumberSetS-Bottom" : @"Content\Textures\Resources\Start\NumberSet-Bottom", pos + basePosition, Color.White * alpha);
            if (NowNumber == null) NowNumber = 0;
            int width = 0;
            if (MaxNumber != 0) width = (int)NowNumber * fillTextureWidth / MaxNumber;
            else width = 0;

            CacheManager.Draw(IsModeS ? @"Content\Textures\Resources\Start\NumberSetS-Fill" : @"Content\Textures\Resources\Start\NumberSet-Fill", new Vector2(Convert.ToInt32(pos.X + basePosition.X) + 2, Convert.ToInt32(pos.Y + basePosition.Y) + 2), new Rectangle(0, 0, width, fillTextureHeight), Color.Orange * alpha);

            rightTexture.Draw(basePosition, Color.White * alpha);
            leftTexture.Draw(basePosition, Color.White * alpha);

            if (DisNumber)
            {
                CacheManager.DrawString(Session.Current.Font, MinNumber.ToString(), leftTexture.Position + basePosition + new Vector2(0, 50), NumColor * alpha);
                CacheManager.DrawString(Session.Current.Font, NowNumber.ToString(), leftTexture.Position + basePosition + new Vector2(64, 50), NumColor * alpha);
                CacheManager.DrawString(Session.Current.Font, MaxNumber.ToString(), leftTexture.Position + basePosition + new Vector2(64 + groundTextureWidth, 50), NumColor * alpha);
            }
            else
            {
                CacheManager.DrawString(Session.Current.Font, DisNumberText ? ViewText + NowNumber.ToString() : NowNumber.ToString(), leftTexture.Position + basePosition + (IsModeS ? new Vector2(55, 3) : new Vector2(156, 8)), Color.Black * alpha);
            }
        }

    }
}
