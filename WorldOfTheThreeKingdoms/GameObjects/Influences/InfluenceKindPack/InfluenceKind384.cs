using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind384 : InfluenceKind
    {
        private int radius = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.BaseAreaAttackRadius = this.radius;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.radius = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.BaseAreaAttackRadius = 0;
        }

    }
}

