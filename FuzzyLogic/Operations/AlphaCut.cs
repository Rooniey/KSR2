namespace FuzzyLogic.Operations
{
    public class AlphaCut : FuzzySetOperation
    {
        public double Alpha { get; }

        public AlphaCut(FuzzySet set, double alpha) : base(set)
        {
            Alpha = alpha;
        }

        public override double Perform(double value) => Set.MembershipFunction.GetMembershipDegree(value) > Alpha ? 1d : 0d;
    }
}
