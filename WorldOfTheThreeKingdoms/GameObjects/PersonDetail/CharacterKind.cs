using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class CharacterKind : GameObject
    {
        private int challengeChance;
        private int controversyChance;
        private float intelligenceRate;

        public void Init()
        {
            generationChance = new int[10];
        }

        [DataMember]
        public int ChallengeChance
        {
            get
            {
                return this.challengeChance;
            }
            set
            {
                this.challengeChance = value;
            }
        }

        [DataMember]
        public int ControversyChance
        {
            get
            {
                return this.controversyChance;
            }
            set
            {
                this.controversyChance = value;
            }
        }

        [DataMember]
        public float IntelligenceRate
        {
            get
            {
                return this.intelligenceRate;
            }
            set
            {
                this.intelligenceRate = value;
            }
        }

        private int[] generationChance = new int[10];

        [DataMember]
        public int[] GenerationChance
        {
            get
            {
                return generationChance;
            }
            set
            {
                generationChance = value;
            }
        }
    }
}

