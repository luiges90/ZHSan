using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class PersonDialog
    {
        [DataMember]
        public int SpeakingPersonID { get; set; }

        public Person SpeakingPerson;

        [DataMember]
        public string Text;

        public override string ToString()
        {
            if (this.SpeakingPerson == null)
            {
                return ("---- " + this.Text);
            }
            return (this.SpeakingPerson.Name + " " + this.Text);
        }
    }
}

