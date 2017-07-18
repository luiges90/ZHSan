using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Animations
{
    [DataContract]
    public class AnimationTable
    {
        [DataMember]
        public Dictionary<int, Animation> Animations = new Dictionary<int, Animation>();

        public bool AddAnimation(Animation animation)
        {
            if (this.Animations.ContainsKey(animation.ID))
            {
                return false;
            }
            this.Animations.Add(animation.ID, animation);
            return true;
        }

        public void Clear()
        {
            this.Animations.Clear();
        }

        public Animation GetAnimation(int animationID)
        {
            Animation animation = null;
            this.Animations.TryGetValue(animationID, out animation);
            return animation;
        }

        public GameObjectList GetAnimationList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Animation animation in this.Animations.Values)
            {
                list.Add(animation);
            }
            return list;
        }
    }
}

