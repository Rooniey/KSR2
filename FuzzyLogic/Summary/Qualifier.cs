using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Summary
{
    public class Qualifier : FuzzySet
    {
        public string Column { get; set; }

        public Qualifier(string label, string column, IMembershipFunction membershipFunction, double xmin, double xmax) : base(label, membershipFunction, xmin, xmax)
        {
            Column = column ?? throw new ArgumentNullException(nameof(column));
        }

    }
}
