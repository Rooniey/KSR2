using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic.Summary;

namespace FuzzyLogic.Files
{
    public class SummaryResultWriter
    {
        private const string ResultsFolderPath = "../../../Results";
        public const string DefaultPath = "results.txt";

        public static void Write(IEnumerable<(LinguisticSummary, MeasuresValues)> summariesWithMeasures, string path = DefaultPath)
        {
            path = $"{ResultsFolderPath}/{path}";

            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var (linguisticSummary, measures) in summariesWithMeasures)
                {
                    sw.WriteLine(
                        $"{linguisticSummary.GenerateSummarization()} T1: [{measures.T1:F2}]  T1-T5: [{measures.T1T5:F2}]  T1-T11: [{measures.T1T11:F2}]");
                }
            }
        }
    }
}
