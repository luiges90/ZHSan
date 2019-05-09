using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using System;
using GameObjects.Animations;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CombatMethod : GameObject
    {
        private TileAnimationKind animationKind;
        
        public Influence AI;
        private bool architectureTarget;
        private AttackDefaultKind attackDefault;
        private AttackTargetKind attackTarget;

        public ConditionTable CastConditions = new ConditionTable();
        private int combativity;
        private string description;

        public InfluenceTable Influences = new InfluenceTable();
        private bool viewingHostile;

        public Dictionary<Condition, float> AIConditionWeightSelf = new Dictionary<Condition, float>();

        public Dictionary<Condition, float> AIConditionWeightEnemy = new Dictionary<Condition, float>();

        public void Init()
        {
            CastConditions = new ConditionTable();
            Influences = new InfluenceTable();
            AIConditionWeightSelf = new Dictionary<Condition, float>();
            AIConditionWeightEnemy = new Dictionary<Condition, float>();
        }

        [DataMember]
        public string InfluencesString
        {
            get;
            set;
        }

        [DataMember]
        public string CastConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string AIConditionWeightSelfString
        {
            get;
            set;
        }

        [DataMember]
        public string AIConditionWeightEnemyString
        {
            get;
            set;
        }

        public void Apply(Troop troop)
        {
            if ((troop.Combativity + troop.DecrementOfCombatMethodCombativityConsuming) >= this.Combativity)
            {
                troop.CombatMethodApplied = true;
                troop.DecreaseCombativity(this.Combativity - troop.DecrementOfCombatMethodCombativityConsuming);
                troop.ShowNumber = true;
                foreach (Influence influence in this.Influences.Influences.Values)
                {
                    influence.ApplyInfluence(troop.Leader, Applier.CombatMethod, 0, false);
                }
            }
        }

        public bool IsCastable(Troop troop)
        {
            return Condition.CheckConditionList(this.CastConditions.Conditions.Values, troop);
        }

        public void Purify(Troop troop)
        {
            if (troop.CombatMethodApplied)
            {
                troop.CombatMethodApplied = false;
                foreach (Influence influence in this.Influences.Influences.Values)
                {
                    influence.PurifyInfluence(troop.Leader, Applier.CombatMethod, 0, false);
                }
            }
        }

        public bool SimulateApply(Troop troop)
        {
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                influence.ApplyInfluence(troop.Leader, Applier.CombatMethod, 0, false);
            }
            return true;
        }

        public void SimulatePurify(Troop troop)
        {
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                influence.PurifyInfluence(troop.Leader, Applier.CombatMethod, 0, false);
            }
        }

        [DataMember]
        public bool ArchitectureTarget
        {
            get
            {
                return this.architectureTarget;
            }
            set
            {
                this.architectureTarget = value;
            }
        }

        [DataMember]
        public int AttackDefaultString { get; set; }

        [DataMember]
        public int AttackTargetString { get; set; }
        
        //[DataMember]
        public AttackDefaultKind AttackDefault
        {
            get
            {
                return this.attackDefault;
            }
            set
            {
                this.attackDefault = value;
            }
        }

        //[DataMember]
        public AttackTargetKind AttackTarget
        {
            get
            {
                return this.attackTarget;
            }
            set
            {
                this.attackTarget = value;
            }
        }

        [DataMember]
        public int Combativity
        {
            get
            {
                return this.combativity;
            }
            set
            {
                this.combativity = value;
            }
        }
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        [DataMember]
        public bool ViewingHostile
        {
            get
            {
                return this.viewingHostile;
            }
            set
            {
                this.viewingHostile = value;
            }
        }
        [DataMember]
        public TileAnimationKind AnimationKind
        {
            get
            {
                return this.animationKind;
            }
            set
            {
                this.animationKind = value;
            }
        }
    }
}

