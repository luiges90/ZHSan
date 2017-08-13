using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class TitleKind : GameObject
    {
        [DataMember]
        public bool Combat { get; set; }
        [DataMember]
        public int StudyDay { get; set; }
        [DataMember]
        public int SuccessRate { get; set; }
        [DataMember]
        public bool Recallable { get; set; }
        [DataMember]
        public bool RandomTeachable { get; set; }

        private bool? inheritable;
        public bool IsInheritable(TitleTable allTitles)
        {
            if (inheritable.HasValue)
            {
                return inheritable.Value;
            }
            foreach (Title t in allTitles.GetTitleList())
            {
                if (t.Kind.Equals(this) && t.CanBeBorn())
                {
                    inheritable = true;
                    return true;
                }
            }
            inheritable = false;
            return false;
        }
    }
}

