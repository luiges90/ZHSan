using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind860 : InfluenceKind
    {
        private int id;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop != null && !troop.AllowedStrategems.Contains(id))
            {
                troop.AllowedStrategems.Add(id);
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            if (troop != null && troop.AllowedStrategems.Contains(id))
            {
                troop.AllowedStrategems.Remove(id);
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.id = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

