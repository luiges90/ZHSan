using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6595 : InfluenceKind
    {
        private int rate = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.StealFood += this.rate;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.StealFood -= this.rate;
        }
    }
}

