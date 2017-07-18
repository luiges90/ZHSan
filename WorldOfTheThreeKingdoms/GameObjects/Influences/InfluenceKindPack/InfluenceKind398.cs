using GameObjects;
using GameObjects.Influences;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind398 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(null, false, false, false))
            {
                troop.Scenario.SetPositionOnFire(troop.SelfCastPosition);
            }
        }

        public override int GetCreditWithPosition(Troop source, out Point? position)
        {
            //position = 0;
            position = new Point(0, 0);
            TroopList hostileTroopsInView = source.GetHostileTroopsInView();
            TroopList list2 = new TroopList();
            foreach (Troop troop in hostileTroopsInView)
            {
                if ((troop.IsInArchitecture || !troop.DaysToReachPosition(source.Position, 1)) || (troop.Army.Kind.Type == MilitaryType.水军))
                {
                    list2.Add(troop);
                }
            }
            foreach (Troop troop in list2)
            {
                hostileTroopsInView.Remove(troop);
            }
            if (hostileTroopsInView.Count == 0)
            {
                return 0;
            }
            List<Point> orientations = new List<Point>();
            int num = 0;
            foreach (Troop troop in hostileTroopsInView)
            {
                orientations.Add(troop.Position);
                num += troop.FightingForce;
            }
            int num2 = 0;
            int fightingForce = source.FightingForce;
            int num4 = source.TroopIntelligence + source.ChanceIncrementOfStratagem;
            if (num4 > 100)
            {
                num4 = 100;
            }
            num2 = (((GameObject.Square(num4) / 60) * num) / fightingForce) / 100;
            if (num2 > 0)
            {
                GameArea area = new GameArea();
                foreach (Point point in source.GetStratagemArea(source.Position).Area)
                {
                    if (!source.Scenario.PositionIsOnFire(point) && (source.Scenario.IsPositionEmpty(point) && source.Scenario.IsFireVaild(point, false, MilitaryType.步兵)))
                    {
                        area.Area.Add(point);
                    }
                }
                if (area.Count > 0)
                {
                    position = source.Scenario.GetClosestPosition(area, orientations);
                }
                else
                {
                    num2 = 0;
                }
            }
            return num2;
        }
    }
}

