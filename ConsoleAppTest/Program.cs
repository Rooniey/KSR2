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
                csvReader.Configuration.MissingFieldFound = (headerNames, index, context) => Console.WriteLine("huj");
                csvReader.Configuration.BadDataFound = (context) => Console.WriteLine("DJ wielki huj");
                int i = 0;
                while (csvReader.Read())
                {
                    Player player = csvReader.GetRecord<Player>();
                    players.Add(player);
                    i++;
                }
            }


            var qunatifier = new Quantifier("około połowa", new TrapezoidalMembershipFunction(0.3, 0.4, 0.6,0.8), QuantifierType.Relative);
            var qualifier = new Qualifier("słabą mase", "StandingTackle", new TrapezoidalMembershipFunction(0, 0, 20, 40));
            var summarizer = new Summarizer("słaby wslizg", "SlidingTackle", new TrapezoidalMembershipFunction(0, 0, 20, 40) );
            

            var summary = new LinguisticSummary(qunatifier, qualifier, summarizer, players, true);
            var strsum = summary.GenerateSummarization();

            var t1 = new Measures().CalculateT1(summary);

            StringBuilder result = new StringBuilder(qunatifier.Label);
            result.Append(" days");

            result.Append(" being/having ");
            result.Append(qualifier.Label);
            result.Append(" ");
            result.Append(qualifier.Column);

            

            result.Append(" are/have ");
            result.Append(summarizer.Label);
            result.Append(" ");
            result.Append(summarizer.Column);

            Console.WriteLine(result);
            Console.WriteLine(strsum);
            Console.WriteLine();
        }
    }
}
