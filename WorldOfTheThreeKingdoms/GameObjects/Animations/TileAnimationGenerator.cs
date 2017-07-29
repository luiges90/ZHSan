using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;



namespace GameObjects.Animations
{

    public class TileAnimationGenerator
    {
        public Dictionary<int, TileAnimation> TileAnimations = new Dictionary<int, TileAnimation>();

        public TileAnimationGenerator()
        {
        }

        public TileAnimation AddTileAnimation(TileAnimationKind kind, Point position, bool looping)
        {
            TileAnimation animation = new TileAnimation();
            animation.Kind = kind;
            animation.Looping = looping;
            animation.Position = position;
            int hashCode = animation.GetHashCode();
            if (!this.TileAnimations.ContainsKey(hashCode))
            {
                animation.LinkedAnimation = Session.Current.Scenario.GameCommonData.AllTileAnimations.GetAnimation((int) kind);
                animation.Drawing = true;
                animation.currentFrameIndex = 0;
                animation.currentStayIndex = 0;
                this.TileAnimations.Add(hashCode, animation);
                return animation;
            }
            return null;
        }

        public void Clear()
        {
            this.TileAnimations.Clear();
        }

        public void ClearFinishedAnimation()
        {
            List<TileAnimation> list = new List<TileAnimation>();
            foreach (TileAnimation animation in this.TileAnimations.Values)
            {
                if (!animation.Drawing)
                {
                    list.Add(animation);
                }
            }
            foreach (TileAnimation animation in list)
            {
                this.TileAnimations.Remove(animation.GetHashCode());
            }
        }

        public bool HasTileAnimation(TileAnimationKind kind, Point position, bool looping)
        {
            TileAnimation animation = new TileAnimation();
            animation.Kind = kind;
            animation.Looping = looping;
            animation.Position = position;
            return this.TileAnimations.ContainsKey(animation.GetHashCode());
        }

        public void RemoveTileAnimation(TileAnimationKind kind, Point position, bool looping)
        {
            TileAnimation animation = new TileAnimation();
            animation.Kind = kind;
            animation.Looping = looping;
            animation.Position = position;
            this.TileAnimations.Remove(animation.GetHashCode());
        }
    }
}

