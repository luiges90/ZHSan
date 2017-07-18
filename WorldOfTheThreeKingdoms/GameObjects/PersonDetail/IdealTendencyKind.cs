using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class IdealTendencyKind : GameObject
    {
        private int offset;

        public override string ToString()
        {
            return (base.Name + " " + this.Offset.ToString());
        }
        [DataMember]
        public int Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
    }
}

