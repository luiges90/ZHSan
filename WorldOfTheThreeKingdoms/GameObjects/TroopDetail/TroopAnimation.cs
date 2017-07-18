using GameGlobal;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class TroopAnimation
    {
        [DataMember]
        public Dictionary<Point, List<Point>> Animations = new Dictionary<Point, List<Point>>();

        public List<Point> GetDirectionAnimation(Point direction)
        {
            if (Math.Abs(direction.X) > 1)
            {
                direction.X = direction.X > 0 ? 1 : -1;
            }
            if (Math.Abs(direction.Y) > 1)
            {
                direction.Y = direction.Y > 0 ? 1 : -1;
            }
            return this.Animations[direction];
        }

        public void UpdateDirectionAnimations(int width)
        {
            int num;
            this.Animations.Clear();
            List<Point> list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount)), 0));
            }
            this.Animations.Add(new Point(1, 0), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point(0, (int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(0, 1), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount)), 0));
            }
            this.Animations.Add(new Point(-1, 0), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point(0, (int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(0, -1), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount)), (int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(1, 1), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount)), (int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(-1, 1), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((width * num) / GlobalVariables.TroopMoveFrameCount)), (int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(1, -1), list);
            list = new List<Point>();
            for (num = 1; num < GlobalVariables.TroopMoveFrameCount; num++)
            {
                list.Add(new Point((int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount)), (int) Math.Round((decimal) ((-width * num) / GlobalVariables.TroopMoveFrameCount))));
            }
            this.Animations.Add(new Point(-1, -1), list);
        }
    }
}

