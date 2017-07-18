using GameObjects;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.InteropServices;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind392 : InfluenceKind
    {
        private int days = 1;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if ((troop.Scenario.IsPlayer(troop.BelongedFaction) && !troop.Auto) && ((((troop.StartingArchitecture == null) || (troop.StartingArchitecture.BelongedFaction != troop.BelongedFaction)) || (troop.StartingArchitecture.BelongedSection == null)) || !troop.StartingArchitecture.BelongedSection.AIDetail.AutoRun))
            {
                troop.Investigate(this.days);
            }
            else
            {
                troop.BelongedLegion.SetInformationPosition();
                if (troop.BelongedLegion.InformationDestination.HasValue)
                {
                    troop.SelfCastPosition = troop.BelongedLegion.InformationDestination.Value;
                    troop.BelongedLegion.InformationDestination = null;
                    troop.Investigate(this.days);
                }
            }
        }

        public override int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);

            if (source == null || source.BelongedLegion == null || source.BelongedLegion.Troops == null
                || source.BelongedLegion.WillArchitecture == null || source.BaseViewArea == null) return 0;
            
            if (((source.BelongedLegion != null) && (GameObject.Random(source.BelongedLegion.Troops.Count) == 0)) && ((source.BelongedLegion.WillArchitecture.BelongedFaction != null) && !source.BelongedFaction.IsArchitectureKnown(source.BelongedLegion.WillArchitecture)))
            {
                bool flag = false;
                foreach (Point point in source.BaseViewArea.Area)
                {
                    Architecture architectureByPosition = source.Scenario.GetArchitectureByPosition(point);
                    if (source.BelongedLegion.WillArchitecture == architectureByPosition)
                    {
                        flag = true;
                        break;
                    }
                }
                if (!(!flag || source.BelongedLegion.InformationDestination.HasValue))
                {
                    position = new Point?(source.Position);
                    return 150;
                }
            }
            return 0;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.days = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

