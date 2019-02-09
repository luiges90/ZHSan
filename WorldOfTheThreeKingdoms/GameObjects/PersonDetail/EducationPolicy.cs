using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace GameObjects.PersonDetail
{
    [DataContract]
	public class EducationPolicy : GameObject
	{
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public float Command { get; set; }
        [DataMember]
        public float Strength { get; set; }
        [DataMember]
        public float Intelligence { get; set; }
        [DataMember]
        public float Politics { get; set; }
        [DataMember]
        public float Glamour { get; set; }
        [DataMember]
        public float Skill { get; set; }
        [DataMember]
        public float Stunt { get; set; }
        [DataMember]
        public float Title { get; set; }

        public Dictionary<int, float> Weighting
        {
            get
            {
                Dictionary<int, float> dict = new Dictionary<int, float>();
                dict.Add(1, this.Command);
                dict.Add(2, this.Strength);
                dict.Add(3, this.Intelligence);
                dict.Add(4, this.Politics);
                dict.Add(5, this.Glamour);
                dict.Add(6, this.Skill);
                dict.Add(7, this.Stunt);
                dict.Add(8, this.Title);
                return dict;
            }
        }

        public float WeightSum
        {
            get
            {
                return this.Command + this.Strength + this.Intelligence + this.Politics + this.Glamour + this.Skill + this.Stunt + this.Title;
            }
        }
	}
}
