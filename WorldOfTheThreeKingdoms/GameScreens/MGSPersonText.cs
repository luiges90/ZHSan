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

//using GameObjects.PersonDetail.PersonMessages;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    partial class MainGameScreen : Screen
    {
        public override void renwudaodatishi(Person person, Architecture architecture)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction))
            {
                person.TextResultString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonArrive", architecture.Position);
            }
        }

        public override void renwukaishitishi(Person person, Architecture architecture)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction))
            {
                person.TextResultString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonTravel", architecture.Position);
            }
        }

        public override void ArchitectureBeginRecentlyAttacked(Architecture architecture)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction) && architecture.BelongedFaction != null)
            {
                Person reporter = architecture.Advisor;
                if (reporter == null)
                {
                    reporter = architecture.BelongedFaction.Leader;
                }

                if (Session.MainGame.mainGameScreen != null)
                {
                    this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(reporter, architecture, TextMessageKind.ArchitectureUnderAttack, "ArchitectureBeginRecentlyAttacked", "zaoshougongji.jpg", "");
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                    /*architecture.BelongedFaction.Leader.TextResultString = architecture.Name;
                    architecture.BelongedFaction.TextResultString = architecture.Name;
                    this.xianshishijiantupian(architecture.BelongedFaction.Leader, architecture.BelongedFaction.Leader.TextResultString, "ArchitectureBeginRecentlyAttacked", "zaoshougongji.jpg", "", false);
                    */
                }
                architecture.BelongedFaction.TextResultString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(architecture.BelongedFaction, "zaoshougongji", architecture.Position);
                if (architecture.BelongedFaction.StopToControl)
                {
                    this.Plugins.DateRunnerPlugin.Pause();
                    architecture.BelongedFaction.StopToControl = false;


                }
            }

        }

        public override void ArchitectureFacilityCompleted(Architecture architecture, Facility facility)
        {
            if (Session.Current.Scenario.CurrentPlayer == null || 
                (Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction) && !architecture.BelongedSection.AIDetail.AutoRun) || 
                Session.GlobalVariables.SkyEye)
            {
                architecture.TextDestinationString = facility.Name;
                if (Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction) && architecture.BelongedFaction != null)
                {
                    if (!((facility.PositionOccupied <= 1) && Setting.Current.GlobalVariables.NoHintOnSmallFacility))
                    {
                        string sheshitupian = "sheshi\\sheshi" + facility.KindID.ToString() + ".jpg";
                        this.xianshishijiantupian(architecture.BelongedFaction.Leader,architecture.Name, TextMessageKind.FacilityCompleted, "ArchitectureFacilityCompleted", sheshitupian, "sheshiwancheng",facility.Kind.Name,false );
                        this.Plugins.GameRecordPlugin.AddBranch(architecture, "FacilityCompleted", architecture.Position);
                    }
                }
                else if (facility.ArchitectureLimit <= 1 && (facility.PositionOccupied > 4))
                {
                    this.Plugins.GameRecordPlugin.AddBranch(architecture, "FacilityCompleted", architecture.Position);
                }
            }
        }

        public override void Architecturefashengzainan(Architecture architecture, int zainanID)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && architecture.BelongedFaction!=null  && Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                string zainanming = Session.Current.Scenario.GameCommonData.suoyouzainanzhonglei.Getzainanzhonglei(zainanID).Name;
                if (Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction) && architecture.BelongedFaction != null)
                {
                    this.xianshishijiantupian(architecture.BelongedFaction.Leader, architecture.Name, TextMessageKind.DisasterHappened, "fashengzainan", "zainan" + zainanID.ToString() + ".jpg", "zainan" + zainanID.ToString(), zainanming, false);
                    /*
                    architecture.TextDestinationString = zainanming;
                    this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(architecture.BelongedFaction.Leader, architecture, "fashengzainan");
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                    */
                }
                architecture.TextDestinationString = zainanming;

                this.Plugins.GameRecordPlugin.AddBranch(architecture, "fashengzainan", architecture.Position);

            }
        }

        public override void ArchitectureHirePerson(PersonList personList)
        {
            Person person = personList[0] as Person;
            //if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            if ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction))
            {
                foreach (Person person2 in personList)
                {
                    person2.TextResultString = person2.LocationArchitecture.Name;
                    person2.TextDestinationString = person2.BelongedFaction.Name;

                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person2, person2, TextMessageKind.HiredPerson, "ArchitectureHirePerson");
                    this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                    this.Plugins.tupianwenziPlugin.IsShowing = true;

                    //this.Plugins.PersonBubblePlugin.AddPerson(person2, person2.Position, "HirePerson");
                    //person2.TextDestinationString = person2.BelongedFaction.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(person2, "HirePerson", person2.Position);
                }
            }
        }



        public override void ArchitecturePopulationEnter(Architecture a, int quantity)
        {
            if ((Setting.Current.GlobalVariables.HintPopulation && (Setting.Current.GlobalVariables.HintPopulationUnder1000 || (quantity >= 0x3e8))) && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsArchitectureKnown(a)) || Session.GlobalVariables.SkyEye))
            {
                a.TextResultString = quantity.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(a, "ArchitecturePopulationEnter", a.Position);
            }
        }

        public override void ArchitecturePopulationEscape(Architecture a, int quantity)
        {
            if ((Setting.Current.GlobalVariables.HintPopulation && (Setting.Current.GlobalVariables.HintPopulationUnder1000 || (quantity >= 0x3e8))) && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsArchitectureKnown(a)) || Session.GlobalVariables.SkyEye))
            {
                a.TextResultString = quantity.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(a, "ArchitecturePopulationEscape", a.Position);
            }
        }

        public override void ArchitectureReleaseCaptiveAfterOccupied(Architecture architecture, PersonList persons)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsArchitectureKnown(architecture)) || Session.GlobalVariables.SkyEye)
            {
                Person person = persons[StaticMethods.Random(persons.Count)] as Person;
                architecture.TextDestinationString = person.Name;
                this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureReleaseCaptive", architecture.Position);
            }
        }

        public override void ArchitectureRewardPersons(Architecture architecture, GameObjectList personlist)
        {
            if ((personlist.Count > 0) && (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction)) || Session.GlobalVariables.SkyEye))
            {
                this.Plugins.PersonBubblePlugin.AddPerson(personlist[GameObject.Random(personlist.Count)], architecture.Position, TextMessageKind.Rewarded, "RewardPerson");
            }
        }

        public override void CaptivePlayerRelease(Faction from, Faction to, Captive captive)
        {
            if ((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsPlayer(from))
            {
                this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(captive.ReleaseCaptive), new GameDelegates.VoidFunction(captive.ReturnRansom));
                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(from.Leader, captive, TextMessageKind.ReleaseCaptive, "ReleaseCaptive");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void CaptiveRelease(bool success, Faction from, Faction to, Person person)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(from)) || Session.Current.Scenario.IsCurrentPlayer(to)) || Session.GlobalVariables.SkyEye)
            {
                to.TextDestinationString = from.Name;
                to.TextResultString = person.Name;
                if (success)
                {
                    this.Plugins.GameRecordPlugin.AddBranch(to, "CaptiveReleaseSuccess", from.Capital.Position);
                }
                else
                {
                    if ((Session.Current.Scenario.CurrentPlayer != null) && Session.Current.Scenario.IsCurrentPlayer(to))
                    {
                        this.Plugins.GameRecordPlugin.AddBranch(to, "CaptiveReleaseFailed", from.Capital.Position);
                    }
                }
            }
        }

        public override void FactionAfterCatchLeader(Person leader, Faction faction)
        {
            if (Session.Current.Scenario.IsPlayer(faction))
            {
                this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(leader.PlayerKillLeader), null);
                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(leader, leader, TextMessageKind.AsLeaderCaught, "FactionAfterCatchLeader");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void FactionDestroy(Faction faction)
        {
            this.Plugins.GameRecordPlugin.AddBranch(faction, "FactionDestroy", (faction.Capital != null) ? faction.Capital.Position : Session.Current.Scenario.ScenarioMap.JumpPosition);
            Person neutralPerson = Session.Current.Scenario.NeutralPerson;
            if (neutralPerson == null)
            {
                if (Session.Current.Scenario.CurrentPlayer != null)
                {
                    neutralPerson = Session.Current.Scenario.CurrentPlayer.Leader;
                }
                else
                {
                    if (Session.Current.Scenario.Factions.Count <= 0)
                    {
                        return;
                    }
                    neutralPerson = (Session.Current.Scenario.Factions[0] as Faction).Leader;
                }
            }

            this.xianshishijiantupian(neutralPerson, faction.Name, "FactionDestroy", "shilimiewang.jpg", "shilimiewang",true );
            /*
            neutralPerson.TextDestinationString = faction.Name;
            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, neutralPerson, "FactionDestroy");
            this.Plugins.tupianwenziPlugin.IsShowing = true;
             */
        }

        public override void FactionForcedChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
            faction.TextDestinationString = oldCapital.Name;
            faction.TextResultString = newCapital.Name;
            this.Plugins.GameRecordPlugin.AddBranch(faction, "FactionForcedChangeCapital", newCapital.Position);
        }

        public override void FactionGetControl(Faction faction)
        {
            Session.Current.Scenario.CurrentPlayer = faction;
            //this.Plugins.AirViewPlugin.ReloadTroopView();
            this.gengxinyoucelan();

            if (Session.Current.Scenario.needAutoSave)
            {
                this.SaveGameAutoPosition();
                Session.Current.Scenario.needAutoSave = false;
            }

            if (faction.IsPositionKnown(faction.Leader.Position) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(faction.Leader, faction.Leader.Position, TextMessageKind.GetTurn, "GetControl");
            }
            base.PlayNormalSound("Content/Sound/Control/Control");
        }

        public override void FactionInitialtiveChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
            if (oldCapital != null)
            {
                faction.TextDestinationString = oldCapital.Name;
            }
            else
            {
                faction.TextDestinationString = "----";
            }
            faction.TextResultString = newCapital.Name;
            this.Plugins.GameRecordPlugin.AddBranch(faction, "FactionInitialtiveChangeCapital", newCapital.Position);
        }

        public override void FactionTechniqueFinished(Faction faction, Technique technique)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(faction)) || Session.GlobalVariables.SkyEye)
            {
                faction.Leader.TextDestinationString = technique.Name;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(faction.Leader, faction.Leader, TextMessageKind.FactionTechniqueFinished, "FactionTechniqueFinished");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void FactionUpgradeTechnique(Faction faction, Technique technique, Architecture architecture)
        {
            /*
            if (!Session.Current.Scenario.IsCurrentPlayer(faction))
            {
                faction.TextDestinationString = technique.Name;
                this.Plugins.GameRecordPlugin.AddBranch(faction, "UpgradeTechnique", faction.Capital.Position);
            }
             * */
        }

        public override void shilijingong(Faction faction,int jingongzijin,string jingongzhonglei)
        {
            //this.xianshishijiantupian(faction.Leader, jingongzijin.ToString(), "shilijingong", "shilijingong.jpg", "");
            faction.TextResultString = jingongzijin.ToString();
            faction.TextDestinationString = jingongzhonglei;
            
            this.Plugins.GameRecordPlugin.AddBranch(faction, "shilijingong", faction.Capital.Position);


        }

        public override void GameEndWithUnite(Faction faction)
        {
            faction.TextResultString = Session.Current.Scenario.Date.ToDateString();
            this.Plugins.GameRecordPlugin.AddBranch(faction, "GameEndWithUnite", faction.Capital.Position);
            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(faction.Leader, faction.Leader, TextMessageKind.EndWithUnite, "GameEndWithUnite");
            this.Plugins.tupianwenziPlugin.IsShowing = true;
            Session.Current.Scenario.YearTable.addGameEndWithUniteEntry(Session.Current.Scenario.Date, faction);
        }

        public override void PersonBeAwardedTreasure(Person person, Treasure t)
        {
            /*
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextResultString = t.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.BeAwardedTreasure, "PersonBeAwardedTreasure");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                this.Plugins.tupianwenziPlugin.IsShowing = true;


            }*/
        }

        public override void selfFoundPregnant(Person person)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && person.BelongedArchitecture != null &&
                    Session.Current.Scenario.IsCurrentPlayer(person.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                //person.TextResultString = t.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.SelfFoundPregnant, "selfFoundPregnant");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;


            }
        }

        public override void coupleFoundPregnant(Person person, Person reporter)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && person.BelongedArchitecture != null &&
                    Session.Current.Scenario.IsCurrentPlayer(person.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                //person.TextResultString = t.Name;
                //reporter.TextResultString = person.Name;
                //this.Plugins.tupianwenziPlugin.SetGameObjectBranch(reporter, reporter, TextMessageKind.CoupleFoundPregnant, "coupleFoundPregnant");
                //this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                //this.Plugins.tupianwenziPlugin.IsShowing = true;
                if (person.BelongedArchitecture != null)
                {
                    this.Plugins.GameRecordPlugin.AddBranch(person, "OfficerPregnant", person.Position);
                }
            }
        }

        public override void faxianhuaiyun(Person person)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && person.BelongedArchitecture != null && 
                    Session.Current.Scenario.IsCurrentPlayer(person.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                //person.TextResultString = t.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.FoundPregnant, "faxianhuaiyun");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;


            }
        }

        public override void xiaohaichusheng(Person father, Person mother, Person person)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && father.BelongedArchitecture != null &&
                    Session.Current.Scenario.IsCurrentPlayer(father.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                if (father.BelongedFaction != null && father.BelongedFaction.Leader == father && mother.Status == PersonStatus.Princess)
                {
                    person.TextResultString = person.Name;
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(father, father, TextMessageKind.ChildrenBorn, "xiaohaichusheng");
                    this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }

                if (father.BelongedArchitecture != null)
                {
                    father.TextResultString = mother.Name;
                    father.TextDestinationString = person.Name;
                    if (person.Sex)
                    {
                        this.Plugins.GameRecordPlugin.AddBranch(father, "GirlPersonBorn", mother.Position);
                    }
                    else
                    {
                        this.Plugins.GameRecordPlugin.AddBranch(father, "BoyPersonBorn", mother.Position);
                    }
                }
            }

        }
        public override void haizizhangdachengren(Person father, Person person, bool showChildTalk)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && person.BelongedArchitecture != null &&
                    Session.Current.Scenario.IsCurrentPlayer(person.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                //person.TextResultString = t.Name;
                if (showChildTalk)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.BeChildrenBorn, "haizizhangdachengren");
                    this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                    this.Plugins.tupianwenziPlugin.IsShowing = true;
                }

                if (person.BelongedArchitecture != null && person.BelongedArchitecture.BelongedFaction != null)
                {
                    person.TextResultString = person.BelongedArchitecture.Name;
                    person.TextDestinationString = person.BelongedArchitecture.BelongedFaction.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(person, "ChildrenJoin", person.Position);
                }
            }
        }

        public override void xianshishijiantupian(Person p, string TextResultString, TextMessageKind msgKind, string shijian, string tupian, string shengyin, bool zongshixianshi)
        {
            if (Session.Current.Scenario.CurrentPlayer == null) return;

            if (shijian == "CaptiveEscape")
            {
                if (p.BelongedFaction == Session.Current.Scenario.CurrentPlayer || Session.Current.Scenario.CurrentPlayer.Captives.HasGameObject(p.BelongedCaptive))
                {
                    zongshixianshi = true;
                    p.TextResultString = TextResultString;
                    this.Plugins.GameRecordPlugin.AddBranch(p, "CaptiveEscape", p.Position);
                }
            }
            if ((zongshixianshi) || p.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                p.TextResultString = TextResultString;
                //p.TextDestinationString = architecture.BelongedFaction.LeaderName;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, msgKind, shijian, tupian, shengyin);
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                //jokosany取消暂停音乐，下面共计4个
               // this.PauseMusic();
               // this.tufashijianzantingyinyue = true;
            }
        }

        public override void xianshishijiantupian(Person p, string TextResultString, TextMessageKind msgKind, string shijian, string tupian, string shengyin, string TextDestinationString, bool zongshixianshi)
        {
            /*
            if (troop.BelongedFaction == Session.Current.Scenario.CurrentPlayer || architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {

            }
            */
            if (Session.Current.Scenario.CurrentPlayer == null) return;

            if ((zongshixianshi) || p.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                p.TextResultString = TextResultString;
                p.TextDestinationString = TextDestinationString;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, msgKind, shijian, tupian, shengyin);
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
               // this.PauseMusic();
               // this.tufashijianzantingyinyue = true;
            }
        }

        public override void xianshishijiantupian(Person p, string TextResultString, string shijian, string tupian, string shengyin, bool zongshixianshi)
        {
            if (Session.Current.Scenario.CurrentPlayer == null) return;

            if (shijian == "CaptiveEscape")
            {
                if (p.BelongedFaction == Session.Current.Scenario.CurrentPlayer || Session.Current.Scenario.CurrentPlayer.Captives.HasGameObject(p.BelongedCaptive))
                {
                    zongshixianshi = true;
                    p.TextResultString = TextResultString;
                    this.Plugins.GameRecordPlugin.AddBranch(p, "CaptiveEscape", p.Position);
                }
            }
            if ((zongshixianshi) || p.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                p.TextResultString = TextResultString;
                //p.TextDestinationString = architecture.BelongedFaction.LeaderName;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, shijian, tupian, shengyin);
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
               // this.PauseMusic();
              //  this.tufashijianzantingyinyue = true;
            }
        }

        public override void xianshishijiantupian(Person p, string TextResultString, string shijian, string tupian, string shengyin, string TextDestinationString, bool zongshixianshi)
        {
            /*
            if (troop.BelongedFaction == Session.Current.Scenario.CurrentPlayer || architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {

            }
            */
            if (Session.Current.Scenario.CurrentPlayer == null) return;

            if ((zongshixianshi) || p.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                p.TextResultString = TextResultString;
                p.TextDestinationString = TextDestinationString;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, shijian, tupian, shengyin);
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
              //  this.PauseMusic();
               // this.tufashijianzantingyinyue = true;

            }
        }

        public override void xianshishijiantupian(object person, object gameObject, string branchName, bool zongshixianshi)
        {
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, branchName);
            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
            this.Plugins.tupianwenziPlugin.IsShowing = true;
        }

        public override void PersonBeConfiscatedTreasure(Person person, Treasure t)
        {
            /*
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextResultString = t.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.BeConfiscatedTreasure, "PersonBeConfiscatedTreasure");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
            */

        }

        public void PersonBeiDuoqi(Person person, Faction t)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(t)) || Session.GlobalVariables.SkyEye)
            {
                person.TextResultString = t.LeaderName;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.BeTakenSpouse, "wujiangbeiduoqi");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }


        }


        public override void PersonBeKilled(Person person, Architecture location)
        {
            location.TextDestinationString = person.Name;
            this.Plugins.GameRecordPlugin.AddBranch(location, "PersonBeKilled", location.Position);
            Person neutralPerson = Session.Current.Scenario.NeutralPerson;
            if (neutralPerson == null)
            {
                if (Session.Current.Scenario.CurrentPlayer != null)
                {
                    neutralPerson = Session.Current.Scenario.CurrentPlayer.Leader;
                }
                else
                {
                    if (Session.Current.Scenario.Factions.Count <= 0)
                    {
                        return;
                    }
                    neutralPerson = (Session.Current.Scenario.Factions[0] as Faction).Leader;
                }
            }
            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, location, "PersonBeKilled");
            this.Plugins.tupianwenziPlugin.IsShowing = true;
        }

        public override void PersonChangeLeader(Faction faction, Person leader, bool changeName, string oldName)
        {
            if (!changeName)
            {
                leader.TextDestinationString = oldName;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(leader, leader, TextMessageKind.ChangeLeaderKeepName, "FactionChangeLeaderKeepName");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
            else
            {
                leader.TextDestinationString = oldName;
                leader.TextResultString = faction.Name;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(leader, leader, TextMessageKind.ChangeLeaderChangeName, "FactionChangeLeaderChangeName");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonConvinceFailed(Person source, Person destination)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(source.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(destination.BelongedFaction))
            {
                source.TextDestinationString = destination.Name;
                this.Plugins.GameRecordPlugin.AddBranch(source, "PersonConvinceFailed", source.OutsideDestination.Value);
            }
        }
        
        /*
        public override void QuanXiangFailed(Person source, Faction targetFaction) //劝降失败
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(source.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(targetFaction))
            {
                source.TextResultString = targetFaction.Name;
                this.Plugins.GameRecordPlugin.AddBranch(source, "QuanXiangFailed", source.OutsideDestination.Value);
            }
        }
        */
        public override void PersonConvinceSuccess(Person source, Person destination, Faction oldFaction)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(source.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(oldFaction)) || Session.GlobalVariables.SkyEye)
            {
                Person neutralPerson = Session.Current.Scenario.NeutralPerson;
                if (neutralPerson == null)
                {
                    neutralPerson = source;
                }

                source.TextDestinationString = destination.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, source, "PersonConvinceSuccess");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(source, "PersonConvinceSuccess", source.Position);

            }
        }

        public override void TroopPersonChallenge(int win, Troop sourceTroop, Person P1, Troop destinationTroop, Person P2)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sourceTroop.Position)) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(destinationTroop.Position)) || Session.GlobalVariables.SkyEye)
            {
                sourceTroop.TextDestinationString = destinationTroop.DisplayName;
                sourceTroop.TextResultString = P1.Name;
                sourceTroop.CurrentSourceChallengePersonName = P1.Name;
                sourceTroop.CurrentDestinationChallengePersonName = P2.Name;

                destinationTroop.TextDestinationString = sourceTroop.DisplayName;
                destinationTroop.TextResultString = P2.Name;
                destinationTroop.CurrentSourceChallengePersonName = P1.Name;
                destinationTroop.CurrentDestinationChallengePersonName = P2.Name;

                Person neutralPerson = Session.Current.Scenario.NeutralPerson;
                if (neutralPerson == null)
                {
                    neutralPerson = P1;
                }
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);

                switch (win)
                {
                    case 1: //P1武将胜利
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeSourceWin");
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(P1, sourceTroop, TextMessageKind.DualActiveWin, "TroopPersonChallengeAfterSourceWin");

                        this.Plugins.tupianwenziPlugin.IsShowing = true;

                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeSourceWin", sourceTroop.Position);
                        break;
                    case 2: //2：P2武将胜利
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeSourceLose");
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(P2, sourceTroop, TextMessageKind.DualPassiveWin, "TroopPersonChallengeAfterSourceLose");
 
                        this.Plugins.tupianwenziPlugin.IsShowing = true;

                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeSourceLose", sourceTroop.Position);
                        break;
                    case 3: //3：P1武将被杀
                        this.PersonDeathInChallenge(P1, sourceTroop);

                        break;
                    case 4: //4：P2武将被杀
                        this.PersonDeathInChallenge(P2, destinationTroop);

                        break;
                    case 5: //5：P1武将逃跑
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeEscape");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeEscape", sourceTroop.Position);
                        break;
                    case 6: //6：P2武将逃跑
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, destinationTroop, "TroopPersonChallengeEscape");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        this.Plugins.GameRecordPlugin.AddBranch(destinationTroop, "TroopPersonChallengeEscape", destinationTroop.Position);
                        break;
                    case 7: //7、P1武将被俘虏
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeSourceBeCaptured");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeSourceBeCaptured", sourceTroop.Position);
                        break;
                    case 8: //8、P2武将被俘虏
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, destinationTroop, "TroopPersonChallengeDestinationBeCaptured");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        this.Plugins.GameRecordPlugin.AddBranch(destinationTroop, "TroopPersonChallengeDestinationBeCaptured", destinationTroop.Position);
                        break;
                    case 9: //9、P1武将被拉拢
                        //直接使用在建筑里说服的话语
                        break;
                    case 10: //10、P2武将被拉拢
                        //直接使用在建筑里说服的话语
                        break;
                    case -1: //-1：平局
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeDraw");
                        this.Plugins.tupianwenziPlugin.IsShowing = true;

                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeDraw", sourceTroop.Position);
                        break;
                    case -2: //-2：平局：P1武将被杀
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeDraw");
                        this.PersonDeathInChallenge(P1, sourceTroop);

                        //this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeDraw", sourceTroop.Position);
                        break;
                    case -3: //-3：平局：P2武将被杀
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeDraw");
                        this.PersonDeathInChallenge(P2, destinationTroop);

                        //this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeDraw", sourceTroop.Position);
                        break;
                    case -4: //-4：平局：双方武将被杀
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonChallengeDraw");

                        this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonChallengeDraw", sourceTroop.Position);
                        this.PersonDeathInChallenge(P1, sourceTroop);
                        this.PersonDeathInChallenge(P2, destinationTroop);
                        break;
                }





            }
        }

        public override void TroopPersonControversy(bool win, Troop sourceTroop, Person source, Troop destinationTroop, Person destination)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(sourceTroop.Position)) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(destinationTroop.Position)) || Session.GlobalVariables.SkyEye)
            {
                sourceTroop.TextDestinationString = destinationTroop.DisplayName;
                Person neutralPerson = Session.Current.Scenario.NeutralPerson;
                if (neutralPerson == null)
                {
                    neutralPerson = source;
                }
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
#pragma warning disable CS0168 // The variable 'msg' is declared but never used
                List<string> msg;
#pragma warning restore CS0168 // The variable 'msg' is declared but never used
                if (win)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonControversySourceWin");
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(source, sourceTroop, TextMessageKind.ControversyActiveWin, "TroopPersonControversyAfterSourceWin");
                }
                else
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(neutralPerson, sourceTroop, "TroopPersonControversySourceLose");
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(destination, sourceTroop, TextMessageKind.ControversyPassiveWin, "TroopPersonControversyAfterSourceLose");
                }
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                if (win)
                {
                    this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonControversySourceWin", sourceTroop.Position);
                }
                else
                {
                    this.Plugins.GameRecordPlugin.AddBranch(sourceTroop, "TroopPersonControversySourceLose", sourceTroop.Position);
                }
            }
        }

        public override void PersonDeath(Person person, Person killerInBattle, Architecture location, Troop troopLocation)
        {
            String personFaction, killerFaction;
            personFaction = person.BelongedFaction == null ? "" : person.BelongedFaction.Name + "军的";
            killerFaction = killerInBattle == null || killerInBattle.BelongedFaction == null ? "" : killerInBattle.BelongedFaction.Name + "军的";
            if (location != null)
            {
                if (killerInBattle == null)
                {
                    location.TextDestinationString = personFaction + person.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(location, "PersonDeath", location.Position);
                }
                else
                {
                    location.TextResultString = killerFaction + killerInBattle.Name;
                    location.TextDestinationString = personFaction + person.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(location, "PersonKilledInBattle", location.Position);
                }
            }
            else if (troopLocation != null)
            {
                if (killerInBattle == null)
                {
                    troopLocation.TextDestinationString = personFaction + person.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(troopLocation, "PersonDeath", troopLocation.Position);
                }
                else
                {
                    troopLocation.TextResultString = killerFaction + killerInBattle.Name;
                    troopLocation.TextDestinationString = personFaction + person.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(troopLocation, "PersonKilledInBattle", troopLocation.Position);
                }
            }
            else
            {
                return;
            }

            Person neutralPerson;
            neutralPerson = this.getNeutralPerson();

            this.xianshishijiantupian(neutralPerson, person.Name, TextMessageKind.Died, killerInBattle == null ? "renwusiwang" : "renwusiwangInBattle",
                person.ID.ToString(), "renwusiwang", location == null ? troopLocation.Name : location.Name, true);
        }

        public override void PersonDeathInChallenge(Person person, Troop troop)
        {
            String personFaction = person.BelongedFaction == null ? "" : person.BelongedFaction.Name + "的";
            troop.TextDestinationString = personFaction + person.Name;
            this.Plugins.GameRecordPlugin.AddBranch(troop, "PersonDeathInChallenge", troop.Position);
            Person neutralPerson;
            neutralPerson = this.getNeutralPerson();

            this.xianshishijiantupian(neutralPerson, person.Name, TextMessageKind.DiedInChallenge, "renwusiwangInChallenge", person.ID.ToString(), "renwusiwang", troop.DisplayName, true);

        }

        private Person getNeutralPerson()
        {
            Person neutralPerson = Session.Current.Scenario.NeutralPerson;
            if (neutralPerson == null)
            {
                if (Session.Current.Scenario.CurrentPlayer != null)
                {
                    neutralPerson = Session.Current.Scenario.CurrentPlayer.Leader;
                }
                else
                {
                    if (Session.Current.Scenario.Factions.Count <= 0)
                    {
                        return null;
                    }
                    neutralPerson = (Session.Current.Scenario.Factions[0] as Faction).Leader;
                }
            }
            return neutralPerson;
        }

        public override void PersonDeathChangeFaction(Person dead, Person leader, string oldName)
        {
            leader.BelongedFaction.TextDestinationString = oldName;
            leader.BelongedFaction.TextResultString = leader.BelongedFaction.Name;
            this.Plugins.GameRecordPlugin.AddBranch(leader.BelongedFaction, "FactionLeaderDeathFactionChange", leader.BelongedFaction.Capital.Position);
            this.xianshishijiantupian(leader, dead.RespectfulName, TextMessageKind.DiedChangeFaction, "FactionLeaderDeathChangeFaction", "", "", oldName,true );
            /*
            leader.BelongedFaction.TextResultString = dead.RespectfulName;
            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(leader, leader.BelongedFaction, "FactionLeaderDeathChangeFaction");
            this.Plugins.tupianwenziPlugin.IsShowing = true;
             */
 
        }

        public override void PersonDestroyFailed(Person person, Architecture architecture)
        {
            if (person.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonDestroyArchitectureFailed", architecture.Position);
            }
            else if (architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureDestroyedFailed", architecture.Position);
            }
        }

        public override void PersonDestroySuccess(Person person, Architecture architecture, int down)
        {
            if ((person.BelongedFaction == Session.Current.Scenario.CurrentPlayer) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                person.TextResultString = down.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonDestroyArchitectureSuccess", architecture.Position);
            }
            else if (architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                architecture.TextResultString = down.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureDestroyedSuccess", architecture.Position);
            }
        }

        public override void PersonGossipFailed(Person person, Architecture architecture)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction))
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonGossipFailed", person.OutsideDestination.Value);
            }
        }

        public override void PersonGossipSuccess(Person person, Architecture architecture)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonGossipSuccess", person.OutsideDestination.Value);
            }
        }

        public override void PersonInformationObtained(Person person, Information information)
        {
            if (Session.Current.Scenario.CurrentPlayer == person.BelongedFaction)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(person, person.Position, TextMessageKind.InformationSuccess, "InformationObtained");
                this.Plugins.GameRecordPlugin.AddBranch(person, "qingbaochenggong", person.Position);
            }
        }

        public override void qingbaoshibai(Person person)
        {
            if (Session.Current.Scenario.CurrentPlayer == person.BelongedFaction)
            {
                this.Plugins.PersonBubblePlugin.AddPerson(person, person.Position, TextMessageKind.InformationFailure, "qingbaoshibai");
                this.Plugins.GameRecordPlugin.AddBranch(person, "qingbaoshibai", person.Position);

            }
        }


        public override void PersonInstigateFailed(Person person, Architecture architecture)
        {
            if (person.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonInstigateArchitectureFailed", architecture.Position);
            }
            else if (architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureInstigatedFailed", architecture.Position);
            }
        }

        public override void PersonInstigateSuccess(Person person, Architecture architecture, int down)
        {
            if ((person.BelongedFaction == Session.Current.Scenario.CurrentPlayer) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                person.TextResultString = down.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonInstigateArchitectureSuccess", architecture.Position);
            }
            else if (architecture.BelongedFaction == Session.Current.Scenario.CurrentPlayer)
            {
                architecture.TextResultString = down.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(architecture, "ArchitectureInstigatedSuccess", architecture.Position);
            }
        }

        public override void PersonLeave(Person person, Architecture location)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(location.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                location.TextDestinationString = person.Name;
                this.Plugins.GameRecordPlugin.AddBranch(location, "PersonLeave", location.Position);
                person.TextDestinationString = location.BelongedFaction.Name;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.LeaveFaction, "PersonLeave");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonSearchFinished(Person person, Architecture architecture, SearchResultPack resultPack)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                if (person.shoudongsousuo)
                {
                    switch (resultPack.Result)
                    {
                        case SearchResult.资金:
                            person.TextResultString = resultPack.Number.ToString();
                            //this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, "SearchFundFound");
                            this.xianshishijiantupian(person, person.TextResultString, TextMessageKind.SearchFundFound, "SearchFundFound", "", "", architecture.Name,false );
                            break;

                        case SearchResult.粮草:
                            person.TextResultString = resultPack.Number.ToString();
                            //this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, "SearchFoodFound");
                            this.xianshishijiantupian(person, person.TextResultString, TextMessageKind.SearchFoodFound, "SearchFoodFound", "", "", architecture.Name,false );

                            break;

                        case SearchResult.技巧:
                            person.TextResultString = resultPack.Number.ToString();
                            //this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, "SearchTechniqueFound");
                            this.xianshishijiantupian(person, person.TextResultString, TextMessageKind.SearchTechniqueFound, "SearchTechniqueFound", "", "", architecture.Name,false );

                            break;
                            /*
                        case SearchResult.间谍:
                            person.TextResultString = resultPack.FoundPerson.Name;
                            //this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, "SearchSpyFound");
                            this.xianshishijiantupian(person, person.TextResultString, TextMessageKind.SearchSpyFound, "SearchSpyFound", "", "", architecture.Name,false );

                            this.Plugins.GameRecordPlugin.AddBranch(person, "SearchSpyFound", person.Position);
                            break;
                            */
                        case SearchResult.隐士:
                            person.TextResultString = resultPack.FoundPerson.Name;
                            //this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, "SearchPersonFound");
                            this.xianshishijiantupian(person, person.TextResultString, TextMessageKind.SearchPersonFound, "SearchPersonFound", "", "", architecture.Name,false );

                            this.Plugins.GameRecordPlugin.AddBranch(person, "SearchPersonFound", person.Position);
                            break;
                        case SearchResult.无:
                            //person.TextResultString = architecture.Name;
                            //this.xianshishijiantupian(person, person.TextResultString, "SearchPersonFound", "", "", architecture.Name);
                            //this.Plugins.GameRecordPlugin.AddBranch(person, "SearchNoFound", person.Position);
                            break;
                    }
                    person.shoudongsousuo = false;

                }
                else
                {
                    switch (resultPack.Result)
                    {
                        case SearchResult.资金:
                            person.TextResultString = resultPack.Number.ToString();
                            this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, TextMessageKind.SearchFundFound, "SearchFundFound");
                            break;

                        case SearchResult.粮草:
                            person.TextResultString = resultPack.Number.ToString();
                            this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, TextMessageKind.SearchFoodFound, "SearchFoodFound");

                            break;

                        case SearchResult.技巧:
                            person.TextResultString = resultPack.Number.ToString();
                            this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, TextMessageKind.SearchTechniqueFound, "SearchTechniqueFound");

                            break;
                            /*
                        case SearchResult.间谍:
                            person.TextResultString = resultPack.FoundPerson.Name;
                            this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, TextMessageKind.SearchSpyFound, "SearchSpyFound");

                            this.Plugins.GameRecordPlugin.AddBranch(person, "SearchSpyFound", person.Position);
                            break;
                            */
                        case SearchResult.隐士:
                            person.TextResultString = resultPack.FoundPerson.Name;
                            this.Plugins.PersonBubblePlugin.AddPerson(person, architecture.Position, TextMessageKind.SearchPersonFound, "SearchPersonFound");

                            this.Plugins.GameRecordPlugin.AddBranch(person, "SearchPersonFound", person.Position);
                            break;
                    }

                }// end if (person.shoudongsousuo)
            }
        }

        /*
        public override void PersonShowMessage(Person person, PersonMessage message)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) && (message is SpyMessage))
            {
                SpyMessage gameObject = message as SpyMessage;
                switch (gameObject.Kind)
                {
                    case SpyMessageKind.NewMilitary:
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, "SpyMessageNewMilitary");
                        this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        break;

                    case SpyMessageKind.MilitaryScale:
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, "SpyMessageMilitaryScale");
                        this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        break;

                    case SpyMessageKind.NewFacility:
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, "SpyMessageNewFacility");
                        this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        break;

                    case SpyMessageKind.NewTroop:
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, "SpyMessageNewTroop");
                        this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        break;

                    case SpyMessageKind.HireNewPerson:
                        this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, gameObject, "SpyMessageHireNewPerson");
                        this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                        this.Plugins.tupianwenziPlugin.IsShowing = true;
                        break;
                }
            }
        }

        public override void PersonSpyFailed(Person person, Architecture architecture)
        {
            if ((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction))
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonSpyFailed", person.OutsideDestination.Value);
            }
        }

        public override void PersonSpyFound(Person person, Person spy)
        {
            if ((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(spy.BelongedFaction))
            {
                spy.TextDestinationString = person.TargetArchitecture.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(spy, spy, "PersonSpyFound");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonSpySuccess(Person person, Architecture architecture)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                person.TextResultString = person.SpyDays.ToString();
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonSpySuccess", person.OutsideDestination.Value);
            }
        }
        */

        public override void PersonStudySkillFinished(Person person, string skillString, bool success)
        {
            if ((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction))
            {
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                if (success)
                {
                    person.TextDestinationString = skillString;
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudySkillSuccess, "PersonStudySkillSuccess");
                }
                else
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudySkillFailure, "PersonStudySkillFailed");
                }
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonStudyStuntFinished(Person person, Stunt stunt, bool success)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = stunt.Name;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                if (success)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudyStuntSuccess, "PersonStudyStuntSuccess");
                }
                else
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudyStuntFailure, "PersonStudyStuntFailed");
                }
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonStudyTitleFinished(Person person, Title title, bool success)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = title.Name;
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                if (success)
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudyTitleSuccess, "PersonStudyTitleSuccess");
                }
                else
                {
                    this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.StudyTitleFailure, "PersonStudyTitleFailed");
                }
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void PersonTreasureFound(Person person, Treasure treasure)
        {
            if (((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = person.TargetArchitecture.Name;
                person.TextResultString = treasure.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(person, person, TextMessageKind.TreasureFound, "PersonTreasureFound");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(person, "SearchTreasureFound", person.Position);
            }
        }

        public override void PersonCapturedByArchitecture(Person person, Architecture architecture)
        {
            if (Session.Current.Scenario.CurrentPlayer == null || Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction) 
                || Session.Current.Scenario.IsCurrentPlayer(architecture.BelongedFaction) || Session.GlobalVariables.SkyEye)
            {
                person.TextDestinationString = architecture.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "CapturedByArchitecture", person.Position);
            }
        }

        public override void PersonJailBreak(Person person, Captive captive)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(captive.BelongedFaction))
            {
                person.TextDestinationString = captive.LocationArchitecture.Name;
                person.TextResultString = captive.CaptivePerson.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonJailBreak", person.Position);
            }
        }

        public override void PersonJailBreakFailed(Person person, Architecture target)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(person.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(target.BelongedFaction))
            {
                person.TextDestinationString = target.Name;
                this.Plugins.GameRecordPlugin.AddBranch(person, "PersonJailBreakFailed", person.Position);
            }
        }

        public override void PersonAssassinateSuccess(Person killer, Person killed, Architecture a)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(killer.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(killed.BelongedFaction))
            {
                killer.TextDestinationString = killed.Name;
                this.Plugins.GameRecordPlugin.AddBranch(killer, "PersonAssassinateSuccess", killed.Position);
            }
        }

        public override void PersonAssassinateSuccessKilled(Person killer, Person killed, Architecture a)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(killer.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(killed.BelongedFaction))
            {
                killer.TextDestinationString = killed.Name;
                this.Plugins.GameRecordPlugin.AddBranch(killer, "PersonAssassinateSuccessKilled", killed.Position);
            }
        }

        public override void PersonAssassinateSuccessCaptured(Person killer, Person killed, Architecture a)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(killer.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(killed.BelongedFaction))
            {
                killer.TextDestinationString = killed.Name;
                this.Plugins.GameRecordPlugin.AddBranch(killer, "PersonAssassinateSuccessCaptured", killed.Position);
            }
        }

        public override void PersonAssassinateFailed(Person killer, Person killed, Architecture a)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(killer.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(killed.BelongedFaction))
            {
                killer.TextDestinationString = killed.Name;
                this.Plugins.GameRecordPlugin.AddBranch(killer, "PersonAssassinateFailed", killed.Position);
            }
        }

        public override void PersonAssassinateFailedKilled(Person killer, Person killed, Architecture a)
        {
            if (Session.Current.Scenario.IsCurrentPlayer(killer.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(killed.BelongedFaction))
            {
                killer.TextDestinationString = killed.Name;
                this.Plugins.GameRecordPlugin.AddBranch(killer, "PersonAssassinateFailedKilled", killed.Position);
            }
        }

        public override void SelfCaptiveRelease(Captive captive)
        {
            if ((((Session.Current.Scenario.CurrentPlayer == null) || Session.Current.Scenario.IsCurrentPlayer(captive.BelongedFaction)) || Session.Current.Scenario.IsCurrentPlayer(captive.CaptiveFaction)) || Session.GlobalVariables.SkyEye)
            {
                this.Plugins.GameRecordPlugin.AddBranch(captive, "SelfCaptiveRelease", captive.BelongedFaction.Capital.Position);
            }
        }

        public override void TechniqueComplete(Faction f, Technique t)
        {
            f.TextResultString = t.Name;
            this.Plugins.GameRecordPlugin.AddBranch(f, "TechniqueComplete", f.Capital.Position);
        }

        public override void xiejinxingjilu(string shijian, string TextResultString, string TextDestinationString,Point point)
        {
            Person p = new Person();
            p.TextResultString = TextResultString;
            p.TextDestinationString = TextDestinationString;
            this.Plugins.GameRecordPlugin.AddBranch(p, shijian, point );
        }

        public override void AskWhenTransportArrived(Troop transport, Architecture destination)
        {
            if ((Session.Current.Scenario.CurrentPlayer == null) || (Session.Current.Scenario.IsCurrentPlayer(transport.BelongedFaction) &&
                transport.BelongedFaction == transport.StartingArchitecture.BelongedFaction && !transport.StartingArchitecture.BelongedSection.AIDetail.AutoRun))
            {
                transport.transportReturningTo = destination;
                this.Plugins.tupianwenziPlugin.SetConfirmationDialog(this.Plugins.ConfirmationDialogPlugin, new GameDelegates.VoidFunction(transport.TransportReturn), new GameDelegates.VoidFunction(transport.TransportEnter));
                this.Plugins.ConfirmationDialogPlugin.SetPosition(ShowPosition.Center);

                transport.TextDestinationString = destination.Name;
                transport.TextResultString = transport.StartingArchitecture.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(transport.Leader, transport, TextMessageKind.TransportReturn, "TransportReturn");
                this.Plugins.tupianwenziPlugin.IsShowing = true;
            }
        }

        public override void CreateBrother(Person p, Person q)
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                p.TextResultString = q.Name;
                q.TextResultString = q.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, q, TextMessageKind.CreateBrother, "CreateBrother", "CreateBrother.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "CreateBrother", p.Position);
            }
        }

        public override void CreateSister(Person p, Person q)
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                p.TextResultString = q.Name;
                q.TextResultString = q.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, q, TextMessageKind.CreateSister, "CreateSister", "CreateSister.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "CreateSister", p.Position);
            }
        }

        public override void CreateSpouse(Person p, Person q)
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                p.TextResultString = q.Name;
                q.TextResultString = q.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, q, TextMessageKind.CreateSpouse, "CreateSpouse", "CreateSpouse.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "CreateSpouse", p.Position);
            }
        }

        public override void MakeMarriage(Person p, Person q)
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)))
            {
                p.TextResultString = q.Name;
                p.TextDestinationString = p.BelongedFaction.Leader.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p.BelongedFaction.Leader, p, TextMessageKind.MakeMarriage, "MakeMarriage", "CreateSpouse.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "CreateSpouse", p.Position);
            }
        }


        public override void Selectprince(Person p, Person q)  //立储
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)))
            {
                p.TextResultString = q.Name;
                q.TextResultString = p.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, TextMessageKind.SelectPrince, "SelectPrince", "SelectPrince.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "SelectPrince", p.Position);
            }
        }

        public override void Appointmayor(Person p, Person q)  //太守
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction)) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction) && 
                p.BelongedArchitecture != null && p.BelongedArchitecture.BelongedSection != null &&
                !p.BelongedArchitecture.BelongedSection.AIDetail.AutoRun)
            {
                p.TextResultString = q.Name;
                q.TextResultString = p.Name;
                p.TextDestinationString = q.LocationArchitecture.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, TextMessageKind.AppointMayor, "AppointMayor", "AppointMayor.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "AppointMayor", p.Position);
            }
        }

        public override void Zhaoxian(Person p, Person q)
        {
            if ((Session.Current.Scenario.IsCurrentPlayer(p.BelongedFaction) && Session.Current.Scenario.IsCurrentPlayer(q.BelongedFaction)))
            {
                p.TextResultString = q.Name;
                q.TextResultString = p.Name;
                //p.TextDestinationString = q.LocationArchitecture.Name;
                this.Plugins.tupianwenziPlugin.SetGameObjectBranch(p, p, TextMessageKind.ZhaoXian, "ZhaoXian", "ZhaoXian.jpg", "");
                this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
                this.Plugins.tupianwenziPlugin.IsShowing = true;
                this.Plugins.GameRecordPlugin.AddBranch(p, "ZhaoXian", p.Position);
            }
        }


        public override void NoFactionPersonArrivesAtArchitecture(Person p, Architecture a)
        {
            if (a.BelongedFaction != null && Session.Current.Scenario.IsPlayer(a.BelongedFaction) && p.Status == PersonStatus.NoFaction)
            {
                p.TextDestinationString = a.Name;
                this.Plugins.GameRecordPlugin.AddBranch(p, "NoFactionPersonArrivesAtArchitecture", a.Position);
            }
        }

        public override void TransferMilitaryArrivesAtArchitecture(Military m, Architecture a)
        {
            //if (Session.Current.Scenario.IsCurrentPlayer(a.BelongedFaction) && m.ArrivingDays == 0)
            if (Session.Current.Scenario.IsCurrentPlayer(a.BelongedFaction) && m.ArrivingDays <= 0)
            {
                m.TextResultString = m.StartingArchitecture.Name;
                m.TextDestinationString = a.Name;
                this.Plugins.GameRecordPlugin.AddBranch(m, "TransferMilitaryArrivesAtArchitecture", a.Position);
            }
        }

        public override void OnOfficerInjured(Person p) {
            if (((Session.Current.Scenario.CurrentPlayer != null) && p.BelongedArchitecture != null &&
                   Session.Current.Scenario.IsCurrentPlayer(p.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                p.TextResultString = p.InjuryString;
                this.Plugins.GameRecordPlugin.AddBranch(p, "OfficerInjured", p.Position);
            }
        }

        public override void OnOfficerSick(Person p) {
            if (((Session.Current.Scenario.CurrentPlayer != null) && p.BelongedArchitecture != null &&
                  Session.Current.Scenario.IsCurrentPlayer(p.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                if (Person.GetInjuryString(p.InjureRate) != Person.GetInjuryString(p.OldInjureRate) && 
                    (p.OldInjureRate - p.InjureRate >= 0.1 || (p.OldInjureRate >= 1 && p.InjureRate < 1)))
                {
                    p.OldInjureRate = p.InjureRate;
                    p.TextResultString = p.InjuryString;
                    this.Plugins.GameRecordPlugin.AddBranch(p, "OfficerSick", p.Position);
                }
            }
        }

        public override void OnOfficerRecovered(Person p)
        {
            if (((Session.Current.Scenario.CurrentPlayer != null) && p.BelongedArchitecture != null &&
                Session.Current.Scenario.IsCurrentPlayer(p.BelongedArchitecture.BelongedFaction)) || Session.GlobalVariables.SkyEye)
            {
                p.TextResultString = p.InjuryString;
                this.Plugins.GameRecordPlugin.AddBranch(p, "OfficerRecovered", p.Position);
            }
        }

        public override void OnExecute(Person executor, Person executed)
        {

            Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.NeutralPerson, executor.Name, TextMessageKind.KillCaptive, "KillCaptive", "chuzhan.jpg", "chuzhan", executed.Name, true);

            executed.TextResultString = executor.Name;
            this.Plugins.GameRecordPlugin.AddBranch(executed, "OfficerExecute", executed.Position);
        }

        public override void OnTroopRout(Troop router, Troop routed)
        {
            if (routed != null && ((Session.Current.Scenario.CurrentPlayer != null &&
                ((router == null && Session.Current.Scenario.IsCurrentPlayer(routed.BelongedFaction)) ||
                (router != null && Session.Current.Scenario.IsCurrentPlayer(router.BelongedFaction) || Session.Current.Scenario.IsCurrentPlayer(routed.BelongedFaction)))) || Session.GlobalVariables.SkyEye))
            {
                if (router != null)
                {
                    routed.TextResultString = router.Name;
                    this.Plugins.GameRecordPlugin.AddBranch(routed, "TroopRout", routed.Position);
                }
                else
                {
                    this.Plugins.GameRecordPlugin.AddBranch(routed, "TroopRoutNoRouter", routed.Position);
                }
            }
        }

        public override void OnAIMergeAgainstPlayer(Faction strongestPlayer, Faction merger, Faction merged)
        {
            base.OnAIMergeAgainstPlayer(strongestPlayer, merger, merged);

            merger.TextResultString = merged.Name;
            merger.TextDestinationString = strongestPlayer.Name;

            this.Plugins.tupianwenziPlugin.SetPosition(ShowPosition.Bottom, Session.MainGame.mainGameScreen);
            this.Plugins.tupianwenziPlugin.SetGameObjectBranch(merger.Leader, merger, TextMessageKind.AIMergeAgainstPlayer, "AIMergeAgainstPlayer", "AIMergeAgainstPlayer.jpg", "");
            this.Plugins.tupianwenziPlugin.IsShowing = true;
        }
    }
}
