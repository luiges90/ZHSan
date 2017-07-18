using GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace GameObjects.TroopDetail
{

    public class PathSearcher
    {
        public event CheckPosition OnCheckPosition;

        public bool Search(Troop troop, int startingIndex, int count)
        {
            return false;
        }

        public delegate PathResult CheckPosition(Point position, List<Point> middlePath, MilitaryKind kind);
    }
}

