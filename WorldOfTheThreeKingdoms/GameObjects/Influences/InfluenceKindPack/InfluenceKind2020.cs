using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2020 : InfluenceKind
    {
        private int increment = 0;
        private int type = 0;

        public override void ApplyInfluenceKind(Faction faction)
        {
            switch (this.type)
            {
                case 0:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfBubing += this.increment;
                    break;

                case 1:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfNubing += this.increment;
                    break;

                case 2:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfQibing += this.increment;
                    break;

                case 3:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfShuijun += this.increment;
                    break;

                case 4:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfQixie += this.increment;
                    break;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
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

        public override void PurifyInfluenceKind(Faction faction)
        {
            switch (this.type)
            {
                case 0:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfBubing -= this.increment;
                    break;

                case 1:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfNubing -= this.increment;
                    break;

                case 2:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfQibing -= this.increment;
                    break;

                case 3:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfShuijun -= this.increment;
                    break;

                case 4:
                    faction.CriticalStrikeChanceIncrementWhileCombatMethodOfQixie -= this.increment;
                    break;
            }
        }
    }
}

