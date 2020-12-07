using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser
{
    public class BrowserOfAsm
    {
         public BrowsingResult Browse(string assemblyPath)
        {
            var assembly = Assembly.LoadFrom(assemblyPath);
            var types = new List<Type>(assembly.GetTypes());

            var browsingResult = new BrowsingResult();
            var namespaces = new Dictionary<string, List<Type>>();
            foreach (var type in types)
            {
                var namespaceName = type.Namespace ?? "";
                if (!(namespaces.TryGetValue(namespaceName, out types)))
                {
                    types = new List<Type>();
                    namespaces.Add(namespaceName, types);
                }

                types.Add(type);
            }

            foreach (KeyValuePair<string, List<Type>> namespaceType in namespaces)
            {
                var assemblyNamespace = new AsmNamespace(namespaceType.Key);
                foreach (var type in namespaceType.Value)
                {
                    var assemblyType = new AsmType(type);
                    assemblyNamespace.AssemblyTypes.Add(assemblyType);
                }

                browsingResult.Namespaces.Add(assemblyNamespace);
            }

            return browsingResult;
        }
        
        public BrowsingResult Browse(Type t)
        {
            var types = new List<Type> {t};

            var browsingResult = new BrowsingResult();
            var namespaces = new Dictionary<string, List<Type>>();
            foreach (var type in types)
            {
                var namespaceName = type.Namespace ?? "";
                if (!namespaces.TryGetValue(namespaceName, out types))
                {
                    types = new List<Type>();
                    namespaces.Add(namespaceName, types);
                }

                types.Add(type);
            }

            foreach (KeyValuePair<string, List<Type>> namespaceType in namespaces)
            {
                var assemblyNamespace = new AsmNamespace(namespaceType.Key);
                foreach (var type in namespaceType.Value)
                {
                    var assemblyType = new AsmType(type);
                    assemblyNamespace.AssemblyTypes.Add(assemblyType);
                }

                browsingResult.Namespaces.Add(assemblyNamespace);
            }

            return browsingResult;
        }
    }
}