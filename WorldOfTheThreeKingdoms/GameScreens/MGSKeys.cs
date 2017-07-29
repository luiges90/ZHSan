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
using System.Diagnostics;
using GameManager;

//using GameObjects.PersonDetail.PersonMessages;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    partial class MainGameScreen : Screen
    {

        private void HandleKey(GameTime gameTime)
        {
            if (this.currentKey != Keys.None)
            {
                if (!base.KeyState.IsKeyUp(this.currentKey))
                {
                    return;
                }
                this.currentKey = Keys.None;
            }
            if ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.CurrentPlayer.Controlling)
            {
                if (this.keyState.IsKeyDown(Keys.D1))
                {
                    this.currentKey = Keys.D1;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 1;
                    }
                    else
                    {
                        this.DateGo(1);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D2))
                {
                    this.currentKey = Keys.D2;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 2;
                    }
                    else
                    {
                        this.DateGo(2);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D3))
                {
                    this.currentKey = Keys.D3;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 3;
                    }
                    else
                    {
                        this.DateGo(3);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D4))
                {
                    this.currentKey = Keys.D4;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 4;
                    }
                    else
                    {
                        this.DateGo(4);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D5))
                {
                    this.currentKey = Keys.D5;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 5;
                    }
                    else
                    {
                        this.DateGo(5);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D6))
                {
                    this.currentKey = Keys.D6;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 6;
                    }
                    else
                    {
                        this.DateGo(6);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D7))
                {
                    this.currentKey = Keys.D7;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 7;
                    }
                    else
                    {
                        this.DateGo(7);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D8))
                {
                    this.currentKey = Keys.D8;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 8;
                    }
                    else
                    {
                        this.DateGo(8);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D9))
                {
                    this.currentKey = Keys.D9;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 9;
                    }
                    else
                    {
                        this.DateGo(9);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.D0))
                {
                    this.currentKey = Keys.D0;
                    if (this.editMode)
                    {
                        this.ditukuaidezhi = 10;
                    }
                    else
                    {
                        this.DateGo(10);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.F1))
                {
                    this.currentKey = Keys.F1;
                    if (!this.editMode)
                    {
                        this.DateGo(30);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.F2))
                {
                    this.currentKey = Keys.F2;
                    if (!this.editMode)
                    {
                        this.DateGo(60);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.F3))
                {
                    this.currentKey = Keys.F3;
                    if (!this.editMode)
                    {
                        this.DateGo(90);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.F5))
                {
                    this.currentKey = Keys.F5;
                    if (!this.editMode)
                    {
                        this.DateGo(-999);
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.W))
                {
                    this.currentKey = Keys.W;
                    Session.GlobalVariables.ShowGrid = !Session.GlobalVariables.ShowGrid;
                }
                else if (this.keyState.IsKeyDown(Keys.OemPlus) || this.keyState.IsKeyDown(Keys.Add))
                {
                    this.currentKey = Keys.OemPlus;
                }
                else if (this.keyState.IsKeyDown(Keys.OemMinus) || this.keyState.IsKeyDown(Keys.Subtract))
                {
                    this.currentKey = Keys.OemMinus;
                }
                else if (this.keyState.IsKeyDown(Keys.LeftAlt) && this.keyState.IsKeyDown(Keys.C) && Session.GlobalVariables.EnableCheat)
                {
                    this.currentKey = Keys.C;
                    if (!this.editMode)
                    {
                        changeFaction();
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.LeftAlt) && this.keyState.IsKeyDown(Keys.E) && Session.GlobalVariables.EnableCheat)
                {
                    this.currentKey = Keys.E;
                    this.editMode = true;
                    this.mainMapLayer.xianshidituxiaokuai = true;
                    this.Plugins.youcelanPlugin.IsShowing = false;
                    this.mapEdited = true;
                }
                else if (this.keyState.IsKeyDown(Keys.LeftAlt) && this.keyState.IsKeyDown(Keys.Q) && Session.GlobalVariables.EnableCheat)
                {
                    this.currentKey = Keys.Q;
                    this.editMode = false;
                    this.mainMapLayer.xianshidituxiaokuai = false;
                    this.Plugins.youcelanPlugin.IsShowing = true;
                    this.mapEdited = true;
                }
                else if (this.keyState.IsKeyDown(Keys.T))
                {
                    this.currentKey = Keys.T;
                    if (this.editMode)
                    {
                        this.mainMapLayer.xianshidituxiaokuai = !this.mainMapLayer.xianshidituxiaokuai;
                    }
                }
                else if (this.keyState.IsKeyDown(Keys.L))
                {
                    this.currentKey = Keys.L;
                    this.ShowArchitectureConnectedLine = !this.ShowArchitectureConnectedLine;
                }
            }
            if (this.keyState.IsKeyDown(Keys.Space))
            {

                this.currentKey = Keys.Space;
                if (!this.editMode)
                {
                    this.Plugins.DateRunnerPlugin.Run();
                }
            }
            //if (this.keyState.IsKeyDown(Keys.Z) && this.keyState.IsKeyDown(Keys.LeftControl))
            //{

            //    this.currentKey = Keys.Z;
            //    this.Player.settings.volume += 10;
            //}
            //if (this.keyState.IsKeyDown(Keys.X) && this.keyState.IsKeyDown(Keys.LeftControl))
            //{

            //    this.currentKey = Keys.X;
            //    this.Player.settings.volume -= 10;
            //}
        }



    }
}
