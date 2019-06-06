using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Summary
{
    public class Quantifier : FuzzySet
    {
        public Quantifier(string label, IMembershipFunction membership, QuantifierType type, double xmin, double xmax) : base(label, membership, xmin, xmax)
        {
            Type = type;
        }

        public QuantifierType Type { get; }

        public bool IsAbsolute => Type == QuantifierType.Absolute;
    }

    public enum QuantifierType
    {
        Relative,
        Absolute
    }
}
