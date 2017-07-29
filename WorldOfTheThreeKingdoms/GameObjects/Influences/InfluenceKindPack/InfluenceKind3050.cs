using GameManager;
using GameObjects;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3050 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            if (architecture.BelongedFaction != null)
            {
                architecture.BelongedFaction.RemoveArchitectureKnownData(architecture);
                if (architecture.AutoRefillFoodInLongViewArea)
                {
                    foreach (Point point in architecture.LongViewArea.Area)
                    {
                        if (!Session.Current.Scenario.PositionOutOfRange(point))
                        {
                            Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveSupplyingArchitecture(architecture);
                        }
                    }
                }
                architecture.IncrementOfViewRadius += this.increment;
                architecture.ViewArea = null;
                architecture.LongViewArea = null;
                if (!Session.Current.Scenario.Preparing)
                {
                    foreach (Architecture architecture2 in Session.Current.Scenario.Architectures)
                    {
                        architecture2.RefreshViewArea();
                    }
                    foreach (Troop troop in Session.Current.Scenario.Troops)
                    {
                        troop.RefreshViewArchitectureRelatedArea();
                    }
                }
                if (architecture.AutoRefillFoodInLongViewArea)
                {
                    foreach (Point point in architecture.LongViewArea.Area)
                    {
                        if (!Session.Current.Scenario.PositionOutOfRange(point))
                        {
                            Session.Current.Scenario.MapTileData[point.X, point.Y].AddSupplyingArchitecture(architecture);
                        }
                    }
                }
                architecture.BelongedFaction.AddArchitectureKnownData(architecture);
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            if (architecture.BelongedFaction != null)
            {
                architecture.BelongedFaction.RemoveArchitectureKnownData(architecture);
                if (architecture.AutoRefillFoodInLongViewArea)
                {
                    foreach (Point point in architecture.LongViewArea.Area)
                    {
                        if (!Session.Current.Scenario.PositionOutOfRange(point))
                        {
                            Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveSupplyingArchitecture(architecture);
                        }
                    }
                }
                architecture.IncrementOfViewRadius -= this.increment;
                architecture.ViewArea = null;
                architecture.LongViewArea = null;
                if (!Session.Current.Scenario.Preparing)
                {
                    foreach (Architecture architecture2 in Session.Current.Scenario.Architectures)
                    {
                        architecture2.RefreshViewArea();
                    }
                    foreach (Troop troop in Session.Current.Scenario.Troops)
                    {
                        troop.RefreshViewArchitectureRelatedArea();
                    }
                }
                if (architecture.AutoRefillFoodInLongViewArea)
                {
                    foreach (Point point in architecture.LongViewArea.Area)
                    {
                        if (!Session.Current.Scenario.PositionOutOfRange(point))
                        {
                            Session.Current.Scenario.MapTileData[point.X, point.Y].AddSupplyingArchitecture(architecture);
                        }
                    }
                }
                architecture.BelongedFaction.AddArchitectureKnownData(architecture);
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.increment * this.increment * (a.FrontLine ? 1 : 0.001) * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

