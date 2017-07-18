using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind200 : InfluenceKind
    {
        private int number = 0;
        private int terrain = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop != null){
                switch (this.terrain)
                {
                    case 1:
                        if (troop.DecrementOfPlainAdaptability < this.number)
                        {
                            troop.DecrementOfPlainAdaptability = this.number;
                        }
                        break;

                    case 2:
                        if (troop.DecrementOfGrasslandAdaptability < this.number)
                        {
                            troop.DecrementOfGrasslandAdaptability = this.number;
                        }
                        break;

                    case 3:
                        if (troop.DecrementOfForrestAdaptability < this.number)
                        {
                            troop.DecrementOfForrestAdaptability = this.number;
                        }
                        break;

                    case 4:
                        if (troop.DecrementOfMarshAdaptability < this.number)
                        {
                            troop.DecrementOfMarshAdaptability = this.number;
                        }
                        break;

                    case 5:
                        if (troop.DecrementOfMountainAdaptability < this.number)
                        {
                            troop.DecrementOfMountainAdaptability = this.number;
                        }
                        break;

                    case 6:
                        if (troop.DecrementOfWaterAdaptability < this.number)
                        {
                            troop.DecrementOfWaterAdaptability = this.number;
                        }
                        break;

                    case 7:
                        if (troop.DecrementOfRidgeAdaptability < this.number)
                        {
                            troop.DecrementOfRidgeAdaptability = this.number;
                        }
                        break;

                    case 8:
                        if (troop.DecrementOfWastelandAdaptability < this.number)
                        {
                            troop.DecrementOfWastelandAdaptability = this.number;
                        }
                        break;

                    case 9:
                        if (troop.DecrementOfDesertAdaptability < this.number)
                        {
                            troop.DecrementOfDesertAdaptability = this.number;
                        }
                        break;

                    case 10:
                        if (troop.DecrementOfCliffAdaptability < this.number)
                        {
                            troop.DecrementOfCliffAdaptability = this.number;
                        }
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
                this.number = int.Parse(parameter);
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
                        troop.DecrementOfPlainAdaptability = 0;
                        break;

                    case 2:
                        troop.DecrementOfGrasslandAdaptability = 0;
                        break;

                    case 3:
                        troop.DecrementOfForrestAdaptability = 0;
                        break;

                    case 4:
                        troop.DecrementOfMarshAdaptability = 0;
                        break;

                    case 5:
                        troop.DecrementOfMountainAdaptability = 0;
                        break;

                    case 6:
                        troop.DecrementOfWaterAdaptability = 0;
                        break;

                    case 7:
                        troop.DecrementOfRidgeAdaptability = 0;
                        break;

                    case 8:
                        troop.DecrementOfWastelandAdaptability = 0;
                        break;

                    case 9:
                        troop.DecrementOfDesertAdaptability = 0;
                        break;

                    case 10:
                        troop.DecrementOfCliffAdaptability = 0;
                        break;
                }
            }
        }
    }
}

