using FuzzyLogic.Files;
using GUI.Base;
using GUI.Utility;
using Model;
using System;
using System.Linq;

namespace GUI.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public object _activeViewModel;

        public object ActiveViewModel {
            get => _activeViewModel;
            set => SetProperty(ref _activeViewModel, value);
        }
        
        public RelayCommand<string> Navigate { get; }

        public RelayCommand Load { get; }

        public RelayCommand Save { get; }


        public MainViewModel()
        {

            Navigate = new RelayCommand<string>((dest) => NavigateTo(dest));

            SummaryContext sumContext = SummaryContext.Instance;
            sumContext.Players.AddRange(PlayerDbContext.ReadData());

            Load = new RelayCommand(LoadFuzzySetsFromFile);
            Save = new RelayCommand(SaveFuzzySetsToFile);


            //default file
            var (quants, quals, summs, logicalOperation) = FuzzySetParser.ParseFuzzySetFile(sumContext.Players.Count);
            SummaryContext.Instance.Quantifiers.AddRange(quants.Select(q => new Model.CheckableQuantifier(q, true)));
            SummaryContext.Instance.Qualifiers.AddRange(quals.Select(q => new Model.CheckableQualifier(q, true)));
            SummaryContext.Instance.Summarizers.AddRange(summs.Select(s => new Model.CheckableSummarizer(s, true)));
            SummaryContext.Instance.SummaryOperation = logicalOperation;
        }


        public void NavigateTo(string dest)
        {
            switch(dest) {

                case "SUMMARIZERS":
                    ActiveViewModel = new SummarizersViewModel();
                    break;
                case "QUALIFIERS":
                    ActiveViewModel = new QualifiersViewModel();
                    break;
                case "QUANTIFIERS":
                    ActiveViewModel = new QuantifiersViewModel();
                    break;
                case "SUMMARY":
                    ActiveViewModel = new LinguisticSummaryViewModel();
                    break;
            }
        }

        public void LoadFuzzySetsFromFile()
        {
            string filepath = FileSystemHelper.GetFilePath();
            if (string.IsNullOrEmpty(filepath)) return;
            
            SummaryContext sumContext = SummaryContext.Instance;
            try
            {
                var (quants, quals, summs, logicalOperation) = FuzzySetParser.ParseFuzzySetFile(sumContext.Players.Count, filepath);
                SummaryContext.Instance.Quantifiers.AddRange(quants.Select(q => new Model.CheckableQuantifier(q, true)));
                SummaryContext.Instance.Qualifiers.AddRange(quals.Select(q => new Model.CheckableQualifier(q, true)));
                SummaryContext.Instance.Summarizers.AddRange(summs.Select(s => new Model.CheckableSummarizer(s, true)));
                SummaryContext.Instance.SummaryOperation = logicalOperation;
            } catch(Exception ex)
            {
                Messanger.DisplayError(ex.Message);
            }

            NavigateTo("QUANTIFIERS");
        }

        public void SaveFuzzySetsToFile()
        {
            string dest = FileSystemHelper.GetSaveFilePath();
            if (string.IsNullOrEmpty(dest)) return;

            SummaryContext sumContext = SummaryContext.Instance;

            FuzzySetParser.SaveFuzzySetsToFile(
                sumContext.Quantifiers.Select(q => q.Quantifier).ToList(),
                sumContext.Qualifiers.Select(q => q.Qualifier).ToList(),
                sumContext.Summarizers.Select(q => q.Summarizer).ToList(),
                sumContext.SummaryOperation,
                dest);
        }
    }
}
