using System;
using System.Globalization;
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

        public TrapezoidalMembershipFunction(string a, string b, string c, string d)
        {
            _a = Double.Parse(a, CultureInfo.InvariantCulture);
            _b = Double.Parse(b,CultureInfo.InvariantCulture);
            _c = Double.Parse(c,CultureInfo.InvariantCulture);
            _d = Double.Parse(d, CultureInfo.InvariantCulture);
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

        public double Cardinality => (Math.Abs(_a - _d) + Math.Abs(_c - _b)) / 2d;
        public double Support => Math.Abs(_d - _a);
    }
}
