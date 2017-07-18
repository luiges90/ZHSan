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
            /*if (!base.Scenario.LoadAndSaveAvail())
            {
                GlobalVariables.FastBattleSpeed = this.mouseState.LeftButton == ButtonState.Pressed;
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
                    this.CurrentArchitecture = base.Scenario.GetArchitectureByPosition(this.position);
                    this.CurrentTroop = base.Scenario.GetTroopByPosition(this.position);
                    this.CurrentRouteway = base.Scenario.GetRoutewayByPositionAndFaction(this.position, base.Scenario.CurrentPlayer);
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
        //        if (!StaticMethods.PointInViewport(new Point(this.mouseState.X, this.mouseState.Y), base.viewportSize))
        //        {
        //            this.UpdateViewMove();
        //        }
        //        else
        //        {
        //            this.ResetCurrentStatus();
        //            this.CurrentArchitecture = base.Scenario.GetArchitectureByPosition(this.position);
        //            this.CurrentTroop = base.Scenario.GetTroopByPosition(this.position);
        //            this.CurrentRouteway = base.Scenario.GetRoutewayByPositionAndFaction(this.position, base.Scenario.CurrentPlayer);
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
            //if (((this.previousMouseState.LeftButton == ButtonState.Released) && (this.mouseState.LeftButton == ButtonState.Pressed)) && (this.viewMove == ViewMove.Stop))
            if (InputManager.IsReleasePre && InputManager.IsDown && this.viewMove == ViewMove.Stop)
            {
                if (this.editMode)
                {
                    int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / base.Scenario.ScenarioMap.TileWidth;
                    int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / base.Scenario.ScenarioMap.TileHeight;
                    this.mainMapLayer.mainMap.MapData[x, y] = this.ditukuaidezhi;
                    this.mainMapLayer.chongsheditukuaitupian(x, y);
                } 
                else if (base.Scenario.CurrentPlayer != null && 
                    this.PeekUndoneWork().Kind == UndoneWorkKind.None && base.Scenario.CurrentPlayer == base.Scenario.CurrentFaction)
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
            if (base.Scenario.CurrentPlayer == null) return;
            if (((this.previousMouseState.LeftButton == ButtonState.Released) && (this.mouseState.LeftButton == ButtonState.Pressed)) && (this.viewMove == ViewMove.Stop))
            {
                if (((GlobalVariables.SkyEye || base.Scenario.NoCurrentPlayer) || base.Scenario.CurrentPlayer.IsPositionKnown(this.position)) && ((this.Plugins.ContextMenuPlugin != null) && (this.PeekUndoneWork().Kind == UndoneWorkKind.None)))
                {
                    if ((((this.CurrentArchitecture != null) && (this.CurrentTroop != null)) && (this.CurrentTroop.BelongedFaction == base.Scenario.CurrentPlayer)) && (this.CurrentArchitecture.BelongedFaction == base.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
                    {
                        if (!(this.Plugins.ContextMenuPlugin.IsShowing || !base.Scenario.CurrentPlayer.Controlling))
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureTroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(this.mouseState.X, this.mouseState.Y, base.viewportSize);
                            this.bianduiLiebiaoBiaoji = "ArchitectureTroopLeftClick";
                        }
                    }
                    else if ((this.CurrentTroop != null) && (this.CurrentTroop.BelongedFaction == base.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false )
                    {
                        if (!this.Plugins.ContextMenuPlugin.IsShowing && base.Scenario.IsPlayerControlling())
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(this.mouseState.X, this.mouseState.Y, base.viewportSize);
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
                    else if (((this.CurrentArchitecture != null) && (this.CurrentArchitecture.BelongedFaction == base.Scenario.CurrentPlayer)) && !(this.Plugins.ContextMenuPlugin.IsShowing || !base.Scenario.IsPlayerControlling()))
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureLeftClick");
                        this.Plugins.ContextMenuPlugin.Prepare(this.mouseState.X, this.mouseState.Y, base.viewportSize);
                        
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
            if (base.Scenario.CurrentPlayer == null||this.editMode) return;

            if (this.Plugins.youcelanPlugin.IsShowing && StaticMethods.PointInRectangle(this.MousePosition, this.Plugins.youcelanPlugin.FrameRectangle))
            {
                return;
            }

            //if ((this.previousMouseState.LeftButton == ButtonState.Pressed) && (this.mouseState.LeftButton == ButtonState.Released) && (this.viewMove == ViewMove.Stop))
            if (InputManager.IsDownPre && InputManager.IsReleased && this.viewMove == ViewMove.Stop)
            {
                if (((GlobalVariables.SkyEye || base.Scenario.NoCurrentPlayer) || base.Scenario.CurrentPlayer.IsPositionKnown(this.position)) && ((this.Plugins.ContextMenuPlugin != null) && (this.PeekUndoneWork().Kind == UndoneWorkKind.None)))
                {
                    if ((((this.CurrentArchitecture != null) && (this.CurrentTroop != null)) && (this.CurrentTroop.BelongedFaction == base.Scenario.CurrentPlayer)) && (this.CurrentArchitecture.BelongedFaction == base.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
                    {
                        if (!(this.Plugins.ContextMenuPlugin.IsShowing || !base.Scenario.CurrentPlayer.Controlling))
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureTroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                            this.bianduiLiebiaoBiaoji = "ArchitectureTroopLeftClick";
                        }
                    }
                    else if ((this.CurrentTroop != null) && (this.CurrentTroop.BelongedFaction == base.Scenario.CurrentPlayer) && this.CurrentTroop.Operated == false)
                    {
                        if (!this.Plugins.ContextMenuPlugin.IsShowing && base.Scenario.IsPlayerControlling())
                        {
                            this.Plugins.ContextMenuPlugin.IsShowing = true;
                            this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                            this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopLeftClick");
                            this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                            this.bianduiLiebiaoBiaoji = "TroopLeftClick";
                            if (!this.Plugins.ContextMenuPlugin.IsShowing && (this.CurrentTroop.CutRoutewayDays > 0))
                            {
                                this.CurrentTroop.Leader.TextDestinationString = this.CurrentTroop.CutRoutewayDays.ToString();
                                this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(this.CurrentTroop.StopCutRouteway), null);
                                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(this.CurrentTroop.Leader, this.CurrentTroop.Leader, TextMessageKind.StopCutRouteway, "StopCutRouteway");
                                this.Plugins.tupianwenziPlugin.IsShowing = true;
                            }
                        }
                    }
                    else if (((this.CurrentArchitecture != null) && (this.CurrentArchitecture.BelongedFaction == base.Scenario.CurrentPlayer)) && !(this.Plugins.ContextMenuPlugin.IsShowing || !base.Scenario.IsPlayerControlling()))
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureLeftClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);

                        this.bianduiLiebiaoBiaoji = "ArchitectureLeftClick";
                        this.ShowBianduiLiebiao(UndoneWorkKind.None, FrameKind.Military, FrameFunction.Browse, false, true, false, true,
                            this.CurrentArchitecture.Militaries, this.CurrentArchitecture.ZhengzaiBuchongDeBiandui(), "", "", this.CurrentArchitecture.MilitaryPopulation);
                        this.ShowArchitectureSurveyPlugin(this.CurrentArchitecture);
                    }
                }
            }
        }

        private void HandleLaterMouseMove()
        {
            /*
            if ((this.mouseState.X != this.previousMouseState.X) || (this.mouseState.Y != this.previousMouseState.Y))
            {
                this.UpdateViewMove();
                if ((this.mouseState.LeftButton != ButtonState.Pressed) && (this.mouseState.RightButton != ButtonState.Pressed))
                {
                }
            }
            */

            if (InputManager.IsPosChanged)
            {
                this.UpdateViewMove();
                if (this.editMode)
                {
                    if (InputManager.IsDown && (this.mouseState.RightButton != ButtonState.Pressed))
                    {
                        int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / base.Scenario.ScenarioMap.TileWidth;
                        int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / base.Scenario.ScenarioMap.TileHeight;
                        if (this.mainMapLayer.mainMap.MapData[x, y] != this.ditukuaidezhi)
                        {
                            this.mainMapLayer.mainMap.MapData[x, y] = this.ditukuaidezhi;
                            this.mainMapLayer.chongsheditukuaitupian(x, y);

                        }
                    }
                    if (InputManager.IsDown && (this.mouseState.RightButton == ButtonState.Pressed))
                    {
                        int x = (InputManager.PoX - this.mainMapLayer.LeftEdge) / base.Scenario.ScenarioMap.TileWidth;
                        int y = (InputManager.PoY - this.mainMapLayer.TopEdge) / base.Scenario.ScenarioMap.TileHeight;
                        if (this.mainMapLayer.mainMap.MapData[x, y] != 0)
                        {
                            this.mainMapLayer.mainMap.MapData[x, y] = 0;
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
                if ((this.previousMouseState.RightButton == ButtonState.Released) && (this.mouseState.RightButton == ButtonState.Pressed))
                {
                    int x = (this.mouseState.X - this.mainMapLayer.LeftEdge) / base.Scenario.ScenarioMap.TileWidth;
                    int y = (this.mouseState.Y - this.mainMapLayer.TopEdge) / base.Scenario.ScenarioMap.TileHeight;
                    this.mainMapLayer.mainMap.MapData[x, y] = 0;
                    this.mainMapLayer.chongsheditukuaitupian(x, y);
                }
            }
            else
            {

                GameDelegates.VoidFunction optionFunction = null;
                if ((this.previousMouseState.RightButton == ButtonState.Released) && (this.mouseState.RightButton == ButtonState.Pressed))
                {
                    if ((this.Plugins.OptionDialogPlugin != null) && (GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway))
                    {
                        List<Routeway> routewaysByPositionAndFaction = base.Scenario.GetRoutewaysByPositionAndFaction(this.position, base.Scenario.CurrentPlayer);
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
                        if ((routewaysByPositionAndFaction.Count > 1) && !base.Scenario.PositionIsTroop(this.position))
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
            if ((this.previousMouseState.RightButton == ButtonState.Pressed) && (this.mouseState.RightButton == ButtonState.Released))
            {
            }
        }

        private void HandleLaterMouseScroll()
        {
            if (this.currentKey == Keys.OemPlus || this.currentKey == Keys.OemMinus || (this.mouseState.ScrollWheelValue != this.previousMouseState.ScrollWheelValue && this.oldScrollWheelValue != this.mouseState.ScrollWheelValue))
            {
                float num = this.mouseState.ScrollWheelValue - this.oldScrollWheelValue;
                this.oldScrollWheelValue = this.mouseState.ScrollWheelValue;

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
                    if (this.mainMapLayer.TileWidth == this.mainMapLayer.tileWidthMax)
                    {
                        return;
                    }
                }
                if (num < 0f)
                {
                    num = -0.1f;
                    if (this.mainMapLayer.TileWidth == this.mainMapLayer.tileWidthMin)
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
            if (tileWidthMax > this.mainMapLayer.tileWidthMax)
            {
                tileWidthMax = this.mainMapLayer.tileWidthMax;
            }
            else if (tileWidthMax < this.mainMapLayer.tileWidthMin)
            {
                tileWidthMax = this.mainMapLayer.tileWidthMin;
            }

            int tileWidth = this.mainMapLayer.TileWidth;
            this.mainMapLayer.TileWidth = tileWidthMax;
            num = (((float)tileWidthMax) / ((float)tileWidth)) - 1f;
            int num4 = this.mainMapLayer.LeftEdge + ((int)(num * (this.mainMapLayer.LeftEdge - InputManager.PoX)));  // this.mouseState.X)));
            int num5 = this.mainMapLayer.TopEdge + ((int)(num * (this.mainMapLayer.TopEdge - InputManager.PoY)));  // this.mouseState.Y)));
            if (((((this.viewportSize.X - num4) <= this.mainMapLayer.TotalTileWidth) && (num4 <= 0)) && ((this.viewportSize.Y - num5) <= this.mainMapLayer.TotalTileHeight)) && (num5 <= 0))
            {
                this.mainMapLayer.LeftEdge = num4;
                this.mainMapLayer.TopEdge = num5;
            }
            else
            {
                this.mainMapLayer.TileWidth = tileWidth;
            }
            this.ResetScreenEdge();
            this.mainMapLayer.ReCalculateTileDestination(base.spriteBatch.GraphicsDevice);
            base.Scenario.TroopAnimations.UpdateDirectionAnimations(this.mainMapLayer.TileWidth);
            this.Plugins.AirViewPlugin.ResetFrameSize(base.viewportSize, this.mainMapLayer.TotalMapSize);
            this.Plugins.AirViewPlugin.ResetFramePosition(base.viewportSize, this.mainMapLayer.LeftEdge, this.mainMapLayer.TopEdge, this.mainMapLayer.TotalMapSize);
        }


    }
}
