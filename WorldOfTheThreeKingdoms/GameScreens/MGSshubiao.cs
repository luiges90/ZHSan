using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using GameFreeText;
using GameGlobal;
using GameObjects;
using GameObjects.FactionDetail;
using GameObjects.PersonDetail;
using GameObjects.SectionDetail;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PluginInterface;
using WorldOfTheThreeKingdoms.GameLogic;
using WorldOfTheThreeKingdoms.GameScreens;
using WorldOfTheThreeKingdoms.GameScreens.ScreenLayers;
using WorldOfTheThreeKingdoms.Resources;
using GameManager;
using Platforms;

//using GameObjects.PersonDetail.PersonMessages;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    partial class MainGameScreen : Screen
    {

        public override void EarlyMouseLeftDown()
        {
            base.EarlyMouseLeftDown();

            this.scrollSpeedScale = this.scrollSpeedScaleSpeedy;
            /*if (!Session.Current.Scenario.LoadAndSaveAvail())
            {
                Session.GlobalVariables.FastBattleSpeed = InputManager.NowMouse.LeftButton == ButtonState.Pressed;
            }*/
        }

        public override void EarlyMouseLeftUp()
        {
            base.EarlyMouseLeftUp();
            this.DrawingSelector = false;
            this.scrollSpeedScale = this.scrollSpeedScaleDefault;
            //GlobalVariables.FastBattleSpeed = false;
        }

        public override void EarlyMouseMove()
        {
            base.EarlyMouseMove();
        }

        public override void EarlyMouseRightDown()
        {
            base.EarlyMouseRightDown();
        }

        public override void EarlyMouseRightUp()
        {
            base.EarlyMouseRightUp();
        }

        public override void EarlyMouseScroll()
        {
            base.EarlyMouseScroll();
        }

        private void HandleLaterMouseEvent(GameTime gameTime)
        {
            if (base.EnableMouseEvent && base.EnableLaterMouseEvent)
            {
                if (!StaticMethods.PointInViewport(new Point(InputManager.PoX, InputManager.PoY), base.viewportSize))
                {
                    this.UpdateViewMove();
                }
                else
                {
                    this.ResetCurrentStatus();
                    this.CurrentArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.position);
                    this.CurrentTroop = Session.Current.Scenario.GetTroopByPosition(this.position);
                    this.CurrentRouteway = Session.Current.Scenario.GetRoutewayByPositionAndFaction(this.position, Session.Current.Scenario.CurrentPlayer);
                    this.HandleLaterMouseMove();
                    this.HandleLaterMouseScroll();
                    if (this.viewMove == ViewMove.Stop)
                    {
                        this.HandleLaterMouseLeftDown();
                        this.HandleLaterMouseLeftUp();
                        this.HandleLaterMouseRightDown();
                        this.HandleLaterMouseRightUp();
                        this.UpdateConmentText(gameTime);
                        this.UpdateSurvey(gameTime);
                    }
                }
            }
        }

        //private void HandleLaterMouseEventOld(GameTime gameTime)
        //{
        //    if (base.EnableMouseEvent && base.EnableLaterMouseEvent)
        //    {
        //        if (!StaticMethods.PointInViewport(new Point(InputManager.NowMouse.X, InputManager.NowMouse.Y), base.viewportSize))
        //        {
        //            this.UpdateViewMove();
        //        }
        //        else
        //        {
        //            this.ResetCurrentStatus();
        //            this.CurrentArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.position);
        //            this.CurrentTroop = Session.Current.Scenario.GetTroopByPosition(this.position);
        //            this.CurrentRouteway = Session.Current.Scenario.GetRoutewayByPositionAndFaction(this.position, Session.Current.Scenario.CurrentPlayer);
        //            this.HandleLaterMouseMove();
        //            this.HandleLaterMouseScroll();
        //            if (this.viewMove == ViewMove.Stop)
        //            {
        //                this.HandleLaterMouseLeftDown();
        //                this.HandleLaterMouseLeftUp();
        //                this.HandleLaterMouseRightDown();
        //                this.HandleLaterMouseRightUp();
        //                this.UpdateConmentText(gameTime);
        //                this.UpdateSurvey(gameTime);
        //            }
        //        }
        //    }
        //}

        private void HandleLaterMouseLeftDown()
        {
            //if (((this.previousMouseState.LeftButton == ButtonState.Released) && (InputManager.NowMouse.LeftButton == ButtonState.Pressed)) && (this.viewMove == ViewMove.Stop))
            if (InputManager.IsDown && this.viewMove == ViewMove.Stop)
            {
                if (this.editMode)
                {
                    int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / Session.Current.Scenario.ScenarioMap.TileWidth;
                    int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / Session.Current.Scenario.ScenarioMap.TileHeight;
                    Session.Current.Scenario.ScenarioMap.MapData[x, y] = this.ditukuaidezhi;
                    this.mainMapLayer.chongsheditukuaitupian(x, y);
                } 
                else if (Session.Current.Scenario.CurrentPlayer != null && 
                    this.PeekUndoneWork().Kind == UndoneWorkKind.None && Session.Current.Scenario.CurrentPlayer == Session.Current.Scenario.CurrentFaction)
                {
                    if (this.CurrentArchitecture == null && this.CurrentTroop == null && this.CurrentRouteway == null)
                    {
                        if (this.Plugins.youcelanPlugin.IsShowing && StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.youcelanPlugin.FrameRectangle))
                        {
                        }
                        else
                        {
                            this.DrawingSelector = !this.Plugins.ContextMenuPlugin.IsShowing && !this.Plugins.RoutewayEditorPlugin.IsShowing;
                        }
                    }
                }
            }
            /*
            if (Session.Current.Scenario.CurrentPlayer == null) return;
            if (((this.previousMouseState.LeftButton == ButtonState.Released) && (InputManager.NowMouse.LeftButton == ButtonState.Pressed)) && (this.viewMove == ViewMove.Stop))
            {
                if (((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)) && ((this.Plugins.ContextMenuPlugin != null) && (this.PeekUndoneWork().Kind == UndoneWorkKind.None)))
                {
                    if ((((this.CurrentArchitecture != null) && (this.CurrentTroop != null)) && (this.CurrentTroop.BelongedFaction == Session.Current.Scenario.CurrentPlayer)) && (this.CurrentArchitecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
                    {
                        if (!(this.Plugins.ContextMenuPlugin.IsShowing || !Session.Current.Scenario.CurrentPlayer.Controlling))
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureTroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(InputManager.NowMouse.X, InputManager.NowMouse.Y, base.viewportSize);
                            this.bianduiLiebiaoBiaoji = "ArchitectureTroopLeftClick";
                        }
                    }
                    else if ((this.CurrentTroop != null) && (this.CurrentTroop.BelongedFaction == Session.Current.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false )
                    {
                        if (!this.Plugins.ContextMenuPlugin.IsShowing && Session.Current.Scenario.IsPlayerControlling())
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(InputManager.NowMouse.X, InputManager.NowMouse.Y, base.viewportSize);
                            this.bianduiLiebiaoBiaoji="TroopLeftClick";
                            if (!this.Plugins.ContextMenuPlugin.IsShowing && (this.CurrentTroop.CutRoutewayDays > 0))
                            {
                                this.CurrentTroop.Leader.TextDestinationString = this.CurrentTroop.CutRoutewayDays.ToString();
                                this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(this.CurrentTroop.StopCutRouteway), null);
                                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(this.CurrentTroop.Leader, this.CurrentTroop.Leader, "StopCutRouteway");
                                this.Plugins.tupianwenziPlugin.IsShowing = true;
                            }
                        }
                    }
                    else if (((this.CurrentArchitecture != null) && (this.CurrentArchitecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)) && !(this.Plugins.ContextMenuPlugin.IsShowing || !Session.Current.Scenario.IsPlayerControlling()))
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureLeftClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.NowMouse.X, InputManager.NowMouse.Y, base.viewportSize);
                        
                        this.bianduiLiebiaoBiaoji = "ArchitectureLeftClick";
                        this.ShowBianduiLiebiao(UndoneWorkKind.None, FrameKind.Military, FrameFunction.Browse , false, true, false ,true,
                            this.CurrentArchitecture.Militaries, this.CurrentArchitecture.ZhengzaiBuchongDeBiandui(), "", "", this.CurrentArchitecture.MilitaryPopulation);
                        this.ShowArchitectureSurveyPlugin(this.CurrentArchitecture);
                    }
                }
            }
            */
        }

        private void HandleLaterMouseLeftUp()
        {
            if (Session.Current.Scenario.CurrentPlayer == null||this.editMode) return;

            if (this.Plugins.youcelanPlugin.IsShowing && StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.youcelanPlugin.FrameRectangle))
            {
                return;
            }

            //if ((this.previousMouseState.LeftButton == ButtonState.Pressed) && (InputManager.NowMouse.LeftButton == ButtonState.Released) && (this.viewMove == ViewMove.Stop))
            if (InputManager.IsDownPre && InputManager.IsReleased && this.viewMove == ViewMove.Stop)
            {
                if (((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)) && ((this.Plugins.ContextMenuPlugin != null) && (this.PeekUndoneWork().Kind == UndoneWorkKind.None)))
                {
                    // Assign the current mouse position so that selectorStartPosition can cache the selected target's accurate coordinates
                    this.SelectorStartPosition = base.MousePosition;
                    this.updateGameScreenByCurrentTarget();
                }
            }
        }

        private void HandleLaterMouseMove()
        {
            /*
            if ((InputManager.NowMouse.X != this.previousMouseState.X) || (InputManager.NowMouse.Y != this.previousMouseState.Y))
            {
                this.UpdateViewMove();
                if ((InputManager.NowMouse.LeftButton != ButtonState.Pressed) && (InputManager.NowMouse.RightButton != ButtonState.Pressed))
                {
                }
            }
            */

            if (InputManager.IsPosChanged)
            {
                this.UpdateViewMove();
                if (this.editMode)
                {
                    if (InputManager.IsDown && (InputManager.NowMouse.RightButton != ButtonState.Pressed))
                    {
                        int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / Session.Current.Scenario.ScenarioMap.TileWidth;
                        int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / Session.Current.Scenario.ScenarioMap.TileHeight;
                        if (Session.Current.Scenario.ScenarioMap.MapData[x, y] != this.ditukuaidezhi)
                        {
                            Session.Current.Scenario.ScenarioMap.MapData[x, y] = this.ditukuaidezhi;
                            this.mainMapLayer.chongsheditukuaitupian(x, y);

                        }
                    }
                    if (InputManager.IsDown && (InputManager.NowMouse.RightButton == ButtonState.Pressed))
                    {
                        int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / Session.Current.Scenario.ScenarioMap.TileWidth;
                        int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / Session.Current.Scenario.ScenarioMap.TileHeight;
                        if (Session.Current.Scenario.ScenarioMap.MapData[x, y] != 0)
                        {
                            Session.Current.Scenario.ScenarioMap.MapData[x, y] = 0;
                            this.mainMapLayer.chongsheditukuaitupian(x, y);

                        }
                    }
                }
            }


        }

        private void HandleLaterMouseRightDown()
        {
            if (this.editMode)
            {
                if ((InputManager.MouseStatePre.RightButton == ButtonState.Released) && (InputManager.NowMouse.RightButton == ButtonState.Pressed))
                {
                    int x = (InputManager.NowMouse.X - this.mainMapLayer.LeftEdge) / Session.Current.Scenario.ScenarioMap.TileWidth;
                    int y = (InputManager.NowMouse.Y - this.mainMapLayer.TopEdge) / Session.Current.Scenario.ScenarioMap.TileHeight;
                    Session.Current.Scenario.ScenarioMap.MapData[x, y] = 0;
                    this.mainMapLayer.chongsheditukuaitupian(x, y);
                }
            }
            else
            {

                GameDelegates.VoidFunction optionFunction = null;
                if ((InputManager.MouseStatePre.RightButton == ButtonState.Released) && (InputManager.NowMouse.RightButton == ButtonState.Pressed))
                {
                    if ((this.Plugins.OptionDialogPlugin != null) && (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway))
                    {
                        List<Routeway> routewaysByPositionAndFaction = Session.Current.Scenario.GetRoutewaysByPositionAndFaction(this.position, Session.Current.Scenario.CurrentPlayer);
                        List<Routeway> list2 = new List<Routeway>();
                        foreach (Routeway routeway in routewaysByPositionAndFaction)
                        {
                            if ((routeway.StartArchitecture == null) || ((((routeway.DestinationArchitecture != null) && routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun) && !routeway.Building) && (routeway.LastActivePointIndex < 0)))
                            {
                                list2.Add(routeway);
                            }
                        }
                        foreach (Routeway routeway in list2)
                        {
                            routewaysByPositionAndFaction.Remove(routeway);
                        }
                        if ((routewaysByPositionAndFaction.Count > 1) && !Session.Current.Scenario.PositionIsTroop(this.position))
                        {
                            this.Plugins.OptionDialogPlugin.SetStyle("Small");
                            this.Plugins.OptionDialogPlugin.SetTitle("粮道");
                            this.Plugins.OptionDialogPlugin.Clear();
                            this.Plugins.OptionDialogPlugin.SetReturnObjectFunction(new GameDelegates.ObjectFunction(this.RoutewayOptionDialogClickCallback));
                            foreach (Routeway routeway in routewaysByPositionAndFaction)
                            {
                                if (optionFunction == null)
                                {
                                    optionFunction = delegate
                                    {
                                        this.ContextMenuRightClick();
                                    };
                                }
                                this.Plugins.OptionDialogPlugin.AddOption(routeway.DisplayName, routeway, optionFunction);
                            }
                            this.Plugins.OptionDialogPlugin.EndAddOptions();
                            this.Plugins.OptionDialogPlugin.ShowOptionDialog(ShowPosition.Mouse);
                            return;
                        }
                    }
                    this.ContextMenuRightClick();
                }
            }
        }

        private void HandleLaterMouseRightUp()
        {
            if ((InputManager.MouseStatePre.RightButton == ButtonState.Pressed) && (InputManager.NowMouse.RightButton == ButtonState.Released))
            {
            }
        }

        private void HandleLaterMouseScroll()
        {
            if (this.currentKey == Keys.OemPlus || this.currentKey == Keys.OemMinus || (InputManager.NowMouse.ScrollWheelValue != InputManager.MouseStatePre.ScrollWheelValue && this.oldScrollWheelValue != InputManager.NowMouse.ScrollWheelValue))
            {
                float num = InputManager.NowMouse.ScrollWheelValue - this.oldScrollWheelValue;
                this.oldScrollWheelValue = InputManager.NowMouse.ScrollWheelValue;

                if (this.currentKey == Keys.OemPlus)
                {
                    num = 0.1f;
                }
                else if (this.currentKey == Keys.OemMinus)
                {
                    num = -0.1f;
                }

                if (num > 0f)
                {
                    num = 0.1f;
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileWidth == Session.Current.Scenario.ScenarioMap.TileWidthMax)
                    {
                        return;
                    }
                }
                if (num < 0f)
                {
                    num = -0.1f;
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileWidth == Session.Current.Scenario.ScenarioMap.TileWidthMin)
                    {
                        return;
                    }
                }

                ProcessScrollWheel(num);
            }

            if (InputManager.PinchMove != 0f)
            {
                ProcessScrollWheel(InputManager.PinchMove);
                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
                {
                    InputManager.PinchMove = 0f;
                }
            }
        }

        private void ProcessScrollWheel(float num)
        {
            int tileWidthMax = (int)((1f + num) * this.mainMapLayer.TileWidth);
            if (tileWidthMax > Session.Current.Scenario.ScenarioMap.TileWidthMax)
            {
                tileWidthMax = Session.Current.Scenario.ScenarioMap.TileWidthMax;
            }
            else if (tileWidthMax < Session.Current.Scenario.ScenarioMap.TileWidthMin)
            {
                tileWidthMax = Session.Current.Scenario.ScenarioMap.TileWidthMin;
            }
            int tileHeightMax = (int)((1f + num) * this.mainMapLayer.TileHeight);
            if (tileHeightMax > Session.Current.Scenario.ScenarioMap.TileWidthMax)
            {
                tileHeightMax = Session.Current.Scenario.ScenarioMap.TileWidthMax;
            }
            else if (tileHeightMax < Session.Current.Scenario.ScenarioMap.TileWidthMin)
            {
                tileHeightMax = Session.Current.Scenario.ScenarioMap.TileWidthMin;
            }

            int tileWidth = this.mainMapLayer.TileWidth;
            int tileHeight = this.mainMapLayer.TileHeight;
            this.mainMapLayer.TileWidth = tileWidthMax;
            this.mainMapLayer.TileHeight = tileHeightMax;

            num = (((float)tileWidthMax) / ((float)tileWidth)) - 1f;
            int num4 = this.mainMapLayer.LeftEdge + ((int)(num * (Session.MainGame.mainGameScreen.mainMapLayer.LeftEdge - InputManager.PoX)));  // InputManager.NowMouse.X)));
            int num5 = this.mainMapLayer.TopEdge + ((int)(num * (Session.MainGame.mainGameScreen.mainMapLayer.TopEdge - InputManager.PoY)));  // InputManager.NowMouse.Y)));
            if (((((this.viewportSize.X - num4) <= this.mainMapLayer.TotalTileWidth) && (num4 <= 0)) && ((this.viewportSize.Y - num5) <= this.mainMapLayer.TotalTileHeight)) && (num5 <= 0))
            {
                this.mainMapLayer.LeftEdge = num4;
                this.mainMapLayer.TopEdge = num5;
            }
            else
            {
                this.mainMapLayer.TileWidth = tileWidth;
                this.mainMapLayer.TileHeight = tileHeight;
            }
            this.ResetScreenEdge();
            this.mainMapLayer.ReCalculateTileDestination(this);
            Session.Current.Scenario.TroopAnimations.UpdateDirectionAnimations(Session.MainGame.mainGameScreen.mainMapLayer.TileWidth);
            this.Plugins.AirViewPlugin.ResetFrameSize(base.viewportSize, Session.MainGame.mainGameScreen.mainMapLayer.TotalMapSize);
            this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, Session.MainGame.mainGameScreen.mainMapLayer.LeftEdge, Session.MainGame.mainGameScreen.mainMapLayer.TopEdge, Session.MainGame.mainGameScreen.mainMapLayer.TotalMapSize);
        }


    }
}
