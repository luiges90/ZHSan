using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class BiographyAdjectives : GameObject
    {
        [DataMember]
        public int Strength { get; set; }
        [DataMember]
        public int Command { get; set; }
        [DataMember]
        public int Intelligence { get; set; }
        [DataMember]
        public int Politics { get; set; }
        [DataMember]
        public int Glamour { get; set; }
        [DataMember]
        public int Braveness { get; set; }
        [DataMember]
        public int Calmness { get; set; }
        [DataMember]
        public int PersonalLoyalty { get; set; }
        [DataMember]
        public int Ambition { get; set; }
        [DataMember]
        public Boolean Male { get; set; }
        [DataMember]
        public Boolean Female { get; set; }

        private List<String> text = new List<string>();
        [DataMember]
        public List<String> Text { get { return text; } set { text = value; } }

        private List<String> suffixText = new List<string>();
        [DataMember]
        public List<String> SuffixText { get { return suffixText; } set { suffixText = value; } }
    }
}

