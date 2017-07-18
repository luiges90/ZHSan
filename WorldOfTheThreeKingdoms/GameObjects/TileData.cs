using GameObjects.Influences;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace GameObjects
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TileData
    {
        public Architecture TileArchitecture;
        public Troop TileTroop;
        public int TroopCount;
        public List<AreaInfluenceData> AreaInfluenceList;
        public List<Troop> ContactingTroops;
        public List<Troop> OffencingTroops;
        public List<Troop> StratagemingTroops;
        public List<Troop> ViewingTroops;
        public List<Routeway> TileRouteways;
        public List<RoutePoint> SupplyingRoutePoints;
        public List<Architecture> SupplyingArchitectures;
        public List<Architecture> ViewingArchitectures;
        public List<Architecture> HighViewingArchitectures;
        public void AddContactingTroop(Troop troop)
        {
            if (this.ContactingTroops == null)
            {
                this.ContactingTroops = new List<Troop>();
            }
            if (this.ContactingTroops.IndexOf(troop) >= 0)
            {
                this.ContactingTroops.Remove(troop);
            }
            this.ContactingTroops.Add(troop);
        }

        public void AddOffencingTroop(Troop troop)
        {
            if (this.OffencingTroops == null)
            {
                this.OffencingTroops = new List<Troop>();
            }
            if (this.OffencingTroops.IndexOf(troop) >= 0)
            {
                this.OffencingTroops.Remove(troop);
            }
            this.OffencingTroops.Add(troop);
        }

        public void AddStratagemingTroop(Troop troop)
        {
            if (this.StratagemingTroops == null)
            {
                this.StratagemingTroops = new List<Troop>();
            }
            if (this.StratagemingTroops.IndexOf(troop) >= 0)
            {
                this.StratagemingTroops.Remove(troop);
            }
            this.StratagemingTroops.Add(troop);
        }

        public void AddViewingTroop(Troop troop)
        {
            if (this.ViewingTroops == null)
            {
                this.ViewingTroops = new List<Troop>();
            }
            if (this.ViewingTroops.IndexOf(troop) >= 0)
            {
                this.ViewingTroops.Remove(troop);
            }
            this.ViewingTroops.Add(troop);
        }

        public void RemoveContactingTroop(Troop troop)
        {
            if (this.ContactingTroops != null)
            {
                this.ContactingTroops.Remove(troop);
            }
        }

        public void RemoveOffencingTroop(Troop troop)
        {
            if (this.OffencingTroops != null)
            {
                this.OffencingTroops.Remove(troop);
            }
        }

        public void RemoveStratagemingTroop(Troop troop)
        {
            if (this.StratagemingTroops != null)
            {
                this.StratagemingTroops.Remove(troop);
            }
        }

        public void RemoveViewingTroop(Troop troop)
        {
            if (this.ViewingTroops != null)
            {
                this.ViewingTroops.Remove(troop);
            }
        }

        public int HostileContactingTroopsCount(Faction faction)
        {
            if (this.ContactingTroops == null)
            {
                return 0;
            }
            int num = 0;
            foreach (Troop troop in this.ContactingTroops)
            {
                if (((faction == null) && (troop.BelongedFaction != faction)) || ((faction != null) && !faction.IsFriendly(troop.BelongedFaction)))
                {
                    num++;
                }
            }
            return num;
        }

        public int HostileOffencingTroopsCount(Faction faction)
        {
            if (this.OffencingTroops == null)
            {
                return 0;
            }
            int num = 0;
            foreach (Troop troop in this.OffencingTroops)
            {
                if (((faction == null) && (troop.BelongedFaction != faction)) || ((faction != null) && !faction.IsFriendly(troop.BelongedFaction)))
                {
                    num++;
                }
            }
            return num;
        }

        public int GetPositionHostileOffencingDiscredit(Troop troop)
        {
            if (this.OffencingTroops == null)
            {
                return 0;
            }
            int num = 0;
            foreach (Troop troop2 in this.OffencingTroops)
            {
                if (!troop.IsFriendly(troop2.BelongedFaction))
                {
                    if (troop.BelongedFaction != null)
                    {
                        if (troop.BelongedFaction.IsPositionKnown(troop2.Position))
                        {
                            num += troop2.GetAttackTroopDiscredit(troop);
                        }
                    }
                    else if (troop.ViewArea.HasPoint(troop2.Position))
                    {
                        num += troop2.GetAttackTroopDiscredit(troop);
                    }
                }
            }
            return num;
        }

        public int HostileStratagemingTroopsCount(Faction faction)
        {
            if (this.StratagemingTroops == null)
            {
                return 0;
            }
            int num = 0;
            foreach (Troop troop in this.StratagemingTroops)
            {
                if (((faction == null) && (troop.BelongedFaction != faction)) || ((faction != null) && !faction.IsFriendly(troop.BelongedFaction)))
                {
                    num++;
                }
            }
            return num;
        }

        public int HostileViewingTroopsCount(Faction faction)
        {
            if (this.ViewingTroops == null)
            {
                return 0;
            }
            int num = 0;
            foreach (Troop troop in this.ViewingTroops)
            {
                if (((faction == null) && (troop.BelongedFaction != faction)) || ((faction != null) && !faction.IsFriendly(troop.BelongedFaction)))
                {
                    num++;
                }
            }
            return num;
        }

        public bool IsTroopViewing(Troop troop)
        {
            if (this.ViewingTroops != null)
            {
                foreach (Troop troop2 in this.ViewingTroops)
                {
                    if (troop2 == troop)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddAreaInfluence(Troop owner, AreaInfluenceKind kind, int offset, float rate, Troop troop)
        {
            if (this.AreaInfluenceList == null)
            {
                this.AreaInfluenceList = new List<AreaInfluenceData>();
            }
            AreaInfluenceData item = new AreaInfluenceData();
            item.Owner = owner;
            item.Kind = kind;
            item.Offset = offset;
            item.Rate = rate;
            this.AreaInfluenceList.Add(item);
            if (troop != null)
            {
                item.ApplyAreaInfluence(troop);
            }
        }

        public void RemoveAreaInfluence(Troop owner, Troop troop)
        {
            if (this.AreaInfluenceList != null)
            {
                List<AreaInfluenceData> list = new List<AreaInfluenceData>();
                foreach (AreaInfluenceData data in this.AreaInfluenceList)
                {
                    if (data.Owner == owner)
                    {
                        list.Add(data);
                    }
                }
                foreach (AreaInfluenceData data in list)
                {
                    if (troop != null)
                    {
                        data.PurifyAreaInfluence(troop);
                    }
                    this.AreaInfluenceList.Remove(data);
                }
            }
        }

        public void AddTileRouteway(Routeway routeway)
        {
            if (this.TileRouteways == null)
            {
                this.TileRouteways = new List<Routeway>();
            }
            if (this.TileRouteways.IndexOf(routeway) >= 0)
            {
                this.TileRouteways.Remove(routeway);
            }
            this.TileRouteways.Add(routeway);
        }

        public void RemoveTileRouteway(Routeway routeway)
        {
            if (this.TileRouteways != null)
            {
                this.TileRouteways.Remove(routeway);
            }
        }

        public void AddSupplyingRoutePoint(RoutePoint routePoint)
        {
            if (this.SupplyingRoutePoints == null)
            {
                this.SupplyingRoutePoints = new List<RoutePoint>();
            }
            if (this.SupplyingRoutePoints.IndexOf(routePoint) >= 0)
            {
                this.SupplyingRoutePoints.Remove(routePoint);
            }
            this.SupplyingRoutePoints.Add(routePoint);
        }

        public void RemoveSupplyingRoutePoint(RoutePoint routePoint)
        {
            if (this.SupplyingRoutePoints != null)
            {
                this.SupplyingRoutePoints.Remove(routePoint);
            }
        }

        public void AddSupplyingArchitecture(Architecture a)
        {
            if (this.SupplyingArchitectures == null)
            {
                this.SupplyingArchitectures = new List<Architecture>();
            }
            if (this.SupplyingArchitectures.IndexOf(a) >= 0)
            {
                this.SupplyingArchitectures.Remove(a);
            }
            this.SupplyingArchitectures.Add(a);
        }

        public void RemoveSupplyingArchitecture(Architecture a)
        {
            if (this.SupplyingArchitectures != null)
            {
                this.SupplyingArchitectures.Remove(a);
            }
        }

        public void AddViewingArchitecture(Architecture a)
        {
            if (this.ViewingArchitectures == null)
            {
                this.ViewingArchitectures = new List<Architecture>();
            }
            if (this.ViewingArchitectures.IndexOf(a) >= 0)
            {
                this.ViewingArchitectures.Remove(a);
            }
            this.ViewingArchitectures.Add(a);
        }

        public void RemoveViewingArchitecture(Architecture a)
        {
            if (this.ViewingArchitectures != null)
            {
                this.ViewingArchitectures.Remove(a);
            }
        }

        public void AddHighViewingArchitecture(Architecture a)
        {
            if (this.HighViewingArchitectures == null)
            {
                this.HighViewingArchitectures = new List<Architecture>();
            }
            if (this.HighViewingArchitectures.IndexOf(a) >= 0)
            {
                this.HighViewingArchitectures.Remove(a);
            }
            this.HighViewingArchitectures.Add(a);
        }

        public void RemoveHighViewingArchitecture(Architecture a)
        {
            if (this.HighViewingArchitectures != null)
            {
                this.HighViewingArchitectures.Remove(a);
            }
        }

        public override string ToString()
        {
            string str = "";
            if (this.TileArchitecture != null)
            {
                str = "建筑(" + this.TileArchitecture.Name + ")";
            }
            if (this.TileTroop != null)
            {
                str = str + "部队(" + this.TileTroop.DisplayName + ")";
            }
            return str;
        }
    }
}

