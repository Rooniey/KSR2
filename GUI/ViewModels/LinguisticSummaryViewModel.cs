using FuzzyLogic.Files;
using FuzzyLogic.Summary;
using FuzzyLogic.utility;
using GUI.Base;
using GUI.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
    public class LinguisticSummaryViewModel : BindableBase
    {
        private double _t1Threshold = 0.3d;

        public double T1Threshold {
            get => _t1Threshold;
            set => SetProperty(ref _t1Threshold, value);
        }


        public string[] SummarizersOperationsList { get; set; } = new[] { "AND", "OR" };

        private LogicalOperation _summarizersOperation;
        private string _currentSummarizersOperation = "AND";

        public string SummarizersOperation
        {
            get { return _currentSummarizersOperation; }
            set
            {
                _summarizersOperation = value == "AND" ? LogicalOperation.And : LogicalOperation.Or;
                SetProperty(ref _currentSummarizersOperation, value);
            }
        }

        private string _generationStep;

        public string GenerationStep
        {
            get => _generationStep;
            set => SetProperty(ref _generationStep, value);
        }

        public int SummarizersCount { get; }
        public int QualifiersCount { get; }
        public int QuantifiersCount { get; }


        public AsyncCommand GenerateLingusiticSummaryFirstForm { get; }
        public AsyncCommand GenerateLingusiticSummarySecondForm { get; }


        public LinguisticSummaryViewModel()
        {
            SummarizersCount = SummaryContext.Instance.Summarizers.Count(sum => sum.IsChecked);
            QualifiersCount = SummaryContext.Instance.Qualifiers.Count(qual => qual.IsChecked);
            QuantifiersCount = SummaryContext.Instance.Quantifiers.Count(quan => quan.IsChecked);

            GenerateLingusiticSummaryFirstForm = new AsyncCommand(async () =>
                await Task.Run(GenerateFirstFormSummary));
            GenerateLingusiticSummarySecondForm = new AsyncCommand(async () =>
                await Task.Run(GenerateSecondFormSummary));

        }

        private void GenerateFirstFormSummary()
        {
            var destPath = FileSystemHelper.GetSaveFilePath();
            if (string.IsNullOrEmpty(destPath)) return;

            var quants = SummaryContext.Instance.Quantifiers.Where(q => q.IsChecked).Select(q => q.Quantifier).ToList();
            var summs = SummaryContext.Instance.Summarizers.Where(q => q.IsChecked).Select(s => s.Summarizer).ToList();
            var logicalOperation = _summarizersOperation;
            var players = SummaryContext.Instance.Players;

            GenerationStep = "Generating summaries";

            var summaries = SummaryGenerator.GetFirstTypeSummaries(quants, summs, players, logicalOperation);

            GenerationStep = $"Generated {summaries.Count} summaries";

            SummaryResultWriter.Write(
                summaries.Select((sum, i) => {
                        GenerationStep = $"Calculating quality measures {i}/{summaries.Count}";
                        return (sum, new QualityMeasures().CalculateAll(sum));
                    })
                    .Where(t1 => t1.Item2.T1 >= _t1Threshold)
                    .OrderByDescending(m => m.Item2.T1),
                destPath);

            GenerationStep = "Done";
        }

        private void GenerateSecondFormSummary()
        {
            var destPath = FileSystemHelper.GetSaveFilePath();
            if (string.IsNullOrEmpty(destPath)) return;

            var quants = SummaryContext.Instance.Quantifiers.Where(q => q.IsChecked).Select(q => q.Quantifier).ToList();
            var quals = SummaryContext.Instance.Qualifiers.Where(q => q.IsChecked).Select(q => q.Qualifier).ToList();
            var summs = SummaryContext.Instance.Summarizers.Where(q => q.IsChecked).Select(s => s.Summarizer).ToList();
            var players = SummaryContext.Instance.Players;
            var logicalOperation = _summarizersOperation;

            List<LinguisticSummary> summaries;

            GenerationStep = "Generating summaries";

            summaries = SummaryGenerator.GetSecondTypeSummaries(quants, summs, players, quals, logicalOperation);

            GenerationStep = $"Generated {summaries.Count} summaries";

            SummaryResultWriter.Write(
                summaries.Select( (sum, i) => {
                        GenerationStep = $"Calculating quality measures {i}/{summaries.Count}";
                        return (sum, new QualityMeasures().CalculateAll(sum));
                    })
                    .Where(t1 => t1.Item2.T1 >= _t1Threshold)
                    .OrderByDescending(t1 => t1.Item2.T1),
                destPath);
        }

    }
}
