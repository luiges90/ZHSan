using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.SectionDetail
{
    [DataContract]
    public class SectionAIDetail : GameObject
    {
        private bool allowNewMilitary;
        private bool allowFoodTransfer;
        private bool allowFundTransfer;
        private bool allowInvestigateTactics;
        private bool allowMilitaryTransfer;
        private bool allowOffensiveCampaign;
        private bool allowOffensiveTactics;
        private bool allowPersonTactics;
        private bool autoRun;
        private string description;
        private SectionOrientationKind orientationKind;
        private bool valueAgriculture;
        private bool valueCommerce;
        private bool valueDomination;
        private bool valueEndurance;
        private bool valueMorale;
        private bool valueNewMilitary;
        private bool valueOffensiveCampaign;
        private bool valueRecruitment;
        private bool valueTechnology;
        private bool valueTraining;

        private bool allowFacilityRemoval;

        [DataMember]
        public bool AllowFoodTransfer
        {
            get
            {
                return this.allowFoodTransfer;
            }
            set
            {
                this.allowFoodTransfer = value;
            }
        }

        [DataMember]
        public bool AllowFundTransfer
        {
            get
            {
                return this.allowFundTransfer;
            }
            set
            {
                this.allowFundTransfer = value;
            }
        }

        [DataMember]
        public bool AllowInvestigateTactics
        {
            get
            {
                return this.allowInvestigateTactics;
            }
            set
            {
                this.allowInvestigateTactics = value;
            }
        }

        [DataMember]
        public bool AllowMilitaryTransfer
        {
            get
            {
                return this.allowMilitaryTransfer;
            }
            set
            {
                this.allowMilitaryTransfer = value;
            }
        }
        [DataMember]
        public bool AllowOffensiveCampaign
        {
            get
            {
                return this.allowOffensiveCampaign;
            }
            set
            {
                this.allowOffensiveCampaign = value;
            }
        }
        [DataMember]
        public bool AllowOffensiveTactics
        {
            get
            {
                return this.allowOffensiveTactics;
            }
            set
            {
                this.allowOffensiveTactics = value;
            }
        }
        [DataMember]
        public bool AllowPersonTactics
        {
            get
            {
                return this.allowPersonTactics;
            }
            set
            {
                this.allowPersonTactics = value;
            }
        }
        [DataMember]
        public bool AutoRun
        {
            get
            {
                return this.autoRun;
            }
            set
            {
                this.autoRun = value;
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
        public SectionOrientationKind OrientationKind
        {
            get
            {
                return this.orientationKind;
            }
            set
            {
                this.orientationKind = value;
            }
        }

        public string OrientationKindString
        {
            get
            {
                return this.orientationKind.ToString();
            }
        }
        [DataMember]
        public bool ValueAgriculture
        {
            get
            {
                return this.valueAgriculture;
            }
            set
            {
                this.valueAgriculture = value;
            }
        }
        [DataMember]
        public bool ValueCommerce
        {
            get
            {
                return this.valueCommerce;
            }
            set
            {
                this.valueCommerce = value;
            }
        }
        [DataMember]
        public bool ValueDomination
        {
            get
            {
                return this.valueDomination;
            }
            set
            {
                this.valueDomination = value;
            }
        }
        [DataMember]
        public bool ValueEndurance
        {
            get
            {
                return this.valueEndurance;
            }
            set
            {
                this.valueEndurance = value;
            }
        }
        [DataMember]
        public bool ValueMorale
        {
            get
            {
                return this.valueMorale;
            }
            set
            {
                this.valueMorale = value;
            }
        }
        [DataMember]
        public bool ValueNewMilitary
        {
            get
            {
                return this.valueNewMilitary;
            }
            set
            {
                this.valueNewMilitary = value;
            }
        }
        [DataMember]
        public bool ValueOffensiveCampaign
        {
            get
            {
                return this.valueOffensiveCampaign;
            }
            set
            {
                this.valueOffensiveCampaign = value;
            }
        }
        [DataMember]
        public bool ValueRecruitment
        {
            get
            {
                return this.valueRecruitment;
            }
            set
            {
                this.valueRecruitment = value;
            }
        }
        [DataMember]
        public bool ValueTechnology
        {
            get
            {
                return this.valueTechnology;
            }
            set
            {
                this.valueTechnology = value;
            }
        }
        [DataMember]
        public bool ValueTraining
        {
            get
            {
                return this.valueTraining;
            }
            set
            {
                this.valueTraining = value;
            }
        }
        [DataMember]
        public bool AllowFacilityRemoval
        {
            get
            {
                return this.allowFacilityRemoval;
            }
            set
            {
                this.allowFacilityRemoval = value;
            }
        }
        [DataMember]
        public bool AllowNewMilitary
        {
            get
            {
                return this.allowNewMilitary;
            }
            set
            {
                this.allowNewMilitary = value;
            }
        }
    }
}

