using System.Windows;

namespace GUI.Utility
{
    public static class Messanger
    {
        public static void DisplayError(string errorMessage)
        {
            string caption = "Error";
            MessageBox.Show(errorMessage, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
