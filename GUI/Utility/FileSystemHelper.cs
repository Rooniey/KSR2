using Microsoft.Win32;

namespace GUI.Utility
{
    public static class FileSystemHelper
    {
        public static string GetSaveFilePath()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".txt";
            bool result = sfd.ShowDialog() ?? false;
            return result ? sfd.FileName : "";
        }

        public static string GetFilePath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            bool result = dlg.ShowDialog() ?? false;
            return result ? dlg.FileName : "";
        }

    }
}
