using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Operations
{
    public class Support : IMembershipFunction
    {
        public IMembershipFunction Source { get; }

        public Support(IMembershipFunction source)
        {
            Source = source;
        }

        public double GetMembershipDegree(double value) => Source.GetMembershipDegree(value) > 0d ? 1d : 0d; 

    }
}
