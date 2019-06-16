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

        //private const string FuzzySetsFolderPath = "../../../FuzzySetFiles";
        private const string DefaultPath = "../../../FuzzySetFiles/fuzzy_sets.sh";

        public static (List<Quantifier>, List<Qualifier>, List<Summarizer>, LogicalOperation) ParseFuzzySetFile(
            int dataCount,
            string path = DefaultPath)
        {
            //path = $"{FuzzySetsFolderPath}/{path}";

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

                    int xmin = 0;
                    int xmax = 100;
                    if (input.Length >= 5)
                    {
                        xmin = Int32.Parse(input[3]);
                        xmax = Int32.Parse(input[4]);
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
                                xmin, xmax));
                            break;
                        case ParsingMode.Summarizer:
                            summarizers.Add(new Summarizer(
                                label,
                                input[1],
                                membership,
                                xmin, xmax));
                            break;
                    }
                }
            }

            return (quantifiers, qualifiers, summarizers, currentSummarizersOperation);
        }

        public static void SaveFuzzySetsToFile(
            List<Quantifier> quants, 
            List<Qualifier> quals,
            List<Summarizer> summs, 
            LogicalOperation op,
            string dest)
        {
            using(var fs = new StreamWriter(dest))
            {
                fs.WriteLine("QUANTIFIERS");
                foreach(var quan in quants)
                {
                    fs.WriteLine($"{quan.Label}:{(quan.IsAbsolute ? "abs" : "rel")}:{GetMembershipString(quan.MembershipFunction)}");
                }
                fs.WriteLine();


                fs.WriteLine("QUALIFIERS");
                foreach (var qual in quals)
                {
                    fs.WriteLine($"{qual.Label}:{qual.Column}:{GetMembershipString(qual.MembershipFunction)}");
                }
                fs.WriteLine();

                fs.WriteLine($"SUMMARIZERS {op.ToString()}");
                foreach (var summ in summs)
                {
                    fs.WriteLine($"{summ.Label}:{summ.Column}:{GetMembershipString(summ.MembershipFunction)}");
                }
                fs.WriteLine();
            }
        }

        public static string GetMembershipString(IMembershipFunction memFun)
        {
            var memFunName = memFun.Name;
            memFunName = memFunName.Replace("triangular", "tri");
            memFunName = memFunName.Replace("trapezoidal", "trap");
            return memFunName;
        }
    }
}
