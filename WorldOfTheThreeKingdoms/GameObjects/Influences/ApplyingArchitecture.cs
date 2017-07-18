

namespace GameObjects.Influences
{
    public class ApplyingArchitecture
    {
        public Architecture arch;
        public Applier applier;
        public int applierID;

        public ApplyingArchitecture(Architecture a, Applier p, int i)
        {
            this.arch = a;
            this.applier = p;
            this.applierID = i;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ApplyingArchitecture)) return false;
            ApplyingArchitecture a = (ApplyingArchitecture) obj;
            return a.applierID == this.applierID && a.applier.Equals(this.applier) && a.arch.Equals(this.arch);
        }

        public override int GetHashCode()
        {
            return 158 * this.arch.GetHashCode() + 37 * this.applier.GetHashCode() + this.applierID;
        }
    }
}
