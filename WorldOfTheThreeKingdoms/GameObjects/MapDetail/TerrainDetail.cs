using GameGlobal;
using GameObjects;
using System;
using GameObjects.TroopDetail;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects.MapDetail
{
    [DataContract]
    public class TerrainDetail : GameObject
    {
        private float fireDamageRate;
        private float foodAutumnRate;
        private int foodDeposit;
        private int foodRegainDays;
        private float foodSpringRate;
        private float foodSummerRate;
        private float foodWinterRate;
        private int graphicLayer;
        private int routewayActiveFundCost;
        private int routewayBuildFundCost;
        private int routewayBuildWorkCost;
        private float routewayConsumptionRate;

        public TerrainTextures Textures = new TerrainTextures();
        private bool viewThrough;
        
        public void Init()
        {
            Textures = new TerrainTextures();
        }

        public int GetFood(GameSeason season)
        {
            int foodDeposit = this.foodDeposit;
            switch (season)
            {
                case GameSeason.春:
                    return (int) (foodDeposit * this.FoodSpringRate);

                case GameSeason.夏:
                    return (int) (foodDeposit * this.FoodSummerRate);

                case GameSeason.秋:
                    return (int) (foodDeposit * this.FoodAutumnRate);

                case GameSeason.冬:
                    return (int) (foodDeposit * this.FoodWinterRate);
            }
            return foodDeposit;
        }

        public int GetRandomFood(GameSeason season)
        {
            int num = GameObject.Random(this.FoodDeposit / 2) + ((this.FoodDeposit * 3) / 4);
            switch (season)
            {
                case GameSeason.春:
                    return (int) (num * this.FoodSpringRate);

                case GameSeason.夏:
                    return (int) (num * this.FoodSummerRate);

                case GameSeason.秋:
                    return (int) (num * this.FoodAutumnRate);

                case GameSeason.冬:
                    return (int) (num * this.FoodWinterRate);
            }
            return num;
        }
        [DataMember]
        public float FireDamageRate
        {
            get
            {
                return this.fireDamageRate;
            }
            set
            {
                this.fireDamageRate = value;
            }
        }

        [DataMember]
        public bool CanExtendInto;

        public string FireDamageRateString
        {
            get
            {
                return StaticMethods.GetPercentString(this.fireDamageRate, 3);
            }
        }
        [DataMember]
        public float FoodAutumnRate
        {
            get
            {
                return this.foodAutumnRate;
            }
            set
            {
                this.foodAutumnRate = value;
            }
        }

        public string FoodAutumnRateString
        {
            get
            {
                return StaticMethods.GetPercentString(this.foodAutumnRate, 3);
            }
        }
        [DataMember]
        public int FoodDeposit
        {
            get
            {
                return this.foodDeposit;
            }
            set
            {
                this.foodDeposit = value;
            }
        }
        [DataMember]
        public int FoodRegainDays
        {
            get
            {
                return this.foodRegainDays;
            }
            set
            {
                this.foodRegainDays = value;
            }
        }
        [DataMember]
        public float FoodSpringRate
        {
            get
            {
                return this.foodSpringRate;
            }
            set
            {
                this.foodSpringRate = value;
            }
        }

        public string FoodSpringRateString
        {
            get
            {
                return StaticMethods.GetPercentString(this.foodSpringRate, 3);
            }
        }
        [DataMember]
        public float FoodSummerRate
        {
            get
            {
                return this.foodSummerRate;
            }
            set
            {
                this.foodSummerRate = value;
            }
        }

        public string FoodSummerRateString
        {
            get
            {
                return StaticMethods.GetPercentString(this.foodSummerRate, 3);
            }
        }
        [DataMember]
        public float FoodWinterRate
        {
            get
            {
                return this.foodWinterRate;
            }
            set
            {
                this.foodWinterRate = value;
            }
        }

        public string FoodWinterRateString
        {
            get
            {
                return StaticMethods.GetPercentString(this.foodWinterRate, 3);
            }
        }
        [DataMember]
        public int GraphicLayer
        {
            get
            {
                return this.graphicLayer;
            }
            set
            {
                this.graphicLayer = value;
            }
        }

        public int RandomRegainDays
        {
            get
            {
                return (GameObject.Random(this.FoodRegainDays / 2) + ((this.FoodRegainDays * 3) / 4));
            }
        }
        [DataMember]
        public int RoutewayActiveFundCost
        {
            get
            {
                return this.routewayActiveFundCost;
            }
            set
            {
                this.routewayActiveFundCost = value;
            }
        }
        [DataMember]
        public int RoutewayBuildFundCost
        {
            get
            {
                return this.routewayBuildFundCost;
            }
            set
            {
                this.routewayBuildFundCost = value;
            }
        }
        [DataMember]
        public int RoutewayBuildWorkCost
        {
            get
            {
                return this.routewayBuildWorkCost;
            }
            set
            {
                this.routewayBuildWorkCost = value;
            }
        }
        [DataMember]
        public float RoutewayConsumptionRate
        {
            get
            {
                return this.routewayConsumptionRate;
            }
            set
            {
                this.routewayConsumptionRate = value;
            }
        }
        [DataMember]
        public bool ViewThrough
        {
            get
            {
                return this.viewThrough;
            }
            set
            {
                this.viewThrough = value;
            }
        }

        private bool? troopPassable = null;
        public bool TroopPassable
        {
            get
            {
                if (troopPassable == null)
                {
                    troopPassable = false;
                    foreach (MilitaryKind mk in Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKindList())
                    {
                        bool passable = false;
                        switch (this.ID)
                        {
                            case 1:
                                passable = mk.PlainAdaptability <= mk.Movability;
                                break;
                            case 2:
                                passable = mk.GrasslandAdaptability <= mk.Movability;
                                break;
                            case 3:
                                passable = mk.ForrestAdaptability <= mk.Movability;
                                break;
                            case 4:
                                passable = mk.MarshAdaptability <= mk.Movability;
                                break;
                            case 5:
                                passable = mk.MountainAdaptability <= mk.Movability;
                                break;
                            case 6:
                                passable = mk.WaterAdaptability <= mk.Movability;
                                break;
                            case 7:
                                passable = mk.RidgeAdaptability <= mk.Movability;
                                break;
                            case 8:
                                passable = mk.WastelandAdaptability <= mk.Movability;
                                break;
                            case 9:
                                passable = mk.DesertAdaptability <= mk.Movability;
                                break;
                            case 10:
                                passable = mk.CliffAdaptability <= mk.Movability;
                                break;
                        }
                        if (passable)
                        {
                            troopPassable = true;
                            break;
                        }
                    }
                }
                return troopPassable.Value;
            }
        }
    }
}

