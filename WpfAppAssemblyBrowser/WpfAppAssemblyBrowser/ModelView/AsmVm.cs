using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WpfAppAssemblyBrowser.Annotations;
using WpfAppAssemblyBrowser.Model;

namespace WpfAppAssemblyBrowser.ModelView
{
    public class AsmVm : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnAssemblyPathChanged([CallerMemberName] string path = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(path));
            }
        }
        
                private string _filePath;
        
                public string FilePath
                {
                    get { return _filePath; }
                    set
                    {
                        _filePath = value;
                        OnAssemblyPathChanged();
                    }
                }
        
        
                public ICommand Browse
                {
                    get
                    {
                        return new ClickCommand((obj) =>
                        {
                            var dialog = new AsmOpenFileDialog();
                            dialog.OpenFile();
                            FilePath = dialog.FileName;
                            var browser = new Browser();
                            Nodes = new ObservableCollection<TreeNode>(browser.Browse(FilePath));
                        }, (obj) => true);
                    }
                }
        
                private ObservableCollection<TreeNode> _nodes;
        
                public ObservableCollection<TreeNode> Nodes
                {
                    get { return _nodes; }
                    set
                    {
                        _nodes = value;
                        OnAssemblyBrowsed();
                    }
                }
        
                public void OnAssemblyBrowsed([CallerMemberName] string path = "")
                {
                    if (PropertyChanged != null)
                    {
                        PropertyChanged.Invoke(this, new PropertyChangedEventArgs(path));
                    }
                }
    }
}