namespace FuzzyLogic.Operations
{
    public class Support : FuzzySetOperation
    {

        public Support(FuzzySet set) : base(set)
        {

        }

        public override double Perform(double value) => Set.MembershipFunction.GetMembershipDegree(value) > 0d ? 1d : 0d;
        
    }
}
