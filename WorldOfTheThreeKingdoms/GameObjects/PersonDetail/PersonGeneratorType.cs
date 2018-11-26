using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class PersonGeneratorType : GameObject
    {
        [DataMember]
        public int commandLo;
        [DataMember]
        public int commandHi;
        [DataMember]
        public int strengthLo;
        [DataMember]
        public int strengthHi;
        [DataMember]
        public int intelligenceLo;
        [DataMember]
        public int intelligenceHi;
        [DataMember]
        public int politicsLo;
        [DataMember]
        public int politicsHi;
        [DataMember]
        public int glamourLo;
        [DataMember]
        public int glamourHi;
        [DataMember]
        public int braveLo;
        [DataMember]
        public int braveHi;
        [DataMember]
        public int calmnessLo;
        [DataMember]
        public int calmnessHi;
        [DataMember]
        public int personalLoyaltyLo;
        [DataMember]
        public int personalLoyaltyHi;
        [DataMember]
        public int ambitionLo;
        [DataMember]
        public int ambitionHi;
        [DataMember]
        public int generationChance;
        [DataMember]
        public bool affectedByRateParameter;
        [DataMember]
        public int titleChance;
        [DataMember]
        public int genderFix;
        [DataMember]
        public int CostFund { get; set; }
        [DataMember]
        public int TypeCount { get; set; }
        [DataMember]
        public int FactionLimit { get; set; }
    }
}
