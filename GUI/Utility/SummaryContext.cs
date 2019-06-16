using FuzzyLogic.Summary;
using GUI.Model;
using Model;
using System;
using System.Collections.Generic;

namespace GUI.Utility
{
    public class SummaryContext
    {
        private static Lazy<SummaryContext> _lazy = new Lazy<SummaryContext>(() => new SummaryContext());

        public static SummaryContext Instance => _lazy.Value;


        public List<CheckableQuantifier> Quantifiers { get; }
        public List<CheckableSummarizer> Summarizers { get; }
        public List<CheckableQualifier> Qualifiers { get; }

        public List<Player> Players { get; }

        public LogicalOperation SummaryOperation { get; set; }


        public SummaryContext()
        {
            Quantifiers = new List<CheckableQuantifier>();
            Summarizers = new List<CheckableSummarizer>();
            Qualifiers = new List<CheckableQualifier>();
            Players = new List<Player>();
        }

    }
}
