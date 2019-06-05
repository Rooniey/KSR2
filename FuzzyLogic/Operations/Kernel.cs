using System;
using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Operations
{
    public class Kernel : IMembershipFunction
    {
        public Kernel(IMembershipFunction source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public IMembershipFunction Source { get; }

        public double GetMembershipDegree(double columnValue)
            => Math.Abs(Source.GetMembershipDegree(columnValue) - 1d) < Constants.DOUBLE_COMPARISON_TOLERANCE ? 1d : 0d; 

    }
}
