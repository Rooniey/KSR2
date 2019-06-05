using FuzzyLogic.Data;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Membership
{
    public class TrapezoidalMembershipFunction : IMembershipFunction
    {
        private readonly double _a;
        private readonly double _b;
        private readonly double _c;
        private readonly double _d;

        public TrapezoidalMembershipFunction(double a, double b, double c, double d)
        {
            _a = a;
            _b = b;
            _c = c;
            _d = d;
        }

        public double GetMembershipDegree(double columnValue)
        {
            double parsed = columnValue;           
            if (parsed >= _b && parsed <= _c)
            {
                return 1d;
            }
            else if (parsed > _a && parsed < _b)
            {
                return 1d / (_b - _a) * parsed + 1d - (1d / (_b - _a)) * _b;
            }
            else if (parsed > _c && parsed < _d)
            {
                return 1d / (_c - _d) * parsed + 1d - (1d / (_c - _d)) * _c;
            }
            else
            {
                return 0d;
            }
        }
    }
}
