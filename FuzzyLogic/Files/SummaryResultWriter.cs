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
        private const string DefaultPath = "results.txt";

        public static void Write(IEnumerable<(LinguisticSummary, MeasuresValues)> summariesWithMeasures, string path = DefaultPath)
        {
            //path = $"{ResultsFolderPath}/{path}";

            using (StreamWriter sw = new StreamWriter(path))
            {
                foreach (var (linguisticSummary, measures) in summariesWithMeasures)
                {
                    sw.WriteLine($"{linguisticSummary.GenerateSummarization()}");
                    sw.Write($"T1={measures.T1:0.##}  T2={measures.T2:0.##}  T3={measures.T3:0.##}  T4={measures.T4:0.##}  T5={measures.T5:0.##}  ");
                    sw.Write($"T6={measures.T6:0.##}  T7={measures.T7:0.##}  T8={measures.T8:0.##}  ");

                    if (measures.T9 != null && measures.T10 != null && measures.T11 != null)
                    {
                        //sw.Write($"T9={measures.T9:0.##}  T10={measures.T10:0.##}  T11={measures.T11:0.##}  ");
                        sw.Write($"T9={measures.T9:0.##}  T10={measures.T10:0.##}  ");
                    }

                    sw.WriteLine($"Tavg={measures.Average:0.##}");
                    sw.WriteLine();

                }
            }
        }
    }
}
