using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyLogic;
using FuzzyLogic.Membership;
using FuzzyLogic.Summary;
using CsvHelper;
using FuzzyLogic.Files;
using FuzzyLogic.utility;
using Model;

namespace ConsoleAppTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var players = new List<Player>();
            using (var textReader = File.OpenText("../../../raw_data/parsed_fifa.csv"))
            {
                CsvReader csvReader = new CsvReader(textReader);
                csvReader.Configuration.Delimiter = ",";
                csvReader.Configuration.MissingFieldFound = null;

                int i = 0;
                while (csvReader.Read())
                {
                    Player player = csvReader.GetRecord<Player>();
                    players.Add(player);
                    i++;
                }
            }

            var (quants, quals, summs, logicalOperation) = FuzzySetParser.ParseFuzzySetFile(players.Count);

//            var summaries = SummaryGenerator.GetFirstTypeSummaries(quants, summs, players, logicalOperation);
            var summaries = SummaryGenerator.GetSecondTypeSummaries(quants, summs, players, quals, logicalOperation);

            var seld = summaries.Select(sum => (sum, new QualityMeasures().CalculateAll(sum)))
                .Where(t1 => t1.Item2.T1 > 0.1);

            foreach (var (linguisticSummary, measuresValues) in seld)
            {
                Console.WriteLine($"{linguisticSummary.GenerateSummarization()}: {measuresValues.T1:F2}, {measuresValues.T2:F2}, {measuresValues.T6:F2}");
            }


            var qunatifier = new Quantifier("około 5000", new TriangularMembershipFunction(3000, 4000, 5000), QuantifierType.Absolute, 0, players.Count);
//            var qualifier = new Qualifier("słabą mase", "StandingTackle", new TrapezoidalMembershipFunction(70, 80, 90, 100), 0, 100);
            var summarizer = new Summarizer("słaby wslizg", "SlidingTackle", new TrapezoidalMembershipFunction(60, 70, 75, 90), 0, 100 );
            

            var summary = new LinguisticSummary(qunatifier, null, new List<Summarizer>(){ summarizer }, players);
            var strsum = summary.GenerateSummarization();

            var quality = new QualityMeasures().CalculateAll(summary);


        }
    }
}
