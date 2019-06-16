using FuzzyLogic.Interfaces;
using FuzzyLogic.Summary;
using GUI.Base;
using GUI.Model;
using GUI.Utility;
using System.Collections.ObjectModel;

namespace GUI.ViewModels
{
    public class QuantifiersViewModel : BindableBase
    {
        public ObservableCollection<CheckableQuantifier> Quantifiers { get; }

        public MembershipViewModel MembershipViewModel { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateQuantifier { get; }


        private string _label;

        public string Label
        {
            get => _label;
            set => SetProperty(ref _label, value);
        }

        private double _xMin = 0d;

        public double XMin
        {
            get => _xMin;
            set => SetProperty(ref _xMin, value);
        }

        private double _xMax = 100d;

        public double XMax
        {
            get => _xMax;
            set => SetProperty(ref _xMax, value);
        }

        private bool _isAbsolute = false;

        public bool IsAbsolute
        {
            get => _isAbsolute;
            set => SetProperty(ref _isAbsolute, value);
        }


        public QuantifiersViewModel()
        {
            MembershipViewModel = new MembershipViewModel();
            Quantifiers = new ObservableCollection<CheckableQuantifier>(
                SummaryContext.Instance.Quantifiers);
            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateQuantifier = new RelayCommand(() => CreateQuantifierFromForm()); 

        }

        public void CreateQuantifierFromForm()
        {
            if (string.IsNullOrEmpty(Label))
            {
                Messanger.DisplayError("Unable to create quantifier withour label");
                return;
            }

            IMembershipFunction memFun = MembershipViewModel.GetMembershipFunction();
            if (memFun == null)
            {
                Messanger.DisplayError("Unable to create membership function with the given parameters");
                return;
            }

            var quan = new Quantifier(Label, memFun, IsAbsolute ? QuantifierType.Absolute : QuantifierType.Relative, XMin, XMax);
            var checkableQuan = new CheckableQuantifier(quan, true);


            SummaryContext.Instance.Quantifiers.Add(checkableQuan);
            Quantifiers.Add(checkableQuan);

        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var quan in Quantifiers)
            {
                quan.IsChecked = isChecked;
            }
        }
    }
}
