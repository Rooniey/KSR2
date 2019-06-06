using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic.Interfaces;
using FuzzyLogic.Membership;
using FuzzyLogic.Summary;

namespace FuzzyLogic.Files
{
    public static class FuzzySetParser
    {
        private enum ParsingMode
        {
            Quantifier,
            Qualifier,
            Summarizer
        }

        private const string FuzzySetsFolderPath = "../../../FuzzySetFiles";
        private const string DefaultPath = "fuzzy_sets.sh";

        public static (List<Quantifier>, List<Qualifier>, List<Summarizer>, LogicalOperation) ParseFuzzySetFile(
            int dataCount,
            string path = DefaultPath)
        {
            path = $"{FuzzySetsFolderPath}/{path}";

            var quantifiers = new List<Quantifier>();
            var qualifiers = new List<Qualifier>();
            var summarizers = new List<Summarizer>();

            var currentParsingMode = ParsingMode.Quantifier;
            var currentSummarizersOperation = LogicalOperation.And;

            using (StreamReader sr = File.OpenText(path))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine().Trim();

                    if(line.StartsWith("#") || String.IsNullOrWhiteSpace(line)) continue;

                    if (line.StartsWith("QUALIFIERS"))
                    {
                        currentParsingMode = ParsingMode.Qualifier;
                        continue;
                    }

                    if (line.StartsWith("SUMMARIZERS"))
                    {
                        currentParsingMode = ParsingMode.Summarizer;
                        if (line.ToLowerInvariant().Contains("or")) currentSummarizersOperation = LogicalOperation.Or;
                        continue;
                    }

                    if (line.StartsWith("QUANTIFIERS"))
                    {
                        currentParsingMode = ParsingMode.Quantifier;
                        continue;
                    }
                        

//                    var parameters = line.Split(':');
                    var input = line.Split(new[] { ':' }, StringSplitOptions.None);

                    string label = input[0];
                    string membershipExpression = input[2];
                    var memInfo = membershipExpression.Split(new[] {'(', ')'});
                    string memType = memInfo[0];
                    var memParams = memInfo[1].Split(',');
                    IMembershipFunction membership;
                    if (memType.ToLowerInvariant().StartsWith("trap"))
                    {
                        membership = new TrapezoidalMembershipFunction(memParams[0], memParams[1], memParams[2], memParams[3]);
                    }
                    else
                    {
                        membership = new TriangularMembershipFunction(memParams[0], memParams[1], memParams[2]);
                    }

                    switch (currentParsingMode)
                    {
                        case ParsingMode.Quantifier:
                            bool isAbsolute = input[1].ToLowerInvariant().StartsWith("a");
                            quantifiers.Add(new Quantifier(
                                label,
                                membership,
                                isAbsolute ? QuantifierType.Absolute : QuantifierType.Relative,
                                0,
                                isAbsolute ? dataCount : 1));
                            break;
                        case ParsingMode.Qualifier:
                            qualifiers.Add(new Qualifier(
                                label,
                                input[1],
                                membership,
                                0, 100));
                            break;
                        case ParsingMode.Summarizer:
                            summarizers.Add(new Summarizer(
                                label,
                                input[1],
                                membership,
                                0, 100) { });
                            break;
                    }
                }
            }

            return (quantifiers, qualifiers, summarizers, currentSummarizersOperation);
        }
    }
}
