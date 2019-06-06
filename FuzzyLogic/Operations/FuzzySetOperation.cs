using System;

namespace FuzzyLogic.Operations
{
    public abstract class FuzzySetOperation
    {
        public FuzzySet Set { get; }

        protected FuzzySetOperation(FuzzySet set)
        {
            Set = set ?? throw new ArgumentNullException(nameof(set));
        }

        public abstract double Perform(double value);
    }
}
