using GameManager;
using GameObjects;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind396 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.PutOutFire();
        }

        public override int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            int num = 0;
            if (((source.BelongedLegion == null) || Session.Current.Scenario.PositionIsOnFireNoCheck(source.Position)) || (GameObject.Random(source.BelongedLegion.Troops.Count) <= 0))
            {
                int pureFightingForce = source.PureFightingForce;
                int num3 = source.TroopIntelligence + source.ChanceIncrementOfStratagem;
                if (num3 > 100)
                {
                    num3 = 100;
                }
                num3 = GameObject.Square(num3) / 60;
                if (num3 > 0)
                {
                    List<Point> list = new List<Point>();
                    foreach (Point point in source.GetStratagemArea(source.Position).Area)
                    {
                        if (Session.Current.Scenario.PositionIsOnFire(point))
                        {
                            num += num3 / 40;
                            list.Add(point);
                            Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                            if (troopByPosition == source)
                            {
                                num += num3 * 3;
                            }
                            else if (troopByPosition != null)
                            {
                                if (source.IsFriendly(troopByPosition.BelongedFaction))
                                {
                                    num += (num3 * troopByPosition.FightingForce) / pureFightingForce;
                                }
                                else
                                {
                                    num -= (num3 * troopByPosition.FightingForce) / pureFightingForce;
                                }
                            }
                        }
                    }
                    if (num > 0)
                    {
                        position = new Point?(source.Position);
                    }
                }
            }
            return num;
        }
    }
}

