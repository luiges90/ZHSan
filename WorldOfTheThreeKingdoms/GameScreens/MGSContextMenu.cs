using System;
using System.Collections.Generic;
//using System.Data.OleDb;
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

//using GameObjects.PersonDetail.PersonMessages;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    partial class MainGameScreen : Screen
    {
        private void ContextMenuRightClick()
        {
            if ((this.Plugins.ContextMenuPlugin != null) && (this.PeekUndoneWork().Kind == UndoneWorkKind.None))
            {
                if (((this.CurrentArchitecture != null) && (this.CurrentTroop != null)) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureTroopRightClick");
                        this.bianduiLiebiaoBiaoji = "ArchitectureTroopRightClick";
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                    }
                }
                else if ((((((this.CurrentRouteway != null) && (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway)) && (Session.Current.Scenario.IsCurrentPlayer(this.CurrentRouteway.BelongedFaction) && (this.CurrentRouteway.StartArchitecture != null))) && (((this.CurrentRouteway.DestinationArchitecture == null) || !this.CurrentRouteway.StartArchitecture.BelongedSection.AIDetail.AutoRun) || (this.CurrentRouteway.Building || (this.CurrentRouteway.LastActivePointIndex >= 0)))) && (this.CurrentTroop != null)) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopRoutewayRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "TroopRoutewayRightClick";
                    }
                }
                else if ((this.CurrentTroop != null) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "TroopRightClick";
                    }
                }
                else if ((this.CurrentArchitecture != null) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "ArchitectureRightClick";
                    }
                }
                else if (((((this.CurrentRouteway != null) && (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway)) && (Session.Current.Scenario.IsCurrentPlayer(this.CurrentRouteway.BelongedFaction) && (this.CurrentRouteway.StartArchitecture != null))) && (((this.CurrentRouteway.DestinationArchitecture == null) || !this.CurrentRouteway.StartArchitecture.BelongedSection.AIDetail.AutoRun) || (this.CurrentRouteway.Building || (this.CurrentRouteway.LastActivePointIndex >= 0)))) && ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(this.position)))
                {
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentRouteway);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("RoutewayRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "RoutewayRightClick";
                    }
                }
                else if (!this.Plugins.ContextMenuPlugin.IsShowing)
                {
                    this.Plugins.ContextMenuPlugin.IsShowing = true;
                    this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this);
                    this.Plugins.ContextMenuPlugin.SetMenuKindByName("MapRightClick");
                    this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                    this.bianduiLiebiaoBiaoji = "MapRightClick";
                }
            }
        }


        private void HandleContextMenuResult(ContextMenuResult result)
        {
            GameDelegates.VoidFunction function = null;
            GameDelegates.VoidFunction function2 = null;
            GameDelegates.VoidFunction function3 = null;
            GameDelegates.VoidFunction function4 = null;
            GameDelegates.VoidFunction function5 = null;
            GameDelegates.VoidFunction function6 = null;
            this.bianduiLiebiaoBiaoji = "";
            this.Plugins.BianduiLiebiao.IsShowing = false;
            this.Plugins.youcelanPlugin.IsShowing = true;
            this.Plugins.ContextMenuPlugin.ShezhiBianduiLiebiaoXinxi(this.Plugins.BianduiLiebiao.IsShowing, this.Plugins.BianduiLiebiao.Weizhi);

            //switch (result)
            //{

            //    case UndoneWorkKind.ContextMenu:
            //        this.HandleContextMenuResult(this.Plugins.ContextMenuPlugin.Result);
            //        this.gengxinyoucelan();
            //        break;
            //}

            switch (result)
            {
                case ContextMenuResult.Architecture_Detail:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.GetGameObjectList(), null, "", "");
                    break;

                case ContextMenuResult.Architecture_Persons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.GetAllPersons(), null, "", "");
                    break;

                case ContextMenuResult.Architecture_Militaries:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.Militaries.GetList(), null, "", "");
                    break;

                case ContextMenuResult.Architecture_NoFactionPersons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.NoFactionPersons.GetList(), null, "在野人物", "");
                    break;

                case ContextMenuResult.Architecture_Facilities:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Facility, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.Facilities.GetList(), null, "", "");
                    break;

                case ContextMenuResult.Architecture_Captive:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.Browse, true, true, false, false, this.CurrentArchitecture.Captives.GetList(), null, "", "");
                    break;

                case ContextMenuResult.Architecture_Treasure:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.Browse, true, true, false, false, this.CurrentArchitecture.GetAllTreasureInArchitecture(), null, "", "");
                    break;
                //case ContextMenuResult.Architecture_xiangxixinxi:
                //    break;
                case ContextMenuResult.Architecture_Princesses:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.Feiziliebiao, null, "", "");
                    break;
                case ContextMenuResult.Architecture_Informations:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Information, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.Informations, null, "", "");
                    break;
                case ContextMenuResult.Architecture_LandLink:
                    this.mapEdited = true;
                    function5 = delegate
                    {
                        this.screenManager.FrameFunction_Architecture_SelectLandLink();
                    };
                    /*
                    this.CurrentArchitecture.ArchitectureListWithoutSelf().ClearSelected();
                    this.CurrentArchitecture.ArchitectureListWithoutSelf().SetSelected(this.CurrentArchitecture.AILandLinks);
                    */
                    this.SetTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.SelectLandLink, true, true, true, true, this.CurrentArchitecture.ArchitectureListWithoutSelf(), this.CurrentArchitecture.AILandLinks, "陆上连接", "");

                    this.ShowMapViewSelector(true, this.CurrentArchitecture.ArchitectureListWithoutSelf(), function5, MapViewSelectorKind.建筑);

                    break;
                case ContextMenuResult.Architecture_WaterLink:
                    this.mapEdited = true;
                    function6 = delegate
                    {
                        this.screenManager.FrameFunction_Architecture_SelectWaterLink();
                    };
                    this.SetTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.SelectWaterLink, true, true, true, true, this.CurrentArchitecture.ArchitectureListWithoutSelf(), this.CurrentArchitecture.AIWaterLinks, "水上连接", "");
                    this.ShowMapViewSelector(true, this.CurrentArchitecture.ArchitectureListWithoutSelf(), function6, MapViewSelectorKind.建筑);

                    break;
                case ContextMenuResult.Faction_Detail:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Faction, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.GetGameObjectList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Architectures:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Architectures.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Troops:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Troop, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Troops.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Persons:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Persons.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Children:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Children.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Militaries:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Militaries.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_TransferingMilitaries:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.TransferingMilitaries.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_AvailableMilitaryKinds:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.MilitaryKind, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.AvailableMilitaryKinds.GetMilitaryKindList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Captive:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.Browse, true, true, false, false, this.CurrentArchitecture.BelongedFaction.Captives.GetList(), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_SelfCaptive:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.Browse, true, true, false, false, this.CurrentArchitecture.BelongedFaction.SelfCaptives.GetList(), null, "被俘虏列表", "");
                    }
                    break;

                case ContextMenuResult.Faction_DiplomaticRelations:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.CurrentArchitecture.BelongedFaction.ID), null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Techniques:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowFactionTechniques(this.CurrentArchitecture.BelongedFaction, this.CurrentArchitecture);
                    }
                    break;

                case ContextMenuResult.Faction_Sections:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Section, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.Sections, null, "", "");
                    }
                    break;

                case ContextMenuResult.Faction_Treasure:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.Browse, true, true, false, false, this.CurrentArchitecture.GetAllTreasureInFaction(), null, "", "");
                    }
                    break;
                case ContextMenuResult.Faction_Informations:
                    if (this.CurrentArchitecture.BelongedFaction != null)
                    {
                        this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Information, FrameFunction.Browse, false, true, false, false, this.CurrentArchitecture.BelongedFaction.GetAllInformationList(), null, "", "");
                    }
                    break;
                case ContextMenuResult.Internal_StopWork:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.无;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, null, "停止工作", "");
                    break;

                case ContextMenuResult.Internal_zhenzai:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.赈灾;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.ZhenzaiWorkingPersons, "赈灾", "赈灾");

                    break;

                case ContextMenuResult.Internal_Agriculture:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.农业;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.AgricultureWorkingPersons, "农业", "农业");
                    break;

                case ContextMenuResult.Internal_Commerce:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.商业;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.CommerceWorkingPersons, "商业", "商业");
                    break;

                case ContextMenuResult.Internal_Technology:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.技术;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.TechnologyWorkingPersons, "技术", "技术");
                    break;

                case ContextMenuResult.Internal_Domination:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.统治;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.DominationWorkingPersons, "统治", "统治");
                    break;

                case ContextMenuResult.Internal_Morale:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.民心;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.MoraleWorkingPersons, "民心", "民心");
                    break;

                case ContextMenuResult.Internal_Endurance:
                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.耐久;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.EnduranceWorkingPersons, "耐久", "耐久");
                    break;

                case ContextMenuResult.Internal_Facility_Build:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Facility, FrameFunction.GetFacilityToBuild, false, true, true, false, this.CurrentArchitecture.GetBuildableFacilityKindList(), null, "选择设施", "");
                    break;

                case ContextMenuResult.Internal_Facility_Demolish:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Facility, FrameFunction.GetFacilityToDemolish, false, true, true, true, this.CurrentArchitecture.kechaichudesheshi(), null, "选择设施", "");
                    break;
                    
                case ContextMenuResult.Internal_Facility_StopBuilding:
                    this.CurrentArchitecture.StopBuildingFacility();
                    break;

                case ContextMenuResult.Internal_Trade_BuyFood:
                    this.Plugins.NumberInputerPlugin.SetMax(this.CurrentArchitecture.Fund);
                    this.Plugins.NumberInputerPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.NumberInputerPlugin.SetDepthOffset(-0.01f);
                    if (function == null)
                    {
                        function = delegate
                        {
                            this.CurrentArchitecture.BuyFood(this.Plugins.NumberInputerPlugin.Number);
                        };
                    }
                    this.Plugins.NumberInputerPlugin.SetEnterFunction(function);
                    this.Plugins.NumberInputerPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Internal_Trade_SellFood:
                    this.Plugins.NumberInputerPlugin.SetMax(this.CurrentArchitecture.Food);
                    this.Plugins.NumberInputerPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.NumberInputerPlugin.SetDepthOffset(-0.01f);
                    if (function2 == null)
                    {
                        function2 = delegate
                        {
                            this.CurrentArchitecture.SellFood(this.Plugins.NumberInputerPlugin.Number);
                        };
                    }
                    this.Plugins.NumberInputerPlugin.SetEnterFunction(function2);
                    this.Plugins.NumberInputerPlugin.IsShowing = true;
                    break;
                case ContextMenuResult.Internal_Expand:

                    this.screenManager.ArchitectureExpand();
                    break;

                case ContextMenuResult.Military_CampaignAuto://自动出征

                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetAutoCampaignMilitaries, false, true, true, true, this.CurrentArchitecture.GetCampaignMilitaryList(), null, "选择编队", "");
                    break;

                case ContextMenuResult.Military_Campaign:
                    if (function3 == null)
                    {
                        function3 = delegate
                        {
                            this.CurrentArchitecture = this.Plugins.CreateTroopPlugin.CreatingArchitecture as Architecture;
                            this.CurrentMilitary = this.Plugins.CreateTroopPlugin.CreatingMilitary as Military;
                            this.CurrentMilitaries = new GameObjectList();
                            this.CurrentMilitaries.Add(this.CurrentMilitary);
                            this.CurrentGameObjects = this.Plugins.CreateTroopPlugin.CreatingPersons as GameObjectList;
                            this.CurrentPerson = this.Plugins.CreateTroopPlugin.CreatingLeader as Person;
                            this.CurrentNumber = this.Plugins.CreateTroopPlugin.CreatingFood;
                            this.Currentzijin = this.Plugins.CreateTroopPlugin.Creatingzijin;
                        };
                    }
                    this.Plugins.CreateTroopPlugin.SetCreateFunction(function3);
                    this.Plugins.CreateTroopPlugin.SetShellMilitaryKind(null);
                    this.Plugins.CreateTroopPlugin.SetArchitecture(this.CurrentArchitecture);
                    this.Plugins.CreateTroopPlugin.SetPosition(ShowPosition.Center);
                    this.Plugins.CreateTroopPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Military_Troopership:
                    if (function4 == null)
                    {
                        function4 = delegate
                        {
                            this.CurrentArchitecture = this.Plugins.CreateTroopPlugin.CreatingArchitecture as Architecture;
                            this.CurrentMilitary = this.Plugins.CreateTroopPlugin.CreatingMilitary as Military;
                            this.CurrentGameObjects = this.Plugins.CreateTroopPlugin.CreatingPersons as GameObjectList;
                            this.CurrentPerson = this.Plugins.CreateTroopPlugin.CreatingLeader as Person;
                            this.CurrentNumber = this.Plugins.CreateTroopPlugin.CreatingFood;
                            this.Currentzijin = this.Plugins.CreateTroopPlugin.Creatingzijin;
                        };
                    }
                    this.Plugins.CreateTroopPlugin.SetCreateFunction(function4);
                    this.Plugins.CreateTroopPlugin.SetShellMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(28));
                    this.Plugins.CreateTroopPlugin.SetArchitecture(this.CurrentArchitecture);
                    this.Plugins.CreateTroopPlugin.SetPosition(ShowPosition.Center);
                    this.Plugins.CreateTroopPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Military_Training:
                    //this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetTrainingMilitary, false, true, true, false, this.CurrentArchitecture.GetTrainingMilitaryList(), null, "选择编队", "");

                    this.screenManager.CurrentArchitectureWorkKind = ArchitectureWorkKind.训练 ;
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.Architecture_WorkingList, true, true, true, true, this.CurrentArchitecture.Persons, this.CurrentArchitecture.TrainingWorkingPersons , "训练", "训练");


                    break;

                case ContextMenuResult.Military_Transfer://运输编队
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetTransferMilitary, false, true, true, true, this.CurrentArchitecture.movableMilitaries, null, "选择编队", "");
                    break;


                case ContextMenuResult.Military_New:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.MilitaryKind, FrameFunction.GetNewMilitaryKind, false, true, true, false, this.CurrentArchitecture.GetNewMilitaryKindList(), null, "选择兵种", "");
                    break;

                case ContextMenuResult.Military_Recruitment:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetRecruitmentMilitary, false, true, true, false, this.CurrentArchitecture.GetRecruitmentMilitaryList(), null, "选择编队", "");
                    break;

                case ContextMenuResult.Military_Merge:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetMergeMilitary, false, true, true, false, this.CurrentArchitecture.GetMergeMilitaryList(), null, "选择编队", "");
                    break;

                case ContextMenuResult.Military_Disband:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetBeDisbandedMilitaries, false, true, true, true, this.CurrentArchitecture.Militaries, null, "解散编队", "");
                    break;

                case ContextMenuResult.Military_LevelUp:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetLevelUpMilitaries, false, true, true, false, this.CurrentArchitecture.GetLevelUpMilitaryList(), null, "选择编队", "");
                    break;

                case ContextMenuResult.Routeway_Design:  //粮道手动设计
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint));
                    break;

                case ContextMenuResult.Routeway_PointShortestNormal:
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.RoutewayPointShortestNormal));
                    break;

                case ContextMenuResult.Routeway_PointShortestNoWater:
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.RoutewayPointShortestNoWater));
                    break;

                case ContextMenuResult.Routeway_ArchitectureShortestNormal:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.SimpleArchitecture, FrameFunction.GetShortestRouteway, false, true, true, true, this.CurrentArchitecture.GetAILinks(2), null, "粮道", "");
                    break;

                case ContextMenuResult.Routeway_ArchitectureShortestNoWater:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.SimpleArchitecture, FrameFunction.GetShortestNoWaterRouteway, false, true, true, true, this.CurrentArchitecture.GetAILinks(2), null, "粮道", "");
                    break;

                case ContextMenuResult.Routeway_CloseAll:
                    this.CurrentArchitecture.CloseAllRouteways();
                    break;

                case ContextMenuResult.Routeway_DemolishAll:
                    this.CurrentArchitecture.DemolishAllRouteways();
                    break;
                    /*
                case ContextMenuResult.Transport_Resource:
                    this.Plugins.TransportDialogPlugin.SetSourceArchiecture(this.CurrentArchitecture);
                    this.Plugins.TransportDialogPlugin.SetKind(TransportKind.Resource);
                    this.Plugins.TransportDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.TransportDialogPlugin.IsShowing = true;
                    break;
                    */
                    
                case ContextMenuResult.Transport_Fund:
                    this.Plugins.TransportDialogPlugin.SetSourceArchiecture(this.CurrentArchitecture);
                    this.Plugins.TransportDialogPlugin.SetKind(TransportKind.Fund);
                    this.Plugins.TransportDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.TransportDialogPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Transport_Food:
                    this.Plugins.TransportDialogPlugin.SetSourceArchiecture(this.CurrentArchitecture);
                    this.Plugins.TransportDialogPlugin.SetKind(TransportKind.Food);
                    this.Plugins.TransportDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.TransportDialogPlugin.IsShowing = true;
                    break;
                    

                case ContextMenuResult.Person_Transfer:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.PersonTransfer, false, true, true, true, this.CurrentArchitecture.MovablePersons, null, "调动", "");
                    break;

                case ContextMenuResult.Person_Convene:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.PersonConvene, false, true, true, true, this.CurrentArchitecture.GetPersonConveneList(), null, "召集", "");
                    break;

                case ContextMenuResult.Person_AutoHire:
                    this.CurrentArchitecture.AutoHiring = !this.CurrentArchitecture.AutoHiring;
                    break;

                case ContextMenuResult.Person_Appointment_AppointMayor: //任命县令
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.AppointMayor, false, true, true, false, this.CurrentArchitecture.MayorCandicate, null, "任命县令", "");
                    break;

                case ContextMenuResult.Person_Appointment_RecallMayor: //罢免县令
                    this.CurrentArchitecture.RecallMayor();
                    break;

                case ContextMenuResult.Person_Appointment_AppointOfficer://手动封官
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetAppointPerson, false, true, true, false, this.CurrentArchitecture.Kerenmingdeguanyuan, null, "册封", "");
                    break;

                case ContextMenuResult.Person_Appointment_RecallOfficer://免除职位
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetRecallablePerson, false, true, true, false, this.CurrentArchitecture.RecallableOfficer, null, "罢免", "");
                    break;

                case ContextMenuResult.Person_Hire:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.PersonManualHire, false, true, true, false, this.CurrentArchitecture.NoFactionPersons, null, "录用", "");

                    //this.CurrentArchitecture.shoudongluyong();
                    break;

                case ContextMenuResult.Person_Convince:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.ConvincePersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetConvinceSourcePerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "说服", "说服");
                    break;

                case ContextMenuResult.Person_Reward:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.RewardPersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetRewardPerson, false, true, true, true, this.CurrentArchitecture.GetRewardPersons(), null, "褒奖", "Personal");
                    break;

                case ContextMenuResult.Person_Redeem:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.GetRedeemCaptive, false, true, true, false, this.CurrentArchitecture.GetRedeemCaptiveList(), null, "赎回俘虏", "");
                    break;

                case ContextMenuResult.Person_Study_Skill:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetStudySkillPerson, false, true, true, true, this.CurrentArchitecture.GetPersonStudySkillList(), null, "研习", "");
                    break;

                case ContextMenuResult.Person_Study_Title:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetStudyTitlePerson, false, true, true, false, this.CurrentArchitecture.GetPersonStudyTitleList(), null, "研习", "");
                    break;

                case ContextMenuResult.Person_Study_Stunt:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetStudyStuntPerson, false, true, true, false, this.CurrentArchitecture.GetPersonStudyStuntList(), null, "研习", "");
                    break;

                case ContextMenuResult.RoutewayEdit:  //编辑粮道
                    this.Plugins.RoutewayEditorPlugin.SetRouteway(this.CurrentRouteway);
                    this.Plugins.RoutewayEditorPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.RoutewayShowArea:
                    this.CurrentRouteway.ShowArea = !this.CurrentRouteway.ShowArea;
                    break;

                case ContextMenuResult.RoutewayBuild:
                    this.CurrentRouteway.Build();
                    break;

                case ContextMenuResult.RoutewayClose:
                    this.CurrentRouteway.CloseAt(this.position);
                    break;

                case ContextMenuResult.RoutewayDemolish:
                    this.TryToDemolishRouteway();
                    break;

                case ContextMenuResult.Tactics_Information:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.InformationKind, FrameFunction.GetInformationKind, false, true, true, false, Session.Current.Scenario.GameCommonData.AllInformationKinds.GetAvailList(this.CurrentArchitecture), null, "情报种类", "");
                    break;

                case ContextMenuResult.Tactics_StopInformation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Information, FrameFunction.GetInformationToStop, false, true, true, true, this.CurrentArchitecture.Informations, null, "停止情报", "情报");
                    break;
                    /*
                case ContextMenuResult.Tactics_Spy:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.SpyPersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetSpyPerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "间谍", "间谍");
                    break;
                    */
                case ContextMenuResult.Tactics_Destroy:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.DestroyPersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetDestroyPerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "破坏", "破坏");
                    break;

                case ContextMenuResult.Tactics_Instigate:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.InstigatePersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetInstigatePerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "煽动", "煽动");
                    break;

                case ContextMenuResult.Tactics_Gossip:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.GossipPersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetGossipPerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "流言", "流言");
                    break;

                case ContextMenuResult.Tactics_Assassinate:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetAssassinatePerson, false, true, true, false, this.CurrentArchitecture.Persons, null, "暗杀", "暗杀");
                    break;

                case ContextMenuResult.Tactics_Search:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetSearchPerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "搜索", "搜索");
                    break;

                case ContextMenuResult.Tactics_JailBreak:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.JailBreakPersonMaxCount);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetJailBreakPerson, false, true, true, true, this.CurrentArchitecture.Persons, null, "劫狱", "劫狱");
                    break;

                case ContextMenuResult.Monarch_officePosition_jingongzijin:
                    this.Plugins.TransportDialogPlugin.SetSourceArchiecture(this.CurrentArchitecture);
                    this.Plugins.TransportDialogPlugin.SetKind(TransportKind.EmperorFund);
                    this.Plugins.TransportDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.TransportDialogPlugin.SetGameRecord(this.Plugins.GameRecordPlugin);
                    this.Plugins.TransportDialogPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Monarch_officePosition_jingongliangcao:
                    this.Plugins.TransportDialogPlugin.SetSourceArchiecture(this.CurrentArchitecture);
                    this.Plugins.TransportDialogPlugin.SetKind(TransportKind.EmperorFood);
                    this.Plugins.TransportDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.TransportDialogPlugin.SetGameRecord(this.Plugins.GameRecordPlugin);
                    this.Plugins.TransportDialogPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Monarch_MakeMarriage:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.SelectMarryablePerson, false, true, true, false, this.CurrentArchitecture.makeMarryablePersons(), null, "赐婚", "");
                    break;

                case ContextMenuResult.Monarch_TrainChildren:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.SelectTrainableChildren, false, true, true, true, this.CurrentArchitecture.BelongedFaction.Leader.TrainableChildren, null, "子女培育", "");
                    break;

                case ContextMenuResult.Monarch_ChangeCapital:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.GetNewCapital, false, true, true, false, this.CurrentArchitecture.GetChangeCapitalArchitectureList(), null, "迁都", "");
                    break;
                case ContextMenuResult.Monarch_SelectPrince :
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.SelectPrince, false, true, true, false, this.CurrentArchitecture.BelongedFaction.Leader.ChildrenCanBeSelectedAsPrince(), null, "立储", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_QuanXiangDiplomaticRelation: //劝降
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetQuanXiangDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetQuanXiangDiplomaticRelationList() , null, "劝降", "");
                    break;

                case ContextMenuResult.Monarch_Diplomatic_GeDiDiplomaticRelation: //割地
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetGeDiDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetGeDiDiplomaticRelationList(), null, "割地", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_EnhanceDiplomaticRelation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetEnhanceDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetEnhanceDiplomaticRelationList(), null, "亲善", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_AllyDiplomaticRelation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetAllyDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetAllyDiplomaticRelationList(), null, "结盟", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_ResetDiplomaticRelation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetFriendlyDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetResetDiplomaticRelationList(), null, "解盟", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_TruceDiplomaticRelation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetTruceDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetTruceDiplomaticRelationList(), null, "停战", "");
                    break;
                case ContextMenuResult.Monarch_Diplomatic_DenounceDiplomaticRelation:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.DiplomaticRelation, FrameFunction.GetDenounceDiplomaticRelation, false, true, true, false, this.CurrentArchitecture.GetDenounceDiplomaticRelationList(), null, "声讨", "");
                    break;

                case ContextMenuResult.Monarch_Techniques:
                    this.ShowFactionTechniques(this.CurrentArchitecture.BelongedFaction, this.CurrentArchitecture);
                    break;
                case ContextMenuResult.Monarch_KillRelease_ReleaseSelfPerson:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.ReleaseSelfPerson, false, true, true, false, this.CurrentArchitecture.CanKilledPersons(), null, "流放下属", "");
                    break;
                case ContextMenuResult.Monarch_KillRelease_ReleaseCaptive:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.GetReleaseCaptive, false, true, true, true, this.CurrentArchitecture.BelongedFaction.Captives, null, "释放俘虏", "");
                    break;

                case ContextMenuResult.Monarch_KillRelease_MoveCaptive: //移动俘虏
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.MoveCaptive, false, true, true, true, this.CurrentArchitecture.Captives, null, "转移俘虏", "");
                    break;

                case ContextMenuResult.Monarch_KillRelease_KillPerson:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.KillPerson, false, true, true,false , this.CurrentArchitecture.CanKilledPersons(), null, "处斩下属", "");
                    break;

                case ContextMenuResult.Monarch_KillRelease_KillCaptive:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.KillCaptive, false, true, true, false, this.CurrentArchitecture.BelongedFaction.Captives, null, "处斩俘虏", "");
                    break;
                    /*
                case ContextMenuResult.Monarch_ZhaoXianBang_AutoCreatePerson: //招贤榜
                    this.CurrentArchitecture.AutoCreatePerson();
                    break;
                    */
                case ContextMenuResult.Monarch_ZhaoXianBang_GenerateOfficer:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.PersonGeneratorType, FrameFunction.GetOfficerType, false, true, true, false, this.CurrentArchitecture.AvailGeneratorTypeList(), null, "武将类型", "");
                    break;
                    /*
                case ContextMenuResult.Monarch_ZhaoXianBang_DengYong: //登用
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.DengYong, false, true, true, false, this.CurrentArchitecture.NoFactionOfficers, null, "登用", "");
                    break;
                    */
                case ContextMenuResult.Monarch_ZhaoXianBang_DismissOfficer: //遣散
                    this.CurrentArchitecture.DismissOfficer();
                    break;
                    

                case ContextMenuResult.Monarch_hougongTop_nafei:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.xuanzemeinv, true, true, true, false, this.CurrentArchitecture.nvxingwujiang(), null, "纳妃", "");

                    break;
                case ContextMenuResult.Monarch_hougongTop_hougong:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.chongxingmeinv, true, true, true, false, this.CurrentArchitecture.meifaxianhuaiyundefeiziliebiao(), null, "后宫", "");

                    break;

                case ContextMenuResult.Monarch_hougongTop_moveFeizi:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.MoveFeizi, false, true, true, false, this.CurrentArchitecture.movableFeizis, null, "移动妃子", "");
                    break;

                case ContextMenuResult.Monarch_hougongTop_releaseFeizi:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.ReleaseFeizi, false, true, true, true, this.CurrentArchitecture.ReleasableFeizis, null, "释放妃子", "");
                    break;

                case ContextMenuResult.Monarch_Refuse:
                    this.CurrentArchitecture.BelongedFaction.AutoRefuse = !this.CurrentArchitecture.BelongedFaction.AutoRefuse;
                    break;

                case ContextMenuResult.Monarch_Treasure_Confiscate:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.GetConfiscateTreasure, false, true, true, false, this.CurrentArchitecture.BelongedFaction.AllTreasuresExceptLeader, null, "", "");
                    break;

                case ContextMenuResult.Monarch_Treasure_Award:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.GetAwardTreasure, false, true, true, false, this.CurrentArchitecture.GetTreasureListOfLeader(), null, "", "");
                    break;
                case ContextMenuResult.Monarch_officePosition_SelfBecomeEmperor:
                    this.CurrentArchitecture.BelongedFaction.SelfBecomeEmperor();
                    break;
                case ContextMenuResult.Monarch_officePosition_BecomeEmperorLegally:
                    this.CurrentArchitecture.BelongedFaction.BecomeEmperorLegally();
                    break;


                case ContextMenuResult.Section_New:
                    this.Plugins.MarshalSectionDialogPlugin.SetFaction(this.CurrentArchitecture.BelongedFaction);
                    this.Plugins.MarshalSectionDialogPlugin.SetSection(null);
                    this.Plugins.MarshalSectionDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.MarshalSectionDialogPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Section_Regroup:
                    this.Plugins.MarshalSectionDialogPlugin.SetFaction(this.CurrentArchitecture.BelongedFaction);
                    this.Plugins.MarshalSectionDialogPlugin.SetSection(this.CurrentArchitecture.BelongedSection);
                    this.Plugins.MarshalSectionDialogPlugin.SetMapPosition(ShowPosition.Center);
                    this.Plugins.MarshalSectionDialogPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.Section_Disband:
                    this.Plugins.TabListPlugin.SetSelectedItemMaxCount(this.CurrentArchitecture.BelongedFaction.SectionCount - 1);
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Section, FrameFunction.GetSectionToDemolish, false, true, true, true, this.CurrentArchitecture.BelongedFaction.Sections, null, "", "");
                    break;


                case ContextMenuResult.Internal_AutoWorking:
                    this.CurrentArchitecture.AutoWorking = !this.CurrentArchitecture.AutoWorking;
                    if (this.CurrentArchitecture.AutoWorking)
                    {
                        this.CurrentArchitecture.PlayerAIWork();
                    }
                    break;

                case ContextMenuResult.Military_AutoRecruiting:
                    this.CurrentArchitecture.AutoRecruiting = !this.CurrentArchitecture.AutoRecruiting;
                    if (this.CurrentArchitecture.AutoRecruiting)
                    {
                        this.CurrentArchitecture.PlayerAIRecruit();
                    }
                    else if (this.CurrentArchitecture.AutoWorking)
                    {
                        this.CurrentArchitecture.PlayerAIWork();
                    }
                    break;

                case ContextMenuResult.Tactics_AutoSearching:
                    this.CurrentArchitecture.AutoSearching = !this.CurrentArchitecture.AutoSearching;
                    if (this.CurrentArchitecture.AutoSearching)
                    {
                        this.CurrentArchitecture.PlayerAISearch();
                    }
                    break;
                    /*
                case ContextMenuResult .Monarch_ZhaoXianBang_AutoZhaoXian:
                    this.CurrentArchitecture.AutoZhaoXian = !this.CurrentArchitecture.AutoZhaoXian;
                    if (this.CurrentArchitecture.AutoZhaoXian)
                    {
                        this.CurrentArchitecture.PlayAIZhaoXian();
                    }
                    break;
                    */
                case ContextMenuResult.AllEnter:
                    this.CurrentArchitecture.AllEnter();
                    //this.Plugins.AirViewPlugin.ReloadTroopView();
                    Session.Current.Scenario.ClearPersonStatusCache();
                    break;

                case ContextMenuResult.DateGo_1Day:
                    this.DateGo(1);
                    break;

                case ContextMenuResult.DateGo_2Days:
                    this.DateGo(2);
                    break;

                case ContextMenuResult.DateGo_5Days:
                    this.DateGo(5);
                    break;

                case ContextMenuResult.DateGo_10Days:
                    this.DateGo(10);
                    break;

                case ContextMenuResult.DateGo_30Days:
                    this.DateGo(30);
                    break;

                case ContextMenuResult.Jump_Architecture:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Architectures, null, "跳转", "");

                    break;

                case ContextMenuResult.Jump_Troop:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Troop, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Troops, null, "跳转", "");
                    break;

                case ContextMenuResult.Jump_Person:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Jump, false, true, false, false, Session.Current.Scenario.CurrentPlayer.Persons, null, "跳转", "");
                    break;

                case ContextMenuResult.Switch_Smog:
                    Setting.Current.GlobalVariables.DrawMapVeil = !Setting.Current.GlobalVariables.DrawMapVeil;
                    break;

                case ContextMenuResult.Switch_StopOnAttack:
                    Setting.Current.GlobalVariables.StopToControlOnAttack = !Setting.Current.GlobalVariables.StopToControlOnAttack;
                    break;

                case ContextMenuResult.Switch_TroopTitle:
                    this.Plugins.TroopTitlePlugin.IsShowing = !this.Plugins.TroopTitlePlugin.IsShowing;
                    break;

                case ContextMenuResult.Switch_Music:
                    Session.GlobalVariables.PlayMusic = !Session.GlobalVariables.PlayMusic;
                    if (Session.GlobalVariables.PlayMusic)
                    {
                        this.Date_OnSeasonChange(Session.Current.Scenario.Date.Season);
                    }
                    else
                    {
                        Session.StopSong();
                        //this.StopMusic();
                    }
                    break;

                case ContextMenuResult.Switch_NormalSound:
                    Setting.Current.GlobalVariables.PlayNormalSound = !Setting.Current.GlobalVariables.PlayNormalSound;
                    break;

                case ContextMenuResult.Switch_BattleSound:
                    Setting.Current.GlobalVariables.PlayBattleSound = !Setting.Current.GlobalVariables.PlayBattleSound;
                    break;

                case ContextMenuResult.Switch_TroopVoice:
                    Setting.Current.GlobalVariables.TroopVoice = !Setting.Current.GlobalVariables.TroopVoice;
                    break;

                case ContextMenuResult.Switch_TroopAnimation:
                    Setting.Current.GlobalVariables.DrawTroopAnimation = !Setting.Current.GlobalVariables.DrawTroopAnimation;
                    break;

                case ContextMenuResult.Switch_FullScreen:
                    this.ToggleFullScreen();
                    break;

                case ContextMenuResult.Switch_SkyEyeSimpleNotification:
                    Session.GlobalVariables.SkyEyeSimpleNotification = !Session.GlobalVariables.SkyEyeSimpleNotification;
                    if (Session.GlobalVariables.SkyEyeSimpleNotification) {
                        Session.GlobalVariables.SkyEye = true;
                    }
                    break;

                case ContextMenuResult.Switch_SkyEye:
                    Session.GlobalVariables.SkyEye = !Session.GlobalVariables.SkyEye;
                    if (!Session.GlobalVariables.SkyEye)
                    {
                        Session.GlobalVariables.SkyEyeSimpleNotification = false;
                    }
                    break;

                case ContextMenuResult.Switch_MultipleResource:
                    Session.GlobalVariables.MultipleResource = !Session.GlobalVariables.MultipleResource;
                    break;

                case ContextMenuResult.Information_AllSkills:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Skill, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllSkills.GetSkillList(), null, "", "");
                    break;

                case ContextMenuResult.Information_AllTitles:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Title, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllTitles.GetTitleList(), null, "", "");
                    break;

                case ContextMenuResult.Information_AllMilitaryKinds:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.MilitaryKind, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList(), null, "", "");
                    break;

                case ContextMenuResult.Information_AllStunts:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Stunt, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllStunts.GetStuntList(), null, "", "");
                    break;

                case ContextMenuResult.Information_AllFactions:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Faction, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Factions, null, "", "");
                    break;

                case ContextMenuResult.Information_AllArchitectures:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Architectures, null, "", "");
                    break;

                case ContextMenuResult.Information_AllTroops:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Troop, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Troops, null, "", "");
                    break;

                case ContextMenuResult.Information_AllPersons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.AvailablePersons, null, "", "");
                    break;

                case ContextMenuResult.Information_AllDeadPersons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.DeadPersons, null, "", "");
                    break;

                case ContextMenuResult.Information_AllMilitaries:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Militaries, null, "", "");
                    break;

                case ContextMenuResult.Information_AllFacilities:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Facility, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKindList(), null, "设施种类", "");
                    break;

                case ContextMenuResult.Information_AllTerrainDetails:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.TerrainDetail, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.AllTerrainDetails.GetTerrainDetailList(), null, "", "");
                    break;

                case ContextMenuResult.Information_AllSections:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Section, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Sections, null, "", "");
                    break;

                case ContextMenuResult.Information_AllRegions:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Region, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Regions, null, "", "");
                    break;

                case ContextMenuResult.Information_AllStates:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.State, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.States, null, "", "");
                    break;

                case ContextMenuResult.Information_AllTreasures:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.Treasures, null, "", "");
                    break;

                case ContextMenuResult.Information_AllGuanjues:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Guanjue, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao(), null, "", "");
                    break;

                case ContextMenuResult.Load:
                    this.LoadGame();
                    break;

                case ContextMenuResult.Save:
                    this.SaveGame();
                    break;

                case ContextMenuResult.System:
                    this.Plugins.GameSystemPlugin.ShowOptionDialog(ShowPosition.Center);
                    break;

                case ContextMenuResult.TroopMove:
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopDestination));
                    break;

                case ContextMenuResult.TroopTarget:
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopTarget));
                    break;

                case ContextMenuResult.TroopCombatMethod_Cancel:
                    this.CurrentTroop.CurrentCombatMethod = null;
                    break;

                case ContextMenuResult.TroopCombatMethod:
                    this.SetTroopCombatMethod(this.Plugins.ContextMenuPlugin.CurrentParamID);
                    break;

                case ContextMenuResult.TroopStunt:
                    this.SetTroopStunt(this.Plugins.ContextMenuPlugin.CurrentParamID);
                    break;

                case ContextMenuResult.TroopStratagem_Cancel:
                    this.CurrentTroop.CurrentStratagem = null;
                    if (this.CurrentTroop.Status == TroopStatus.埋伏)
                    {
                        this.CurrentTroop.EndAmbush();
                    }
                    break;

                case ContextMenuResult.TroopStratagem_0:  //攻心
                    this.SetTroopStratagem(0);
                    break;

                case ContextMenuResult.TroopStratagem_1:  //扰乱
                    this.SetTroopStratagem(1);
                    break;

                case ContextMenuResult.TroopStratagem_2: //侦查
                    this.SetTroopStratagem(2);
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopInvestigatePosition));
                    break;

                case ContextMenuResult.TroopStratagem_3:  //埋伏
                    this.SetTroopStratagem(3);
                    this.CurrentTroop.Operated = true;
                    break;

                case ContextMenuResult.TroopStratagem_4:  //火攻
                    this.SetTroopStratagem(4);
                    break;

                case ContextMenuResult.TroopStratagem_5:  //镇静
                    this.SetTroopStratagem(5);
                    break;

                case ContextMenuResult.TroopStratagem_6:  //灭火
                    this.SetTroopStratagem(6);
                    this.CurrentTroop.SelfCastPosition = this.CurrentTroop.Position;
                    break;

                case ContextMenuResult.TroopStratagem_7:  //鼓舞
                    this.SetTroopStratagem(7);
                    break;

                case ContextMenuResult.TroopStratagem_8:  //点火
                    this.SetTroopStratagem(8);
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.TroopSetFirePosition));
                    break;

                case ContextMenuResult.TroopStratagem_9:  //医治
                    this.SetTroopStratagem(9);
                    break;

                case ContextMenuResult.TroopStratagem_10: 
                    this.SetTroopStratagem(10);
                    break;

                case ContextMenuResult.TroopStratagem_11: 
                    this.SetTroopStratagem(11);
                    break;

                case ContextMenuResult.TroopStratagem_12:
                    this.SetTroopStratagem(12);
                    break;

                case ContextMenuResult.TroopStratagem_13:
                    this.SetTroopStratagem(13);
                    break;
                case ContextMenuResult.TroopStratagem_14:
                    this.SetTroopStratagem(14);
                    break;

                case ContextMenuResult.TroopStratagem_15:
                    this.SetTroopStratagem(15);
                    break;
                case ContextMenuResult.TroopStratagem_16:
                    this.SetTroopStratagem(16);
                    break;

                case ContextMenuResult.TroopStratagem_17:
                    this.SetTroopStratagem(17);
                    break;
                case ContextMenuResult.TroopStratagem_18:
                    this.SetTroopStratagem(18);
                    break;

                case ContextMenuResult.TroopStratagem_19:
                    this.SetTroopStratagem(19);
                    break;

                case ContextMenuResult.TroopEnter:
                    this.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.Trooprucheng));
                    break;

                case ContextMenuResult.TroopMorph:
                    this.CurrentTroop.Morph();
                    break;

                case ContextMenuResult.TroopOccupy:
                    this.CurrentTroop.Occupy();

                    this.CurrentTroop.RealDestination = this.CurrentTroop.Position ;
                    this.CurrentTroop.TargetTroop = null;
                    this.CurrentTroop.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.CurrentTroop.Position);

                    break;

                case ContextMenuResult.TroopAction_LevyFood:
                    this.CurrentTroop.Operated = true;
                    this.CurrentTroop.LevyFood();
                    break;

                case ContextMenuResult.TroopCutRouteway:
                    this.CurrentTroop.Leader.TextDestinationString = this.CurrentTroop.CutRoutewayDaysNeeded.ToString();
                    this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(this.CurrentTroop.CutRouteway), null);
                    this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(this.CurrentTroop.Leader, this.CurrentTroop.Leader, TextMessageKind.StartCutRouteway, "CutRouteway");
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                    break;

                case ContextMenuResult.TroopConfig_AttackDefaultKind:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.AttackDefaultKind, FrameFunction.GetAttackDefaultKind, false, true, true, false, Session.Current.Scenario.GameCommonData.AllAttackDefaultKinds, Session.Current.Scenario.GameCommonData.AllAttackDefaultKinds.GetSelectedList(this.CurrentTroop.AttackDefaultKind), "攻击默认", "");
                    break;

                case ContextMenuResult.TroopConfig_AttackTargetKind:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.AttackTargetKind, FrameFunction.GetAttackTargetKind, false, true, true, false, Session.Current.Scenario.GameCommonData.AllAttackTargetKinds, Session.Current.Scenario.GameCommonData.AllAttackTargetKinds.GetSelectedList(this.CurrentTroop.AttackTargetKind), "攻击目标", "");
                    break;

                case ContextMenuResult.TroopConfig_CastDefaultKind:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.CastDefaultKind, FrameFunction.GetCastDefaultKind, false, true, true, false, Session.Current.Scenario.GameCommonData.AllCastDefaultKinds, Session.Current.Scenario.GameCommonData.AllCastDefaultKinds.GetSelectedList(this.CurrentTroop.CastDefaultKind), "施展默认", "");
                    break;

                case ContextMenuResult.TroopConfig_CastTargetKind:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.CastTargetKind, FrameFunction.GetCastTargetKind, false, true, true, false, Session.Current.Scenario.GameCommonData.AllCastTargetKinds, Session.Current.Scenario.GameCommonData.AllCastTargetKinds.GetSelectedList(this.CurrentTroop.CastTargetKind), "施展目标", "");
                    break;

                case ContextMenuResult.TroopSetAuto:
                    this.CurrentTroop.Auto = !this.CurrentTroop.Auto;
                    if (!this.CurrentTroop.Auto)
                    {
                        this.CurrentTroop.CurrentCombatMethod = null;
                        this.CurrentTroop.CurrentStratagem = null;
                        this.CurrentTroop.TargetTroop = null;
                        this.CurrentTroop.TargetArchitecture = null;
                        this.CurrentTroop.AttackDefaultKind = TroopAttackDefaultKind.防最弱;
                        this.CurrentTroop.AttackTargetKind = TroopAttackTargetKind.遇敌;
                        this.CurrentTroop.CastDefaultKind = TroopCastDefaultKind.智最弱;
                        this.CurrentTroop.CastTargetKind = TroopCastTargetKind.可能;
                        break;
                    }
                    if (this.CurrentTroop.BelongedLegion != null)
                    {
                        this.CurrentTroop.BelongedLegion.ResetCoreTroop();
                        this.CurrentTroop.AI();
                    }
                    break;

                case ContextMenuResult.TroopIdle:
                    this.CurrentTroop.Operated = true;
                    break;

                case ContextMenuResult.TroopDetail:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Troop, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.GetGameObjectList(), null, "", "");
                    break;

                case ContextMenuResult.TroopPersons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Persons, null, "", "");
                    break;

                case ContextMenuResult.TroopMilitary:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Army.GetGameObjectList(), null, "", "");
                    break;

                case ContextMenuResult.TroopCaptive:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Captives, null, "", "");
                    break;

                case ContextMenuResult.TroopTreasure:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.GetTreasureList(), null, "", "");
                    break;

                case ContextMenuResult.TroopInfo_TroopDetail:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Troop, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.GetGameObjectList(), null, "", "");
                    break;

                case ContextMenuResult.TroopInfo_TroopPersons:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Persons, null, "", "");
                    break;

                case ContextMenuResult.TroopInfo_TroopMilitary:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Army.GetGameObjectList(), null, "", "");
                    break;

                case ContextMenuResult.TroopInfo_TroopCaptive:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Captive, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.Captives, null, "", "");
                    break;

                case ContextMenuResult.TroopInfo_TroopTreasure:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Treasure, FrameFunction.Browse, true, true, false, false, this.CurrentTroop.GetTreasureList(), null, "", "");
                    break;

                case ContextMenuResult.Plugins:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Plugin, FrameFunction.Browse, true, true, false, false, base.PluginList, null, "", "");
                    break;

                case ContextMenuResult.CurrentArchitectureLeftClick:
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
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
                    break;

                case ContextMenuResult.CurrentTroopLeftClick:
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopLeftClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "TroopLeftClick";
                    }
                    break;

                case ContextMenuResult.CurrentArchitectureRightClick:
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentArchitecture);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("ArchitectureRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "ArchitectureRightClick";
                    }
                    break;

                case ContextMenuResult.CurrentTroopRightClick:
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentTroop);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("TroopRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "TroopRightClick";
                    }
                    break;

                case ContextMenuResult.CurrentRoutewayRightClick:
                    if (!this.Plugins.ContextMenuPlugin.IsShowing)
                    {
                        this.Plugins.ContextMenuPlugin.IsShowing = true;
                        this.Plugins.ContextMenuPlugin.SetCurrentGameObject(this.CurrentRouteway);
                        this.Plugins.ContextMenuPlugin.SetMenuKindByName("RoutewayRightClick");
                        this.Plugins.ContextMenuPlugin.Prepare(InputManager.PoX, InputManager.PoY, base.viewportSize);
                        this.bianduiLiebiaoBiaoji = "RoutewayRightClick";
                    }
                    break;
                case ContextMenuResult.YearTable_Year5:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.YearTable, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.getFactionYearTableRecentYears(Session.Current.Scenario.CurrentPlayer, 5), null, "年表", "");
                    break;
                case ContextMenuResult.YearTable_OwnFaction:
                    this.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.YearTable, FrameFunction.Browse, false, true, false, false, Session.Current.Scenario.getOnlyFactionYearTable(Session.Current.Scenario.CurrentPlayer), null, "年表", "");
                    break;
                case ContextMenuResult.ChangeFaction:
                    this.changeFaction();
                    break;

            }
        }


    }
}
