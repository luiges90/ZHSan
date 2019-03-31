using GameObjects;
using GameObjects.Animations;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class Stratagem : GameObject
    {
        private TileAnimationKind animationKind;
        private bool architectureTarget;
        private CastDefaultKind castDefault;
        private CastTargetKind castTarget;
        private int chance;
        private int combativity;
        private string description;
        private bool friendly;

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

        public InfluenceTable Influences = new InfluenceTable();
        
        public ConditionTable CastConditions = new ConditionTable();
        
        public Dictionary<Condition, float> AIConditionWeightSelf = new Dictionary<Condition, float>();
        
        public Dictionary<Condition, float> AIConditionWeightEnemy = new Dictionary<Condition, float>();

        public void Init()
        {
            Influences = new InfluenceTable();
            CastConditions = new ConditionTable();
            AIConditionWeightSelf = new Dictionary<Condition, float>();
            AIConditionWeightEnemy = new Dictionary<Condition, float>();
        }

        private bool self;
        private int techniquePoint;

#pragma warning disable CS0169 // The field 'Stratagem.requireInfluenceToUse' is never used
        private bool requireInfluenceToUse;
#pragma warning restore CS0169 // The field 'Stratagem.requireInfluenceToUse' is never used

        public void Apply(Troop troop)
        {
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                influence.ApplyInfluence(troop, Applier.Stratagem, 0);
            }
        }

        public int GetCredit(Troop source, Troop destination)
        {
            if (!source.HasStratagem(this.ID)) { return 0; }
            int num = 0;
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                num += influence.GetCredit(source, destination);
            }
            return num;
        }

        public GameObjectList GetCastConditionList()
        {
            return this.CastConditions.GetConditionList();
        }

        public bool IsCastable(Troop troop)
        {
            return Condition.CheckConditionList(this.CastConditions.Conditions.Values, troop);
        }

        public string CastConditionString
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.CastConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                return str;
            }
        }

        public int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            int num = 0;
            List<Point?> list = new List<Point?>();
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                Point? nullable = null;
                num += influence.GetCreditWithPosition(source, out nullable);
                list.Add(nullable);
            }
            if (list.Count > 0)
            {
                position = list[0];
            }
            return num;
        }

        public bool IsValid(Troop troop)
        {
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                if (!influence.IsVaild(troop))
                {
                    return false;
                }
            }
            return true;
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
        public int CastDefaultString { get; set; }

        [DataMember]
        public int CastTargetString { get; set; }

        public CastDefaultKind CastDefault
        {
            get
            {
                return this.castDefault;
            }
            set
            {
                this.castDefault = value;
            }
        }
        
        public CastTargetKind CastTarget
        {
            get
            {
                return this.castTarget;
            }
            set
            {
                this.castTarget = value;
            }
        }
        [DataMember]
#pragma warning disable CS0108 // 'Stratagem.Chance' hides inherited member 'GameObject.Chance(int)'. Use the new keyword if hiding was intended.
        public int Chance
#pragma warning restore CS0108 // 'Stratagem.Chance' hides inherited member 'GameObject.Chance(int)'. Use the new keyword if hiding was intended.
        {
            get
            {
                return this.chance;
            }
            set
            {
                this.chance = value;
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
        public bool Friendly
        {
            get
            {
                return this.friendly;
            }
            set
            {
                this.friendly = value;
            }
        }
        [DataMember]
        public bool Self
        {
            get
            {
                return this.self;
            }
            set
            {
                this.self = value;
            }
        }
        [DataMember]
        public int TechniquePoint
        {
            get
            {
                return this.techniquePoint;
            }
            set
            {
                this.techniquePoint = value;
            }
        }
        [DataMember]
        public bool RequireInfluenceToUse
        {
            get;
            set;
        }
    }
}

