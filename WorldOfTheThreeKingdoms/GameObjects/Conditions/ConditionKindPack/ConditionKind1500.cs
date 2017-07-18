using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1500 : ConditionKind
    {
        private int number = 0;
        private int terrain = 0;

        public override bool CheckConditionKind(Troop troop)
        {
            switch (this.terrain)
            {
                case 1:
                    return (troop.ViewingPlainFriendlyTroopCount >= this.number);

                case 2:
                    return (troop.ViewingGrasslandFriendlyTroopCount >= this.number);

                case 3:
                    return (troop.ViewingForestFriendlyTroopCount >= this.number);

                case 4:
                    return (troop.ViewingMarshFriendlyTroopCount >= this.number);

                case 5:
                    return (troop.ViewingMountainFriendlyTroopCount >= this.number);

                case 6:
                    return (troop.ViewingWaterFriendlyTroopCount >= this.number);

                case 7:
                    return (troop.ViewingRidgeFriendlyTroopCount >= this.number);

                case 8:
                    return (troop.ViewingWastelandFriendlyTroopCount >= this.number);

                case 9:
                    return (troop.ViewingDesertFriendlyTroopCount >= this.number);

                case 10:
                    return (troop.ViewingCliffFriendlyTroopCount >= this.number);
            }
            return false;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.terrain = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.number = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

