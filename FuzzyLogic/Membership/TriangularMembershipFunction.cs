using System;
using System.Globalization;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Membership
{
    public class TriangularMembershipFunction : IMembershipFunction
    {
        public TriangularMembershipFunction(double a, double b, double c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public TriangularMembershipFunction(string a, string b, string c)
        {
            _a = Double.Parse(a, CultureInfo.InvariantCulture);
            _b = Double.Parse(b, CultureInfo.InvariantCulture);
            _c = Double.Parse(c, CultureInfo.InvariantCulture);
        }

        private readonly double _a;
        private readonly double _b;
        private readonly double _c;

        public double GetMembershipDegree(double columnValue)
        {
            double parsed = columnValue;
            if (Math.Abs(parsed - _b) < Constants.DOUBLE_COMPARISON_TOLERANCE)
            {
                return 1d;
            }
            else if (parsed > _a && parsed < _b)
            {
                return 1d / (_b - _a) * parsed + 1d - (1d / (_b - _a)) * _b;
            }
            else if (parsed > _b && parsed < _c)
            {
                return 1d / (_b - _c) * parsed + 1d - (1d / (_b - _c)) * _b;
            }
            else
            {
                return 0d;
            }
        }

        public double Cardinality => Math.Abs(_c - _a) / 2d;
        public double Support => Math.Abs(_c - _a);
    }
}
