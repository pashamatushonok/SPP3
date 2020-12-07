using System;
using System.Collections.Generic;
using AssemblyBrowser;
using AssemblyBrowser.TypeComponents;

namespace WpfAppAssemblyBrowser.Model
{
    public class Browser
    {
        public BrowserOfAsm BrowserOfAssembly { get; set;}

        public Browser()
        {
            BrowserOfAssembly = new BrowserOfAsm();
        }

        public List<TreeNode> Browse(string assemblyPath)
        {
            BrowsingResult browsingResult = BrowserOfAssembly.Browse(assemblyPath);
            return GetNodesList(browsingResult);
        }

        private List<TreeNode> GetNodesList(BrowsingResult browseResult)
        {
            List<TreeNode> result = new List<TreeNode>();
            foreach (AsmNamespace assemblyNamespace in browseResult.Namespaces)
            {
                var namespaceNode = new TreeNode(assemblyNamespace.Name);

                foreach (AsmType asmType in assemblyNamespace.AssemblyTypes)
                {
                    var typeNode = new TreeNode(Converter.ConvertToString(asmType));
                    foreach (Field field in asmType.Fields)
                    {
                        var elementNode = new TreeNode(Converter.ConvertToString(field));
                        typeNode.Nodes.Add(elementNode);
                    }

                    foreach (Property property in asmType.Properties)
                    {
                        var propNode = new TreeNode(Converter.ConvertToString(property));
                        typeNode.Nodes.Add(propNode);
                    }

                    foreach (Constructor ctor in asmType.Constructors)
                    {
                        var methodNode = new TreeNode(Converter.ConvertToString(ctor));
                        typeNode.Nodes.Add(methodNode);
                    }

                    foreach (Method method in asmType.Methods)
                    {
                        var methodNode = new TreeNode(Converter.ConvertToString(method));
                        typeNode.Nodes.Add(methodNode);
                    }

                    namespaceNode.Nodes.Add(typeNode);
                }

                result.Add(namespaceNode);
            }

            return result;
        }
    }
}