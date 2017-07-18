using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind220 : InfluenceKind
    {
        private int increment = 0;
        private int terrain = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                switch (this.terrain)
                {
                    case 1:
                        troop.CriticalChanceIncrementOfPlain += this.increment;
                        break;

                    case 2:
                        troop.CriticalChanceIncrementOfGrassland += this.increment;
                        break;

                    case 3:
                        troop.CriticalChanceIncrementOfForrest += this.increment;
                        break;

                    case 4:
                        troop.CriticalChanceIncrementOfMarsh += this.increment;
                        break;

                    case 5:
                        troop.CriticalChanceIncrementOfMountain += this.increment;
                        break;

                    case 6:
                        troop.CriticalChanceIncrementOfWater += this.increment;
                        break;

                    case 7:
                        troop.CriticalChanceIncrementOfRidge += this.increment;
                        break;

                    case 8:
                        troop.CriticalChanceIncrementOfWasteland += this.increment;
                        break;

                    case 9:
                        troop.CriticalChanceIncrementOfDesert += this.increment;
                        break;

                    case 10:
                        troop.CriticalChanceIncrementOfCliff += this.increment;
                        break;
                }
            }
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
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                switch (this.terrain)
                {
                    case 1:
                        troop.CriticalChanceIncrementOfPlain -= this.increment;
                        break;

                    case 2:
                        troop.CriticalChanceIncrementOfGrassland -= this.increment;
                        break;

                    case 3:
                        troop.CriticalChanceIncrementOfForrest -= this.increment;
                        break;

                    case 4:
                        troop.CriticalChanceIncrementOfMarsh -= this.increment;
                        break;

                    case 5:
                        troop.CriticalChanceIncrementOfMountain -= this.increment;
                        break;

                    case 6:
                        troop.CriticalChanceIncrementOfWater -= this.increment;
                        break;

                    case 7:
                        troop.CriticalChanceIncrementOfRidge -= this.increment;
                        break;

                    case 8:
                        troop.CriticalChanceIncrementOfWasteland -= this.increment;
                        break;

                    case 9:
                        troop.CriticalChanceIncrementOfDesert -= this.increment;
                        break;

                    case 10:
                        troop.CriticalChanceIncrementOfCliff -= this.increment;
                        break;
                }
            }
        }


    }
}

