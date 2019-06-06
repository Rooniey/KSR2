using System;

namespace FuzzyLogic.Operations
{
    public class Kernel : FuzzySetOperation
    {
        public Kernel(FuzzySet set) : base(set)
        {

        }

        public override double Perform(double value)
            => Set.MembershipFunction.GetMembershipDegree(value) > 1d ? 1d : 0d;
    }
}
