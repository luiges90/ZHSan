

using System.Runtime.Serialization;

namespace GameObjects.Influences
{
    [DataContract]
    public class ApplyingTroop
    {
        [DataMember]
        public Troop troop;
        [DataMember]
        public Applier applier;
        [DataMember]
        public int applierID;

        public ApplyingTroop(Troop a, Applier p, int i)
        {
            this.troop = a;
            this.applier = p;
            this.applierID = i;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ApplyingTroop)) return false;
            ApplyingTroop a = (ApplyingTroop)obj;
            return a.applierID == this.applierID && a.applier.Equals(this.applier) && a.troop.Equals(this.troop);
        }

        public override int GetHashCode()
        {
            return 158 * this.troop.GetHashCode() + 37 * this.applier.GetHashCode() + this.applierID;
        }
    }
}
