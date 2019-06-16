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
        enum SummaryType
        {
            First,
            Second
        }

        static void Main(string[] args)
        {
            double T1_THRESHOLD = 0.7;
            SummaryType typeToGenerate = SummaryType.First;

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

            var dbPlayers = PlayerDbContext.ReadData();

            var (quants, quals, summs, logicalOperation) = FuzzySetParser.ParseFuzzySetFile(players.Count);

            List<LinguisticSummary> summaries;
            if (typeToGenerate == SummaryType.First)
            {
                summaries = SummaryGenerator.GetFirstTypeSummaries(quants, summs, players, logicalOperation);
            }
            else
            {
                summaries = SummaryGenerator.GetSecondTypeSummaries(quants, summs, players, quals, logicalOperation);
            }

            SummaryResultWriter.Write(
                summaries.Select(sum => (sum, new QualityMeasures().CalculateAll(sum)))
                    .Where(t1 => t1.Item2.T1 > T1_THRESHOLD && t1.Item2.Average > 0.6)
                    .OrderByDescending(t1 => t1.Item2.Average));
        }
    }
}