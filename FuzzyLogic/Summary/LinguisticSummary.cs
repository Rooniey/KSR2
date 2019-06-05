using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace FuzzyLogic.Summary
{
    public class LinguisticSummary
    {
        public Quantifier Quantifier { get; set; }
        public Qualifier Qualifier { get; set; }
        public Summarizer Summarizer { get; set; }
        
        public List<Player> Data { get; set; }
        public bool IsAbsolute { get; set; }

        public LinguisticSummary(Quantifier quantifier, Qualifier qualifier, Summarizer summarizer, List<Player> data, bool isAbsolute = true)
        {
            Quantifier = quantifier;
            Qualifier = qualifier;
            Summarizer = summarizer;
            Data = data;
            IsAbsolute = isAbsolute;
        }

        public string GenerateSummarization()
        {
            StringBuilder result = new StringBuilder(Quantifier.Label);
            result.Append($" players");


            result.Append(" being/having ");
            result.Append(Qualifier.Label);
            result.Append(" ");
            result.Append(Qualifier.Column);

            result.Append(" are/have ");
            result.Append(Summarizer.Label);
            result.Append(" ");
            result.Append(Summarizer.Column);

            return result.ToString();
        }
    }
}
