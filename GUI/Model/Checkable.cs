using GUI.Base;

namespace GUI.Model
{
    public class Checkable : BindableBase
    {
        public string Name { get; set;  } = "quantifiers";

        private bool _isChecked;
        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); } 
       
    }
}
