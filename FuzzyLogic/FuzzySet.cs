using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    public class FuzzySet
    {
        public String Label { get; }
        public IMembershipFunction MembershipFunction { get; }
        public double Xmin { get; }
        public double Xmax { get; }

        public FuzzySet(string label, IMembershipFunction membershipFunction, double xmin, double xmax)
        {
            Label = label ?? throw new ArgumentNullException(nameof(label));
            MembershipFunction = membershipFunction ?? throw new ArgumentNullException(nameof(membershipFunction));
            Xmin = xmin;
            Xmax = xmax;
        }

        public double X => Xmax - Xmin;

        public double CalculateMembership(double value)
        {
            return MembershipFunction.GetMembershipDegree(value);
        }

        public double DegreeOfFuzziness() => MembershipFunction.Support / X;


    }
}
