using System;
using FuzzyLogic.Interfaces;

namespace FuzzyLogic.Summary
{
    public class Summarizer : FuzzySet
    {
        public string Column { get; set; }

        public Summarizer(string label, string column, IMembershipFunction membershipFunction) : base(label, membershipFunction)
        {
            Column = column ?? throw new ArgumentNullException(nameof(column));
        }

    }
}
