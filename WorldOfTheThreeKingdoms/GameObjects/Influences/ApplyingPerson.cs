

namespace GameObjects.Influences
{
    public class ApplyingPerson
    {
        public Person person;
        public Applier applier;
        public int applierID;

        public ApplyingPerson(Person a, Applier p, int i)
        {
            this.person = a;
            this.applier = p;
            this.applierID = i;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ApplyingPerson)) return false;
            ApplyingPerson a = (ApplyingPerson)obj;
            return a.applierID == this.applierID && a.applier.Equals(this.applier) && a.person.Equals(this.person);
        }

        public override int GetHashCode()
        {
            return 158 * this.person.GetHashCode() + 37 * this.applier.GetHashCode() + this.applierID;
        }
    }
}
