using FuzzyLogic.Summary;

namespace GUI.Model
{
    public class CheckableSummarizer : Checkable
    {
        public Summarizer Summarizer { get; }

        public CheckableSummarizer(Summarizer summ, bool isChecked = false)
        {
            Summarizer = summ;
            IsChecked = isChecked;
        }
    }
}
