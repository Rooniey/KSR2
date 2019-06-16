using FuzzyLogic.Summary;

namespace GUI.Model
{
    public class CheckableQualifier : Checkable
    {
        public Qualifier Qualifier{ get; }

        public CheckableQualifier(Qualifier qual, bool isChecked = false)
        {
            Qualifier = qual;
            IsChecked = isChecked;
        }
    }
}
