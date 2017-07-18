using System;
using System.Runtime.CompilerServices;
using GameGlobal;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class Captive : GameObject
    {
        public Faction CaptiveFaction; // the captive's original faction

        [DataMember]
        public int CaptiveFactionID;

        public Person CaptivePerson;

        [DataMember]
        public int CaptivePersonID;

        public Architecture RansomArchitecture;

        [DataMember]
        public int RansomArchitectureID;

        [DataMember]
        public int RansomArriveDays;

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

        public static Captive Create(GameScenario scenario, Person person, Faction capturingFaction)
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
            captive.Scenario = scenario;
            captive.ID = scenario.Captives.GetFreeGameObjectID();
            captive.CaptivePerson = person;
            person.DecreaseReputation(50);
            captive.CaptiveFaction = person.BelongedFaction;
            person.SetBelongedCaptive(captive, GameObjects.PersonDetail.PersonStatus.Captive);
            person.HeldCaptiveCount++;
            scenario.Captives.AddCaptiveWithEvent(captive);
            return captive;
        }

        public Faction BelongedFaction
        {
            get
            {
                if (this.CaptivePerson.LocationArchitecture != null)
                {
                    return this.CaptivePerson.LocationArchitecture.BelongedFaction;
                }
                else if (this.CaptivePerson.LocationTroop != null)
                {
                    return this.CaptivePerson.LocationTroop.BelongedFaction;
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
                return this.CaptivePerson.LocationArchitecture;
            }
        }

        public Troop LocationTroop
        {
            get
            {
                return this.CaptivePerson.LocationTroop;
            }
        }

        public void DayEvent()
        {


            if (((this.BelongedFaction != null) && (this.CaptiveFaction != null)) && (this.RansomArriveDays > 0))
            {
                this.RansomArriveDays--;
                if (this.RansomArriveDays == 0)
                {
                    if (this.BelongedFaction.Capital != null)
                    {
                        if (this.BelongedFaction.Capital == this.RansomArchitecture)
                        {
                            if (base.Scenario.IsPlayer(this.BelongedFaction))
                            {
                                if (!(!this.BelongedFaction.AutoRefuse))
                                {
                                    this.ReturnRansom();
                                }
                                else if (this.OnPlayerRelease != null)
                                {
                                    this.OnPlayerRelease(this.BelongedFaction, this.CaptiveFaction, this);
                                }
                                else
                                {
                                    this.Scenario.GameScreen.CaptivePlayerRelease(this.BelongedFaction, this.CaptiveFaction, this);
                                }
                            }
                            else
                            {
                                int diplomaticRelation = base.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, this.CaptiveFaction.ID);
                                //if (((diplomaticRelation >= 0) || (GameObject.Random(this.RansomFund) > GameObject.Random(this.RansomFund + this.BelongedFaction.Capital.Fund))) || (GameObject.Random(Math.Abs(diplomaticRelation) + 100) < GameObject.Random(100)))
                                if (diplomaticRelation >= 0 || ((!this.CaptivePerson.RecruitableBy(this.BelongedFaction, 0) || (this.CaptivePerson.Loyalty >= 100 && GameObject.Chance(80 - this.CaptivePerson.PersonalLoyalty * 20))) 
                                    && (!this.BelongedFaction.Capital.IsFundEnough || GameObject.Random(this.RansomFund) > GameObject.Random(this.RansomFund + this.BelongedFaction.Capital.Fund)))
                                    && (!GameGlobal.GlobalVariables.AIAutoTakePlayerCaptives))
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
                            this.RansomArriveDays = (int) (base.Scenario.GetDistance(this.RansomArchitecture.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0);
                            if (this.RansomArriveDays <= 0)
                            {
                                this.RansomArriveDays = 1;
                            }
                            this.RansomArchitecture = this.BelongedFaction.Capital;
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
            Point position = this.CaptivePerson.Position;
            if (this.CaptivePerson.BelongedFaction != null && this.CaptivePerson.BelongedFaction.Capital != null)
            {
                this.CaptivePerson.LocationArchitecture = this.CaptivePerson.BelongedFaction.Capital;
                this.CaptivePerson.Status = GameObjects.PersonDetail.PersonStatus.Normal;
                this.CaptivePerson.MoveToArchitecture(this.CaptivePerson.BelongedFaction.Capital, position, true);
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
            this.CaptivePerson.SetBelongedCaptive(null, this.CaptivePerson.ArrivingDays > 0 ? GameObjects.PersonDetail.PersonStatus.Moving : GameObjects.PersonDetail.PersonStatus.Normal);
            this.RansomArchitecture = null;
            this.RansomFund = 0;
        }

        private void DoReturn()
        {
            if ((this.CaptiveFaction!=null) && ((this.CaptiveFaction.Capital != null)) && (this.BelongedFaction != null) && (this.BelongedFaction.Capital != null))
            {
                int num = (int) (base.Scenario.GetDistance(this.CaptiveFaction.Capital.ArchitectureArea, this.BelongedFaction.Capital.ArchitectureArea) / 5.0);
                if (num <= 0)
                {
                    num = 1;
                }
                this.CaptiveFaction.Capital.AddFundPack(this.RansomFund, this.RansomArriveDays + num);
            }
        }

        public void ReleaseCaptive()
        {
            if (this.OnRelease != null)
            {
                this.OnRelease(true, this.BelongedFaction, this.CaptiveFaction, this.CaptivePerson);
            }
            this.RansomArchitecture.IncreaseFund(this.RansomFund);
            this.DoRelease();
        }

        public void ReturnRansom()
        {
            if (this.OnRelease != null)
            {
                this.OnRelease(false, this.BelongedFaction, this.CaptiveFaction, this.CaptivePerson);
            }
            this.DoReturn();
        }

        public void SelfReleaseCaptive()
        {
            if (this.OnSelfRelease != null)
            {
                this.OnSelfRelease(this);
            }
            if (this.BelongedFaction !=null && this.CaptiveFaction != null)
            {
                base.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.CaptiveFaction.ID, this.ReleaseRelation / 400);
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
            this.CaptivePerson.FleeCount++;
            if (this.BelongedFaction != null)
            {
                this.Scenario.GameScreen.xianshishijiantupian(this.CaptivePerson, this.BelongedFaction.Name, GameObjects.PersonDetail.TextMessageKind.CaptiveEscape, "CaptiveEscape", "", "", false);
            }
            this.DoReturn();
            this.DoRelease();
        }

        public void CaptiveEscapeNoHint()
        {
            this.CaptivePerson.FleeCount++;
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
            this.RansomFund = this.Ransom;
            from.DecreaseFund(this.RansomFund);
            this.RansomArchitecture = to;
            this.RansomArriveDays = (int) (base.Scenario.GetDistance(from.ArchitectureArea, to.ArchitectureArea) / 5.0);
            if (this.RansomArriveDays <= 0)
            {
                this.RansomArriveDays = 1;
            }
        }

        public void TransformToNoFactionCaptive()
        {
            if ((this.CaptivePerson != null) && (this.CaptivePerson.BelongedFaction != null))
            {
                this.CaptiveFaction = null;

            }

        }

        public void TransformToNoFaction()  //变成在野人物
        {
            if (this.CaptivePerson != null)
            {
                this.CaptivePerson.Status = GameObjects.PersonDetail.PersonStatus.NoFaction;
                if (this.LocationTroop == null)
                {
                    
                }
                else
                {
                    this.CaptivePerson.LocationArchitecture = base.Scenario.Architectures[GameObject.Random(base.Scenario.Architectures.Count)] as Architecture;
                    if ((this.CaptivePerson.BelongedFaction != null) && this.BelongedFaction.Capital != null)
                    {
                        this.CaptivePerson.MoveToArchitecture(this.BelongedFaction.Capital, this.CaptivePerson.LocationTroop.Position);
                    }
                    else if (base.Scenario.Architectures.Count > 0)
                    {
                        this.CaptivePerson.MoveToArchitecture(base.Scenario.Architectures[GameObject.Random(base.Scenario.Architectures.Count)] as Architecture, this.CaptivePerson.LocationTroop.Position);
                    }
                }
                this.CaptivePerson.SetBelongedCaptive(null, GameObjects.PersonDetail.PersonStatus.NoFaction);
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
                return ((this.CaptiveFaction != null) ? this.CaptiveFaction.Name : "----");
            }
        }

        public string LocationString
        {
            get
            {
                if (!base.Scenario.IsCurrentPlayer(this.CaptiveFaction) || GameGlobal.GlobalVariables.SkyEye)
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
                if (this.CaptivePerson != null)
                {
                    return this.CaptivePerson.Loyalty;
                }
                return 100;
            }
        }

        public string LoyaltyString
        {
            get
            {
                if (this.CaptivePerson != null)
                {
                    if (base.Scenario.IsCurrentPlayer(this.CaptiveFaction))
                    {
                        return "----";
                    }
                    return this.CaptivePerson.Loyalty.ToString();
                }
                return "----";
            }
        }

        public string Travel
        {
            get
            {
                if (this.CaptivePerson != null && this.CaptivePerson.LocationTroop == null)
                {
                    if (this.CaptivePerson.ArrivingDays > 0)
                    {
                        return (this.CaptivePerson.ArrivingDays + "天");
                    }
                    
                }
                
                return "----";
            }
        }

        public string RansomArriveDaysString
        {
            get
            {
                if (this.CaptivePerson != null)
                {
                    if (this.RansomArriveDays > 0)
                    {
                        return (this.RansomArriveDays + "天");
                    }
                }
                return "----";
            }
        }

        public string Name
        {
            get
            {
                return ((this.CaptivePerson != null) ? this.CaptivePerson.Name : "----");
            }
        }

        public int Ransom
        {
            get
            {
                if (this.CaptivePerson != null)
                {
                    return (int)(this.CaptivePerson.UntiredMerit * Parameters.RansomRate);
                    //return 10 * (int)(((float)((this.CaptivePerson.UntiredMerit * ((this.CaptiveFaction.Leader == this.CaptivePerson) ? 2 : 1)) / 50)) ));
                }
                return 0;
            }
        }

        [DataMember]
        public int RansomFund
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
                if (this.CaptivePerson != null)
                {
                    return (int) (((this.CaptivePerson.Merit * ((this.CaptiveFaction.Leader == this.CaptivePerson) ? 2 : 1)) / 50) * ((this.CaptiveFaction != null) ? Math.Pow((double) this.CaptiveFaction.InternalSurplusRate, 1.7999999523162842) : 1.0));
                }
                return 0;
            }
        }

        public int AIWantsTheCaptive
        {
            get
            {
                if (!this.CaptivePerson.WillLoseLoyaltyWhenHeldCaptive) return this.CaptivePerson.Merit / 2;
                return this.CaptivePerson.Merit + (110 - this.Loyalty) * 500 + 
                    (this.LocationArchitecture.noEscapeChance - this.CaptivePerson.captiveEscapeChance) * 300;
            }
        }

        public int Merit
        {
            get
            {
                return this.CaptivePerson.Merit;
            }
        }

        public delegate void PlayerRelease(Faction from, Faction to, Captive captive);

        public delegate void Release(bool success, Faction from, Faction to, Person person);

        public delegate void SelfRelease(Captive captive);

        public delegate void Escape(Captive captive);


    }
}

