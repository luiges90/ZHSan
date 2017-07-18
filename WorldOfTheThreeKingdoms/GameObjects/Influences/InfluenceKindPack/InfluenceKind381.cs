using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind381 : InfluenceKind
    {
        private int defence = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.BaseDefenceConsidered = this.defence;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.defence = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                troop.BaseDefenceConsidered = 0;
            }
        }
    }
}

