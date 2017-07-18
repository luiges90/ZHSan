using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind572 : InfluenceKind
    {
        private int radius = 1;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.AreaStratagemRadius < this.radius)
            {
                troop.AreaStratagemRadius = this.radius;
            }
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
            troop.AreaStratagemRadius = 0;
        }
    }
}

