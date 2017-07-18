using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1520 : ConditionKind
    {
        private int number = 0;
        private int terrain = 0;

        public override bool CheckConditionKind(Troop troop)
        {
            switch (this.terrain)
            {
                case 1:
                    return (troop.ViewingPlainHostileTroopCount >= this.number);

                case 2:
                    return (troop.ViewingGrasslandHostileTroopCount >= this.number);

                case 3:
                    return (troop.ViewingForestHostileTroopCount >= this.number);

                case 4:
                    return (troop.ViewingMarshHostileTroopCount >= this.number);

                case 5:
                    return (troop.ViewingMountainHostileTroopCount >= this.number);

                case 6:
                    return (troop.ViewingWaterHostileTroopCount >= this.number);

                case 7:
                    return (troop.ViewingRidgeHostileTroopCount >= this.number);

                case 8:
                    return (troop.ViewingWastelandHostileTroopCount >= this.number);

                case 9:
                    return (troop.ViewingDesertHostileTroopCount >= this.number);

                case 10:
                    return (troop.ViewingCliffHostileTroopCount >= this.number);
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

