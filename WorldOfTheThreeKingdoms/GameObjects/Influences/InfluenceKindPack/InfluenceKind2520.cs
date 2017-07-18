using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2520 : InfluenceKind
    {
        private float rate = 0;
        private int type = 0;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.ArchitectureDamageOfMillitaryType[this.type] += this.rate;
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
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.ArchitectureDamageOfMillitaryType[this.type] -= this.rate;
        }
    }
}

