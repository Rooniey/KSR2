using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic.Summary;
using Model;

namespace FuzzyLogic.utility
{
    public class SummaryGenerator
    {
        public static List<LinguisticSummary> GetFirstTypeSummaries(List<Quantifier> quantifiers, List<Summarizer> summarizers, List<Player> data, LogicalOperation logicalOperation)
        {
            List<LinguisticSummary> summaries = new List<LinguisticSummary>();

            foreach (var (quan, summs, players) in GetSummaryQuantifierCombinations(quantifiers, summarizers, data))
            {
                summaries.Add(new LinguisticSummary(quan, null, summs, players) { LogicalOperationSummarizer = logicalOperation });
            }

            return summaries.ToList();
        }

        public static List<LinguisticSummary> GetSecondTypeSummaries(List<Quantifier> quantifiers, List<Summarizer> summarizers, List<Player> data, List<Qualifier> qualifiers, LogicalOperation logicalOperation)
        {
            List<LinguisticSummary> summaries = new List<LinguisticSummary>();
            List<Quantifier> quans = quantifiers.Where(q => !q.IsAbsolute).ToList();

            foreach (var (quan, summs, players) in GetSummaryQuantifierCombinations(quans, summarizers, data))
            {
                foreach (var qualifier in qualifiers)
                {
                    summaries.Add(new LinguisticSummary(quan, qualifier, summs, players) { LogicalOperationSummarizer = logicalOperation });
                }
            }

            return summaries.ToList();
        }

        private static IEnumerable<(Quantifier, List<Summarizer>, List<Player>)> GetSummaryQuantifierCombinations(List<Quantifier> quantifiers, List<Summarizer> summarizers, List<Player> data)
        {
            foreach (var quantifier in quantifiers)
            {
                for (int i = 1; i <= 2; i++)
                {
                    var indexLists = Enumerable.Range(0, summarizers.Count).GetKCombs(i);

                    foreach (var indexList in indexLists)
                    {
                        List<Summarizer> summarizersToAdd = new List<Summarizer>();
                        foreach (var index in indexList)
                        {
                            summarizersToAdd.Add(summarizers[index]);
                        }

                        yield return (quantifier, summarizersToAdd, data);
                    }
                }
            }
            
        }
    }

   
}
