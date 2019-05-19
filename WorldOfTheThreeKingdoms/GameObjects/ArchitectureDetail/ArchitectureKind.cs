using GameManager;
using GameObjects;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Runtime.Serialization;
using WorldOfTheThreeKingdoms;



namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class ArchitectureKind : GameObject
    {
        private int agricultureBase;
        private int agricultureUnit;
        private int commerceBase;
        private int commerceUnit;
        private int dominationBase;
        private int dominationUnit;
        private int enduranceBase;
        private int enduranceUnit;
        private int facilityPositionUnit;
        private int foodMaxUnit;
        private int fundMaxUnit;
        private bool hasAgriculture;
        private bool hasCommerce;
        private bool hasDomination;
        private bool hasEndurance;
        private bool hasHarbor;
        private bool hasLongView;
        private bool hasMorale;
        private bool hasObliqueView;
        private bool hasPopulation;
        private bool hasTechnology;
        private int moraleBase;
        private int moraleUnit;
        private int populationBase;
        private int populationBoundary;
        private int populationUnit;
        private int technologyBase;
        private int technologyUnit;
        private PlatformTexture texture;
        public String TextureFileName;
        private int viewDistance;
        private int viewDistanceIncrementDivisor;
        private bool countToMerit;
        private int expandable;
        private bool shipCanEnter;

        public PlatformTexture Texture
        {
            get
            {
                if (this.texture == null)
                {
                    this.texture = CacheManager.GetTempTexture("Content/Textures/Resources/Architecture/" + this.ID.ToString() + ".png");
                }

                return this.texture;
            }
        }

        //public void ClearTexture()
        //{
        //    if (this.texture != null)
        //    {
        //        this.texture.Dispose();
        //        this.texture = null;
        //    }
        //}

        [DataMember]
        public int AgricultureBase
        {
            get
            {
                return this.agricultureBase;
            }
            set
            {
                this.agricultureBase = value;
            }
        }

        [DataMember]
        public int AgricultureUnit
        {
            get
            {
                return this.agricultureUnit;
            }
            set
            {
                this.agricultureUnit = value;
            }
        }

        [DataMember]
        public int CommerceBase
        {
            get
            {
                return this.commerceBase;
            }
            set
            {
                this.commerceBase = value;
            }
        }

        [DataMember]
        public int CommerceUnit
        {
            get
            {
                return this.commerceUnit;
            }
            set
            {
                this.commerceUnit = value;
            }
        }

        [DataMember]
        public int DominationBase
        {
            get
            {
                return this.dominationBase;
            }
            set
            {
                this.dominationBase = value;
            }
        }

        [DataMember]
        public int DominationUnit
        {
            get
            {
                return this.dominationUnit;
            }
            set
            {
                this.dominationUnit = value;
            }
        }

        [DataMember]
        public int EnduranceBase
        {
            get
            {
                return this.enduranceBase;
            }
            set
            {
                this.enduranceBase = value;
            }
        }

        [DataMember]
        public int EnduranceUnit
        {
            get
            {
                return this.enduranceUnit;
            }
            set
            {
                this.enduranceUnit = value;
            }
        }

        [DataMember]
        public int FacilityPositionUnit
        {
            get
            {
                return this.facilityPositionUnit;
            }
            set
            {
                this.facilityPositionUnit = value;
            }
        }

        [DataMember]
        public int FoodMaxUnit
        {
            get
            {
                return this.foodMaxUnit;
            }
            set
            {
                this.foodMaxUnit = value;
            }
        }

        [DataMember]
        public int FundMaxUnit
        {
            get
            {
                return this.fundMaxUnit;
            }
            set
            {
                this.fundMaxUnit = value;
            }
        }

        [DataMember]
        public bool HasAgriculture
        {
            get
            {
                return this.hasAgriculture;
            }
            set
            {
                this.hasAgriculture = value;
            }
        }

        public string HasAgricultureString
        {
            get
            {
                return (this.hasAgriculture ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasCommerce
        {
            get
            {
                return this.hasCommerce;
            }
            set
            {
                this.hasCommerce = value;
            }
        }

        public string HasCommerceString
        {
            get
            {
                return (this.hasCommerce ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasDomination
        {
            get
            {
                return this.hasDomination;
            }
            set
            {
                this.hasDomination = value;
            }
        }

        public string HasDominationString
        {
            get
            {
                return (this.hasDomination ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasEndurance
        {
            get
            {
                return this.hasEndurance;
            }
            set
            {
                this.hasEndurance = value;
            }
        }

        public string HasEnduranceString
        {
            get
            {
                return (this.hasEndurance ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasHarbor
        {
            get
            {
                return this.hasHarbor;
            }
            set
            {
                this.hasHarbor = value;
            }
        }

        public string HasHarborString
        {
            get
            {
                return (this.hasHarbor ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasLongView
        {
            get
            {
                return this.hasLongView;
            }
            set
            {
                this.hasLongView = value;
            }
        }

        public string HasLongViewString
        {
            get
            {
                return (this.hasLongView ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasMorale
        {
            get
            {
                return this.hasMorale;
            }
            set
            {
                this.hasMorale = value;
            }
        }

        public string HasMoraleString
        {
            get
            {
                return (this.hasMorale ? "○" : "×");
            }
        }

        [DataMember]
        public bool HasObliqueView
        {
            get
            {
                return this.hasObliqueView;
            }
            set
            {
                this.hasObliqueView = value;
            }
        }

        public string HasObliqueViewString
        {
            get
            {
                return (this.hasObliqueView ? "○" : "×");
            }
        }
        [DataMember]
        public bool HasPopulation
        {
            get
            {
                return this.hasPopulation;
            }
            set
            {
                this.hasPopulation = value;
            }
        }

        public string HasPopulationString
        {
            get
            {
                return (this.hasPopulation ? "○" : "×");
            }
        }
        [DataMember]
        public bool HasTechnology
        {
            get
            {
                return this.hasTechnology;
            }
            set
            {
                this.hasTechnology = value;
            }
        }

        public string HasTechnologyString
        {
            get
            {
                return (this.hasTechnology ? "○" : "×");
            }
        }
        [DataMember]
        public int MoraleBase
        {
            get
            {
                return this.moraleBase;
            }
            set
            {
                this.moraleBase = value;
            }
        }
        [DataMember]
        public int MoraleUnit
        {
            get
            {
                return this.moraleUnit;
            }
            set
            {
                this.moraleUnit = value;
            }
        }
        [DataMember]
        public int PopulationBase
        {
            get
            {
                return this.populationBase;
            }
            set
            {
                this.populationBase = value;
            }
        }
        [DataMember]
        public int PopulationBoundary
        {
            get
            {
                return this.populationBoundary;
            }
            set
            {
                this.populationBoundary = value;
            }
        }
        [DataMember]
        public int PopulationUnit
        {
            get
            {
                return this.populationUnit;
            }
            set
            {
                this.populationUnit = value;
            }
        }
        [DataMember]
        public int TechnologyBase
        {
            get
            {
                return this.technologyBase;
            }
            set
            {
                this.technologyBase = value;
            }
        }
        [DataMember]
        public int TechnologyUnit
        {
            get
            {
                return this.technologyUnit;
            }
            set
            {
                this.technologyUnit = value;
            }
        }
        [DataMember]
        public int ViewDistance
        {
            get
            {
                return this.viewDistance;
            }
            set
            {
                this.viewDistance = value;
            }
        }
        [DataMember]
        public int ViewDistanceIncrementDivisor
        {
            get
            {
                return this.viewDistanceIncrementDivisor;
            }
            set
            {
                this.viewDistanceIncrementDivisor = value;
            }
        }
        [DataMember]
        public bool CountToMerit
        {
            get
            {
                return this.countToMerit;
            }
            set
            {
                this.countToMerit = value;
            }
        }
        [DataMember]
        public int Expandable
        {
            get
            {
                return this.expandable;
            }
            set
            {
                this.expandable = value;
            }
        }
        [DataMember]
        public bool ShipCanEnter
        {
            get
            {
                return this.shipCanEnter;
            }
            set
            {
                this.shipCanEnter = value;
            }
        }
    }
}
