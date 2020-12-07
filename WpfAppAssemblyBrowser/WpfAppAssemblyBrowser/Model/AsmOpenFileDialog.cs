using Microsoft.Win32;

namespace WpfAppAssemblyBrowser.Model
{
    public class AsmOpenFileDialog
    {
        public string FileName { get; private set; }

        public void OpenFile()
        {
            var openFileDialog = new OpenFileDialog {Filter = "Assembly|*.dll"};
            var openFileResult = openFileDialog.ShowDialog() ?? false;
            if (openFileResult)
            {
                FileName = openFileDialog.FileName;
            }
        }
    }
}