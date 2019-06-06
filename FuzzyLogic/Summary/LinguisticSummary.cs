using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace FuzzyLogic.Summary
{
    public class LinguisticSummary
    {
        public Quantifier Quantifier { get; set; }
        public Qualifier Qualifier { get; set; }
        public List<Summarizer> Summarizers { get; set; }
        public LogicalOperation LogicalOperationSummarizer { get; set; }

        public List<Player> Data { get; set; }

        public LinguisticSummary(Quantifier quantifier, Qualifier qualifier, List<Summarizer> summarizers, List<Player> data)
        {
            Quantifier = quantifier;
            Qualifier = qualifier;
            Summarizers = summarizers;
            Data = data;
        }

        public string GenerateSummarization()
        {
            StringBuilder result = new StringBuilder(Quantifier.Label);
            result.Append($" players");


            

            if (Qualifier != null)
            {
                result.Append(" being/having ");
                result.Append(Qualifier.Label);
                result.Append(" ");
                result.Append(Qualifier.Column);
            }
            

            result.Append(" are/have ");
            result.Append(Summarizers[0].Label);
            result.Append(" ");
            result.Append(Summarizers[0].Column);

            bool isAnd = LogicalOperationSummarizer == LogicalOperation.And;

            foreach (var summarizer in Summarizers.Skip(1))
            {
                result.Append($" {(isAnd ? "and" : "or")} ");
                result.Append(summarizer.Label);
                result.Append(" ");
                result.Append(summarizer.Column);
            }

            return result.ToString();
        }
    }

    public enum LogicalOperation
    {
        And, Or
    }

    
}
