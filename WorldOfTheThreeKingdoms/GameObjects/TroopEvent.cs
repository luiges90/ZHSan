using GameManager;
using GameObjects.Conditions;
using GameObjects.TroopDetail.EventEffect;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class TroopEvent : GameObject
    {
        [DataMember]
        public int AfterEventHappened = -1;

        public TroopEvent AfterHappenedEvent;

        [DataMember]
        public EventCheckAreaKind CheckArea;

        [DataMember]
        public string ConditionsString { get; set; }

        public ConditionTable Conditions = new ConditionTable();

        //[DataMember]
        public List<PersonDialog> Dialogs = new List<PersonDialog>();

        [DataMember]
        public string dialogString { get; set; }

        [DataMember]
        public string EffectAreasString { get; set; }

        public List<TroopEffectArea> EffectAreas = new List<TroopEffectArea>();

        [DataMember]
        public string EffectPersonsString { get; set; }

        public List<TroopEffectPerson> EffectPersons = new List<TroopEffectPerson>();

        private int happenChance;
        private bool happened;

        [DataMember]
        public int LaunchPersonString { get; set; }
        public Person LaunchPerson;
        private bool repeatable;

        [DataMember]
        public string SelfEffectsString { get; set; }

        public List<GameObjects.TroopDetail.EventEffect.EventEffect> SelfEffects = new List<GameObjects.TroopDetail.EventEffect.EventEffect>();

        [DataMember]
        public string TargetPersonsString { get; set; }

        public List<PersonRelation> TargetPersons = new List<PersonRelation>();

        [DataMember]
        public String Image = "";

        [DataMember]
        public String Sound = "";

        [DataMember]
        public string TryToShowString { get; set; }

        public event ApplyTroopEvent OnApplyTroopEvent;

        public void Init()
        {
            Conditions = new ConditionTable();

            EffectAreas = new List<TroopEffectArea>();

            EffectPersons = new List<TroopEffectPerson>();

            SelfEffects = new List<GameObjects.TroopDetail.EventEffect.EventEffect>();

            TargetPersons = new List<PersonRelation>();

            if (Dialogs == null)
            {
                Dialogs = new List<PersonDialog>();
            }
        }

        public void ApplyEventDialogs(Troop troop)
        {
            if (this.OnApplyTroopEvent != null)
            {
                this.OnApplyTroopEvent(this, troop);
            }
        }

        public void ApplyEventEffects(Troop self)
        {
            if (((self != null) && !self.Destroyed) && (!this.Happened || this.Repeatable))
            {
                Troop troopByPositionNoCheck;
                this.Happened = true;
                TroopList list = new TroopList();
                if (this.SelfEffects.Count > 0)
                {
                    list.Add(self);
                    foreach (GameObjects.TroopDetail.EventEffect.EventEffect effect in this.SelfEffects)
                    {
                        effect.ApplyEffect(self.Leader);
                    }
                }
                foreach (TroopEffectPerson person in this.EffectPersons)
                {
                    person.Effect.ApplyEffect(person.EffectPerson);
                    if ((person.EffectPerson.LocationTroop != null) && (list.GetGameObject(person.EffectPerson.LocationTroop.ID) == null))
                    {
                        list.Add(person.EffectPerson.LocationTroop);
                    }
                }
                List<TroopEffectArea> list2 = new List<TroopEffectArea>();
                List<TroopEffectArea> list3 = new List<TroopEffectArea>();
                List<TroopEffectArea> list4 = new List<TroopEffectArea>();
                foreach (TroopEffectArea area in this.EffectAreas)
                {
                    switch (area.Kind)
                    {
                        case EffectAreaKind.视野敌军:
                            list2.Add(area);
                            break;

                        case EffectAreaKind.视野友军:
                            list2.Add(area);
                            break;

                        case EffectAreaKind.八格敌军:
                            list3.Add(area);
                            break;

                        case EffectAreaKind.八格友军:
                            list3.Add(area);
                            break;

                        case EffectAreaKind.攻击范围敌军:
                            list4.Add(area);
                            break;

                        case EffectAreaKind.攻击范围友军:
                            list4.Add(area);
                            break;
                    }
                }
                foreach (TroopEffectArea area in list2)
                {
                    foreach (Point point in self.BaseViewArea.Area)
                    {
                        if (self.BelongedFaction.IsPositionKnown(point))
                        {
                            troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(point);
                            if (troopByPositionNoCheck != null)
                            {
                                switch (area.Kind)
                                {
                                    case EffectAreaKind.视野敌军:
                                        if (!self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                        {
                                            area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                        }
                                        break;

                                    case EffectAreaKind.视野友军:
                                        if (self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                        {
                                            area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                foreach (TroopEffectArea area in list3)
                {
                    foreach (Point point in GameArea.GetArea(self.Position, 1, true).Area)
                    {
                        if (self.BelongedFaction.IsPositionKnown(point))
                        {
                            troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(point);
                            if (troopByPositionNoCheck != null)
                            {
                                switch (area.Kind)
                                {
                                    case EffectAreaKind.八格敌军:
                                        if (!self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                        {
                                            area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                        }
                                        break;

                                    case EffectAreaKind.八格友军:
                                        if (self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                        {
                                            area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                        }
                                        break;
                                }
                            }
                        }
                    }
                }
                foreach (TroopEffectArea area in list4)
                {
                //Label_0539:
                    foreach (Point point in self.OffenceArea.Area)
                    {
                        if (!self.BelongedFaction.IsPositionKnown(point))
                        {
                            continue;
                        }
                        troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(point);
                        if (troopByPositionNoCheck != null)
                        {
                            switch (area.Kind)
                            {
                                case EffectAreaKind.攻击范围敌军:
                                    if (!self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                    {
                                        area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                    }
                                    break;

                                case EffectAreaKind.攻击范围友军:
                                    if (self.IsFriendly(troopByPositionNoCheck.BelongedFaction))
                                    {
                                        area.Effect.ApplyEffect(troopByPositionNoCheck.Leader);
                                    }
                                    break;
                            }
                        }
                    }
                }
                foreach (Troop troop in list)
                {
                    Troop.CheckTroopRout(troop);
                }
            }
        }

        public bool CheckCondition(Troop troop)
        {
            return Condition.CheckConditionList(this.Conditions.Conditions.Values, troop);
        }

        public bool CheckTroop(Troop troop)
        {
            if (!this.Happened || this.Repeatable)
            {
                if (!((this.AfterHappenedEvent == null) || this.AfterHappenedEvent.Happened))
                {
                    return false;
                }
                if (!GameObject.Chance(this.HappenChance))
                {
                    return false;
                }
                if ((this.LaunchPerson == null) || troop.Persons.HasGameObject(this.LaunchPerson))
                {
                    if (!this.CheckCondition(troop))
                    {
                        return false;
                    }
                    if (this.TargetPersons.Count <= 0)
                    {
                        return true;
                    }
                    GameArea baseViewArea = null;
                    switch (this.CheckArea)
                    {
                        case EventCheckAreaKind.视野:
                            baseViewArea = troop.BaseViewArea;
                            break;

                        case EventCheckAreaKind.八格:
                            baseViewArea = GameArea.GetArea(troop.Position, 1, true);
                            break;

                        case EventCheckAreaKind.攻击范围:
                            baseViewArea = troop.OffenceArea;
                            break;
                    }
                    if (baseViewArea != null)
                    {
                        int num = 0;
                        foreach (Point point in baseViewArea.Area)
                        {
                            if (troop.BelongedFaction != null)
                            {
                                if (troop.BelongedFaction.IsPositionKnown(point))
                                {
                                    Troop troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(point);
                                    if (troopByPositionNoCheck != null)
                                    {
                                        foreach (PersonRelation relation in this.TargetPersons)
                                        {
                                            if (((relation.Relation == PersonRelationKind.友好) == troop.IsFriendly(troopByPositionNoCheck.BelongedFaction)) && troopByPositionNoCheck.Persons.HasGameObject(relation.SpeakingPerson))
                                            {
                                                num++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        return (num == this.TargetPersons.Count);
                    }
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.ID;
        }

        public void LoadDialogFromString(Dictionary<int, Person> persons, string data)
        {
            if (data == null) return;
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Dialogs.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                PersonDialog item = new PersonDialog();
                int num2 = int.Parse(strArray[i]);
                if (num2 >= 0 && !persons.ContainsKey(num2)) continue;
                if (num2 >= 0)
                {
                    item.SpeakingPerson = persons[num2];
                    item.SpeakingPersonID = num2;
                } else
                {
                    item.SpeakingPerson = null;
                    item.SpeakingPersonID = -1;
                }
                item.Text = strArray[i + 1];
                this.Dialogs.Add(item);
            }
        }

        public void LoadEffectAreaFromString(EventEffectTable eventEffects, string data)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.EffectAreas.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                TroopEffectArea item = new TroopEffectArea();
                item.Kind = (EffectAreaKind) int.Parse(strArray[i]);
                item.Effect = eventEffects.GetEventEffect(int.Parse(strArray[i + 1]));
                this.EffectAreas.Add(item);
            }
        }

        public void LoadEffectPersonFromString(Dictionary<int, Person> persons, EventEffectTable eventEffects, string data)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.EffectPersons.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                if (!persons.ContainsKey(int.Parse(strArray[i]))) continue;
                TroopEffectPerson item = new TroopEffectPerson();
                item.EffectPerson = persons[int.Parse(strArray[i])];
                item.Effect = eventEffects.GetEventEffect(int.Parse(strArray[i + 1]));
                this.EffectPersons.Add(item);
            }
        }

        public void LoadSelfEffectFromString(EventEffectTable eventEffects, string data)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.SelfEffects.Clear();
            for (int i = 0; i < strArray.Length; i++)
            {
                GameObjects.TroopDetail.EventEffect.EventEffect eventEffect = eventEffects.GetEventEffect(int.Parse(strArray[i]));
                if (eventEffect != null)
                {
                    this.SelfEffects.Add(eventEffect);
                }
            }
        }

        public void LoadTargetPersonFromString(Dictionary<int, Person> persons, string data)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.TargetPersons.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                if (!persons.ContainsKey(int.Parse(strArray[i + 1]))) continue;
                PersonRelation item = new PersonRelation();
                item.Relation = (PersonRelationKind) int.Parse(strArray[i]);
                item.SpeakingPerson = persons[int.Parse(strArray[i + 1])];
                this.TargetPersons.Add(item);
            }
        }

        public string SaveDialogToString()//剧本的部队事件的对话武将默认全部变成了0，而且目前源码转换中，并没有这一块的安排
        {
            string str = "";
            foreach (PersonDialog dialog in this.Dialogs)
            {
                object obj2;
                if (dialog.SpeakingPerson != null)
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, dialog.SpeakingPerson.ID, " ", dialog.Text, " " });
                }
                else
                {
                    obj2 = str;
                    str = string.Concat(new object[] { obj2, -1, " ", dialog.Text, " " });
                }
            }
            return str;
        }

        public string SaveEffectAreaToString()
        {
            string str = "";
            foreach (TroopEffectArea area in this.EffectAreas)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, (int) area.Kind, " ", area.Effect.ID, " " });
            }
            return str;
        }

        public string SaveEffectPersonToString()
        {
            string str = "";
            foreach (TroopEffectPerson person in this.EffectPersons)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, person.EffectPerson.ID, " ", person.Effect.ID, " " });
            }
            return str;
        }

        public string SaveSelfEffectToString()
        {
            string str = "";
            foreach (GameObjects.TroopDetail.EventEffect.EventEffect effect in this.SelfEffects)
            {
                str = str + effect.ID + " ";
            }
            return str;
        }

        public string SaveTargetPersonToString()
        {
            string str = "";
            foreach (PersonRelation relation in this.TargetPersons)
            {
                object obj2 = str;
                str = string.Concat(new object[] { obj2, (int) relation.Relation, " ", relation.SpeakingPerson.ID, " " });
            }
            return str;
        }

        [DataMember]
        public int HappenChance
        {
            get
            {
                return this.happenChance;
            }
            set
            {
                this.happenChance = value;
            }
        }
        [DataMember]
        public bool Happened
        {
            get
            {
                return this.happened;
            }
            set
            {
                this.happened = value;
            }
        }
        [DataMember]
        public bool Repeatable
        {
            get
            {
                return this.repeatable;
            }
            set
            {
                this.repeatable = value;
            }
        }

        public delegate void ApplyTroopEvent(TroopEvent te, Troop troop);
    }
}

