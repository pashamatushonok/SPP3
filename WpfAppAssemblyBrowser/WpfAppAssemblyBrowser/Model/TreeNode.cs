using System.Collections.Generic;

namespace WpfAppAssemblyBrowser.Model
{
    public class TreeNode
    {
        public string Name { get; set;}
        public IList<TreeNode> Nodes { get; set;}

        public TreeNode(string name)
        {
            Name = name;
            Nodes = new List<TreeNode>();
        }
    }
}