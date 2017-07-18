using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class PersonGeneratorSetting : GameObject
    {
        [DataMember]
        public int femaleChance;
        [DataMember]
        public int ChildrenFemaleChance;
        [DataMember]
        public int bornLo;
        [DataMember]
        public int bornHi;
        [DataMember]
        public int debutLo;
        [DataMember]
        public int debutHi;
        [DataMember]
        public int dieLo;
        [DataMember]
        public int dieHi;
        [DataMember]
        public int debutAtLeast;
    }

}
