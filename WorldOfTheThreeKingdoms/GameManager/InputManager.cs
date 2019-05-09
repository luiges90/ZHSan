using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Text;
using Platforms;
using Tools;

namespace GameManager
{
    /// <summary>
    /// This class handles all keyboard and gamepad actions in the game.
    /// </summary>
    public static class InputManager
    {
        public static Vector2 PositionPre = Vector2.Zero;
        public static Vector2 Position = Vector2.Zero;
        public static float OriginPoX;
        public static float OriginPoY;
        public static int PoX;
        public static int PoY;
        public static Vector2 Scale1 = new Vector2(1, 1);
        public static Vector2 Scale2 = new Vector2(1, 1);
        public static Vector2 ScaleOne = new Vector2(1, 1);
        public static Vector2 ScaleDraw = new Vector2(1, 1);

        public static Vector2 Scale
        {
            get
            {
                if (Session.MainGame.mainGameScreen == null || Session.MainGame.loadingScreen != null)
                {
                    return Scale1;
                }
                else
                {
                    return Scale2;
                }
            }
        }

        public static Vector2 RealScale = Vector2.One;

        public static bool IsPressed = false;
        public static bool IsDown = false;

        public static bool IsReleasePre = false;
        public static bool IsDownPre = false;

        private static bool isBackPressed = false;
        private static DateTime? lastBackPressedTime = null;

        public static bool IsBackPressed
        {
            get
            {
                if (isBackPressed)
                {
                    isBackPressed = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (value == true)
                {
                    if (lastBackPressedTime != null)
                    {
                        var secs = (DateTime.Now - (DateTime)lastBackPressedTime).TotalSeconds;
                        if (secs < 0.5)
                        {
                            return;
                        }
                    }
                    lastBackPressedTime = DateTime.Now;
                }
                isBackPressed = value;
            }
        }

        public static MouseState MouseStatePre;
        public static MouseState NowMouse;
        public static KeyboardState KeyBoardStatePre;
        public static KeyboardState KeyBoardState;

        public static bool IsPosChanged = false;

        public static float PressTimeSpan = 0.1f;
        public static float PressTimeElapsed = 0f;

        public static float SleepTime = 0f;

        public static int PoXStart;
        public static int PoYStart;
        public static bool IsReleased = false;

        public static bool IsMoved = false;
        public static int PoXMove, PoYMove;

        public static float firstPinchDistance = 0f;
        public static float nowPinchDistance = 0f;
        public static float PinchMove;

        public static Vector2 PosMoveStart, PosMoveEnd;

        public static Vector2 CollectionPosBase = Vector2.Zero;
        public static float CollectionScaleBase = 1f;
        public static bool CollectionPos = false;
        public static Vector2[] CollectionPositions = null;

        public static int ClickTime = 1;

        public static int SWidth;
        public static int SHeight;
        
        public static bool HasKeys
        {
            get
            {
                Keys[] keys = InputManager.KeyBoardState.GetPressedKeys();
                return keys != null && keys.Length > 0;
            }
        }

        public static void Update(float gameTime)
        {
			if (Platform.IsActive)
            {
                PressTimeElapsed += gameTime;

                IsDownPre = IsDown;

                IsReleasePre = IsReleased;

                IsDown = IsPressed = IsReleased = IsMoved = IsPosChanged = false;  //IsBackPressed

                if (SleepTime > 0f)
                {
                    SleepTime -= gameTime;
                    if (SleepTime < 0f)
                    {
                        SleepTime = 0f;
                    }
                }

                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.UWP || Platform.PlatFormType == PlatFormType.Desktop)  // Platform.PlatFormType == PlatForm.MacOS || Platform.PlatFormType == PlatForm.Linux)
                {
                    MouseStatePre = NowMouse;

                    NowMouse = Mouse.GetState();

                    PoX = NowMouse.X;
                    PoY = NowMouse.Y;
                    OriginPoX = PoX;
                    OriginPoY = PoY;

                    if (Scale != ScaleOne)
                    {
                        if (Platform.PlatFormType == PlatFormType.UWP)
                        {
                            PoX = Convert.ToInt32(OriginPoX / Scale.X); // ScaleOne.X);
                            PoY = Convert.ToInt32(OriginPoY / Scale.Y); // ScaleOne.Y);
                        }
                        else
                        {
                            PoX = Convert.ToInt32(OriginPoX / Scale.X); // ScaleOne.X);
                            PoY = Convert.ToInt32(OriginPoY / Scale.Y); // ScaleOne.Y);
                        }
                    }
                    else
                    {
                        PoX = Convert.ToInt32(OriginPoX);
                        PoY = Convert.ToInt32(OriginPoY);
                    }

                    //CoreGame.Current.err = CoreGame.Current.graphicsDevice.Viewport.Width + "-" + CoreGame.Current.graphicsDevice.Viewport.Height + " " + OriginPoX + "-" + OriginPoY + "  " + PoX.ToString() + "-" + PoY.ToString();

                    PositionPre = Position;

                    Position.X = PoX;
                    Position.Y = PoY;

                    IsDown = NowMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed;
                    if (MouseStatePre.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Pressed && NowMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        IsPressed = true;
                        PoXStart = PoX;
                        PoYStart = PoY;
                    }
                    if (MouseStatePre.LeftButton != Microsoft.Xna.Framework.Input.ButtonState.Released && NowMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Released)
                    {
                        IsReleased = true;
                    }
                    if (MouseStatePre.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && NowMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                    {
                        IsMoved = true;
                        PoXMove = NowMouse.X - MouseStatePre.X;
                        PoYMove = NowMouse.Y - MouseStatePre.Y;
                        PosMoveStart = new Vector2(MouseStatePre.X, MouseStatePre.Y);
                        PosMoveEnd = new Vector2(NowMouse.X, NowMouse.Y);
                    }
                    
                    KeyBoardStatePre = KeyBoardState;
                    KeyBoardState = Keyboard.GetState();
                    if (KeyBoardStatePre == KeyBoardState)
                    {
                        
                    }
                    if (KeyBoardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                    {
                        IsBackPressed = true;
                    }

                    //if (firstPinchDistance == 0f)
                    //{
                    //    firstPinchDistance = nowPinchDistance;
                    //}
                    //PinchMove = nowPinchDistance - firstPinchDistance;  //范圍在0-10
                }

                if (Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.iOS || Platform.PlatFormType == PlatFormType.UWP)
                {
                    if (PinchMove == 0f)
                    {
                        firstPinchDistance = 0f;
                    }

                    PinchMove = 0f;

                    GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
                    if (gamePadState.Buttons.Back == ButtonState.Pressed)
                    {
                        IsBackPressed = true;
                    }

                    //An exception of type 'System.InvalidOperationException' occurred in Microsoft.Xna.Framework.Input.Touch.ni.dll but was not handled in user code
                    //UpdateErrorObject reference not set to an instance of an object at Microsoft.Xna.Framework.Input.Touch.TouchPanel.GetState () [0x00000] in :0 at SanguoSeason.GameManager.InputManager.Update (Single gameTime) [0x0028e]
                    TouchCollection touchState = TouchPanel.GetState();
                    while (TouchPanel.IsGestureAvailable)
                    {
                        //得到一個動作取樣
                        GestureSample gestureSample = TouchPanel.ReadGesture();

                        Vector2 scale = Vector2.One; // 1f;
                        if (Platform.PlatFormType == PlatFormType.UWP)
                        {
                            scale = new Vector2(1f / InputManager.RealScale.X, 1f / InputManager.RealScale.Y);
                        }
                        else if (Platform.PlatFormType == PlatFormType.iOS)
                        {
                            scale = new Vector2(1.7f, 1.7f);
                        }
                        else if (Platform.PlatFormType == PlatFormType.Android)
                        {
                            scale = new Vector2(1f / Scale.X, 1f / Scale.Y);
                        }
                        else
                        {
                            scale = Vector2.One;  // 1f;
                        }

                        if (gestureSample.GestureType == GestureType.FreeDrag)
                        {
							IsMoved = true;

							PoXMove = Convert.ToInt32 (gestureSample.Delta.X * scale.X); // * 1.7);
							PoYMove = Convert.ToInt32 (gestureSample.Delta.Y * scale.Y); // * 1.7);

                            PosMoveStart = gestureSample.Position;  // new Vector2(MouseStatePre.X, MouseStatePre.Y);
                            PosMoveEnd = gestureSample.Position2;  // new Vector2(NowMouse.X, NowMouse.Y);

                            //return;
                        }
                        else if (gestureSample.GestureType == GestureType.Pinch)
                        {

                            float disY = gestureSample.Position2.Y - gestureSample.Position.Y;
                            float disX = gestureSample.Position2.X - gestureSample.Position.X;
                            nowPinchDistance = Convert.ToSingle(Math.Sqrt(disY * disY + disX * disX));

                            if (firstPinchDistance == 0f)
                            {
                                firstPinchDistance = nowPinchDistance;
                            }

                            PinchMove = (nowPinchDistance - firstPinchDistance) / 1334;  //范圍在0-400                            
                        }
                        else if (gestureSample.GestureType == GestureType.PinchComplete)
                        {
                            firstPinchDistance = 0f;
                            PinchMove = 0f;
                        }
                        //else if (gestureSample.GestureType == GestureType.DoubleTap)
                        //{
                        //    EntryTime = 0f;
                        //}                        
                    }

                    //CoreGame.Current.view = PinchMove.ToString() + "-" + firstPinchDistance;

                    //StringBuilder sb = new StringBuilder();
                    foreach (TouchLocation location in touchState)
                    {
                        PoX = Convert.ToInt32(location.Position.X);
                        PoY = Convert.ToInt32(location.Position.Y);
                        OriginPoX = location.Position.X;
                        OriginPoY = location.Position.Y;

                        if (Platform.PlatFormType == PlatFormType.iOS || Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.UWP)
                        {
                            PoX = Convert.ToInt32(location.Position.X / Scale.X);
                            PoY = Convert.ToInt32(location.Position.Y / Scale.Y);
                        }

                        PositionPre = Position;

                        Position.X = PoX;
                        Position.Y = PoY;

                        switch (location.State)
                        {
                            case TouchLocationState.Pressed://按下
                                IsPressed = false;
                                IsDown = true;
                                IsReleased = false;
                                //……
                                break;

                            case TouchLocationState.Moved://移动
                                IsPressed = false;
                                IsDown = true;
                                IsReleased = false;
                                //……
                                break;

                            case TouchLocationState.Released://释放
                                IsPressed = true;
                                IsDown = false;
                                IsReleased = true;
                                break;
                        }
                        //return;

                        //sb.Append(String.Format("({0},{1})", PoX, PoY));
                    }

                    //CoreGame.Current.view = sb.ToString();

                }

                if (CollectionPos)
                {
                    var newPos = (Position / CollectionScaleBase + CollectionPosBase);
                    if (CollectionPositions == null)
                    {
                        CollectionPositions = new Vector2[] { newPos };
                    }
                    else
                    {
                        if (CollectionPositions.Length > 0)
                        {
                            var last = CollectionPositions[CollectionPositions.Length - 1];
                            if (Math.Abs(newPos.X - last.X) >= 2 || Math.Abs(newPos.Y - last.Y) >= 2)
                            {
                                CollectionPositions = CollectionPositions.Union(new Vector2[] { newPos }).NullToEmptyArray();
                            }
                        }
                    }
                }

                if (Position != PositionPre)
                {
                    IsPosChanged = true;
                }

                //if (IsPressed)
                //{
                //    if (PressTimeElapsed <= PressTimeSpan)
                //    {
                //        IsPressed = false;
                //    }
                //    PressTimeElapsed = 0f;
                //}

            }
        }

        public static void ClearKeyBoard()
        {
            
            
        }
        /// <summary>  
        /// Convert a key to it's respective character or escape sequence.  
        /// </summary>  
        /// <param name="key">The key to convert.</param>  
        /// <param name="shift">Is the shift key pressed or caps lock down.</param>  
        /// <returns>The char for the key that was pressed or string.Empty if it doesn't have a char representation.</returns>  
        public static string ConvertKeyToChar(bool shift)
        {
            Keys[] keys = KeyBoardState.GetPressedKeys();
            if (keys != null && keys.Length > 0)
            {
                Keys key = keys[0];
                if (keys.Length > 1 && (key == Keys.LeftShift || key == Keys.RightShift))
                {
                    key = keys[1];
                }
                switch (key)
                {
                    case Keys.Space: return " ";
                    // Escape Sequences  
                    //case Key.Enter: return "\n";                         // Create a new line  
                    //case Key.Tab: return "\t";                           // Tab to the right  
                    // D-Numerics (strip above the alphabet)  
                    case Keys.D0: return shift ? ")" : "0";
                    case Keys.D1: return shift ? "!" : "1";
                    case Keys.D2: return shift ? "@" : "2";
                    case Keys.D3: return shift ? "#" : "3";
                    case Keys.D4: return shift ? "$" : "4";
                    case Keys.D5: return shift ? "%" : "5";
                    case Keys.D6: return shift ? "^" : "6";
                    case Keys.D7: return shift ? "&" : "7";
                    case Keys.D8: return shift ? "*" : "8";
                    case Keys.D9: return shift ? "(" : "9";
                    // NumberPad  
                    case Keys.NumPad0: return "0";
                    case Keys.NumPad1: return "1";
                    case Keys.NumPad2: return "2";
                    case Keys.NumPad3: return "3";
                    case Keys.NumPad4: return "4";
                    case Keys.NumPad5: return "5";
                    case Keys.NumPad6: return "6";
                    case Keys.NumPad7: return "7";
                    case Keys.NumPad8: return "8";
                    case Keys.NumPad9: return "9";
                    //case Keys.NumPadPlus: return "+";
                    //case Keys.NumberPadMinus: return "-";
                    //case Keys.NumberPadStar: return "*";
                    //case Keys.NumberPadSlash: return "/";
                    //case Keys.NumberPadComma: return ".";
                    // Alphabet  
                    case Keys.A: return shift ? "A" : "a";
                    case Keys.B: return shift ? "B" : "b";
                    case Keys.C: return shift ? "C" : "c";
                    case Keys.D: return shift ? "D" : "d";
                    case Keys.E: return shift ? "E" : "e";
                    case Keys.F: return shift ? "F" : "f";
                    case Keys.G: return shift ? "G" : "g";
                    case Keys.H: return shift ? "H" : "h";
                    case Keys.I: return shift ? "I" : "i";
                    case Keys.J: return shift ? "J" : "j";
                    case Keys.K: return shift ? "K" : "k";
                    case Keys.L: return shift ? "L" : "l";
                    case Keys.M: return shift ? "M" : "m";
                    case Keys.N: return shift ? "N" : "n";
                    case Keys.O: return shift ? "O" : "o";
                    case Keys.P: return shift ? "P" : "p";
                    case Keys.Q: return shift ? "Q" : "q";
                    case Keys.R: return shift ? "R" : "r";
                    case Keys.S: return shift ? "S" : "s";
                    case Keys.T: return shift ? "T" : "t";
                    case Keys.U: return shift ? "U" : "u";
                    case Keys.V: return shift ? "V" : "v";
                    case Keys.W: return shift ? "W" : "w";
                    case Keys.X: return shift ? "X" : "x";
                    case Keys.Y: return shift ? "Y" : "y";
                    case Keys.Z: return shift ? "Z" : "z";
                    // Oem  
                    case Keys.OemOpenBrackets: return shift ? "{" : "[";
                    case Keys.OemCloseBrackets: return shift ? "}" : "]";
                    case Keys.OemComma: return shift ? "<" : ","; //OemComma
                    case Keys.OemPeriod: return shift ? ">" : "."; //OemPeriod
                    case Keys.OemMinus: return shift ? "_" : "-"; //OemMinus
                    case Keys.OemPlus: return shift ? "+" : "=";
                    case Keys.OemQuestion: return shift ? "?" : "/";
                    case Keys.OemSemicolon: return shift ? ":" : ";"; //OemSemicolon
                    case Keys.OemQuotes: return shift ? "\"" : "'";
                    case Keys.OemPipe: return shift ? "|" : "\\";
                    case Keys.OemTilde: return shift ? "~" : "`";
                }
            }
            return string.Empty;
        }

    }

}