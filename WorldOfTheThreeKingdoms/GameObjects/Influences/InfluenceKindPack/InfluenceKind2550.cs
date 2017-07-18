using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2550 : InfluenceKind
    {
        private int rate = 0;
        private int type = 0;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.StratagemOfMillitaryType[this.type] += this.rate;
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
                this.rate = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.StratagemOfMillitaryType[this.type] -= this.rate;
        }
    }
}

