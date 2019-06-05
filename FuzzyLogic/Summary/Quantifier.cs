using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Summary
{
    public class Quantifier : FuzzySet
    {
        public Quantifier(string label, IMembershipFunction membership, QuantifierType type) : base(label, membership)
        {
            Type = type;
        }

        public QuantifierType Type { get; }

        public bool isAbsolute => Type == QuantifierType.Absolute;
    }

    public enum QuantifierType
    {
        Relative,
        Absolute
    }
}
