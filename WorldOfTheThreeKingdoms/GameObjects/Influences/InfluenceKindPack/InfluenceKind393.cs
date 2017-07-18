using GameObjects;
using GameObjects.Influences;
using System;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind393 : InfluenceKind
    {
        private int id = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.SetAmbush();
        }

        public override int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            int num = 0;
            if (!source.IsInHostileTroopView() && !source.IsInHostileArchitectureHighView())
            {
                num += (50 * source.HostileTroopInViewFightingForce) / source.PureFightingForce;
                if (source.OnlyBeDetectedByHighLevelInformation)
                {
                    num *= 3;
                }
                if (num > 0)
                {
                    position = new Point?(source.Position);
                }
            }
            return num;
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

        public override bool IsVaild(Troop troop)
        {
            return troop.AmbushAvail(this.id);
        }
    }
}

