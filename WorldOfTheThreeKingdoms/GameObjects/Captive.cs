using System;
using System.Runtime.CompilerServices;
using GameGlobal;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects
{
    [DataContract]
    public class Captive : GameObject
    {
        private Faction captiveFaction;
        public Faction CaptiveCharFaction
        {
            get
            {
                if (this.CaptiveCharFactionID == -1) return null;

                if (captiveFaction == null)
                {
                    captiveFaction = (Faction) Session.Current.Scenario.Factions.GetGameObject(CaptiveCharFactionID);
                }
                return captiveFaction;
            }
            set
            {
                if (value != null)
                {
                    this.CaptiveCharFactionID = value.ID;
                }
                else
                {
                    this.CaptiveCharFactionID = -1;
                }
                captiveFaction = value;
            }
        }
        // the captive's original faction

        [DataMember]
        public int CaptiveCharFactionID;

        private Person captivePerson;
        public Person CaptiveCharacter
        {
            get
            {
                if (captivePerson == null)
                {
                    captivePerson = (Person)Session.Current.Scenario.Persons.GetGameObject(CaptiveCharacterID);
                }
                return captivePerson;
            }
            set
            {
                CaptiveCharacterID = value.ID;
                captivePerson = value;
            }
        }

        [DataMember]
        public int CaptiveCharacterID;

        public Architecture RansomReceivedCity;

        [DataMember]
        public int RansomReceivedCityID;

        [DataMember]
        public int RansomArrivalDays;

        private int ransomFund;

        public event PlayerRelease OnPlayerRelease;

        public event Release OnRelease;

        public event SelfRelease OnSelfRelease;

        public event Escape OnEscape;

        public void ClearEvents()
        {
            OnPlayerRelease = null;
            OnRelease = null;
            OnSelfRelease = null;
            OnEscape = null;
        }

        public static Captive Create(Person person, Faction capturingFaction)
        {
            if (person.BelongedFaction == null)
            {
                return null;
            }
            if (person.BelongedFaction == capturingFaction)
            {
                return null;
            }
            Captive captive = new Captive();
            captive.ID = Session.Current.Scenario.Captives.GetFreeGameObjectID();
            captive.CaptiveCharacter = person;
            person.DecreaseReputation(50);
            captive.CaptiveCharFaction = person.BelongedFaction;
            person.SetBelongedCaptive(captive, GameObjects.PersonDetail.PersonStatus.Captive);
            person.HeldCaptiveCount++;
            Session.Current.Scenario.Captives.AddCaptiveWithEvent(captive);
            return captive;
        }

        public Faction BelongedFaction
        {
            get
            {
                if (this.CaptiveCharacter.LocationArchitecture != null)
                {
                    return this.CaptiveCharacter.LocationArchitecture.BelongedFaction;
                }
                else if (this.CaptiveCharacter.LocationTroop != null)
                {
                    return this.CaptiveCharacter.LocationTroop.BelongedFaction;
                }
                else
                {
                    // this should not happen...
                    return null;
                }
            }
        }

        public Architecture LocationArchitecture
        {
            get
            {
                return this.CaptiveCharacter.LocationArchitecture;
            }
        }

        public Troop LocationTroop
        {
            get
            {
                return this.CaptiveCharacter.LocationTroop;
            }
        }

        public void DayEvent()
        {


            if (((this.BelongedFaction != null) && (this.CaptiveCharFaction != null)) && (this.RansomArrivalDays > 0))
            {
                //this.RansomArrivalDays--;
                this.RansomArrivalDays -= Session.Parameters.DayInTurn;
                if (this.RansomArrivalDays == 0)
                {
                    if (this.BelongedFaction.Capital != null)
                    {
                        if (this.BelongedFaction.Capital == this.RansomReceivedCity)
                        {
                            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                            {
                                if (!(!this.BelongedFaction.AutoRefuse))
                                {
                                    this.ReturnRansom();
                                }
                                else if (this.OnPlayerRelease != null)
                                {
                                    this.OnPlayerRelease(this.BelongedFaction, this.CaptiveCharFaction, this);
                                }
                                else
                                {
                                    Session.MainGame.mainGameScreen.CaptivePlayerRelease(this.BelongedFaction, this.CaptiveCharFaction, this);
                                }
                            }
                            else
                            {
                                int diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, this.CaptiveCharFaction.ID);
                                //if (((diplomaticRelation >= 0) || (GameObject.Random(this.RansomAmount) > GameObject.Random(this.RansomAmount + this.BelongedFaction.Capital.Fund))) || (GameObject.Random(Math.Abs(diplomaticRelation) + 100) < GameObject.Random(100)))
                                if (diplomaticRelation >= 0 || ((!this.CaptiveCharacter.RecruitableBy(this.BelongedFaction, 0) || (this.CaptiveCharacter.Loyalty >= 100 && GameObject.Chance(80 - this.CaptiveCharacter.PersonalLoyalty * 20))) 
                                    && (!this.BelongedFaction.Capital.IsFundEnough || GameObject.Random(this.RansomAmount) > GameObject.Random(this.RansomAmount + this.BelongedFaction.Capital.Fund)))
                                    && (!Session.GlobalVariables.AIAutoTakePlayerCaptives))
                                {
                                    this.ReleaseCaptive();
                                }
                                else
                                {
                                    this.ReturnRansom();
                                }
                            }
                        }
                        else
                        {
                            //this.RansomArrivalDays = (int) (Session.Current.Scenario.GetDistance(this.RansomReceivedCity.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0);
                            this.RansomArrivalDays = (int)(Session.Current.Scenario.GetDistance(this.RansomReceivedCity.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0) * Session.Parameters.DayInTurn;
                            if (this.RansomArrivalDays <= 0)
                            {
                                this.RansomArrivalDays = 1;
                            }
                            this.RansomReceivedCity = this.BelongedFaction.Capital;
                        }
                    }
                    else
                    {
                        this.ReturnRansom();
                    }
                }
            }
        }

        private void DoRelease()
        {
            Point position = this.CaptiveCharacter.Position;
            if (this.CaptiveCharacter.BelongedFaction != null && this.CaptiveCharacter.BelongedFaction.Capital != null)
            {
                Faction f = this.CaptiveCharacter.BelongedFaction;
                this.CaptiveCharacter.LocationArchitecture = f.Capital;
                this.CaptiveCharacter.Status = GameObjects.PersonDetail.PersonStatus.Normal;
                this.CaptiveCharacter.MoveToArchitecture(f.Capital, position, true);
            }
            
            else
            {

                this.TransformToNoFaction();
                return;
                
            }
            this.Clear();
        }

        public void Clear()
        {
            this.CaptiveCharacter.SetBelongedCaptive(null, this.CaptiveCharacter.ArrivingDays > 0 ? GameObjects.PersonDetail.PersonStatus.Moving : GameObjects.PersonDetail.PersonStatus.Normal);
            this.RansomReceivedCity = null;
            this.RansomAmount = 0;
        }

        private void DoReturn()
        {
            if ((this.CaptiveCharFaction!=null) && ((this.CaptiveCharFaction.Capital != null)) && (this.BelongedFaction != null) && (this.BelongedFaction.Capital != null))
            {
                //int num = (int) (Session.Current.Scenario.GetDistance(this.CaptiveCharFaction.Capital.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0);
                int num = (int) (Session.Current.Scenario.GetDistance(this.CaptiveCharFaction.Capital.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0) * Session.Parameters.DayInTurn;
                if (num <= 0)
                {
                    num = 1;
                }
                this.CaptiveCharFaction.Capital.AddFundPack(this.RansomAmount, this.RansomArrivalDays + num);
            }
        }

        public void ReleaseCaptive()
        {
            if (this.OnRelease != null)
            {
                this.OnRelease(true, this.BelongedFaction, this.CaptiveCharFaction, this.CaptiveCharacter);
            }
            this.RansomReceivedCity.IncreaseFund(this.RansomAmount);
            this.DoRelease();
        }

        public void ReturnRansom()
        {
            if (this.OnRelease != null)
            {
                this.OnRelease(false, this.BelongedFaction, this.CaptiveCharFaction, this.CaptiveCharacter);
            }
            this.DoReturn();
        }

        public void SelfReleaseCaptive()
        {
            if (this.OnSelfRelease != null)
            {
                this.OnSelfRelease(this);
            }
            if (this.BelongedFaction !=null && this.CaptiveCharFaction != null)
            {
                Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.CaptiveCharFaction.ID, this.ReleaseRelation / 400);
            }
            if (GameObject.Chance(this.CaptiveCharacter.Karma + this.CaptiveCharacter.PersonalLoyalty * 10))
            {
                this.BelongedFaction.Leader.IncreaseKarma(1);
            }
            this.DoReturn();
            this.DoRelease();
        }

        public void CaptiveEscape()
        {
            if (this.OnEscape != null)
            {
                this.OnEscape(this);
            }
            this.CaptiveCharacter.FleeCount++;
            if (this.BelongedFaction != null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.CaptiveCharacter, this.BelongedFaction.Name, GameObjects.PersonDetail.TextMessageKind.CaptiveEscape, "CaptiveEscape", "", "", false);
            }
            this.DoReturn();
            this.DoRelease();
        }

        public void CaptiveEscapeNoHint()
        {
            this.CaptiveCharacter.FleeCount++;
            this.DoReturn();
            this.DoRelease();
        }

        public void CaptiveDirectEscape()
        {
            this.DoReturn();
            this.DoRelease();
        }

        public void SendRansom(Architecture to, Architecture from)
        {
            this.RansomAmount = this.Ransom;
            from.DecreaseFund(this.RansomAmount);
            this.RansomReceivedCity = to;
            //this.RansomArrivalDays = (int) (Session.Current.Scenario.GetDistance(from.ArchitectureArea, to.ArchitectureArea) / 5.0);
            this.RansomArrivalDays = (int)(Session.Current.Scenario.GetDistance(from.ArchitectureArea, to.ArchitectureArea) / 5.0) * Session.Parameters.DayInTurn;
            if (this.RansomArrivalDays <= 0)
            {
                this.RansomArrivalDays = 1;
            }
        }

        public void TransformToNoFactionCaptive()
        {
            if ((this.CaptiveCharacter != null) && (this.CaptiveCharacter.BelongedFaction != null))
            {
                this.CaptiveCharFaction = null;

            }

        }

        public void TransformToNoFaction()  //变成在野人物
        {
            if (this.CaptiveCharacter != null)
            {
                this.CaptiveCharacter.Status = GameObjects.PersonDetail.PersonStatus.NoFaction;
                if (this.LocationTroop == null)
                {
                    
                }
                else
                {
                    this.CaptiveCharacter.LocationArchitecture = Session.Current.Scenario.Architectures[GameObject.Random(Session.Current.Scenario.Architectures.Count)] as Architecture;
                    if ((this.CaptiveCharacter.BelongedFaction != null) && this.BelongedFaction.Capital != null)
                    {
                        this.CaptiveCharacter.MoveToArchitecture(this.BelongedFaction.Capital, this.CaptiveCharacter.LocationTroop.Position);
                    }
                    else if (Session.Current.Scenario.Architectures.Count > 0)
                    {
                        this.CaptiveCharacter.MoveToArchitecture(Session.Current.Scenario.Architectures[GameObject.Random(Session.Current.Scenario.Architectures.Count)] as Architecture, this.CaptiveCharacter.LocationTroop.Position);
                    }
                }
                this.CaptiveCharacter.SetBelongedCaptive(null, GameObjects.PersonDetail.PersonStatus.NoFaction);
            }
        }

        public string BelongedFactionString
        {
            get
            {
                return ((this.BelongedFaction != null) ? this.BelongedFaction.Name : "----");
            }
        }

        public string CaptiveFactionString
        {
            get
            {
                return ((this.CaptiveCharFaction != null) ? this.CaptiveCharFaction.Name : "----");
            }
        }

        public string LocationString
        {
            get
            {
                if (!Session.Current.Scenario.IsCurrentPlayer(this.CaptiveCharFaction) || Session.GlobalVariables.SkyEye)
                {
                    if (this.LocationArchitecture != null)
                    {
                        return this.LocationArchitecture.Name;
                    }
                    if (this.LocationTroop != null)
                    {
                        return this.LocationTroop.DisplayName;
                    }
                }
                return "----";
            }
        }

        public int Loyalty
        {
            get
            {
                if (this.CaptiveCharacter != null)
                {
                    return this.CaptiveCharacter.Loyalty;
                }
                return 100;
            }
        }

        public string LoyaltyString
        {
            get
            {
                if (this.CaptiveCharacter != null)
                {
                    if (Session.Current.Scenario.IsCurrentPlayer(this.CaptiveCharFaction))
                    {
                        return "----";
                    }
                    return this.CaptiveCharacter.Loyalty.ToString();
                }
                return "----";
            }
        }

        public string Travel
        {
            get
            {
                if (this.CaptiveCharacter != null && this.CaptiveCharacter.LocationTroop == null)
                {
                    if (this.CaptiveCharacter.ArrivingDays > 0)
                    {
                        return (this.CaptiveCharacter.ArrivingDays + "天");
                    }
                    
                }
                
                return "----";
            }
        }

        public string RansomArriveDaysString
        {
            get
            {
                if (this.CaptiveCharacter != null)
                {
                    if (this.RansomArrivalDays > 0)
                    {
                        return (this.RansomArrivalDays * Session.Parameters.DayInTurn + "天");
                    }
                }
                return "----";
            }
        }

#pragma warning disable CS0108 // 'Captive.Name' hides inherited member 'GameObject.Name'. Use the new keyword if hiding was intended.
        public string Name
#pragma warning restore CS0108 // 'Captive.Name' hides inherited member 'GameObject.Name'. Use the new keyword if hiding was intended.
        {
            get
            {
                return ((this.CaptiveCharacter != null) ? this.CaptiveCharacter.Name : "----");
            }
        }

        public int Ransom
        {
            get
            {
                if (this.CaptiveCharacter != null)
                {
                    return (int)(this.CaptiveCharacter.UntiredMerit * Session.Parameters.RansomRate);
                    //return 10 * (int)(((float)((this.CaptiveCharacter.UntiredMerit * ((this.CaptiveCharFaction.Leader == this.CaptiveCharacter) ? 2 : 1)) / 50)) ));
                }
                return 0;
            }
        }

        [DataMember]
        public int RansomAmount
        {
            get
            {
                return this.ransomFund;
            }
            set
            {
                this.ransomFund = value;
            }
        }

        public int ReleaseRelation
        {
            get
            {
                if (this.CaptiveCharacter != null)
                {
                    return (int) (((this.CaptiveCharacter.Merit * ((this.CaptiveCharFaction.Leader == this.CaptiveCharacter) ? 2 : 1)) / 50) * ((this.CaptiveCharFaction != null) ? Math.Pow((double) this.CaptiveCharFaction.InternalSurplusRate, 1.7999999523162842) : 1.0));
                }
                return 0;
            }
        }

        public int AIWantsTheCaptive
        {
            get
            {
                if (!this.CaptiveCharacter.WillLoseLoyaltyWhenHeldCaptive) return this.CaptiveCharacter.Merit / 2;
                return this.CaptiveCharacter.Merit + (110 - this.Loyalty) * 500 + 
                    (this.LocationArchitecture.noEscapeChance - this.CaptiveCharacter.captiveEscapeChance) * 300;
            }
        }

        public int Merit
        {
            get
            {
                return this.CaptiveCharacter.Merit;
            }
        }

        public delegate void PlayerRelease(Faction from, Faction to, Captive captive);

        public delegate void Release(bool success, Faction from, Faction to, Person person);

        public delegate void SelfRelease(Captive captive);

        public delegate void Escape(Captive captive);


    }
}

