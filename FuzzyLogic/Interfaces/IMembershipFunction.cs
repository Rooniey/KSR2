using FuzzyLogic.Data;

namespace FuzzyLogic.Interfaces
{
    public interface IMembershipFunction
    {
        double GetMembershipDegree(double columnValue);
    }
}
