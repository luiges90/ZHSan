using GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace GameObjects.TroopDetail
{

    public class PathSearcher
    {
#pragma warning disable CS0067 // The event 'PathSearcher.OnCheckPosition' is never used
        public event CheckPosition OnCheckPosition;
#pragma warning restore CS0067 // The event 'PathSearcher.OnCheckPosition' is never used

        public bool Search(Troop troop, int startingIndex, int count)
        {
            return false;
        }

        public delegate PathResult CheckPosition(Point position, List<Point> middlePath, MilitaryKind kind);
    }
}

