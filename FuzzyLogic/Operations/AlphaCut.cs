using System;
using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Operations
{
    public class AlphaCut : IMembershipFunction
    {
        public AlphaCut(IMembershipFunction source, double alpha)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Alpha = alpha;
        }

        public IMembershipFunction Source { get; }
        public double Alpha { get; }

        public double GetMembershipDegree(double columnValue) => Source.GetMembershipDegree(columnValue) > Alpha ? 1d : 0d;

    }
}
