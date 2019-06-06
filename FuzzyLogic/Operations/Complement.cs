namespace FuzzyLogic.Operations
{
    public class Complement : FuzzySetOperation
    {
        public Complement(FuzzySet set) : base(set)
        {

        }

        public override double Perform(double value) => 1d - Set.MembershipFunction.GetMembershipDegree(value);
    }
}
