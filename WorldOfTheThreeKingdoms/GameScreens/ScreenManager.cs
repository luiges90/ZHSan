using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using PluginInterface;
using GameObjects.ArchitectureDetail;
using GameObjects.FactionDetail;
using GameObjects.TroopDetail;
using GameObjects.SectionDetail;
using GameObjects.PersonDetail;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens

{
    public class ScreenManager
    {
        public Troop CreatingTroop;
        public Architecture CurrentArchitecture;
        public ArchitectureWorkKind CurrentArchitectureWorkKind;
        public Faction CurrentFaction;
        public GameObject CurrentGameObject;
        public GameObjectList CurrentGameObjects;
        public Military CurrentMilitary;
        public int CurrentNumber;
        public int Currentzijin;
        public Person CurrentPerson;
        public GameObjectList CurrentPersons;
        public GameObjectList CurrentMilitaries; 
        public Routeway CurrentRouteway;
        public Troop CurrentTroop;
        public DiplomaticRelationDisplay CurrentDiplomaticRelationDisplay;
        
        private FrameFunction lastFrameFunction;

        public ScreenManager()
        {
        }

        private void FrameFunction_Architecture_AfterGetAwardTreasure() // 赏赐宝物
        {
            this.CurrentGameObject = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as GameObject;            
            if (this.CurrentGameObject != null)
            {
                Treasure currentGameObject = this.CurrentGameObject as Treasure;
                if (currentGameObject.BelongedPerson != null)
                {
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetAwardTreasurePerson, false, true, true, false, this.CurrentArchitecture.BelongedFaction.PersonsInArchitecturesExceptLeader, null, "", "");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetAwardTreasurePerson() // 赏赐宝物
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                Treasure currentGameObject = this.CurrentGameObject as Treasure;
                if (currentGameObject.BelongedPerson != null)
                {
                    this.CurrentArchitecture.BelongedFaction.Leader.LoseTreasure(currentGameObject);
                    this.CurrentPerson.AwardedTreasure(currentGameObject);
                }
            }
        }

        private void FrameFunction_Architecture_Afterxuanzemeinv() // 纳妃
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                Person tookSpouse = Session.Current.Scenario.CurrentFaction.Leader.XuanZeMeiNv(this.CurrentPerson);

                String msgKey;
                if (this.CurrentPerson.Hates(Session.Current.Scenario.CurrentFaction.Leader))
                {
                    msgKey = "nafeiHate";
                }
                else
                {
                    msgKey = "nafei";
                }
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.CurrentPerson, (Session.Current.Scenario.CurrentFaction.Leader).Name, TextMessageKind.TakePrincess, msgKey, "nafei.jpg", "nafei", true);

                if (tookSpouse != null)
                {
                    Session.MainGame.mainGameScreen.PersonBeiDuoqi(tookSpouse, this.CurrentArchitecture.BelongedFaction);
                }
            }
        }

        private void FrameFunction_Architecture_chongxingmeinv() // 宠幸
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                Session.Current.Scenario.CurrentFaction.Leader.GoForHouGong(this.CurrentPerson);
                String msgKey;
                if (this.CurrentPerson.Hates(Session.Current.Scenario.CurrentFaction.Leader))
                {
                    msgKey = "chongxingHate";
                }
                else
                {
                    msgKey = "chongxing";
                }
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.CurrentPerson, Session.Current.Scenario.CurrentFaction.Leader.Name, TextMessageKind.Hougong, msgKey, this.CurrentPerson.ID.ToString(), "hougong", true);
                //this.mainGameScreen.DateGo(1);
            }
        }

        private void FrameFunction_Architecture_AfterGetBeDisbandedMilitaries() // 解散编队
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Militaries.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Military military in this.CurrentGameObjects)
                {
                    this.CurrentArchitecture.DisbandMilitary(military);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetBeMergedMilitaries() // 合并
        {
            this.CurrentGameObjects = this.CurrentArchitecture.BeMergedMilitaryList.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Military military in this.CurrentGameObjects)
                {
                    int increment = (military.Quantity + this.CurrentMilitary.Quantity) - this.CurrentMilitary.Kind.MaxScale;
                    if (increment > 0)
                    {
                        this.CurrentMilitary.BelongedArchitecture.IncreasePopulation(increment);
                    }
                    if (military.LeaderID == this.CurrentMilitary.LeaderID)
                    {
                        this.CurrentMilitary.IncreaseQuantity(military.Quantity, military.Morale, military.Combativity, military.Experience, military.LeaderExperience);
                    }
                    else
                    {
                        this.CurrentMilitary.IncreaseQuantity(military.Quantity, military.Morale, military.Combativity, military.Experience, 0);
                    }
                }
                foreach (Military military in this.CurrentGameObjects)
                {
                    this.CurrentArchitecture.RemoveMilitary(military);
                    this.CurrentArchitecture.BelongedFaction.RemoveMilitary(military);
                    Session.Current.Scenario.Militaries.Remove(military);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetConfiscateTreasure() // 没收
        {
            this.CurrentGameObject = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as GameObject;
            if (this.CurrentGameObject != null)
            {
                Treasure currentGameObject = this.CurrentGameObject as Treasure;
                if (currentGameObject.BelongedPerson != null)
                {
                    currentGameObject.BelongedPerson.ConfiscatedTreasure(currentGameObject);
                    this.CurrentArchitecture.BelongedFaction.Leader.ReceiveTreasure(currentGameObject);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetConvinceDestinationPerson() // 说服
        {
            this.CurrentGameObjects = this.CurrentArchitecture.ConvinceDestinationPersonList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                foreach (Person person in this.CurrentPersons)
                {
                    person.GoForConvince(this.CurrentGameObjects[0] as Person);
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetConvinceSourcePerson() // 说服
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Persons.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.ConvincePersonPosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetDestroyPerson() // 破坏
        {
            this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons .GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.DestroyPosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetFacilityToBuild() // 建设设施
        {
            this.CurrentGameObjects = this.CurrentArchitecture.BuildableFacilityKindList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                FacilityKind facilityKind = this.CurrentGameObjects[0] as FacilityKind;
                this.CurrentArchitecture.BeginToBuildAFacility(facilityKind);
            }
        }

        private void FrameFunction_Architecture_AfterGetFacilityToDemolish() // 拆除设施
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Facilities.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Facility facility in this.CurrentGameObjects)
                {
                    this.CurrentArchitecture.DemolishFacility(facility);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetFriendlyDiplomaticRelation()
        {
            GameObjectList selectedList = this.CurrentArchitecture.ResetDiplomaticRelationList.GetSelectedList();
            if (selectedList != null)
            {
                foreach (DiplomaticRelationDisplay display in selectedList)
                {
                    Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.NeutralPerson, this.CurrentArchitecture.BelongedFaction.Leader.Name, "ResetDiplomaticRelation", "ResetDiplomaticRelation.jpg", "ResetDiplomaticRelation", display.FactionName, true);
                    this.CurrentArchitecture.BelongedFaction.Leader.DecreaseKarma(5);
                    display.Relation = 0;
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetEnhanceDiplomaticRelation()
        {
            GameObjectList selectedList = this.CurrentArchitecture.EnhanceDiplomaticRelationList.GetSelectedList();

            if (selectedList != null && (selectedList.Count == 1))
            {
                this.CurrentDiplomaticRelationDisplay = selectedList[0] as DiplomaticRelationDisplay;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetEnhanceDiplomaticRelationPerson, true, true, true, true, this.CurrentArchitecture.Persons, null, "外交人员", "Ability");
            }
        }

        private void FrameFunction_Architecture_AfterGetTruceDiplomaticRelation()
        {
            GameObjectList selectedList = this.CurrentArchitecture.TruceDiplomaticRelationList.GetSelectedList();

            if (selectedList != null && (selectedList.Count == 1))
            {
                this.CurrentDiplomaticRelationDisplay = selectedList[0] as DiplomaticRelationDisplay;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetTruceDiplomaticRelationPerson, true, true, true, true, this.CurrentArchitecture.Persons, null, "外交人员", "Ability");
            }
        }

        private void FrameFunction_Architecture_AfterGetQuanXiangDiplomaticRelation() //劝降
        {
            GameObjectList selectedList = this.CurrentArchitecture.QuanXiangDiplomaticRelationList.GetSelectedList();

            if (selectedList != null && (selectedList.Count == 1))
            {
                this.CurrentDiplomaticRelationDisplay = selectedList[0] as DiplomaticRelationDisplay;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetQuanXiangDiplomaticRelationPerson, false, true, true, false, this.CurrentArchitecture.MovablePersons, null, "外交人员", "Ability");
            }
        }

        private void FrameFunction_Architecture_AfterGetQuanXiangDiplomaticRelationPerson() //劝降
        {
            GameObjectList selectedList = this.CurrentArchitecture.DiplomaticWorkingPersons.GetSelectedList();

            if (selectedList != null && (selectedList.Count == 1))
            {

                Person diplomaticperson = selectedList[0] as Person;
                if (this.CurrentArchitecture.Fund >= 50000)
                {
                    this.CurrentArchitecture.Fund -= 50000;
                    diplomaticperson.GoToQuanXiangDiplomatic(this.CurrentDiplomaticRelationDisplay);
                }    
                
            }
        }

        private void FrameFunction_Architecture_AfterGetAllyDiplomaticRelation()
        {
            GameObjectList selectedList = this.CurrentArchitecture.AllyDiplomaticRelationList.GetSelectedList();

            if (selectedList != null && (selectedList.Count == 1))
            {
                this.CurrentDiplomaticRelationDisplay = selectedList[0] as DiplomaticRelationDisplay;
                //this.CurrentDiplomaticRelationDisplay.Relation = 301;
                //this.mainGameScreen.xianshishijiantupian(this.CurrentArchitecture.BelongedFaction.Leader, this.CurrentArchitecture.BelongedFaction.Leader.Name, "AllyDiplomaticRelation", "AllyDiplomaticRelation.jpg", "AllyDiplomaticRelation", this.CurrentDiplomaticRelationDisplay.FactionName, true);
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.GetAllyDiplomaticRelationPerson, true, true, true, true, this.CurrentArchitecture.Persons, null, "外交人员", "Ability");
            }
        }

        private void FrameFunction_Architecture_AfterGetEnhanceDiplomaticRelationPerson() //亲善
        {
            GameObjectList selectedList = this.CurrentArchitecture.DiplomaticWorkingPersons.GetSelectedList();

            if (selectedList != null)
            {
                foreach (Person diplomaticperson in selectedList)
                {
                    if (this.CurrentArchitecture.Fund >= 10000)
                    {
                        this.CurrentArchitecture.Fund -= 10000;
                        diplomaticperson.GoToDiplomatic(this.CurrentDiplomaticRelationDisplay);
                    }
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetTruceDiplomaticRelationPerson()
        {
            GameObjectList selectedList = this.CurrentArchitecture.DiplomaticWorkingPersons.GetSelectedList();

            if (selectedList != null)
            {
                foreach (Person diplomaticperson in selectedList)
                {
                    if (this.CurrentArchitecture.Fund >= 50000)
                    {
                        this.CurrentArchitecture.Fund -= 50000;
                        diplomaticperson.GoToTruceDiplomatic(this.CurrentDiplomaticRelationDisplay);
                    }
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetAllyDiplomaticRelationPerson()
        {
            GameObjectList selectedList = this.CurrentArchitecture.DiplomaticWorkingPersons.GetSelectedList();

            if (selectedList != null)
            {
                foreach (Person diplomaticperson in selectedList)
                {
                    if (this.CurrentArchitecture.Fund >= 20000)
                    {
                        this.CurrentArchitecture.Fund -= 20000;
                        diplomaticperson.GoToAllyDiplomatic(this.CurrentDiplomaticRelationDisplay);
                    }
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetDenounceDiplomaticRelation()
        {
            GameObjectList selectedList = this.CurrentArchitecture.DenounceDiplomaticRelationList.GetSelectedList();

            if (selectedList != null)
            {
                foreach (DiplomaticRelationDisplay display in selectedList)
                {
                    if (this.CurrentArchitecture.Fund >= 120000)
                    {
                        Faction toEncircle = display.LinkedFaction1 == this.CurrentArchitecture.BelongedFaction ? display.LinkedFaction2 : display.LinkedFaction1;
                        this.CurrentArchitecture.BelongedFaction.Encircle(this.CurrentArchitecture, toEncircle);
                    }
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetGossipPerson()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons .GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.GossipPosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetJailBreakPerson()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons .GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.JailBreakPosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetAssassinatePerson()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Persons.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.AssassinatePosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetAssassinatePersonTarget()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.AssassinatablePersons((this.CurrentPersons[0] as Person).BelongedFaction).GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                foreach (Person person in this.CurrentPersons)
                {
                    person.GoForAssassinate(this.CurrentGameObjects[0] as Person);
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetInformationToStop()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Informations.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Information i in this.CurrentGameObjects)
                {
                    i.Purify();
                    this.CurrentArchitecture.RemoveInformation(i);
                    Session.Current.Scenario.Informations.Remove(i);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetOfficerType()
        {

            this.CurrentGameObjects = this.CurrentArchitecture.AvailGeneratorTypeList().GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {

                //this.CurrentGameObject = Session.Current.Scenario.GameCommonData.PlayerGeneratorTypes.GetSelectedList()[0] as PersonGeneratorType;
                PersonGeneratorType preferredType = this.CurrentArchitecture.AvailGeneratorTypeList().GetSelectedList()[0] as PersonGeneratorType;
                this.CurrentArchitecture.DoZhaoXian(preferredType);
                //this.CurrentArchitecture.DecreaseFund(preferredType.CostFund);
            }
        }

        private void FrameFunction_Architecture_AfterGetInformationKind()
        {
            Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetInformationPerson, false, true, true, false, this.CurrentArchitecture.MovablePersons, null, "情报", "情报");
        }

        private void FrameFunction_Architecture_AfterGetInformationPerson() // 情报
        {
            this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons .GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                this.CurrentPerson = this.CurrentGameObjects[0] as Person;
                this.CurrentPerson.CurrentInformationKind = Session.Current.Scenario.GameCommonData.AllInformationKinds.GetSelectedList()[0] as InformationKind;
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.InformationPosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetInstigatePerson()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons .GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.InstigatePosition));
            }
        }

        private void FrameFunction_Architecture_AfterGetLevelUpMilitaryKind()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.UpgradableMilitaryKindList.GetSelectedList();
                if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
                {
                    this.CurrentArchitecture.LevelUpMilitary(this.CurrentMilitary, this.CurrentGameObjects[0] as MilitaryKind);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetLevelUpMilitaries()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.LevelUpMilitaryList.GetSelectedList();
                if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
                {
                    this.CurrentMilitary = (Military) this.CurrentGameObjects[0];
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.MilitaryKind, FrameFunction.GetLevelUpMiliaryKind, true, true, true, false, this.CurrentArchitecture.GetUpgradableMilitaryKindList(this.CurrentMilitary), null, "编队升级", "编队升级");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetMergeMilitary()
        {
            GameObjectList selectedList = this.CurrentArchitecture.MergeMilitaryList.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                this.CurrentMilitary = selectedList[0] as Military;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Military, FrameFunction.GetBeMergedMilitaries, false, true, true, false, this.CurrentArchitecture.GetBeMergedMilitaryList(this.CurrentMilitary), null, "选择编队", "");
            }
        }

        private void FrameFunction_Architecture_AfterSelectMarryablePerson()
        {
            GameObjectList selectedList = this.CurrentArchitecture.Persons.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                this.CurrentPerson = selectedList[0] as Person;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Person, FrameFunction.SelectMarryTo, false, true, true, false, this.CurrentPerson.MakeMarryable(), null, "选择对象", "");
            }
        }

        private void FrameFunction_Architecture_AfterSelectMarryTo()
        {
            GameObjectList selectedList = this.CurrentArchitecture.Persons.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 2))
            {
                if (this.CurrentPerson == selectedList[0])
                {
                    this.CurrentPerson.Marry(selectedList[1] as Person, this.CurrentArchitecture.BelongedFaction.Leader);
                }
                else
                {
                    this.CurrentPerson.Marry(selectedList[0] as Person, this.CurrentArchitecture.BelongedFaction.Leader);
                }
            }
            this.CurrentArchitecture.Persons.ClearSelected();
        }

        private void FrameFunction_Architecture_AfterSelectTrainableChildren()
        {
            GameObjectList selectedList = this.CurrentArchitecture.BelongedFaction.Children.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count >= 1))
            {
                this.CurrentPersons = selectedList;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.TrainPolicy, FrameFunction.SelectTrainPolicy, false, true, true, false, (this.CurrentPersons[0] as Person).TrainPolicies(), null, "选择培育方针", "");
            }
            this.CurrentArchitecture.Persons.ClearSelected();
        }

        private void FrameFunction_Architecture_AfterSelectTrainPolicy()
        {
            GameObjectList selectedList = Session.Current.Scenario.GameCommonData.AllTrainPolicies.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                foreach (Person p in this.CurrentPersons)
                {
                    p.TrainPolicy = (TrainPolicy)selectedList[0];
                }
            }
            this.CurrentArchitecture.Persons.ClearSelected();
        }

        private void FrameFunction_Architecture_AfterGetNewCapital()
        {
            GameObjectList selectedList = this.CurrentArchitecture.ChangeCapitalArchitectureList.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                this.CurrentArchitecture.DecreaseFund(this.CurrentArchitecture.ChangeCapitalCost);
                this.CurrentArchitecture.BelongedFaction.ChangeCapital(selectedList[0] as Architecture);
            }
        }

        private void FrameFunction_Architecture_AfterGetNewMilitaryKind()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.NewMilitaryKindList.GetSelectedList();
                if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
                {
                    this.CurrentArchitecture.CreateMilitary(this.CurrentGameObjects[0] as MilitaryKind);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetOneArchitecture()
        {
            if (this.CurrentArchitecture != null)
            {
                GameObjectList selectedList = this.CurrentArchitecture.TransferArchitectureList.GetSelectedList();
                if ((selectedList != null) && (selectedList.Count == 1))
                {
                    foreach (Person person in this.CurrentGameObjects)
                    {
                        person.MoveToArchitecture(selectedList[0] as Architecture);
                        //this.CurrentArchitecture.RemovePerson(person);
                    }
                }
            }
        }

        public void FrameFunction_Architecture_AfterGetMoveCaptiveArchitectureBySelecting(Architecture architecture) //移动俘虏
        {
            if (architecture != null && this.CurrentPersons.Count > 0)
            {
                foreach (Captive captive in this.CurrentPersons)
                {
                    captive.CaptivePerson.MoveToArchitecture(architecture);
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        public void FrameFunction_Architecture_AfterGetTransferMilitaryArchitectureBySelecting() //运输编队
        {
            if (this.CurrentArchitecture != null && this.CurrentMilitaries.Count > 0)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.BelongedFaction.ArchitecturesExcluding(this.CurrentArchitecture).GetSelectedList();
                if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
                {
                    Architecture targetArchitecture = this.CurrentGameObjects[0] as Architecture;

                    double distance = (double)Session.Current.Scenario.GetDistance(this.CurrentArchitecture.ArchitectureArea, targetArchitecture.ArchitectureArea);

                    foreach (Military military in this.CurrentMilitaries)
                    {
                        if (this.CurrentArchitecture.Fund >= military.TransferFundCost(distance) && 
                            this.CurrentArchitecture.Food >= military.TransferFoodCost(distance) &&
                            !this.CurrentArchitecture.IsSurrounded() && !targetArchitecture.IsSurrounded())
                        {
                            this.CurrentArchitecture.DecreaseFund(military.TransferFundCost(distance));
                            this.CurrentArchitecture.DecreaseFood(military.TransferFoodCost(distance));
                            military.StartingArchitecture = this.CurrentArchitecture;
                            military.TargetArchitecture = targetArchitecture;
                            //military.ArrivingDays = Math.Max(1, military.TransferDays(distance));
                            military.ArrivingDays = Math.Max(1, military.TransferDays(distance));
                            this.CurrentArchitecture.RemoveMilitary(military);
                            this.CurrentArchitecture.BelongedFaction.TransferingMilitaries.Add(military);
                            this.CurrentArchitecture.BelongedFaction.TransferingMilitaryCount++;
                        }
                    }
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        public void FrameFunction_Architecture_AfterGetOneArchitectureBySelecting(Architecture architecture)
        {
            if (architecture != null && this.CurrentPersons.Count>0)
            {
                foreach (Person person in this.CurrentPersons)
                {
                    person.MoveToArchitecture(architecture);                    
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }            
        }

        private void FrameFunction_Architecture_AfterGetRecruitmentMilitary() // 补充
        {
            GameObjectList selectedList = this.CurrentArchitecture.RecruitmentMilitaryList.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                this.CurrentMilitary = selectedList[0] as Military;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetRecruitmentPerson, false, true, true, false, this.CurrentArchitecture.Persons, null, "补充", "补充");
            }
        }

        private void FrameFunction_Monarch_KillRelease_MoveCaptive() //俘虏可移动
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.Captives.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    //this.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.GetOneArchitecture, false, true, true, false, this.CurrentArchitecture.GetTransferArchitectureList(), null, "目标", "");
                    //this.mainGameScreen.ShowMapViewSelector(false , this.CurrentArchitecture.GetTransferArchitectureList());
                    this.CurrentPersons = this.CurrentGameObjects.GetList();
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.MoveCaptive));

                }
            }
        }

        private void FrameFunction_Architecture_AfterGetAutoCampaignMilitaries() //自动出征
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.GetCampaignMilitaryList().GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    this.CurrentMilitaries = this.CurrentGameObjects.GetList();
                    this.CurrentMilitary= this.CurrentMilitaries[0] as Military;
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.ArchitectureAvailableContactArea));
                }
            }
        }


        private void FrameFunction_Architecture_AfterGetTransferMilitary() //运输编队
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.movableMilitaries.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {

                    //this.CurrentArchitecture.RemoveMilitary(m);
                    this.CurrentMilitaries = this.CurrentGameObjects.GetList();
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.GetTransferArchitecture, false, true, true, false, this.CurrentArchitecture.BelongedFaction .ArchitecturesExcluding(this.CurrentArchitecture), null, "运兵", "运兵");
                    //this.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.MilitaryTransfer));



                }
            }
        }
                 

        private void FrameFunction_Architecture_AfterGetRecruitmentPerson() // 补充
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.Persons.GetSelectedList();
                if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
                {
                    this.CurrentPerson = this.CurrentGameObjects[0] as Person;
                    this.CurrentPerson.RecruitMilitary(this.CurrentMilitary);
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetRedeemCaptive() // 赎回俘虏
        {
            this.CurrentGameObjects = this.CurrentArchitecture.RedeemCaptiveList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                (this.CurrentGameObjects[0] as Captive).SendRansom((this.CurrentGameObjects[0] as Captive).BelongedFaction.Capital, this.CurrentArchitecture);
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetReleaseCaptive() // 释放俘虏
        {
            this.CurrentGameObjects = this.CurrentArchitecture.BelongedFaction.Captives.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Captive captive in this.CurrentGameObjects)
                {
                    captive.SelfReleaseCaptive();
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetRewardPerson() // 奖赏
        {
            this.CurrentGameObjects = this.CurrentArchitecture.RewardPersonList.GetSelectedList();
        }

        private void FrameFunction_Architecture_AfterGetSearchPerson() // 搜索
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Persons.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Person person in this.CurrentGameObjects)
                {
                    person.shoudongjinxingsousuo();
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetSectionToDemolish()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.BelongedFaction.Sections.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                foreach (Section section in this.CurrentGameObjects)
                {
                    this.CurrentArchitecture.BelongedFaction.RemoveSection(section);
                    Session.Current.Scenario.Sections.Remove(section);
                    Section anotherSection = this.CurrentArchitecture.BelongedFaction.GetAnotherSection(section);
                    if (anotherSection != null)
                    {
                        foreach (Architecture architecture in section.Architectures)
                        {
                            anotherSection.AddArchitecture(architecture);
                        }
                    }
                }
                foreach (Section section in this.CurrentArchitecture.BelongedFaction.Sections.GetList())
                {
                    if ((section.OrientationSection != null) && !this.CurrentArchitecture.BelongedFaction.Sections.HasGameObject(section.OrientationSection))
                    {
                        foreach (SectionAIDetail detail in Session.Current.Scenario.GameCommonData.AllSectionAIDetails.SectionAIDetails.Values)
                        {
                            if (detail.OrientationKind == SectionOrientationKind.无)
                            {
                                section.AIDetail = detail;
                                break;
                            }
                        }
                        section.OrientationSection = null;
                    }
                    section.RefreshSectionName();
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetShortestNoWaterRouteway()
        {
            this.CurrentGameObjects = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItemList as GameObjectList;
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count > 0))
            {
                foreach (Architecture architecture in this.CurrentGameObjects)
                {
                    Routeway routeway = this.CurrentArchitecture.BuildShortestRouteway(architecture, true);
                    if (routeway != null)
                    {
                        routeway.Building = true;
                    }
                }
                Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
            }
        }

        private void FrameFunction_Architecture_AfterGetShortestRouteway()   //粮道最短
        {
            this.CurrentGameObjects = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItemList as GameObjectList;
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count > 0))
            {
                foreach (Architecture architecture in this.CurrentGameObjects)
                {
                    Routeway routeway = this.CurrentArchitecture.BuildShortestRouteway(architecture, false);
                    if (routeway != null)
                    {
                        routeway.Building = true;
                    }
                }
                Session.GlobalVariables.CurrentMapLayer = MapLayerKind.Routeway;
            }
        }

        /*
        private void FrameFunction_Architecture_AfterGetSpyPerson()
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Persons.GetSelectedList();
            if (this.CurrentGameObjects != null)
            {
                this.CurrentPersons = this.CurrentGameObjects.GetList();
                this.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.SpyPosition));
            }
        }
        */

        private void FrameFunction_Architecture_AfterGetStudySkillPerson() // 修习技能
        {
            this.CurrentGameObjects = this.CurrentArchitecture.PersonStudySkillList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count > 0))
            {
                foreach (Person person in this.CurrentGameObjects)
                {
                    person.GoForStudySkill();
                    person.ManualStudy = true;
                }
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetStudyStunt() // 修习特技
        {
            this.CurrentGameObjects = this.CurrentPerson.StudyStuntList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                this.CurrentPerson.GoForStudyStunt(this.CurrentGameObjects[0] as Stunt);
                this.CurrentPerson.ManualStudy = true;
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetStudyStuntPerson() // 修习特技
        {
            this.CurrentGameObjects = this.CurrentArchitecture.PersonStudyStuntList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                Person person = this.CurrentGameObjects[0] as Person;
                if (person != null)
                {
                    this.CurrentPerson = person;
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Stunt, FrameFunction.GetStudyStunt, false, true, true, false, person.GetStudyStuntList(), null, "研习", "");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetStudyTitle() // 修习称号
        {
            this.CurrentGameObjects = this.CurrentPerson.StudyTitleList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                this.CurrentPerson.GoForStudyTitle(this.CurrentGameObjects[0] as Title);
                this.CurrentPerson.ManualStudy = true;
                Session.MainGame.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetStudyTitlePerson() // 修习称号
        {
            this.CurrentGameObjects = this.CurrentArchitecture.PersonStudyTitleList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                Person person = this.CurrentGameObjects[0] as Person;
                if (person != null)
                {
                    this.CurrentPerson = person;
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Title, FrameFunction.GetStudyTitle, false, true, true, false, person.GetStudyTitleList(), null, "研习", "");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetAppointableTitle() // 任命官职
        {
            this.CurrentGameObjects = this.CurrentPerson.AppointableTitleList.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                this.CurrentPerson.AwardTitle(this.CurrentGameObjects[0] as Title);
                
               // this.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetAppointPerson() // 任命官职
        {
            this.CurrentGameObjects = this.CurrentArchitecture.Kerenmingdeguanyuan.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                Person person = this.CurrentGameObjects[0] as Person;
                if (person != null)
                {
                    this.CurrentPerson = person;
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Title, FrameFunction.GetAppointableTitle, false, true, true, false, person.GetAppointableTitleList(), null, "任命官职", "");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetRecallablePerson() // 免除职位
        {
            this.CurrentGameObjects = this.CurrentArchitecture.RecallableOfficer.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                Person person = this.CurrentGameObjects[0] as Person;
                if (person != null)
                {
                    this.CurrentPerson = person;
                    Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Title, FrameFunction.GetRecallableTitle, false, true, true, true, person.RecallableTitleList(), null, "免除职位", "");
                }
            }
        }

        private void FrameFunction_Architecture_AfterGetRecallableTitle() // 免除职位
        {
            this.CurrentGameObjects = this.CurrentPerson.RecallableTitleList().GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count > 0))
            {
                foreach (Title t in this.CurrentGameObjects)
                {
                    this.CurrentPerson.RemoveTitle(t);
                }


                // this.mainGameScreen.PlayNormalSound("Content/Sound/Tactics/Outside");
            }
        }

        private void FrameFunction_Architecture_AfterGetTrainingMilitary()  //修改后未用
        {
            GameObjectList selectedList = this.CurrentArchitecture.TrainingMilitaryList.GetSelectedList();
            if ((selectedList != null) && (selectedList.Count == 1))
            {
                this.CurrentMilitary = selectedList[0] as Military;
                Session.MainGame.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Work, FrameFunction.GetTrainingPerson, false, true, true, false, this.CurrentArchitecture.Persons, null, "训练", "训练");
            }
        }

        private void FrameFunction_Architecture_PersonConvene() // 召唤
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.PersonConveneList.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    foreach (Person person in this.CurrentGameObjects)
                    {
                        person.MoveToArchitecture(this.CurrentArchitecture);
                    }
                }
            }
        }

        private void FrameFunction_Architecture_PersonTransfer()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.MovablePersons.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    //this.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.GetOneArchitecture, false, true, true, false, this.CurrentArchitecture.GetTransferArchitectureList(), null, "目标", "");
                    //this.mainGameScreen.ShowMapViewSelector(false , this.CurrentArchitecture.GetTransferArchitectureList());
                    this.CurrentPersons = this.CurrentGameObjects.GetList();
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.WujiangDiaodong));

                }
            }
        }

        private void FrameFunction_Monarch_hougongTop_moveFeizi()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.Feiziliebiao.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    //this.mainGameScreen.ShowTabListInFrame(UndoneWorkKind.Frame, FrameKind.Architecture, FrameFunction.GetOneArchitecture, false, true, true, false, this.CurrentArchitecture.GetTransferArchitectureList(), null, "目标", "");
                    //this.mainGameScreen.ShowMapViewSelector(false , this.CurrentArchitecture.GetTransferArchitectureList());
                    this.CurrentPersons = this.CurrentGameObjects.GetList();
                    Session.MainGame.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.MoveFeizi));

                }
            }
        }

        private void FrameFunction_Monarch_hougongTop_releaseFeizi()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.Feiziliebiao.GetSelectedList();
                if (this.CurrentGameObjects != null)
                {
                    foreach (GameObject o in this.CurrentGameObjects)
                    {
                        ((Person)o).feiziRelease();
                    }

                }
            }
        }

        /*
        private void FrameFunction_Monarch_ZhaoXianBang_DengYong() //强制登用武将
        {
            this.CurrentPerson = this.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            {
                if (this.CurrentPerson != null)
                {
                    if (this.CurrentArchitecture.Fund > this.CurrentPerson.UntiredMerit)
                    {
                        this.CurrentArchitecture.DecreaseFund(CurrentPerson.UntiredMerit);
                        this.CurrentPerson.Status = PersonStatus.Normal;
                        if (this.CurrentPerson.Loyalty < 110)
                        {
                            this.CurrentPerson.Loyalty = 110;
                        }
                        this.CurrentArchitecture.DengYong(CurrentPerson);
                    }


                    
                }
                
                this.mainGameScreen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.Selecting, SelectingUndoneWorkKind.DengYongWujiang));
            }
        }
        */

        private void FrameFunction_Architecture_WorkingList()
        {
            if (this.CurrentArchitecture != null)
            {
                if (this.CurrentArchitectureWorkKind != ArchitectureWorkKind.无)
                {
                    foreach (Person person in this.CurrentArchitecture.Persons)
                    {
                        if (person.Selected)
                        {
                            person.WorkKind = this.CurrentArchitectureWorkKind;
                        }
                        else if (person.WorkKind == this.CurrentArchitectureWorkKind)
                        {
                            person.WorkKind = ArchitectureWorkKind.无;
                        }
                    }
                    Person extremePersonFromWorkingList = this.CurrentArchitecture.GetExtremePersonFromWorkingList(this.CurrentArchitectureWorkKind, true);
                    if (extremePersonFromWorkingList != null)
                    {
                        Session.MainGame.mainGameScreen.Plugins.PersonBubblePlugin.AddPerson(extremePersonFromWorkingList, this.CurrentArchitecture.Position, TextMessageKind.StartWork, "Work");
                    }
                }
                else
                {
                    foreach (Person person in this.CurrentArchitecture.Persons)
                    {
                        if (person.Selected)
                        {
                            person.WorkKind = ArchitectureWorkKind.无;
                            person.OldWorkKind = ArchitectureWorkKind.无;
                        }
                    }
                }
            }
        }

        private void FrameFunction_Troop_AfterGetAttackDefaultKind()
        {
            this.CurrentGameObjects = Session.Current.Scenario.GameCommonData.AllAttackDefaultKinds.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                (this.CurrentGameObjects[0] as AttackDefaultKind).Apply(this.CurrentTroop);
            }
        }

        private void FrameFunction_Troop_AfterGetAttackTargetKind()
        {
            this.CurrentGameObjects = Session.Current.Scenario.GameCommonData.AllAttackTargetKinds.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                (this.CurrentGameObjects[0] as AttackTargetKind).Apply(this.CurrentTroop);
            }
        }

        private void FrameFunction_Troop_AfterGetCastDefaultKind()
        {
            this.CurrentGameObjects = Session.Current.Scenario.GameCommonData.AllCastDefaultKinds.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                (this.CurrentGameObjects[0] as CastDefaultKind).Apply(this.CurrentTroop);
            }
        }

        private void FrameFunction_Troop_AfterGetCastTargetKind()
        {
            this.CurrentGameObjects = Session.Current.Scenario.GameCommonData.AllCastTargetKinds.GetSelectedList();
            if ((this.CurrentGameObjects != null) && (this.CurrentGameObjects.Count == 1))
            {
                (this.CurrentGameObjects[0] as CastTargetKind).Apply(this.CurrentTroop);
            }
        }

        public void HandleFrameFunction(FrameFunction function)
        {
            switch (function)
            {
                case FrameFunction.GetOneArchitecture:
                    this.FrameFunction_Architecture_AfterGetOneArchitecture();
                    break;

                case FrameFunction.Architecture_WorkingList:
                    this.FrameFunction_Architecture_WorkingList();
                    break;

                case FrameFunction.PersonTransfer:
                    this.FrameFunction_Architecture_PersonTransfer();
                    break;

                case FrameFunction.GetAutoCampaignMilitaries://自动出征
                    this.FrameFunction_Architecture_AfterGetAutoCampaignMilitaries();
                    break;

                case FrameFunction.GetTransferMilitary://运输编队
                    this.FrameFunction_Architecture_AfterGetTransferMilitary();
                    break;

                case FrameFunction.GetTransferArchitecture:
                    this.FrameFunction_Architecture_AfterGetTransferMilitaryArchitectureBySelecting();
                    break;

                case FrameFunction.PersonConvene:
                    this.FrameFunction_Architecture_PersonConvene();
                    break;

                case FrameFunction.GetConvinceSourcePerson:
                    this.FrameFunction_Architecture_AfterGetConvinceSourcePerson();
                    break;

                case FrameFunction.GetConvinceDestinationPerson:
                    this.FrameFunction_Architecture_AfterGetConvinceDestinationPerson();
                    break;

                case FrameFunction.GetRewardPerson:
                    this.FrameFunction_Architecture_AfterGetRewardPerson();
                    break;

                case FrameFunction.GetRedeemCaptive:
                    this.FrameFunction_Architecture_AfterGetRedeemCaptive();
                    break;

                case FrameFunction.GetReleaseCaptive:
                    this.FrameFunction_Architecture_AfterGetReleaseCaptive();
                    break;

                case FrameFunction.GetStudySkillPerson:
                    this.FrameFunction_Architecture_AfterGetStudySkillPerson();
                    break;

                case FrameFunction.GetStudyTitlePerson:
                    this.FrameFunction_Architecture_AfterGetStudyTitlePerson();
                    break;

                case FrameFunction.GetStudyTitle:
                    this.FrameFunction_Architecture_AfterGetStudyTitle();
                    break;

                case FrameFunction.GetAppointPerson://封官
                    this.FrameFunction_Architecture_AfterGetAppointPerson();
                    break;

                case FrameFunction.GetAppointableTitle: //封官
                    this.FrameFunction_Architecture_AfterGetAppointableTitle();
                    break;

                case FrameFunction.GetRecallablePerson://免官
                    this.FrameFunction_Architecture_AfterGetRecallablePerson();
                    break;

                case FrameFunction.GetRecallableTitle: //免官
                    this.FrameFunction_Architecture_AfterGetRecallableTitle();
                    break;

                case FrameFunction.GetStudyStuntPerson:
                    this.FrameFunction_Architecture_AfterGetStudyStuntPerson();
                    break;

                case FrameFunction.GetStudyStunt:
                    this.FrameFunction_Architecture_AfterGetStudyStunt();
                    break;

                case FrameFunction.GetNewMilitaryKind:
                    this.FrameFunction_Architecture_AfterGetNewMilitaryKind();
                    break;

                case FrameFunction.GetRecruitmentMilitary:
                    this.FrameFunction_Architecture_AfterGetRecruitmentMilitary();
                    break;

                case FrameFunction.GetRecruitmentPerson:
                    this.FrameFunction_Architecture_AfterGetRecruitmentPerson();
                    break;

                case FrameFunction.GetMergeMilitary:
                    this.FrameFunction_Architecture_AfterGetMergeMilitary();
                    break;

                case FrameFunction.GetBeMergedMilitaries:
                    this.FrameFunction_Architecture_AfterGetBeMergedMilitaries();
                    break;

                case FrameFunction.GetBeDisbandedMilitaries:
                    this.FrameFunction_Architecture_AfterGetBeDisbandedMilitaries();
                    break;

                case FrameFunction.GetLevelUpMilitaries:
                    this.FrameFunction_Architecture_AfterGetLevelUpMilitaries();
                    break;

                case FrameFunction.GetLevelUpMiliaryKind:
                    this.FrameFunction_Architecture_AfterGetLevelUpMilitaryKind();
                    break;

                case FrameFunction.SelectMarryablePerson:
                    this.FrameFunction_Architecture_AfterSelectMarryablePerson();
                    break;

                case FrameFunction.SelectMarryTo:
                    this.FrameFunction_Architecture_AfterSelectMarryTo();
                    break;

                case FrameFunction.SelectTrainableChildren:
                    this.FrameFunction_Architecture_AfterSelectTrainableChildren();
                    break;

                case FrameFunction.SelectTrainPolicy:
                    this.FrameFunction_Architecture_AfterSelectTrainPolicy();
                    break;

                case FrameFunction.GetNewCapital:
                    this.FrameFunction_Architecture_AfterGetNewCapital();
                    break;

                case FrameFunction.GetEnhanceDiplomaticRelation:
                    this.FrameFunction_Architecture_AfterGetEnhanceDiplomaticRelation();
                    break;

                case FrameFunction.GetEnhanceDiplomaticRelationPerson:
                    this.FrameFunction_Architecture_AfterGetEnhanceDiplomaticRelationPerson();
                    break;

                case FrameFunction.GetAllyDiplomaticRelationPerson:
                    this.FrameFunction_Architecture_AfterGetAllyDiplomaticRelationPerson();
                    break;

                case FrameFunction.GetFriendlyDiplomaticRelation:
                    this.FrameFunction_Architecture_AfterGetFriendlyDiplomaticRelation();
                    break;

                case FrameFunction.GetAllyDiplomaticRelation:
                    this.FrameFunction_Architecture_AfterGetAllyDiplomaticRelation();
                    break;

                case FrameFunction.GetTruceDiplomaticRelation:
                    this.FrameFunction_Architecture_AfterGetTruceDiplomaticRelation();
                    break;

                case FrameFunction.GetTruceDiplomaticRelationPerson:
                    this.FrameFunction_Architecture_AfterGetTruceDiplomaticRelationPerson();
                    break;

                case FrameFunction.GetDenounceDiplomaticRelation:
                    this.FrameFunction_Architecture_AfterGetDenounceDiplomaticRelation();
                    break;
                    
                case FrameFunction .GetQuanXiangDiplomaticRelation: //劝降
                    this.FrameFunction_Architecture_AfterGetQuanXiangDiplomaticRelation();
                    break;
         
                case FrameFunction .GetQuanXiangDiplomaticRelationPerson:
                    this.FrameFunction_Architecture_AfterGetQuanXiangDiplomaticRelationPerson();
                    break;

                case FrameFunction.GetAttackDefaultKind:
                    this.FrameFunction_Troop_AfterGetAttackDefaultKind();
                    break;

                case FrameFunction.GetAttackTargetKind:
                    this.FrameFunction_Troop_AfterGetAttackTargetKind();
                    break;

                case FrameFunction.GetCastDefaultKind:
                    this.FrameFunction_Troop_AfterGetCastDefaultKind();
                    break;

                case FrameFunction.GetCastTargetKind:
                    this.FrameFunction_Troop_AfterGetCastTargetKind();
                    break;

                case FrameFunction.GetInformationKind:
                    this.FrameFunction_Architecture_AfterGetInformationKind();
                    break;

                case FrameFunction.GetOfficerType:
                    this.FrameFunction_Architecture_AfterGetOfficerType();
                    break;

                case FrameFunction.GetInformationToStop:
                    this.FrameFunction_Architecture_AfterGetInformationToStop();
                    break;

                case FrameFunction.GetInformationPerson:
                    this.FrameFunction_Architecture_AfterGetInformationPerson();
                    break;
                    /*
                case FrameFunction.GetSpyPerson:
                    this.FrameFunction_Architecture_AfterGetSpyPerson();
                    break;
                     */

                case FrameFunction.GetDestroyPerson:
                    this.FrameFunction_Architecture_AfterGetDestroyPerson();
                    break;

                case FrameFunction.GetInstigatePerson:
                    this.FrameFunction_Architecture_AfterGetInstigatePerson();
                    break;

                case FrameFunction.GetGossipPerson:
                    this.FrameFunction_Architecture_AfterGetGossipPerson();
                    break;

                case FrameFunction.GetJailBreakPerson:
                    this.FrameFunction_Architecture_AfterGetJailBreakPerson();
                    break;

                case FrameFunction.GetAssassinatePerson:
                    this.FrameFunction_Architecture_AfterGetAssassinatePerson();
                    break;

                case FrameFunction.GetAssassinatePersonTarget:
                    this.FrameFunction_Architecture_AfterGetAssassinatePersonTarget();
                    break;

                case FrameFunction.GetSearchPerson:
                    this.FrameFunction_Architecture_AfterGetSearchPerson();
                    break;

                case FrameFunction.GetFacilityToBuild:
                    this.FrameFunction_Architecture_AfterGetFacilityToBuild();
                    break;

                case FrameFunction.GetFacilityToDemolish:
                    this.FrameFunction_Architecture_AfterGetFacilityToDemolish();
                    break;

                case FrameFunction.GetSectionToDemolish:
                    this.FrameFunction_Architecture_AfterGetSectionToDemolish();
                    break;

                case FrameFunction.GetShortestRouteway:
                    this.FrameFunction_Architecture_AfterGetShortestRouteway();
                    break;

                case FrameFunction.GetShortestNoWaterRouteway:
                    this.FrameFunction_Architecture_AfterGetShortestNoWaterRouteway();
                    break;

                case FrameFunction.GetConfiscateTreasure:
                    this.FrameFunction_Architecture_AfterGetConfiscateTreasure();
                    break;

                case FrameFunction.GetAwardTreasure:
                    this.FrameFunction_Architecture_AfterGetAwardTreasure();
                    break;

                case FrameFunction.GetAwardTreasurePerson:
                    this.FrameFunction_Architecture_AfterGetAwardTreasurePerson();
                    break;
                case FrameFunction.xuanzemeinv :
                    this.FrameFunction_Architecture_Afterxuanzemeinv();
                    break;
                case FrameFunction.chongxingmeinv:
                    this.FrameFunction_Architecture_chongxingmeinv();
                    break;
                case FrameFunction.KillPerson:
                    this.FrameFunction_Architecture_KillPerson();
                    break;
                case FrameFunction.KillCaptive:
                    this.FrameFunction_Architecture_KillCaptive();
                    break;
                case FrameFunction.ReleaseSelfPerson:
                    this.FrameFunction_Architecture_ReleaseSelfPerson();
                    break;

                case FrameFunction.SelectPrince:
                    this.FrameFunction_Architecture_SelectPrince();
                    break;
                case FrameFunction.AppointMayor: //任命太守
                    this.FrameFunction_Architecture_AppointMayor();
                    break ;
                case FrameFunction.SelectLandLink:
                    this.FrameFunction_Architecture_SelectLandLink();
                    break;
                case FrameFunction.SelectWaterLink:
                    this.FrameFunction_Architecture_SelectWaterLink();
                    break;

                case FrameFunction.MoveFeizi:
                    this.FrameFunction_Monarch_hougongTop_moveFeizi();
                    break;

                case FrameFunction.ReleaseFeizi:
                    this.FrameFunction_Monarch_hougongTop_releaseFeizi();
                    break;

                case FrameFunction.MoveCaptive: //俘虏可移动
                    this.FrameFunction_Monarch_KillRelease_MoveCaptive();
                    break;

                

            }
            this.lastFrameFunction = function;
        }
        public void FrameFunction_Architecture_SelectLandLink()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.ArchitectureListWithoutSelf().GetSelectedList();
                if (this.CurrentGameObjects != null)
                {

                    this.CurrentArchitecture.ResetLandLink(this.CurrentGameObjects.GetList());

                }
            }
        }

        public void FrameFunction_Architecture_SelectWaterLink()
        {
            if (this.CurrentArchitecture != null)
            {
                this.CurrentGameObjects = this.CurrentArchitecture.ArchitectureListWithoutSelf().GetSelectedList();
                if (this.CurrentGameObjects != null)
                {

                    this.CurrentArchitecture.ResetWaterLink(this.CurrentGameObjects.GetList());

                }
            }
        }

        private void FrameFunction_Architecture_SelectPrince()//立储的作用
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                this.CurrentArchitecture.BelongedFaction.PrinceID = this.CurrentPerson.ID;
                this.CurrentArchitecture.DecreaseFund(Session.Parameters.SelectPrinceCost);
                this.CurrentArchitecture.SelectPrince(this.CurrentPerson);
                //this.mainGameScreen.xianshishijiantupian(this.CurrentArchitecture.BelongedFaction.Leader, this.CurrentPerson.Name, "SelectPrince", "", "", true );
                
            }
        }

        private void FrameFunction_Architecture_AppointMayor()  //太守
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                this.CurrentArchitecture.MayorID = this.CurrentPerson.ID;
                this.CurrentArchitecture.MayorOnDutyDays = 0;
                this.CurrentArchitecture.AppointMayor(this.CurrentPerson);
               
            }
        }

        private void FrameFunction_Architecture_ReleaseSelfPerson()
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.CurrentPerson.BelongedFaction.Leader, this.CurrentPerson.Name, TextMessageKind.ReleaseSelfPerson, "ReleaseSelfPerson", "", "", false );
                this.CurrentPerson.BeLeaveToNoFaction();
            }
        }

        private void FrameFunction_Architecture_KillCaptive()
        {
            Captive captive = new Captive();
            captive = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Captive;
            if (captive != null)
            {
                Person leader = captive.BelongedFaction.Leader;

                Session.MainGame.mainGameScreen.OnExecute(leader, captive.CaptivePerson);
                captive.CaptivePerson.execute(captive.BelongedFaction);
            }
        }

        private void FrameFunction_Architecture_KillPerson()
        {
            this.CurrentPerson = Session.MainGame.mainGameScreen.Plugins.TabListPlugin.SelectedItem as Person;
            if (this.CurrentPerson != null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.NeutralPerson, this.CurrentPerson.BelongedFaction.Leader.Name, "KillSelfPerson", "chuzhan.jpg", "chuzhan", this.CurrentPerson.Name, true);
                Person leader = this.CurrentPerson.BelongedFaction.Leader;
                this.CurrentPerson.execute(this.CurrentPerson.BelongedFaction);
            }
        }

        public void Initialize()
        {

        }

        public void SetTroopsPosition(Point position)
        {
            MilitaryList templist = new MilitaryList();
            foreach(Military military in this.CurrentMilitaries)
            {
                Person leader=new Person();
                PersonList persons=new PersonList();
                if(this.CurrentArchitecture.Persons.Count==0 || this.CurrentArchitecture.GetAllAvailableArea(false).Area.Count==0)
                {
                    break;
                }
                else if (this.CurrentArchitecture.Persons.Count > 0 && this.CurrentArchitecture.GetAllAvailableArea(false).Area.Count > 0)
                {
                    if (this.CurrentArchitecture.Persons.HasGameObject(military.FollowedLeader))
                    {
                        leader = military.FollowedLeader;
                    }
                    else if (this.CurrentArchitecture.Persons.HasGameObject(military.Leader))
                    {
                        leader = military.Leader;
                    }
                    else
                    {
                        templist.Add(military);
                        continue;
                    }
                    persons.Add(leader);
                    foreach (Person p in leader.preferredTroopPersons)
                    {
                        if (this.CurrentArchitecture.Persons.HasGameObject(p) && !persons.HasGameObject(p))
                        {
                            persons.Add(p);
                        }
                    }
                    Point point = Session.Current.Scenario.GetClosestPoint(this.CurrentArchitecture.GetAllAvailableArea(false),position);

                    this.CurrentTroop = this.CurrentArchitecture.CreateTroop(persons, leader, military, this.CurrentArchitecture.Food>military.FoodMax? military.FoodMax:0, point);
                    this.CurrentTroop.zijin = this.CurrentArchitecture.Fund > military.zijinzuidazhi ? military.zijinzuidazhi : 0;
                    this.CurrentTroop.ManualControl = true;
                    this.CurrentArchitecture.DecreaseFund(this.CurrentTroop.zijin);
                    if ((this.CurrentArchitecture.DefensiveLegion == null) || (this.CurrentArchitecture.DefensiveLegion.Troops.Count == 0))
                    {
                        this.CurrentArchitecture.CreateDefensiveLegion();
                    }
                    this.CurrentArchitecture.DefensiveLegion.AddTroop(this.CurrentTroop);
                    Session.MainGame.mainGameScreen.Plugins.PersonBubblePlugin.AddPerson(leader, this.CurrentTroop.Position, TextMessageKind.StartCampaign, "Campaign");
                    //int minlength = 9999;
                    //foreach (Point point2 in this.CurrentArchitecture.GetAllAvailableArea(false).Area)
                    //{
                    //    if(Math.Abs(point2.X-position.X)+Math.Abs())
                    //}
                }
            }
            foreach (Military military in templist)
            {
                Person leader = new Person();
                PersonList persons = new PersonList();
                if (this.CurrentArchitecture.Persons.Count == 0 || this.CurrentArchitecture.GetAllAvailableArea(false).Area.Count == 0)
                {
                    break;
                }
                else if (this.CurrentArchitecture.Persons.Count > 0 && this.CurrentArchitecture.GetAllAvailableArea(false).Area.Count > 0)
                {
                    if (this.CurrentArchitecture.Persons.HasGameObject(military.FollowedLeader))
                    {
                        leader = military.FollowedLeader;
                    }
                    else if (this.CurrentArchitecture.Persons.HasGameObject(military.Leader))
                    {
                        leader = military.Leader;
                    }
                    else
                    {
                        leader=this.CurrentArchitecture.GetMaxFightingForcePerson();
                    }
                    persons.Add(leader);
                    Point point = Session.Current.Scenario.GetClosestPoint(this.CurrentArchitecture.GetAllAvailableArea(false), position);

                    this.CurrentTroop = this.CurrentArchitecture.CreateTroop(persons, leader, military, this.CurrentArchitecture.Food > military.FoodMax ? military.FoodMax : 0, point);
                    this.CurrentTroop.zijin = this.CurrentArchitecture.Fund > military.zijinzuidazhi ? military.zijinzuidazhi : 0;
                    this.CurrentTroop.ManualControl = true;
                    this.CurrentArchitecture.DecreaseFund(this.CurrentTroop.zijin);
                    if ((this.CurrentArchitecture.DefensiveLegion == null) || (this.CurrentArchitecture.DefensiveLegion.Troops.Count == 0))
                    {
                        this.CurrentArchitecture.CreateDefensiveLegion();
                    }
                    this.CurrentArchitecture.DefensiveLegion.AddTroop(this.CurrentTroop);
                    Session.MainGame.mainGameScreen.Plugins.PersonBubblePlugin.AddPerson(leader, this.CurrentTroop.Position, TextMessageKind.StartCampaign, "Campaign");
                    //int minlength = 9999;
                    //foreach (Point point2 in this.CurrentArchitecture.GetAllAvailableArea(false).Area)
                    //{
                    //    if(Math.Abs(point2.X-position.X)+Math.Abs())
                    //}
                }
            }
        }

        public void SetCreatingTroopPosition(Point position)
        {
            this.CurrentTroop = this.CurrentArchitecture.CreateTroop(this.CurrentGameObjects, this.CurrentPerson, this.CurrentMilitary, this.CurrentNumber, position);
            this.CurrentTroop.zijin = this.Currentzijin;
            this.CurrentTroop.ManualControl = true;
            this.CurrentArchitecture.DecreaseFund(this.CurrentTroop.zijin);
            if ((this.CurrentArchitecture.DefensiveLegion == null) || (this.CurrentArchitecture.DefensiveLegion.Troops.Count == 0))
            {
                this.CurrentArchitecture.CreateDefensiveLegion();
            }
            this.CurrentArchitecture.DefensiveLegion.AddTroop(this.CurrentTroop);
            // this.CurrentArchitecture.PostCreateTroop(this.CurrentTroop, true);
            Session.MainGame.mainGameScreen.Plugins.PersonBubblePlugin.AddPerson(this.CurrentPerson, this.CurrentTroop.Position, TextMessageKind.StartCampaign, "Campaign");
            //this.mainGameScreen.Plugins.AirViewPlugin.ReloadTroopView();
        }

        public void ArchitectureExpand()
        {
            this.CurrentArchitecture.Expand();
        }

        
        
    }

 

}
