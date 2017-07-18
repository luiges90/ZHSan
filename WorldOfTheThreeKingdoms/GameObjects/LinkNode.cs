using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class LinkNode
    {
        [DataMember]
        public Architecture A;
        [DataMember]
        public double Distance;
        [DataMember]
        public LinkKind Kind;
        [DataMember]
        public int Level;
        [DataMember]
        public List<Architecture> Path = new List<Architecture>();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { this.Level, " ", this.Kind, " ", Math.Round(this.Distance, 1) }));
            foreach (Architecture architecture in this.Path)
            {
                builder.Append(" " + architecture.Name);
            }
            return builder.ToString();
        }
    }
}

