using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Operations
{
    public class Complement : IMembershipFunction
    {
        public Complement(IMembershipFunction source)
        {
            Source = source;
        }

        public IMembershipFunction Source { get; }

        public double GetMembershipDegree(double columnValue)
            => 1d - Source.GetMembershipDegree(columnValue);
    }
}
