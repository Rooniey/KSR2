using FuzzyLogic.Interfaces;
using FuzzyLogic.Summary;
using GUI.Base;
using GUI.Model;
using GUI.Utility;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GUI.ViewModels
{
    public class QualifiersViewModel : BindableBase
    {
        public ObservableCollection<CheckableQualifier> Qualifiers { get; }

        public ObservableCollection<Checkable> Attributes { get; }


        public MembershipViewModel MembershipViewModel { get; }

        public RelayCommand SelectAll { get; }
        public RelayCommand DeselectAll { get; }
        public RelayCommand CreateQualifier { get; }

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

        public QualifiersViewModel()
        {
            MembershipViewModel = new MembershipViewModel();
            Qualifiers = new ObservableCollection<CheckableQualifier>
                (SummaryContext.Instance.Qualifiers);

            List<string> excludedProperties = new List<string>() { "Id", "Name" };
            Attributes = new ObservableCollection<Checkable>(typeof(Player).GetProperties().Where(p => !excludedProperties.Contains(p.Name)).Select(p => new Checkable() { Name = p.Name }));

            SelectAll = new RelayCommand(() => SetAllCheckables(true));
            DeselectAll = new RelayCommand(() => SetAllCheckables(false));
            CreateQualifier = new RelayCommand(() => CreateQualifierFromForm());
        }

        private void CreateQualifierFromForm()
        {
            if (string.IsNullOrEmpty(Label))
            {
                Messanger.DisplayError("Unable to create quantifier withour label");
                return;
            }

            var selected = Attributes.Where(a => a.IsChecked).ToList();

            foreach(var at in selected)
            {
                IMembershipFunction memFun = MembershipViewModel.GetMembershipFunction();
                if (memFun == null)
                {
                    Messanger.DisplayError("Unable to create membership function with the given parameters");
                    return;
                }

                var qual = new Qualifier(Label, at.Name, memFun, XMin, XMax);
                var checkableQual = new CheckableQualifier(qual, true);


                SummaryContext.Instance.Qualifiers.Add(checkableQual);
                Qualifiers.Add(checkableQual);
            }


        }

        public void SetAllCheckables(bool isChecked)
        {
            foreach (var quan in Qualifiers)
            {
                quan.IsChecked = isChecked;
            }
        }
    }
}
