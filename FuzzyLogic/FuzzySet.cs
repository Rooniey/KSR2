using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic
{
    public class FuzzySet
    {
        public String Label { get; set; }
        public IMembershipFunction MembershipFunction { get; set; }

        public FuzzySet(string label, IMembershipFunction membershipFunction)
        {
            Label = label;
            MembershipFunction = membershipFunction;
        }
    }
}
