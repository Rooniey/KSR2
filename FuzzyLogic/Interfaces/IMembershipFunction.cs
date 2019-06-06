
namespace FuzzyLogic.Interfaces
{
    public interface IMembershipFunction
    {
        double GetMembershipDegree(double columnValue);
        double Cardinality { get; }
        double Support { get; }
    }
}
